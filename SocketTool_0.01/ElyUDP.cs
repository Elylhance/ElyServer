using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace MySocketServer
{
    class ElyUDPClient : IDisposable
    {
        #region private-variable
        private UdpClient UdpC;
        private CancellationTokenSource TokenSource;
        private CancellationToken Token;
        private ConcurrentDictionary<string, RemoteClient> ClientList;
        private Func<string, bool> iConnectedFDback = null;
        private Func<string, bool> iDisconnectedFDback = null;
        private Func<string, byte[], bool> iDataReceivedFDback = null;
        #endregion

        #region Constructor
        /// <summary>
        /// Ely UDP server 初始化构造函数
        /// </summary>
        /// <param name="listenerIp"> 服务器本地IP地址</param>
        /// <param name="listenerPort"> 服务器本地端口</param>
        /// <param name="ConnectedFDback"> 有新客户端接入输出信息</param>
        /// <param name="DisconnectedFDback"> 客户端断开连接输出信息</param>
        /// <param name="DataReceivedFDback"> 收到客户端的数据输出</param>
        /// <param name="ExceptionCatcher"> 异常信息处理</param>
        /// <param name="debug"> 输出Debug信息</param>
        public ElyUDPClient(
            string listenerIp,
            string listenerPort,
            Func<string, bool> ConnectedFDback,
            Func<string, bool> DisconnectedFDback,
            Func<string, byte[], bool> DataReceivedFDback,
            bool debug)
        {
            iConnectedFDback = ConnectedFDback;
            iDisconnectedFDback = DisconnectedFDback;
            iDataReceivedFDback = DataReceivedFDback;

            IPAddress Ipaddr = IPAddress.Parse(listenerIp);
            if (listenerPort == String.Empty)
            {
                throw new ArgumentException("请输入端口号");
            }
            int port = Convert.ToInt32(listenerPort);
            if (port < IPEndPoint.MinPort || port > IPEndPoint.MaxPort)
            {
                throw new ArgumentOutOfRangeException();
            }

            try
            {
                var LocalEndPoint = new IPEndPoint(Ipaddr, port);
                UdpC = new UdpClient(LocalEndPoint);
                // 解决当某个UDP客户端不可达时，上报“远程主机强迫关闭了一个现有的连接”异常导致服务器退出的问题
                uint IOC_IN = 0x80000000;
                uint IOC_VENDOR = 0x18000000;
                uint SIO_UDP_CONNRESET = IOC_IN | IOC_VENDOR | 12;
                UdpC.Client.IOControl((int)SIO_UDP_CONNRESET, new byte[] { Convert.ToByte(false) }, null);
            }
            catch (Exception)
            {
                throw new Exception("IP 地址不可用或端口已被占用");
            }

            TokenSource = new CancellationTokenSource();
            Token = TokenSource.Token;
            ClientList = new ConcurrentDictionary<string, RemoteClient>();
            // Loop Receive data form any addr
            Task.Run(() => ListenAnyAddrData(), Token);
            Task.Run(() => AutoDisconnectChecker(), Token);
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

        #region private-method
        private async void ListenAnyAddrData()
        {
            while (!Token.IsCancellationRequested)
            {
                try
                {
                    var UdpReceiveResult = await UdpC.ReceiveAsync();
                    if (UdpReceiveResult != null)
                    {
                        string IpPortStr = $"UDP:{UdpReceiveResult.RemoteEndPoint.Address.ToString()}:{UdpReceiveResult.RemoteEndPoint.Port}";
                        if (!ClientList.ContainsKey(IpPortStr))
                        {
                            var RmClient = new RemoteClient();
                            RmClient.RemoteEndPoint = UdpReceiveResult.RemoteEndPoint;
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
                        byte[] data = UdpReceiveResult.Buffer;
                        if (iDataReceivedFDback != null)
                        {
                            Task unwait = Task.Run(() => iDataReceivedFDback(IpPortStr, data));
                        }
                    }
                }
                catch (Exception)
                {
                    this.Dispose();
                }
            }
            return;
        }
        private async void AutoDisconnectChecker()
        {
            while (!Token.IsCancellationRequested)
            {
                if (ClientList.Count > 0)
                {
                    foreach (KeyValuePair<string, RemoteClient> RmClientData in ClientList)
                    {
                        var TimeSpan = DateTime.Now - RmClientData.Value.ActiveTime;
                        if (TimeSpan.TotalSeconds > 120) //Two minuts
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
            try
            {
                RemoteClient RmClient;
                ClientList.TryGetValue(IpPortStr, out RmClient);
                await UdpC.SendAsync(data, data.Length, RmClient.RemoteEndPoint);
                RmClient.ActiveTime = DateTime.Now;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (TokenSource != null)
                {
                    TokenSource.Cancel();
                    TokenSource.Dispose();
                }
                if (ClientList != null)
                {
                    foreach (KeyValuePair<string, RemoteClient> CloseItem in ClientList)
                    {
                        Disconnect(CloseItem.Key);
                    }
                    ClientList.Clear();
                }
                if (UdpC != null)
                {
                    UdpC.Client.Close();
                    UdpC.Client.Dispose();
                    UdpC.Close();
                }

                //  托管资源
                if (disposing)
                {
                    UdpC = null;
                    ClientList = null;
                }
                disposedValue = true;
            }
        }

        ~ElyUDPClient()
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

    class RemoteClient
    {
        private IPEndPoint remoteEndPoint;
        private DateTime activeTime;

        public IPEndPoint RemoteEndPoint
        {
            get
            {
                return remoteEndPoint;
            }

            set
            {
                remoteEndPoint = value;
            }
        }

        public DateTime ActiveTime
        {
            get
            {
                return activeTime;
            }

            set
            {
                activeTime = value;
            }
        }
    }
}
