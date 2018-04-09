using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;

namespace MySocketClient
{
    class ElyClient:IDisposable
    {
        private TcpClient tcpHandle;
        private NetworkStream nstream;
        private SslStream sstream;
        private string ipPortStr;

        public ElyClient(TcpClient tcpC)
        {
            tcpHandle = tcpC;
            nstream = tcpHandle.GetStream();
            ipPortStr = $"TCP:{((IPEndPoint)TcpHandle.Client.RemoteEndPoint).Address.ToString()}"
                       + $":{((IPEndPoint)TcpHandle.Client.RemoteEndPoint).Port}";
        }
        #region IDisposable Support
        private bool AlreadyDisposed = false; // 要检测冗余调用

        public TcpClient TcpHandle
        {
            get { return tcpHandle; }
            set { tcpHandle = value;  }
        }

        public NetworkStream Nstream
        { get{ return nstream; }}

        public SslStream Sstream
        {
            get { return sstream; }
            set
            {
                sstream = value;
                ipPortStr.Replace("TCP","TLS");
            }
        }

        public string IpPortStr
        { get{ return ipPortStr; }}

        protected virtual void Dispose(bool disposing)
        {
            if (!AlreadyDisposed)
            {
                /// 非托管资源，句柄，窗体组件...
                if (Sstream != null)
                {
                    Sstream.Dispose();
                }

                if (Nstream != null)
                {
                    Nstream.Dispose();
                }

                if (TcpHandle != null)
                {
                    TcpHandle.Client.Close();
                    TcpHandle.Client.Dispose();
                    TcpHandle.Close();
                }
                if (disposing)
                {
                    /// 托管资源，new的堆内存
                    TcpHandle = null;
                }
                AlreadyDisposed = true;
            }
        }

        public void Dispose()
        {
            /// 用户显示调用，释放全部资源（托管+非托管）
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        ~ElyClient()
        {
            /// GC自动调用，仅释放非托管资源，托管资源由GC择机释放
            Dispose(false);
        }

    }
}
