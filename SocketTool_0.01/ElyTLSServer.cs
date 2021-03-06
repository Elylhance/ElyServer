﻿using MySocketClient;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace MySocketServer
{
    /// <summary>
    /// Elyenhance TCP server
    /// </summary>
    class ElyTLSServer : IDisposable
    {
        #region private-variable
        private TcpListener iTcpListener;
        private bool iAcceptInvalidCert;
        private bool iMaumutually;
        private string iTlsVer;
        private bool AlreadyDisposed = false; // 要检测冗余调用
        private CancellationTokenSource TokenSource;
        private CancellationToken Token;
        private int CurrentClientNum = 0;
        private int maxClientNum = 100;
        private ConcurrentDictionary<string, ElyClient> ClientList;
        private Func<string, bool> iConnectedFDback = null;
        private Func<string, bool> iDisconnectedFDback = null;
        private Func<string, byte[], bool> iDataReceivedFDback = null;
        private Func<string, bool> iPromptMsgPrinter;
        private X509Certificate2 SslCertificate;

        public int MaxClientNum
        {
            get { return maxClientNum; }
            set { maxClientNum = value; }
        }
        #endregion

        #region Construct&Factory
        /// <summary>
        /// Ely TLS server 构造函数
        /// </summary>
        /// <param name="listenerIp">服务器本地IP地址</param>
        /// <param name="listenerPort">服务器本地端口</param>
        /// <param name="pfxCertFile"> 证书文件名称</param>
        /// <param name="pfxCertkey"> 证书访问密码</param>
        /// <param name="Maumutually"> 服务器与客户端均需证书认证</param>
        /// <param name="AcceptInvalidCert"> 是否接受无效证书</param>
        /// <param name="TlsVer"> Tls版本设置</param>
        /// <param name="ConnectedFDback"> 有新客户端接入输出信息</param>
        /// <param name="DisconnectedFDback"> 客户端断开连接输出信息</param>
        /// <param name="DataReceivedFDback"> 收到客户端的数据输出</param>
        /// <param name="PromptMsgPrinter"> 异常信息处理</param>
        public ElyTLSServer(
            string listenerIp,
            string listenerPort,
            string pfxCertFile,
            string pfxCertkey,
            bool Maumutually,
            bool AcceptInvalidCert,
            string TlsVer,
            Func<string, bool> ConnectedFDback,
            Func<string, bool> DisconnectedFDback,
            Func<string, byte[], bool> DataReceivedFDback,
            Func<string, bool> PromptMsgPrinter)
        {
            iTlsVer = TlsVer;
            iMaumutually = Maumutually;
            iAcceptInvalidCert = AcceptInvalidCert;
            iPromptMsgPrinter = PromptMsgPrinter;
            iConnectedFDback = ConnectedFDback;
            iDisconnectedFDback = DisconnectedFDback;
            iDataReceivedFDback = DataReceivedFDback;

            var tcpIpaddr = IPAddress.Parse(listenerIp);
            if (listenerPort == String.Empty)
            {
                throw new ArgumentException("请输入端口号");
            }
            int port = Convert.ToInt32(listenerPort);
            if (port < IPEndPoint.MinPort || port > IPEndPoint.MaxPort || PortInUse(port))
            {
                throw new ArgumentOutOfRangeException();
            }
            if (!File.Exists(pfxCertFile))
            {
                throw new Exception("Could not find cert or key file");
            }

            SslCertificate = null;
            if (string.IsNullOrEmpty(pfxCertkey))
            {
                SslCertificate = new X509Certificate2(pfxCertFile);
            }
            else
            {
                SslCertificate = new X509Certificate2(pfxCertFile, pfxCertkey);
            }

            try
            {
                iTcpListener = new TcpListener(tcpIpaddr, port);
                iTcpListener.Start();
            }
            catch (SocketException)
            {
                throw new Exception("IP 地址不可用或端口已被占用");
            }
            /// 生成取消监听的Token
            TokenSource = new CancellationTokenSource();
            Token = TokenSource.Token;
            /// 存储IpPort字符串与client的线程安全字典，有点耗内存
            ClientList = new ConcurrentDictionary<string, ElyClient>();
            /// 此构造函数由UI调用，另起一个Task循环监听接入请求，以免UI卡住
            Task.Run(() => AccecptConnection(), Token);
        }

        #endregion

        #region Private-methods
    // Write/Read/accept connection
        private bool PortInUse(int port)
        {
            bool inUse = false;
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();

            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }
            return inUse;
        }
        private async Task AccecptConnection()
        {
            while (!Token.IsCancellationRequested)
            {
                try
                {
                    if (CurrentClientNum >= MaxClientNum)
                    {
                        await Task.Delay(100);
                        continue;
                    }
                    #region accept-connection
                    TcpClient tcpClient = await iTcpListener.AcceptTcpClientAsync();
                    #endregion

                    var sslClient = new ElyClient(tcpClient);
                    if (iAcceptInvalidCert)
                    {
                        sslClient.Sstream = new SslStream(sslClient.Nstream, false, new RemoteCertificateValidationCallback(AcceptInvalidCert));
                    }
                    else
                    {
                        sslClient.Sstream = new SslStream(sslClient.Nstream, false);
                    }
                    sslClient.IpPortStr = sslClient.IpPortStr.Replace("TCP", "TLS");
                    Task unwait = Task.Run(()=>StartSslConnection(sslClient), Token);
                }
                catch (SocketException)
                {
                    break;
                }
            }
            this.Dispose();
        }
        private async Task<bool> StartSslConnection(ElyClient sslC)
        {
            try
            {
                SslProtocols SSLVer = GetUserSetProtocalType();
                await sslC.Sstream.AuthenticateAsServerAsync(SslCertificate, iMaumutually, SSLVer, false);
                if (!sslC.Sstream.IsEncrypted)
                {
                    throw new Exception("此连接未进行加密处理");
                }
                if (!sslC.Sstream.IsAuthenticated)
                {
                    throw new Exception("身份验证失败");
                }
                if (iMaumutually && !sslC.Sstream.IsMutuallyAuthenticated)
                {
                    throw new Exception("客户端证书验证失败");
                }
                // SSL证书验证成功，加入客户端列表，监听数据
                HandleConnectedClient(sslC);
                return true;
            }
            catch (Exception ex)
            {
                Task unwait = null;
                if (ex.InnerException != null)
                {
                    unwait = Task.Run(() => iPromptMsgPrinter($"Error: [{sslC.IpPortStr}] {ex.InnerException.Message}"));
                }
                else
                {
                    unwait = Task.Run(() => iPromptMsgPrinter($"Error: [{sslC.IpPortStr}] {ex.Message}"));
                }
                
                sslC.Dispose();
                return false;
            }
        }
        private SslProtocols GetUserSetProtocalType()
        {
            switch (iTlsVer)
            {
                case "Default":
                    return SslProtocols.Ssl3 | SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;
                case "SSL v2":
                    return SslProtocols.Ssl2;
                case "SSL v3":
                    return SslProtocols.Ssl3;
                case "TLS v1.0":
                    return SslProtocols.Tls;
                case "TLS v1.1":
                    return SslProtocols.Tls11;
                case "TLS v1.2":
                    return SslProtocols.Tls12;
                default:
                    return SslProtocols.Default;
            }
        }
        private void HandleConnectedClient(ElyClient tcpC)
        {
            AddClient(tcpC);
            if (iConnectedFDback != null)
            {
                Task<bool> unwaited = Task.Run(() => iConnectedFDback(tcpC.IpPortStr));
            }
            Task.Run(() => DataReceiver(tcpC), Token);
        }
        #region KeepAlive
        /*
        struct ElyKeepalive
        {
            public uint onoff;
            public uint keepalivetime;
            public uint keepaliveinterval;
        };
        ElyKeepalive KAPram;
        KAPram.onoff = 1;
        KAPram.keepalivetime = 10 * 1000;   //10s
        KAPram.keepaliveinterval = 1000;    //1s
        byte[] inValue = StructToBytes(KAPram);
        private static byte[] StructToBytes(object structObj)
        {
            int size = Marshal.SizeOf(structObj);
            byte[] bytes = new byte[size];
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(structObj, structPtr, false);
            Marshal.Copy(structPtr, bytes, 0, size);
            Marshal.FreeHGlobal(structPtr);
            return bytes;
        }
        */
        #endregion
        private async Task DataReceiver(ElyClient tcpC)
        {
            try
            {
                byte[] inValue = new byte[] { 1, 0, 0, 0, 0x10, 0x27, 0, 0, 0xe8, 0x03, 0, 0 };//{1,10000ms,1000ms}
                tcpC.TcpHandle.Client.IOControl(IOControlCode.KeepAliveValues, inValue, null);
                // 循环监听&接收数据
                while (!Token.IsCancellationRequested)
                {
                    byte[] data = await MessageReadAsync(tcpC);
                    if (data == null)
                    {
                        await Task.Delay(10);
                        continue;
                    }
                    if (iDataReceivedFDback != null)
                    {
                        Task<bool> unwaited = Task.Run(() => iDataReceivedFDback(tcpC.IpPortStr, data));
                    }
                }
            }
            finally
            {
                RemoveClient(tcpC);
                if (iDisconnectedFDback != null)
                {
                    Task<bool> unwaited = Task.Run(() => iDisconnectedFDback(tcpC.IpPortStr));
                }
                tcpC.Dispose();
            }
        }

        private async Task<byte[]> MessageReadAsync(ElyClient client)
        {
            byte[] contentBytes = null;

            if (!client.Sstream.CanRead)
            {
                return null;
            }   

            #region Read-Data
            using (MemoryStream dataMs = new MemoryStream())
            {
                int recvd = 0;
                int bufferSize = 2048;
                byte[] buffer = new byte[bufferSize];
                if ((recvd = await client.Sstream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    dataMs.Write(buffer, 0, recvd);
                }
                if (dataMs.Length > 0)
                    contentBytes = dataMs.ToArray();
                else if (recvd == 0)  //Reach EOF
                    throw new SocketException();
            }
            #endregion
            return contentBytes;
        }
        private bool AcceptInvalidCert(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return iAcceptInvalidCert;
        }

        private void AddClient(ElyClient tcpC)
        {
            if (ClientList != null)
            {
                ClientList.TryAdd(tcpC.IpPortStr, tcpC);
                Interlocked.Increment(ref CurrentClientNum);
            }
        }

        private void RemoveClient(ElyClient tcpC)
        {
            if (ClientList != null)
            {
                ElyClient RmClient = null;
                ClientList.TryRemove(tcpC.IpPortStr, out RmClient);
                Interlocked.Decrement(ref CurrentClientNum);
            }
        }
        private async Task<bool> MessageWriteAsync(ElyClient ElyC, byte[] data)
        {
            try
            {
                if (ElyC.TcpHandle.Client.Poll(-1, SelectMode.SelectWrite))
                {
                    await ElyC.Sstream.WriteAsync(data, 0, data.Length);
                    await ElyC.Sstream.FlushAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Public-methods
        // Send/disconnect/is connected
        public async Task<bool> Send(string IpPort, byte[] data)
        {
            ElyClient ElyC = null;
            if (ClientList.TryGetValue(IpPort, out ElyC))
            {
                return await MessageWriteAsync(ElyC, data);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
        public void Disconnect(string IpPort)
        {
            ElyClient ElyC = null;
            if (ClientList.TryGetValue(IpPort, out ElyC))
            {
                RemoveClient(ElyC);
                ElyC.Dispose();
            }
        }
        #endregion

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (!AlreadyDisposed)
            {
                if (TokenSource != null)
                {
                    TokenSource.Cancel();
                    TokenSource.Dispose();
                }
                /// 断开所有客户端
                if (ClientList != null)
                {
                    foreach (var CloseItem in ClientList)
                    {
                        CloseItem.Value.Dispose();
                    }
                    ClientList.Clear();
                }
                if (iTcpListener != null)
                {
                    iTcpListener.Server.Close();
                    iTcpListener.Server.Dispose();
                }
                if (disposing)
                {
                    iTcpListener = null;
                    ClientList = null;
                    TokenSource = null;
                }
                AlreadyDisposed = true;
            }
        }

        ~ElyTLSServer()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
