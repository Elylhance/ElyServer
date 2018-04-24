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
        private ElyUDPClient elyUdpServer;
        private ElyUDPClient elyDtlsServer;
        private int SendTimerSpanMs = 0;
        private int SequenceNum = 0;
        private FileStream LogFile;
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
            Task.Run(async() =>
            {
                await Task.Delay(500);
                if (!File.Exists("init.ini"))
                {
                    string ReleaseNote = File.ReadAllText("ReleaseNote.txt");
                    MessageBox.Show(ReleaseNote, "新版特性");
                    using (FileStream unUse = File.Create("init.ini")) { ;}
                }
                // Tool Chain Check, note the diffirence between x86&x64
                string[] ToolList = { "cmd.exe", "makecert.exe", "Cert2Spc.exe", "pvk2pfx.exe", "openssl.exe" };
                if (!Directory.Exists("cert"))
                {
                    MessageBox.Show("Warrning!!! Directory [cert] is not exist!");
                    return;
                }
                else
                {
                    foreach (string ToolName in ToolList)
                    {
                        if (File.Exists($"cert\\{ToolName}"))
                            continue;
                        else
                            MessageBox.Show($"Warrning!!! Can not find file [{ToolName}]!");
                    }
                }
            });
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
        private async Task<bool> WriteData(string Client, byte[] bytedata)
        {
            bool SendResult = false;
            if (elyTcpServer != null && Client.StartsWith("TCP"))
            {
                SendResult = await elyTcpServer.Send(Client, bytedata);
            }
            else if (elyTlsServer != null && Client.StartsWith("TLS"))
            {
                SendResult = await elyTlsServer.Send(Client, bytedata);
            }
            else if (elyUdpServer != null && Client.StartsWith("UDP"))
            {
                SendResult = await elyUdpServer.Send(Client, bytedata);
            }
            else if (elyDtlsServer != null && Client.StartsWith("DTLS"))
            {
                SendResult = await elyDtlsServer.Send(Client, bytedata);
            }
            else { }
            return SendResult;
        }
        private void HexStrCheckAndConventer(ref string data)
        {
            if (data.StartsWith("[") && data.EndsWith("]"))
            {
                // Hex string
                int i = 0;
                bool ErrorFlag = false;
                string TempData = data.TrimStart('[').TrimEnd(']');
                byte[] bytes = new byte[(TempData.Length + 1) / 3];
                String[] SplitStr = TempData.Split(' ');
                foreach (string Char in SplitStr)
                {
                    try
                    {
                        bytes[i++] = byte.Parse(Char,System.Globalization.NumberStyles.HexNumber);
                    }
                    catch
                    { 
                        ErrorFlag = true;//Wrong format, stop process
                        break;
                    }
                }
                if (!ErrorFlag)
                {
                    data = Encoding.Default.GetString(bytes);
                }
            }
        }
        private async Task<int> SendDataToClient(string data, decimal SendCount, CancellationToken Token)
        {
            int SentSize = 0;
            while (SelectedClientList.Count > 0 && SendCount-- > 0)
            {
                for (int i = 0; i < SelectedClientList.Count; i++)
                {
                    if (Token.IsCancellationRequested)
                    {
                        break;
                    }
                    HexStrCheckAndConventer(ref data);
                    byte[] bytedata;
                    if (!data.Contains(@"[\0]"))
                    {
                        bytedata = Encoding.Default.GetBytes(data);
                    }
                    else
                    {
                        bytedata = Encoding.Default.GetBytes(data.Replace(@"[\0]", "\0"));
                    }

                    object Client = SelectedClientList[i];
                    try
                    {
                        if (Client.ToString() != string.Empty)
                        {
                            bool SendResult = await WriteData(Client.ToString(), bytedata);
                            if (SendResult)
                            {
                                /// Report sent data successfully. 
                                string SendInfo = $"[Send-->{Client.ToString()}] < {bytedata.Length} >--[OK]{Environment.NewLine}{data}";
                                if (ShowHexData.Checked)
                                {
                                    string Hexstr = $"{Environment.NewLine}[{BitConverter.ToString(bytedata).Replace("-", " ")}]";
                                    SendInfo += Hexstr;
                                }
                                MessageOutPutHandler(SendInfo);
                                SentSize += bytedata.Length;
                                TxBytes += bytedata.Length;
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
                        string SendInfo = $"[Send-->{Client.ToString()}]< {bytedata.Length} >--[ERROR]{Environment.NewLine}{data}";
                        if (ShowHexData.Checked)
                        {
                            string Hexstr = $"{Environment.NewLine}[{BitConverter.ToString(bytedata).Replace("-", " ")}]";
                            SendInfo += Hexstr;
                        }
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
                while (Strdata.IndexOf((char)0) != (-1))    //检测数据中是否含有字符串结束符“\0”
                {
                    Strdata = Strdata.Replace("\0", @"[\0]");
                }
                string RecvDataInfo = $"[Recv<--{Sender}] < {Data.Length} >{Environment.NewLine}{Strdata}";
                if (ShowHexData.Checked)
                {
                    string Hexstr = $"{Environment.NewLine}[{BitConverter.ToString(Data).Replace("-"," ")}]";
                    RecvDataInfo += Hexstr;
                }
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
            if (LogTextbox.TextLength < 100*1024*1024)
            {
                LogTextbox.AppendText(Str);
            }
            else
            {
                if (!SaveLogToFile.Checked)
                {
                    string LogHeader = $"{Environment.NewLine}===========内存占用过多，请到Log文件中查看，窗口将停止输出===========";
                    SaveLogToFile.Checked = true;
                    LogTextbox.AppendText(LogHeader);
                    Str = LogHeader + Str;
                }
            }

            if (SaveLogToFile.Checked && LogFile.CanWrite)
            {
                Task.Run(() =>{
                    lock (this)
                    {
                        byte[] Byte = Encoding.Default.GetBytes(Str);
                        LogFile.Write(Byte, 0, Byte.Length);
                        LogFile.Flush();
                    }
                });
            }
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
        private void ConvertCrtKeyToPfx(string certificate_pub, string privatekey, string prikeypasswd,out string pfxPath, out string pfxPasswd)
        {
            if (string.IsNullOrEmpty(certificate_pub) || string.IsNullOrEmpty(privatekey))
            {
                throw new FileNotFoundException();
            }

            pfxPasswd = DateTime.Now.Millisecond.ToString();
            string Result = string.Empty;
            string passinWd = string.Empty;
            string pfxName = $"_TemporaryCert_.pfx";
            string TempPath = Environment.CurrentDirectory;
            Environment.CurrentDirectory += "\\cert";
            try
            {
                File.Copy(certificate_pub, Environment.CurrentDirectory + "\\_PubCert.crt", true);
                File.Copy(privatekey, Environment.CurrentDirectory + "\\_PriKey.key", true);
                if (!string.IsNullOrEmpty(prikeypasswd))
                {
                    passinWd = prikeypasswd;
                }
                string Cmd = $"openssl pkcs12 -export -out {pfxName} -passin pass:{passinWd} -passout pass:{pfxPasswd} " 
                           + "-inkey _PriKey.key -in _PubCert.crt & del _PubCert.crt,_PriKey.key";
                CmdHelper.CmdPath = Environment.CurrentDirectory + "\\cmd.exe";

                CmdHelper.RunCmd(Cmd, out Result);
                if (Result.EndsWith($"Loading 'screen' into random state - done{Environment.NewLine}"))
                {
                    pfxPath = Environment.CurrentDirectory + "\\" + pfxName;
                    return;
                }
                else
                {
                    if (Result.Contains("No certificate matches private key"))
                        throw new Exception("No certificate matches private key");
                    else if (Result.Contains("unable to load private key"))
                        throw new Exception("Unable to load private key");
                    else
                        throw new Exception("处理证书发生未知错误");
                }
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
                if (ListenerPort == String.Empty)
                {
                    MessageOutPutHandler("请输入端口号");
                    return;
                }
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
                            string prikeypasswd = PriKeyPasswd.Text;
                            try
                            {
                                ConvertCrtKeyToPfx(certificate_pub, privatekey, prikeypasswd, out pfxPath, out pfxPasswd);
                                elyTlsServer = new ElyTLSServer(ListenerIp, ListenerPort,
                                                                    pfxPath, pfxPasswd,
                                                                    Maumutually, AcceptInvalidCert, TlsVer,
                                                                    ConnectedFDback,
                                                                    DisconnectedFDback,
                                                                    DataReceivedFDback,
                                                                    PrintPromptMessage);
                                 File.Delete(pfxPath);
                            }
                            catch (Exception ex)
                            {
                                if (pfxPath != string.Empty)
                                    File.Delete(pfxPath);
                                throw ex;
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
                if (ListenerPort == String.Empty)
                {
                    MessageOutPutHandler("请输入端口号");
                    return;
                }
                try
                {
                    // 开启后不允许修改服务器参数
                    UdpIpAddr.Enabled = false;
                    UdpPort.Enabled = false;
                    UdpOnoff.Enabled = false;
                    DTLSOnoff.Enabled = false;

                    if (UdpOnoff.Checked)
                    {
                        elyUdpServer = new ElyUDPClient(ListenerIp, ListenerPort,
                                                ConnectedFDback,
                                                DisconnectedFDback,
                                                DataReceivedFDback, false);
                        MessageOutPutHandler($"Start UDP server[{ListenerIp}:{ListenerPort}] successfully!");
                    }
                    else if (DTLSOnoff.Checked)
                    {
                        throw new Exception("Sorry, DTLS server is not supported now.");
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

        private void SaveLogToFile_CheckedChanged(object sender, EventArgs e)
        {
            if (SaveLogToFile.Checked)
            {
                //Create and Open flie
                string LogDirectory = Environment.CurrentDirectory + "\\log";
                if (!Directory.Exists(LogDirectory))
                {
                    Directory.CreateDirectory(LogDirectory);
                }
                string NewLogFilePath = LogDirectory + $"\\{DateTime.Now.ToString("yyMMddHHmmss")}.txt";
                LogFile = File.Create(NewLogFilePath, 512, FileOptions.Asynchronous);

            }
            else
            {
                //Close file
                lock (this)
                {
                    string LogFilePath = LogFile.Name;
                    if (LogFile.Length > 0)
                    {
                        MessageBox.Show($"已存入文件: {LogFilePath}");
                        LogFile.Dispose();
                    }
                    else
                    {
                        LogFile.Dispose();
                        File.Delete(LogFilePath);
                    }
                }
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
                if (SendTimerSpanMs == 0 && ASendCount.Value > 1)
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
                    ASendCount.Enabled = false;

                    CancellTS = new CancellationTokenSource();
                    int unUse = await SendDataToClient(InputData, ASendCount.Value, CancellTS.Token);
                }
                finally
                {
                    BSendButton.Enabled = true;
                    CSendButton.Enabled = true;

                    ATimerSpan.Enabled = true;
                    ADatablock.Enabled = true;
                    ASendCount.Enabled = true;
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
                ASendCount.Enabled = true;
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
                if (SendTimerSpanMs == 0 && BSendCount.Value > 1)
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
                    BSendCount.Enabled = false;

                    CancellTS = new CancellationTokenSource();
                    int unUse = await SendDataToClient(InputData, BSendCount.Value, CancellTS.Token);
                }
                finally
                {
                    // 取消互斥
                    ASendButton.Enabled = true;
                    CSendButton.Enabled = true;
                    // 反转状态
                    BTimerSpan.Enabled = true;
                    BDatablock.Enabled = true;
                    BSendCount.Enabled = true;
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
                BSendCount.Enabled = true;
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
                if (SendTimerSpanMs == 0 && CSendCount.Value > 1)
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
                    CSendCount.Enabled = false;

                    CancellTS = new CancellationTokenSource();
                    int unUse = await SendDataToClient(InputData, CSendCount.Value, CancellTS.Token);
                }
                finally
                {
                    ASendButton.Enabled = true;
                    BSendButton.Enabled = true;

                    CTimerSpan.Enabled = true;
                    CDatablock.Enabled = true;
                    CSendCount.Enabled = true;
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
                CSendCount.Enabled = true;
                CSendButton.BackColor = TransparencyKey;
                IsSending = false;
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
        private void PemShowPasswd_CheckedChanged(object sender, EventArgs e)
        {
            if (PemShowPasswd.Checked)
            {
                PriKeyPasswd.PasswordChar = (char)0;
            }
            else
            {
                PriKeyPasswd.PasswordChar = '*';
            }
        }
        private void SelectPfxCert_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory + "\\cert";
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
            //openFileDialog.InitialDirectory = Environment.CurrentDirectory;
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
            //openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog.Filter = "加密证书文件|*.pem;*.der;*.crt;*.key;*.cer;*.csr;*.pfx;*.p12|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                PrvtKey.Text = FileName;
            }
        }

        private void DirectToHomePage_Click(object sender, EventArgs e)
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
            //生成证书时，可以使UI无响应
            string Cmd = $@"del CARoot.cer,ServerCert.pfx
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
                if (!Directory.Exists("Backup"))
                {
                    Directory.CreateDirectory("Backup");
                }
                if (File.Exists("CARoot.cer") && File.Exists("ServerCert.pfx"))
                {
                    File.Copy("CARoot.cer", "Backup\\CARootBackup.cer",true);
                    File.Copy("ServerCert.pfx", "Backup\\ServerCertBackup.pfx", true);
                }

                CmdHelper.CmdPath = Environment.CurrentDirectory + "\\cmd.exe";
                CmdHelper.RunCmd(Cmd, out Result);
                //MessageBox.Show(Result); //For debug
                if (Result != string.Empty)
                {
                    if (Result.Contains("Failed") || Result.Contains("Error") || Result.Contains("ERROR"))
                    {
                        Result = $"生成证书失败！";
                        if (File.Exists("Backup\\CARootBackup.cer") && File.Exists("Backup\\ServerCertBackup.pfx"))
                        {
                            File.Copy("Backup\\CARootBackup.cer", "CARoot.cer",true);
                            File.Copy("Backup\\ServerCertBackup.pfx", "ServerCert.pfx",true);
                        }
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
