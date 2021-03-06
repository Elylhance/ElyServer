﻿using MySocketClient;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace MySocketServer
{
    /// <summary>
    /// Elyenhance TCP server
    /// </summary>
    class ElyTCPServer:IDisposable
    {
        #region private-variable
        private TcpListener iTcpListener;
        private bool AlreadyDisposed = false; // 要检测冗余调用
        private CancellationTokenSource TokenSource;
        private CancellationToken Token;
        private int CurrentClientNum = 0;
        private int maxClientNum = 100;
        private ConcurrentDictionary<string,ElyClient> ClientList;
        private Func<string, bool> iConnectedFDback = null;
        private Func<string, bool> iDisconnectedFDback = null;
        private Func<string, byte[], bool> iDataReceivedFDback = null;
        private Func<string, bool> iPrintPromptMessage = null;

        public int MaxClientNum
        {
            get { return maxClientNum; }
            set { maxClientNum = value; }
        }
        #endregion

        #region Construct&Factory
        /// <summary>
        /// Ely TCP server 初始化构造函数
        /// </summary>
        /// <param name="listenerIp"> 服务器本地IP地址</param>
        /// <param name="listenerPort"> 服务器本地端口</param>
        /// <param name="ConnectedFDback"> 有新客户端接入输出信息</param>
        /// <param name="DisconnectedFDback"> 客户端断开连接输出信息</param>
        /// <param name="DataReceivedFDback"> 收到客户端的数据输出</param>
        /// <param name="ExceptionCatcher"> 异常信息处理</param>
        /// <param name="PrintPromptMessage"> 输出Debug信息</param>
        public ElyTCPServer(
            string listenerIp,
            string listenerPort,
            Func<string, bool> ConnectedFDback,
            Func<string, bool> DisconnectedFDback,
            Func<string, byte[], bool> DataReceivedFDback,
            Func<string, bool> PrintPromptMessage)
        {
            iConnectedFDback = ConnectedFDback;
            iDisconnectedFDback = DisconnectedFDback;
            iDataReceivedFDback = DataReceivedFDback;
            iPrintPromptMessage = PrintPromptMessage;

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
            /// 生成取消监听的Token
            TokenSource = new CancellationTokenSource();
            Token = TokenSource.Token;
            /// 存储IpPort字符串与client的可并发字典(线程安全的)
            ClientList = new ConcurrentDictionary<string, ElyClient>();

            iTcpListener = new TcpListener(tcpIpaddr, port);
            iTcpListener.Start();

            /// 此构造函数由UI调用，另起一个Task循环监听接入请求，以免UI卡住
            Task.Run(() => AccecptConnection(), Token);
        }

        #endregion

        #region Private-methods
        // Write/Read/accept connection
        private bool PortInUse(int port)
        {
            bool inUse = false;
            var ipProperties = IPGlobalProperties.GetIPGlobalProperties();
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

                    TcpClient tcpClient = await iTcpListener.AcceptTcpClientAsync();
                    ElyClient tcpC = new ElyClient(tcpClient);
                    Task unwait = Task.Run(() => HandleConnectedClient(tcpC), Token);
                }
                catch (SocketException)
                {
                    break;
                }
            }
            this.Dispose();
        }

        private void HandleConnectedClient(ElyClient tcpC)
        {
            AddClient(tcpC);
            if (iConnectedFDback != null)
            {
                Task<bool> unwaited = Task.Run(() => iConnectedFDback(tcpC.IpPortStr));
            }
            Task.Run(()=> DataReceiver(tcpC),Token);
        }

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
                    if (data != null && iDataReceivedFDback != null)
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
            byte[] ContentBytes = null;
            if (!client.Nstream.CanRead)
            {
                return null;
            }

            #region Read-Data
            using (MemoryStream dataMs = new MemoryStream())
            {
                int recvd = 0;
                int bufferSize = 2048;
                byte[] buffer = new byte[bufferSize];
                //Caller 捕获此函数的异常
                while ((recvd = await client.Nstream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    dataMs.Write(buffer, 0, recvd);
                    if (!client.Nstream.DataAvailable)
                        break;
                }
                //若对端断开了连接，但接收到了一些数据，总是先上报，下次循环再断开连接
                if (dataMs.Length > 0)
                {
                    ContentBytes = dataMs.ToArray();
                }
                else if (recvd == 0)
                {
                    throw new SocketException();
                }
            }
            #endregion
            return ContentBytes;
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
                if (ElyC.TcpHandle.Client.Poll(2*1000*1000, SelectMode.SelectWrite))
                {
                    await ElyC.Nstream.WriteAsync(data, 0, data.Length);
                    await ElyC.Nstream.FlushAsync();
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

        ~ElyTCPServer() {
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
