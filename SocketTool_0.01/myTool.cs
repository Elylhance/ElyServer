using MySocketServer;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SocketTool
{
    public partial class myTool : Form
    {
        #region private-variables
        private int TxBytes = 0, RxBytes = 0;
        private bool IsTCPListening;
        private bool IsUDPListening;
        private bool IsSending;
        private CancellationTokenSource CancellTS;
        private ListBox.SelectedObjectCollection SelectedClientList;
        private ElyTCPServer elyTcpServer;
        private ElyTLSServer elyTlsServer;
        private ElyUDP elyUdpServer;
        private ElyUDP elyDtlsServer;
        private int SendTimerSpanMs = 0;
        private int SequenceNum = 0;
        #endregion

        #region InitMyTool
        public myTool()
        {
            InitializeComponent();
            My_InitFunc();
        }
        #endregion

        #region my_init
        private void My_InitFunc()
        {
            string LocalIP = GetLocalIPAddress();
            TcpIpAddr.Text = LocalIP;
            UdpIpAddr.Text = LocalIP;
            TlsVersion.SelectedIndex = 0;
            SignatureAlgorithm.SelectedIndex = 2;
            SelectedClientList = ClientListBox.SelectedItems;
        }
        private string GetLocalIPAddress()
        {
            string LocalIP = string.Empty;
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    TcpIpAddr.Items.Add(ip.ToString());
                    UdpIpAddr.Items.Add(ip.ToString());
                    LocalIP = ip.ToString();
                }
            }
            return LocalIP;
        }
        #endregion

        #region ConstrutorCallBack
        delegate bool delegateCallBack(string newClient);
        delegate bool delegateCallBackWithData(string Sender, byte[] Data);

        private async Task<int> SendDataToClient(string data, decimal SendCount, CancellationToken Token)
        {
            int SentSize = 0;
            while (SelectedClientList.Count > 0 && SendCount-- > 0)
            {
                if (Token.IsCancellationRequested)
                {
                    break;
                }
                for (int i = 0; i < SelectedClientList.Count; i++)
                {
                    if (Token.IsCancellationRequested)
                    {
                        break;
                    }
                    object Client = SelectedClientList[i];
                    byte[] bytes = Encoding.Default.GetBytes(data);
                    try
                    {
                        if (Client.ToString() != string.Empty)
                        {
                            bool SendResult = false;
                            if (elyTcpServer != null && Client.ToString().StartsWith("TCP"))
                            {
                                SendResult = await elyTcpServer.Send(Client.ToString(), bytes);
                            }
                            else if (elyTlsServer != null && Client.ToString().StartsWith("TLS"))
                            {
                                SendResult = await elyTlsServer.Send(Client.ToString(), bytes);
                            }
                            else if (elyUdpServer != null && Client.ToString().StartsWith("UDP"))
                            {
                                SendResult = await elyUdpServer.Send(Client.ToString(), bytes);
                            }
                            else if (elyDtlsServer != null && Client.ToString().StartsWith("DTLS"))
                            {
                                SendResult = await elyDtlsServer.Send(Client.ToString(), bytes);
                            }
                            else { }

                            if (SendResult)
                            {
                                /// Report sent data successfully. 
                                string SendInfo = $"[Send-->{Client.ToString()}] < {bytes.Length} >--[OK]{Environment.NewLine}{data}";
                                MessageOutPutHandler(SendInfo);
                                SentSize += bytes.Length;
                                TxBytes += bytes.Length;
                                TxRxCounter.Text = $"数据统计：发送 {TxBytes} 字节, 接收 {RxBytes} 字节";
                                TxRxCounter.Update();
                            }
                            else
                            {
                                throw new SocketException();
                            }
                        }
                    }
                    catch (Exception)
                    {
                        /// Report sent data failed.
                        string SendInfo = $"[Send-->{Client.ToString()}]< {bytes.Length} >--[ERROR]{Environment.NewLine}{data}";
                        MessageOutPutHandler(SendInfo);
                        continue;
                    }
                }
                if (SendCount > 0)  ///延时 SendTimerSpanMs
                {
                    await Task.Delay(SendTimerSpanMs);
                }
            }
            return SentSize;
        }

        private bool ConnectedFDback(string newClient)
        {
            if (ClientListBox.InvokeRequired)
            {
                delegateCallBack d = new delegateCallBack(ConnectedFDback);
                this.Invoke(d, new object[] { newClient });
            }
            else
            {
                this.ClientListBox.Items.Add(newClient);
                if (this.SelectedClientList.Count == 0)
                    ClientListBox.SetSelected(0, true);
            }
            return true;
        }

        private bool DisconnectedFDback(string ConnectedClient)
        {
            if (ClientListBox.InvokeRequired)
            {
                delegateCallBack d = new delegateCallBack(DisconnectedFDback);
                this.Invoke(d, new object[] { ConnectedClient });
            }
            else
            {
                this.ClientListBox.Items.Remove(ConnectedClient);
                if (this.SelectedClientList.Count == 0 && this.ClientListBox.Items.Count > 0)
                {
                    ClientListBox.SetSelected(0, true);
                }
                this.ClientListBox.Update();
            }
            return true;
        }

        private bool DataReceivedFDback(string Sender, byte[] Data)
        {
            if (LogTextbox.InvokeRequired)
            {
                delegateCallBackWithData d = new delegateCallBackWithData(DataReceivedFDback);
                this.Invoke(d, new object[] { Sender, Data });
            }
            else
            {
                string Strdata = Encoding.Default.GetString(Data);
                string RecvDataInfo = $"[Recv<--{Sender}] < {Data.Length} >{Environment.NewLine}{Strdata}";
                MessageOutPutHandler(RecvDataInfo);
                RxBytes += Data.Length;
                TxRxCounter.Text = $"数据统计：发送 {TxBytes} 字节, 接收 {RxBytes} 字节";
                TxRxCounter.Update();
            }
            return true;
        }

        private bool MessageOutPutHandler(string msginfo)
        {
            string Str = (SequenceNum != 0 ? Environment.NewLine : string.Empty)
                        + GetSequenceNumStr()
                        + $"  [{DateTime.Now.ToString("HH:mm:ss.fff")}] "
                        + msginfo;
            LogTextbox.AppendText(Str);
            return true;
        }

        private bool PrintPromptMessage(string promptMsg)
        {
            if (LogTextbox.InvokeRequired)
            {
                delegateCallBack d = new delegateCallBack(PrintPromptMessage);
                this.Invoke(d, new object[] { promptMsg });
            }
            else
            {
                MessageOutPutHandler(promptMsg);
            }
            return true;
        }

        private string GetSequenceNumStr()
        {
            if (SequenceNum < 9999)
                return string.Format("{0:D4}", SequenceNum++);
            else if (SequenceNum < 999999)
                return string.Format("{0:D6}", SequenceNum++);
            else if (SequenceNum < 99999999)
                return string.Format("{0:D8}", SequenceNum++);
            else
            {
                SequenceNum = 0;
                return GetSequenceNumStr();
            }
        }
        #endregion

        #region Click&Content-Changed-Event
        //借助openssl工具将.crt&.key文件转换为.pfx格式证书
        private bool CrtAndKey2pfx(string certificate_pub, string privatekey, out string pfxPath, out string pfxPasswd)
        {
            if (string.IsNullOrEmpty(certificate_pub) || string.IsNullOrEmpty(privatekey))
            {
                throw new ArgumentException("证书路径错误");
            }
            pfxPasswd = DateTime.Now.Millisecond.ToString();
            string Result = string.Empty;
            string pfxName = "TempCertificate.pfx";
            string TempPath = Environment.CurrentDirectory;
            Environment.CurrentDirectory += "\\cert";
            try
            {
                File.Copy(certificate_pub, Environment.CurrentDirectory + "\\_PubCert.crt", true);
                File.Copy(privatekey, Environment.CurrentDirectory + "\\_PriKey.key", true);
                string Cmd = $"openssl pkcs12 -export -out {pfxName} -passout pass:{pfxPasswd} -inkey _PriKey.key -in _PubCert.crt";
                CmdHelper.CmdPath = Environment.CurrentDirectory + "\\cmd.exe";
                CmdHelper.RunCmd(Cmd, out Result);
                if (Result.EndsWith($"Loading 'screen' into random state - done{Environment.NewLine}"))
                {
                    pfxPath = Environment.CurrentDirectory + "\\" + pfxName;
                    return true;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                pfxPath = string.Empty;
                return false;
            }
            finally
            {
                Environment.CurrentDirectory = TempPath;
            }
        }

        private void StartTCPServer_Click(object sender, EventArgs e)
        {
            string ListenerIp = TcpIpAddr.Text;
            string ListenerPort = TcpPort.Text;
            if (!IsTCPListening)
            {
                try
                {
                    // 开启后不允许修改服务器参数
                    TcpIpAddr.Enabled = false;
                    TcpPort.Enabled = false;
                    TcpOnoff.Enabled = false;
                    TLSOnoff.Enabled = false;

                    if (TcpOnoff.Checked)
                    {
                        elyTcpServer = new ElyTCPServer(ListenerIp, ListenerPort, ConnectedFDback, DisconnectedFDback,
                                                        DataReceivedFDback, false);
                        MessageOutPutHandler($"Start TCP server[{ListenerIp}:{ListenerPort}] successfully!");
                    }
                    else if (TLSOnoff.Checked)
                    {
                        bool Maumutually = MutualAuth.Checked;
                        bool AcceptInvalidCert = IgnoreCert.Checked;
                        string TlsVer = this.TlsVersion.Text;
                        if (PKCS12.Checked)
                        {
                            string pfxCertFile = pfxFilePath.Text;
                            string pfxCertkey = pfxPasswd.Text;
                            elyTlsServer = new ElyTLSServer(ListenerIp, ListenerPort,
                                                             pfxCertFile, pfxCertkey,
                                                             Maumutually, AcceptInvalidCert, TlsVer,
                                                             ConnectedFDback,
                                                             DisconnectedFDback,
                                                             DataReceivedFDback,
                                                             PrintPromptMessage);
                        }
                        else if (PEM_DER.Checked)
                        {
                            string pfxPath = string.Empty;
                            string pfxPasswd = string.Empty;
                            string certificate_pub = PubCert.Text;
                            string privatekey = PrvtKey.Text;
                            if (CrtAndKey2pfx(certificate_pub, privatekey, out pfxPath, out pfxPasswd))
                            {
                                elyTlsServer = new ElyTLSServer(ListenerIp, ListenerPort,
                                                                 pfxPath, pfxPasswd,
                                                                 Maumutually, AcceptInvalidCert, TlsVer,
                                                                 ConnectedFDback,
                                                                 DisconnectedFDback,
                                                                 DataReceivedFDback,
                                                                 PrintPromptMessage);
                            }
                            else
                            {
                                throw new ArgumentException("处理证书发生未知错误。");
                            }
                        }
                        else { throw new Exception("An unknown error occurred"); }
                        MessageOutPutHandler($"Start TLS server[{ListenerIp}:{ListenerPort}] successfully!");
                    }
                    IsTCPListening = true;
                    StartTcpTlsServer.Text = "已开启";
                    StartTcpTlsServer.BackColor = Color.LightGreen;
                    TcpPort.Items.Add(ListenerPort);
                }
                catch (Exception ex)
                {
                    if (elyTcpServer != null)
                    {
                        elyTcpServer.Dispose();
                        elyTcpServer = null;
                    }
                    if (elyTlsServer != null)
                    {
                        elyTlsServer.Dispose();
                        elyTlsServer = null;
                    }
                    MessageOutPutHandler($"{ex.Message}");
                    StartTcpTlsServer.Text = "打开";
                    StartTcpTlsServer.BackColor = TransparencyKey;
                    TcpIpAddr.Enabled = true;
                    TcpPort.Enabled = true;
                    TcpOnoff.Enabled = true;
                    TLSOnoff.Enabled = true;
                }
            }
            else
            {
                if (elyTcpServer != null)
                {
                    elyTcpServer.Dispose();
                    elyTcpServer = null;
                    MessageOutPutHandler($"Stop TCP server[{ListenerIp}:{ListenerPort}] successfully!");
                }
                if (elyTlsServer != null)
                {
                    elyTlsServer.Dispose();
                    elyTlsServer = null;
                    MessageOutPutHandler($"Stop TLS server[{ListenerIp}:{ListenerPort}] successfully!");
                }
                // TODO:取消控件未执行的委托           
                TcpIpAddr.Enabled = true;
                TcpPort.Enabled = true;
                TcpOnoff.Enabled = true;
                TLSOnoff.Enabled = true;
                StartTcpTlsServer.Text = "打开";
                StartTcpTlsServer.BackColor = TransparencyKey;

                IsTCPListening = false;
                return;
            }
        }

        private void StartUDPServer_Click(object sender, EventArgs e)
        {
            string ListenerIp = UdpIpAddr.Text;
            string ListenerPort = UdpPort.Text;
            if (!IsUDPListening)
            {
                try
                {
                    // 开启后不允许修改服务器参数
                    UdpIpAddr.Enabled = false;
                    UdpPort.Enabled = false;
                    UdpOnoff.Enabled = false;
                    DTLSOnoff.Enabled = false;

                    if (UdpOnoff.Checked)
                    {
                        elyUdpServer = new ElyUDP(ListenerIp, ListenerPort,
                                                ConnectedFDback,
                                                DisconnectedFDback,
                                                DataReceivedFDback, false);
                        MessageOutPutHandler($"Start UDP server[{ListenerIp}:{ListenerPort}] successfully!");
                    }
                    else if (DTLSOnoff.Checked)
                    {
                        throw new Exception("Sorry,DTLS server is not supported now.");
                    }
                    IsUDPListening = true;
                    StartUdpDtlsServer.Text = "已开启";
                    StartUdpDtlsServer.BackColor = Color.LightGreen;
                    UdpPort.Items.Add(ListenerPort);
                }
                catch (Exception ex)
                {
                    if (elyUdpServer != null)
                    {
                        elyUdpServer.Dispose();
                        elyUdpServer = null;
                    }

                    MessageOutPutHandler($"{ex.Message}");
                    StartUdpDtlsServer.Text = "打开";
                    StartUdpDtlsServer.BackColor = TransparencyKey;

                    UdpIpAddr.Enabled = true;
                    UdpPort.Enabled = true;
                    UdpOnoff.Enabled = true;
                    DTLSOnoff.Enabled = true;

                    IsUDPListening = false;
                }
            }
            else
            {
                if (elyUdpServer != null)
                {
                    elyUdpServer.Dispose();
                    elyUdpServer = null;
                }
                MessageOutPutHandler($"Stop UDP server[{ListenerIp}:{ListenerPort}] successfully!");

                UdpIpAddr.Enabled = true;
                UdpPort.Enabled = true;
                UdpOnoff.Enabled = true;
                DTLSOnoff.Enabled = true;
                StartUdpDtlsServer.Text = "打开";
                StartUdpDtlsServer.BackColor = TransparencyKey;

                IsUDPListening = false;
                return;
            }
        }

        private void ClearLog_Click(object sender, EventArgs e)
        {
            LogTextbox.Text = string.Empty;
            RxBytes = TxBytes = 0;
            SequenceNum = 0;
            TxRxCounter.Text = $"数据统计：发送 {TxBytes} 字节, 接收 {RxBytes} 字节";
        }

        private void ClientListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedClientList = ClientListBox.SelectedItems;
        }

        private async void ASendDataReq_Click(object sender, EventArgs e)
        {
            if (!IsSending)
            {
                if (SelectedClientList.Count == 0)
                    return;
                if (ADatablock.Text == string.Empty)
                    return;
                string InputData = ADatablock.Text;
                SendTimerSpanMs = Convert.ToInt32(ATimerSpan.Text);
                if (SendTimerSpanMs == 0 && AnumericUpDown.Value > 1)
                {
                    ATimerSpan.Focus();
                    return;
                }
                try
                {
                    IsSending = true;
                    /// 禁止修改一些参数及触发按钮
                    BSendButton.Enabled = false;
                    CSendButton.Enabled = false;

                    ASendButton.BackColor = Color.SkyBlue;
                    ATimerSpan.Enabled = false;
                    ADatablock.Enabled = false;
                    AnumericUpDown.Enabled = false;

                    CancellTS = new CancellationTokenSource();
                    int unUse = await SendDataToClient(InputData, AnumericUpDown.Value, CancellTS.Token);
                }
                finally
                {
                    BSendButton.Enabled = true;
                    CSendButton.Enabled = true;

                    ATimerSpan.Enabled = true;
                    ADatablock.Enabled = true;
                    AnumericUpDown.Enabled = true;
                    ASendButton.BackColor = TransparencyKey;
                    IsSending = false;
                }
            }
            else
            {
                if (CancellTS != null)
                {
                    CancellTS.Cancel();
                }
                BSendButton.Enabled = true;
                CSendButton.Enabled = true;

                ATimerSpan.Enabled = true;
                ADatablock.Enabled = true;
                AnumericUpDown.Enabled = true;
                ASendButton.BackColor = TransparencyKey;
                IsSending = false;
            }

        }

        private async void BSendDataReq_Click(object sender, EventArgs e)
        {
            if (!IsSending)
            {
                if (SelectedClientList.Count == 0)
                    return;
                if (BDatablock.Text == string.Empty)
                    return;

                string InputData = BDatablock.Text;
                SendTimerSpanMs = Convert.ToInt32(BTimerSpan.Text);
                if (SendTimerSpanMs == 0 && BnumericUpDown.Value > 1)
                {
                    BTimerSpan.Focus();
                    return;
                }
                try
                {
                    IsSending = true;
                    // 开启互斥
                    ASendButton.Enabled = false;
                    CSendButton.Enabled = false;

                    BSendButton.BackColor = Color.SkyBlue;
                    BTimerSpan.Enabled = false;
                    BDatablock.Enabled = false;
                    BnumericUpDown.Enabled = false;

                    CancellTS = new CancellationTokenSource();
                    int unUse = await SendDataToClient(InputData, BnumericUpDown.Value, CancellTS.Token);
                }
                finally
                {
                    // 取消互斥
                    ASendButton.Enabled = true;
                    CSendButton.Enabled = true;
                    // 反转状态
                    BTimerSpan.Enabled = true;
                    BDatablock.Enabled = true;
                    BnumericUpDown.Enabled = true;
                    BSendButton.BackColor = TransparencyKey;
                    IsSending = false;
                }
            }
            else
            {
                if (CancellTS != null)
                {
                    CancellTS.Cancel();
                }
                ASendButton.Enabled = true;
                CSendButton.Enabled = true;

                BTimerSpan.Enabled = true;
                BDatablock.Enabled = true;
                BnumericUpDown.Enabled = true;
                BSendButton.BackColor = TransparencyKey;
                IsSending = false;
            }
        }

        private async void CSendDataReq_Click(object sender, EventArgs e)
        {
            if (!IsSending)
            {
                if (SelectedClientList.Count == 0)
                    return;
                if (CDatablock.Text == string.Empty)
                    return;

                string InputData = CDatablock.Text;
                SendTimerSpanMs = Convert.ToInt32(CTimerSpan.Text);
                if (SendTimerSpanMs == 0 && CnumericUpDown.Value > 1)
                {
                    CTimerSpan.Focus();
                    return;
                }
                try
                {
                    IsSending = true;

                    ASendButton.Enabled = false;
                    BSendButton.Enabled = false;

                    CSendButton.BackColor = Color.SkyBlue;
                    CDatablock.Enabled = false;
                    CTimerSpan.Enabled = false;
                    CnumericUpDown.Enabled = false;

                    CancellTS = new CancellationTokenSource();
                    int unUse = await SendDataToClient(InputData, CnumericUpDown.Value, CancellTS.Token);
                }
                finally
                {
                    ASendButton.Enabled = true;
                    BSendButton.Enabled = true;

                    CTimerSpan.Enabled = true;
                    CDatablock.Enabled = true;
                    CnumericUpDown.Enabled = true;
                    CSendButton.BackColor = TransparencyKey;
                    IsSending = false;
                }
            }
            else
            {
                if (CancellTS != null)
                {
                    CancellTS.Cancel();
                }
                ASendButton.Enabled = true;
                BSendButton.Enabled = true;

                CTimerSpan.Enabled = true;
                CDatablock.Enabled = true;
                CnumericUpDown.Enabled = true;
                CSendButton.BackColor = TransparencyKey;
                IsSending = false;
            }
        }

        private void TimerSpan_Leave(object sender, EventArgs e)
        {
            TextBox TimerSpan = sender as TextBox;
            if (TimerSpan != null)
            {
                if (string.IsNullOrEmpty(TimerSpan.Text))
                {
                    TimerSpan.Text = "500";
                }
            }
        }

        private void numericUpDown_Leave(object sender, EventArgs e)
        {
            /* 解决当删除框内字符后，没有输入新值，显示为null，但是value实际为原来的值的问题 */
            NumericUpDown Num = sender as NumericUpDown;
            if (Num != null)
            {
                /* NumericUpDown 类型的Text属性不可见，转为其基类的Text来判断是否为空 */
                if (string.IsNullOrEmpty(((UpDownBase)Num).Text))
                {
                    ((UpDownBase)Num).Text = Num.Value.ToString();
                }
            }
        }

        private void SelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ClientListBox.Items.Count; i++)
            {
                ClientListBox.SetSelected(i, true);
            }
        }

        private void DisconnectSelectedClient_Click(object sender, EventArgs e)
        {
            if (CancellTS != null)
            {
                CancellTS.Cancel(); // Cancell the opreation that sending data to these Client
            }
            Disconnect();
        }

        private void Disconnect()
        {
            while (SelectedClientList.Count > 0)
            {
                if (SelectedClientList[0].ToString() != string.Empty)
                {
                    if (elyTcpServer != null && SelectedClientList[0].ToString().StartsWith("TCP"))
                        elyTcpServer.Disconnect(SelectedClientList[0].ToString());
                    else if (elyTlsServer != null && SelectedClientList[0].ToString().StartsWith("TLS"))
                        elyTlsServer.Disconnect(SelectedClientList[0].ToString());
                    else if (elyUdpServer != null && SelectedClientList[0].ToString().StartsWith("UDP"))
                        elyUdpServer.Disconnect(SelectedClientList[0].ToString());
                    else if (elyDtlsServer != null && SelectedClientList[0].ToString().StartsWith("DTLS"))
                        elyDtlsServer.Disconnect(SelectedClientList[0].ToString());

                    //删除的是选中的，所以 selectedClientList.Count 会自减 1
                    ClientListBox.Items.Remove(SelectedClientList[0]);
                }
            }
        }

        #endregion

        #region KeyPressEvent
        // Range: 0~9, BackSpace
        private void ComboBoxPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComboBox ComboBox = sender as ComboBox;
            if (ComboBox == null)
            {
                return;
            }

            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
        // Range: 0~9, BackSpace and dot
        private void ComboBoxIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComboBox ComboBox = sender as ComboBox;
            if (ComboBox == null)
            {
                return;
            }

            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)46)
            {
                e.Handled = true;
            }
        }
        // Range: 0~9, BackSpace
        private void TimerSpan_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null)
                return;
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
        // Ctrl^A
        private void Ctrl_A_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null)
                return;
            if (e.KeyChar == (char)1)   // Ctrl-A 相当于输入了AscII=1的控制字符
            {
                textBox.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region SSL_Setup
        private void IgnoreCert_Click(object sender, EventArgs e)
        {
            if (IgnoreCert.Checked == true)
                NoIgnoreCert.Checked = false;
            else
                IgnoreCert.Checked = true;
        }

        private void NoIgnoreCert_Click(object sender, EventArgs e)
        {
            if (NoIgnoreCert.Checked == true)
                IgnoreCert.Checked = false;
            else
                NoIgnoreCert.Checked = true;
        }

        private void MutualAuth_Click(object sender, EventArgs e)
        {
            if (MutualAuth.Checked == true)
                NoMutualAuth.Checked = false;
            else
                MutualAuth.Checked = true;
        }

        private void NoMutualAuth_Click(object sender, EventArgs e)
        {
            if (NoMutualAuth.Checked == true)
                MutualAuth.Checked = false;
            else
                NoMutualAuth.Checked = true;
        }

        #region ControlShowOrHidePassword
        private void ImportShowPasswd_CheckedChanged(object sender, EventArgs e)
        {
            if (ImportShowPasswd.Checked)
            {
                pfxPasswd.PasswordChar = (char)0;
            }
            else
            {
                pfxPasswd.PasswordChar = '*';
            }
        }

        private void GssShowPasswd_CheckedChanged(object sender, EventArgs e)
        {
            if (GssShowPasswd.Checked)
            {
                SelfSignedPasswd.PasswordChar = (char)0;
            }
            else
            {
                SelfSignedPasswd.PasswordChar = '*';
            }
        }
        #endregion
        private void SelectPfxCert_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = System.Environment.CurrentDirectory + "\\cert";
            openFileDialog.Filter = "加密证书文件|*.pem;*.der;*.crt;*.key;*.cer;*.csr;*.pfx;*.p12|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                pfxFilePath.Text = FileName;
            }
        }

        private void SetPubKey_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = System.Environment.CurrentDirectory;
            openFileDialog.Filter = "加密证书文件|*.pem;*.der;*.crt;*.key;*.cer;*.csr;*.pfx;*.p12|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                PubCert.Text = FileName;
            }
        }

        private void SetPrivateKey_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = System.Environment.CurrentDirectory;
            openFileDialog.Filter = "加密证书文件|*.pem;*.der;*.crt;*.key;*.cer;*.csr;*.pfx;*.p12|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                PrvtKey.Text = FileName;
            }
        }

        private void RedirectToHome_Click(object sender, EventArgs e)
        {
            Process.Start("www.mobiletek.cn");
        }

        private void GenerateCert_Click(object sender, EventArgs e)
        {
            string SA = SignatureAlgorithm.SelectedItem.ToString();
            string PassWd = string.Empty;
            if (SelfSignedPasswd.Text != string.Empty)
            {
                PassWd = "-po " + SelfSignedPasswd.Text;
            }

            Task.Run(() =>
            {
                string Cmd = $@"del *.cer *.pfx
                             rem <md5|sha1|sha256|sha384|sha512> L506仅支持md5及sha256算法
                             makecert -r -pe -n ""CN = localhost"" -m 24 -a {SA} -sky exchange -ss my CARoot.cer -sv CARoot.pvk"
                             + "&& cert2spc CARoot.cer CARoot.spc"
                             + $"&& pvk2pfx -pvk CARoot.pvk -spc CARoot.spc {PassWd} -pfx ServerCert.pfx"
                             + "&  del *.pvk *.spc";
                string Result = string.Empty;
                string TempPath = Environment.CurrentDirectory;
                try
                {
                    Environment.CurrentDirectory += "\\cert";
                    CmdHelper.CmdPath = Environment.CurrentDirectory + "\\cmd.exe";
                    CmdHelper.RunCmd(Cmd, out Result);
                    //MessageBox.Show(Result); //For debug
                    if (Result != string.Empty)
                    {
                        if (Result.Contains("Failed") || Result.Contains("Error") || Result.Contains("ERROR"))
                        {
                            Result = $"生成证书失败！";
                        }
                        else
                        {
                            Result = $"生成证书成功！{Environment.NewLine}证书路径：{Environment.CurrentDirectory}";
                        }
                        MessageBox.Show(Result);
                    }
                }
                finally
                {
                    Environment.CurrentDirectory = TempPath;
                }
            });
        }
        #endregion
    }


    #region CmdHelper--Used to call cmd.exe and execute bat command
    public class CmdHelper
    {
        public static string CmdPath = @"C:\Windows\System32\cmd.exe";

        /// <summary>
        /// 执行cmd命令
        /// 多命令请使用批处理命令连接符：
        /// <![CDATA[
        /// &:同时执行两个命令
        /// |:将上一个命令的输出,作为下一个命令的输入
        /// &&：当&&前的命令成功时,才执行&&后的命令
        /// ||：当||前的命令失败时,才执行||后的命令]]>
        /// 其他请百度
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="output"></param>
        public static void RunCmd(string cmd, out string output)
        {
            cmd = cmd.Trim().TrimEnd('&') + "&exit";//说明：不管命令是否成功均执行exit命令，否则当调用ReadToEnd()方法时，会处于假死状态
            try
            {
                using (Process p = new Process())
                {
                    p.StartInfo.FileName = CmdPath;
                    p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动
                    p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
                    p.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
                    p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
                    p.StartInfo.CreateNoWindow = true;          //不显示程序窗口
                    p.Start();//启动程序

                    //向cmd窗口写入命令
                    p.StandardInput.WriteLine(cmd);
                    p.StandardInput.AutoFlush = true;

                    //获取cmd窗口的输出信息
                    output = p.StandardOutput.ReadToEnd();
                    output += p.StandardError.ReadToEnd();
                    p.WaitForExit();//等待程序执行完退出进程
                    p.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                output = string.Empty;
            }
        }
    }
    #endregion
}
