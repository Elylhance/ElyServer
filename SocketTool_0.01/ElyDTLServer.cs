using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Concurrent;
using System.Threading;
using wolfSSL.CSharp;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;

namespace MySocketServer
{
    class ElyDTLServer:IDisposable
    {
        #region private variable
        IntPtr ctx;
        UdpClient udp;
        IPEndPoint EndPoint;
        private CancellationTokenSource TokenSource;
        private CancellationToken Token;
        private Func<string, bool> iConnectedFDback = null;
        private Func<string, bool> iDisconnectedFDback = null;
        private Func<string, byte[], bool> iDataReceivedFDback = null;
        ConcurrentDictionary<string, RemoteClient> ClientList;
        #endregion

        #region Constructor
        public ElyDTLServer(
            string listenerIp,
            string listenerPort,
            string fileCert,
            string fileKey,
            Func<string, bool> ConnectedFDback,
            Func<string, bool> DisconnectedFDback,
            Func<string, byte[], bool> DataReceivedFDback)
        {
            iConnectedFDback = ConnectedFDback;
            iDisconnectedFDback = DisconnectedFDback;
            iDataReceivedFDback = DataReceivedFDback;

            #region Param-Check
            var tcpIpaddr = IPAddress.Parse(listenerIp);
            if (listenerPort == string.Empty)
            {
                throw new ArgumentException("请输入端口号");
            }
            int port = Convert.ToInt32(listenerPort);
            if (port < IPEndPoint.MinPort || port > IPEndPoint.MaxPort || PortInUse(port))
            {
                throw new ArgumentOutOfRangeException();
            }
            if (!File.Exists(fileCert) || !File.Exists(fileKey))
            {
                throw new Exception("Could not find cert or key file");
            }
            #endregion

            EndPoint = new IPEndPoint(tcpIpaddr, port);

            #region wolfssl
            wolfssl.Init();
            #region wolfssl_CTX
            ctx = wolfssl.CTX_dtls_new(wolfssl.useDTLSv1_2_server());
            if (ctx == IntPtr.Zero)
            {
                wolfssl.CTX_free(ctx);
                //udp.Close();
                throw new Exception($"Error creating ctx structure");
            }

            short minDhKey = 128;
            wolfssl.CTX_SetMinDhKey_Sz(ctx, minDhKey);

            if (wolfssl.CTX_use_certificate_file(ctx, fileCert, wolfssl.SSL_FILETYPE_PEM) != wolfssl.SUCCESS)
            {
                if (wolfssl.CTX_use_certificate_file(ctx, fileCert, wolfssl.SSL_FILETYPE_ASN1) != wolfssl.SUCCESS)
                {
                    wolfssl.CTX_free(ctx);
                    //udp.Close();
                    throw new Exception("Error setting cert file");
                }
            }

            if (wolfssl.CTX_use_PrivateKey_file(ctx, fileKey, wolfssl.SSL_FILETYPE_PEM) != wolfssl.SUCCESS)
            {
                if (wolfssl.CTX_use_PrivateKey_file(ctx, fileKey, wolfssl.SSL_FILETYPE_ASN1) != wolfssl.SUCCESS)
                {
                    wolfssl.CTX_free(ctx);
                    //udp.Close();
                    throw new Exception("Error setting key file");
                }
            }
            #endregion

            ClientList = new ConcurrentDictionary<string, RemoteClient>();
            TokenSource = new CancellationTokenSource();
            Token = TokenSource.Token;
            Task.Run(() => ListenAndAccept(port), Token);
            Task.Run(() => AutoDisconnectChecker(), Token);
            #endregion
        }
        #endregion

        #region Private-Func
        private void ListenAndAccept(int port)
        {
            while (Token != null && !Token.IsCancellationRequested)
            {
                udp = new UdpClient(port);
                #region ssl-ctx
                IntPtr ssl = wolfssl.new_ssl(ctx);
                if (ssl == IntPtr.Zero)
                {
                    wolfssl.CTX_free(ctx);
                    udp.Close();
                    throw new Exception("Error creating ssl object");
                }
                if (wolfssl.set_dtls_fd(ssl, udp, EndPoint) != wolfssl.SUCCESS)
                {
                    wolfssl.free(ssl);
                    wolfssl.CTX_free(ctx);
                    udp.Close();
                    throw new Exception(wolfssl.get_error(ssl));
                }
                #endregion
                try
                {
                    Console.WriteLine("Accept next connecttion");
                    if (wolfssl.accept(ssl) != wolfssl.SUCCESS)
                    {
                        wolfssl.free(ssl);
                        udp.Close();
                        Console.WriteLine("Accept new connection failed");
                    }
                    else
                    {
                        /* print out results of TLS/SSL accept */
                        Console.WriteLine("SSL version is " + wolfssl.get_version(ssl));
                        Console.WriteLine("SSL cipher suite is " + wolfssl.get_current_cipher(ssl));
                        DataReceivedHandler(ssl);
                    }
                }
                catch (Exception ex)
                {
                    wolfssl.free(ssl);
                    Console.WriteLine($"Accept() throw an exception:{ex.Message}");
                }       
            }
        }
        private void CilentConnectPrompt(string IpPortStr, IPEndPoint RemoteEndPoint, IntPtr ssl)
        {
            if (!ClientList.ContainsKey(IpPortStr))
            {
                var RmClient = new RemoteClient();
                RmClient.RemoteEndPoint = RemoteEndPoint;
                RmClient.Ssl = ssl;
                RmClient.ActiveTime = DateTime.Now;
                ClientList.TryAdd(IpPortStr, RmClient);
                if (iConnectedFDback != null)
                {
                    Task unwait = Task.Run(() => iConnectedFDback(IpPortStr));
                }
            }
            else
            {
                RemoteClient RmClient;
                ClientList.TryGetValue(IpPortStr as string, out RmClient);
                RmClient.ActiveTime = DateTime.Now;
            }
        }
        private void DataReceivedHandler(IntPtr ssl)
        {
            wolfssl.DTLS_con con = wolfssl.get_dtls_fd(ssl);
            string IpPortStr = $"DTLS:{con.ep.Address.ToString()}:{con.ep.Port.ToString()}";
            var RemoteEndPoint = new IPEndPoint(con.ep.Address, con.ep.Port);
            CilentConnectPrompt(IpPortStr, RemoteEndPoint, ssl);

            byte[] buff = new byte[2048];
            while (true)
            {
                try
                {
                    Console.WriteLine("Begin read message");
                    var recvd = wolfssl.read(ssl, buff, 2048);
                    if (recvd <= 0)
                    {
                        throw new Exception("Error reading message");
                    }
                    Console.WriteLine($"Read {recvd} bytes data");
                    byte[] data = new byte[recvd];
                    Array.Copy(buff, data, recvd);
                    if (iDataReceivedFDback != null)
                    {
                        Task unwait = Task.Run(() => iDataReceivedFDback(IpPortStr, data));
                    }
                }
                catch (Exception)
                {
                    Disconnect(IpPortStr);
                    return;
                }
            }
        }
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
        private async void AutoDisconnectChecker()
        {
            while (Token != null && !Token.IsCancellationRequested)
            {
                if (ClientList.Count > 0)
                {
                    foreach (var RmClientData in ClientList)
                    {
                        var TimeSpan = DateTime.Now - RmClientData.Value.ActiveTime;
                        if (TimeSpan.TotalSeconds > 30)
                        {
                            Disconnect(RmClientData.Key);
                        }
                    }
                }
                await Task.Delay(500);
            }
        }
        private async Task<bool> AsyncWrite(string IpPortStr, byte[] data)
        {
            RemoteClient RmClient = null;
            try
            {
                ClientList.TryGetValue(IpPortStr, out RmClient);
                if (wolfssl.write(RmClient.Ssl, data, data.Length) != data.Length)
                {
                    return false;
                }
                RmClient.ActiveTime = DateTime.Now;
                await Task.Delay(1);
                return true;
            }
            catch (Exception)
            {
                if(RmClient != null) wolfssl.free(RmClient.Ssl);
                return false;
            }
        }
        private void clean(IntPtr ssl, IntPtr ctx)
        {
            if (ssl != null) wolfssl.free(ssl);
            if (ctx != null) wolfssl.CTX_free(ctx);
            wolfssl.Cleanup();
        }
        #endregion

        #region public-method
        public async Task<bool> Send(string IpPortStr, byte[] data)
        {
            return await AsyncWrite(IpPortStr, data);
        }

        public void Disconnect(string IpPortStr)
        {
            try
            {
                RemoteClient RmClient;
                ClientList.TryRemove(IpPortStr, out RmClient);
                wolfssl.shutdown(RmClient.Ssl);
                wolfssl.free(RmClient.Ssl);
                udp.Close();
                if (iDisconnectedFDback != null)
                {
                    Task unwait = Task.Run(() => iDisconnectedFDback(IpPortStr));
                }
            }
            catch (Exception)
            {
                //Do nothing
            }
            return;
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }
                Console.WriteLine($"Dispose DTLS server");
                //释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                if (ClientList != null)
                {
                    foreach (var item in ClientList)
                    {
                        Disconnect(item.Key);
                    }
                }
                //udp.Close();
                clean(IntPtr.Zero, ctx);
                wolfssl.Cleanup();
                TokenSource.Cancel();
                // 将大型字段设置为 null。
                ClientList = null;
                TokenSource = null;
                disposedValue = true;
            }
        }

        ~ElyDTLServer() {
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
