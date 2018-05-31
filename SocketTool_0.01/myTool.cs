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
        private ElyTCPServer elyTcpServer;
        private ElyTLSServer elyTlsServer;
        private ElyUDPClient elyUdpServer;
        private ElyUDPClient elyDtlsServer;
        private int SequenceNum = 0;
        private string NewLogFilePath;
        private CancellationTokenSource CancellTS;
        private ListBox.SelectedObjectCollection SelectedClientList;
        #endregion

        #region InitMyTool
        public myTool()
        {
            InitializeComponent();
            My_InitFunc();
        }
        #endregion

        #region My_InitFunc
        private void My_InitFunc()
        {
            string LocalIP = GetLocalIPAddresses();
            TcpIpAddr.Text = LocalIP;
            UdpIpAddr.Text = LocalIP;
            TlsVersion.SelectedIndex = 0;
            SignatureAlgorithm.SelectedIndex = 2;
            SelectedClientList = ClientListBox.SelectedItems;
            if (File.Exists("init.ini"))
            {
                byte[] bytes = File.ReadAllBytes("init.ini");
                String UserConfig = Encoding.ASCII.GetString(bytes);
                bytes = Convert.FromBase64String(UserConfig);
                UserConfig = Encoding.UTF8.GetString(bytes);
                RestoreUserConfigAndData(UserConfig);
            }
            Task.Run(async() =>
            {
                await Task.Delay(200);
                if (!File.Exists("init.ini"))
                {
                    //First run application
                    var myHelpMsgWindow = new HelpMsgWindow();
                    myHelpMsgWindow.ShowDialog();
                    using (FileStream unUse = File.Create("init.ini")) {; }
                }
                // Check Tool-Chain
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
                            MessageBox.Show($"Warrning!!! Can not find [{ToolName}]!");
                    }
                }
            });
        }
        
        private string GetLocalIPAddresses()
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

        private void BackupUserConfigAndData()
        {
            using (FileStream Fs = new FileStream("init.ini", FileMode.Create))
            {
                string UserConfig = string.Empty;
                UserConfig += $"TcpIpAddr:{TcpIpAddr.SelectedIndex}" + Environment.NewLine;
                UserConfig += $"TcpPort:{TcpPort.Text}" + Environment.NewLine;
                UserConfig += $"UdpIpAddr:{UdpIpAddr.SelectedIndex}" + Environment.NewLine;
                UserConfig += $"UdpPort:{UdpPort.Text}" + Environment.NewLine;

                UserConfig += $"ADataBlock:{ADatablock.Text.Replace(Environment.NewLine,"-*#@#*^l2D`")}" + Environment.NewLine;
                UserConfig += $"ATimerSpan:{ATimerSpan.Text}" + Environment.NewLine;
                UserConfig += $"ASendCount:{ASendCount.Text}" + Environment.NewLine;

                UserConfig += $"BDatablock:{BDatablock.Text.Replace(Environment.NewLine, "-*#@#*^l2D`")}" + Environment.NewLine;
                UserConfig += $"BTimerSpan:{BTimerSpan.Text}" + Environment.NewLine;
                UserConfig += $"BSendCount:{BSendCount.Text}" + Environment.NewLine;

                UserConfig += $"CDatablock:{CDatablock.Text.Replace(Environment.NewLine, "-*#@#*^l2D`")}" + Environment.NewLine;
                UserConfig += $"CTimerSpan:{CTimerSpan.Text}" + Environment.NewLine;
                UserConfig += $"CSendCount:{CSendCount.Text}" + Environment.NewLine;

                //SSL Setup
                UserConfig += $"PKCS12:{PKCS12.Checked}" + Environment.NewLine;
                UserConfig += $"TlsVersion:{TlsVersion.SelectedIndex}" + Environment.NewLine;
                UserConfig += $"IgnoreCert:{IgnoreCert.Checked}" + Environment.NewLine;
                UserConfig += $"MutualAuth:{MutualAuth.Checked}" + Environment.NewLine;
                UserConfig += $"SignatureAlgorithm:{SignatureAlgorithm.SelectedIndex}" + Environment.NewLine;
                UserConfig += $"pfxFilePath:{pfxFilePath.Text}" + Environment.NewLine;
                UserConfig += $"pfxPasswd:{pfxPasswd.Text}:{pfxPasswd.PasswordChar}:{ImportShowPasswd.Checked}" + Environment.NewLine;
                UserConfig += $"PubCert:{PubCert.Text}" + Environment.NewLine;
                UserConfig += $"PrvtKey:{PrvtKey.Text}" + Environment.NewLine;
                UserConfig += $"PriKeyPasswd:{PriKeyPasswd.Text}:{PriKeyPasswd.PasswordChar}:{PemShowPasswd.Checked}" + Environment.NewLine;
                byte[] bytes = Encoding.UTF8.GetBytes(UserConfig);
                UserConfig = Convert.ToBase64String(bytes);
                bytes = Encoding.ASCII.GetBytes(UserConfig);
                Fs.Write(bytes, 0, bytes.Length);
            }
        }

        private void RestoreUserConfigAndData(string UserConfig)
        {
            String[] ConfigSet = UserConfig.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var Config in ConfigSet)
            {
                string Header = Config.Substring(0, Config.IndexOf(':') + 1);
                string Body = Config.Substring(Config.IndexOf(':') + 1);
                switch (Header)
                {
                    case "TcpIpAddr:":
                        TcpIpAddr.SelectedIndex = Convert.ToInt32(Body);
                        break;
                    case "TcpPort:":
                        TcpPort.Text = Body;
                        break;
                    case "UdpIpAddr:":
                        UdpIpAddr.SelectedIndex = Convert.ToInt32(Body);
                        break;
                    case "UdpPort:":
                        UdpPort.Text = Body;
                        break;

                    case "ADataBlock:":
                        ADatablock.Text = Body.Replace("-*#@#*^l2D`",Environment.NewLine);
                        break;
                    case "ATimerSpan:":
                        ATimerSpan.Text = Body;
                        break;
                    case "ASendCount:":
                        ASendCount.Text = Body;
                        break;

                    case "BDatablock:":
                        BDatablock.Text = Body.Replace("-*#@#*^l2D`", Environment.NewLine);
                        break;
                    case "BTimerSpan:":
                        BTimerSpan.Text = Body;
                        break;
                    case "BSendCount:":
                        BSendCount.Text = Body;
                        break;

                    case "CDatablock:":
                        CDatablock.Text = Body.Replace("-*#@#*^l2D`", Environment.NewLine);
                        break;
                    case "CTimerSpan:":
                        CTimerSpan.Text = Body;
                        break;
                    case "CSendCount:":
                        CSendCount.Text = Body;
                        break;

                    //SSL Setup
                    case "PKCS12:":
                        if (Body.Equals("False"))
                        {
                            PKCS12.Checked = false;
                            PEM_DER.Checked = true;
                        }
                        break;
                    case "TlsVersion:":
                        TlsVersion.SelectedIndex = Convert.ToInt32(Body);
                        break;
                    case "IgnoreCert:":
                        if (Body.Equals("True"))
                        {
                            IgnoreCert.Checked = true;
                            NoIgnoreCert.Checked = false;
                        }
                        break;
                    case "MutualAuth:":
                        if (Body.Equals("True"))
                        {
                            MutualAuth.Checked = true;
                            NoMutualAuth.Checked = false;
                        }
                        break;
                    case "SignatureAlgorithm:":
                        SignatureAlgorithm.SelectedIndex = Convert.ToInt32(Body);
                        break;
                    case "pfxFilePath:":
                        pfxFilePath.Text = Body;
                        break;
                    case "pfxPasswd:":
                        string[] pfxSet = Body.Split(':');
                        pfxPasswd.Text = (string)pfxSet.GetValue(0);
                        pfxPasswd.PasswordChar = (char)((string)pfxSet.GetValue(1)).ToCharArray().GetValue(0);
                        if (pfxSet.GetValue(2).Equals("True"))
                        {
                            ImportShowPasswd.Checked = true;
                        }
                        break;
                    case "PubCert:":
                        PubCert.Text = Body;
                        break;
                    case "PrvtKey:":
                        PrvtKey.Text = Body;
                        break;
                    case "PriKeyPasswd:":
                        string[] pemSet = Body.Split(':');
                        PriKeyPasswd.Text = (string)pemSet.GetValue(0);
                        PriKeyPasswd.PasswordChar = (char)((string)pemSet.GetValue(1)).ToCharArray().GetValue(0);
                        if (pemSet.GetValue(2).Equals("True"))
                        {
                            PemShowPasswd.Checked = true;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region ServerCallBack
        delegate bool delegateCallBack(string newClient);
        delegate bool delegateCallBackWithData(string Sender, byte[] Data);

        private bool ConnectedFDback(string newClient)
        {
            if (ClientListBox.InvokeRequired)
            {
                var Cb = new delegateCallBack(ConnectedFDback);
                this.Invoke(Cb, new object[] { newClient });
            }
            else
            {
                this.ClientListBox.Items.Add(newClient);
                if (this.SelectedClientList.Count == 0)
                    ClientListBox.SetSelected(0, true);
                PrintPromptMessage($"A client[{newClient}] is already connected!");
            }
            return true;
        }

        private bool DisconnectedFDback(string Client)
        {
            if (ClientListBox.InvokeRequired)
            {
                var Cb = new delegateCallBack(DisconnectedFDback);
                this.Invoke(Cb, new object[] { Client });
            }
            else
            {
                this.ClientListBox.Items.Remove(Client);
                if (this.SelectedClientList.Count == 0 && this.ClientListBox.Items.Count > 0)
                {
                    ClientListBox.SetSelected(0, true);
                }
                PrintPromptMessage($"The client[{Client}] is already disconnected!");
            }
            return true;
        }

        private bool DataReceivedFDback(string Sender, byte[] Data)
        {
            if (LogTextbox.InvokeRequired)
            {
                var Cb = new delegateCallBackWithData(DataReceivedFDback);
                this.Invoke(Cb, new object[] { Sender, Data });
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
                    string LogHeader = $"{Environment.NewLine}===========内存占用过多，信息自动存入Log文件，窗口将停止输出===========";
                    SaveLogToFile.Checked = true;
                    LogTextbox.AppendText(LogHeader);
                    Str = LogHeader + Str;
                }
            }

            if (SaveLogToFile.Checked)
            {
                Task.Run(() =>{ File.AppendAllText(NewLogFilePath, Str, Encoding.Default); });
            }
            return true;
        }

        private bool PrintPromptMessage(string promptMsg)
        {
            if (LogTextbox.InvokeRequired)
            {
                var d = new delegateCallBack(PrintPromptMessage);
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
                                                        DataReceivedFDback, PrintPromptMessage);
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
                string LogDir = Environment.CurrentDirectory + "\\log";
                if (!Directory.Exists(LogDir))
                {
                    Directory.CreateDirectory(LogDir);
                }
                NewLogFilePath = LogDir + $"\\{DateTime.Now.ToString("yyMMdd_HHmmss")}.txt";
            }
            else if (File.Exists(NewLogFilePath))
            {
                lock (this)
                {
                    var FiInfo = new FileInfo(NewLogFilePath);
                    if (FiInfo.Length > 0)
                    {
                        MessageBox.Show($"日志已存入文件: {NewLogFilePath}");
                    }
                    else
                    {
                        FiInfo.Delete();
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

        #region SendData
        private void Datablock_DragDrop(object sender, DragEventArgs e)
        {
            TextBox TextObject = sender as TextBox;
            string path = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            FileInfo fil = new FileInfo(path);
            if (fil.Length > 128 * 1024)  //128k
            {
                TextObject.Text = "Error: The file size is too large";
            }
            else
            {
                TextObject.Text = path;
                TextObject.Tag = path;
            }
            TextObject.Cursor = Cursors.IBeam;
        }
        private void Datablock_DragEnter(object sender, DragEventArgs e)
        {
            TextBox TextObject = sender as TextBox;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string path = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                if (path.Length > 1)
                {
                    //判断是文件夹吗
                    FileInfo fil = new FileInfo(path);
                    if (fil.Attributes == FileAttributes.Directory)//文件夹
                    {
                        e.Effect = DragDropEffects.None;
                    }
                    else//文件
                    {
                        e.Effect = DragDropEffects.Link;
                        TextObject.Cursor = Cursors.Arrow;
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private async void ASendDataReq_Click(object sender, EventArgs e)
        {
            if (!IsSending)
            {
                if (SelectedClientList.Count == 0)
                    return;
                if (ADatablock.Text == string.Empty)
                    return;
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
                    int unUse = await SendDataToClient(ADatablock, ASendCount.Value, ATimerSpan.Value, CancellTS.Token);
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
                    int unUse = await SendDataToClient(BDatablock, BSendCount.Value, BTimerSpan.Value, CancellTS.Token);
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
                    int unUse = await SendDataToClient(CDatablock, CSendCount.Value, CTimerSpan.Value, CancellTS.Token);
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

        private async Task<int> SendDataToClient(object Sender, decimal SendCount, decimal TimerSpan, CancellationToken Token)
        {
            int SentSize = 0;
            byte[] bytedata = null;
            TextBox DataTB = Sender as TextBox;
            string InputData = DataTB.Text;
            if (DataTB.Tag == null) //非文件发送模式
            {
                if (!HexStrCheckerAndConventer(ref InputData, ref bytedata)) //处理十六进制发送模式
                {
                    //普通模式
                    if (!InputData.Contains(@"[\0]"))
                    {
                        bytedata = Encoding.Default.GetBytes(InputData);
                    }
                    else
                    {
                        bytedata = Encoding.Default.GetBytes(InputData.Replace(@"[\0]", "\0"));
                    }
                }
            }
            else //文件发送模式
            {
                bytedata = File.ReadAllBytes(InputData);
                InputData = Encoding.Default.GetString(bytedata).Replace("\0", @"[\0]");
            }

            while (SelectedClientList.Count > 0 && SendCount-- > 0)
            {
                for (int i = 0; i < SelectedClientList.Count; i++)
                {
                    if (Token.IsCancellationRequested)
                    {
                        break;
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
                                string SendInfo = $"[Send-->{Client.ToString()}] < {bytedata.Length} >--[OK]{Environment.NewLine}{InputData}";
                                if (ShowHexData.Checked)
                                {
                                    string Hexstr = $"{Environment.NewLine}[{BitConverter.ToString(bytedata).Replace("-", " ")}]";
                                    SendInfo += Hexstr;
                                }
                                if (DataTB.Tag != null)
                                {
                                    DataTB.Text = $"文件『{DataTB.Tag}』发送完毕";
                                    DataTB.Tag = null;
                                }
                                MessageOutPutHandler(SendInfo);
                                SentSize += bytedata.Length;
                                TxBytes += bytedata.Length;
                                TxRxCounter.Text = $"数据统计：发送 {TxBytes} 字节, 接收 {RxBytes} 字节";
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
                        string SendInfo = $"[Send-->{Client.ToString()}]< {bytedata.Length} >--[ERROR]{Environment.NewLine}{InputData}";
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
                    await Task.Delay((int)TimerSpan);
                }
            }
            return SentSize;
        }
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
        private bool HexStrCheckerAndConventer(ref string data, ref byte[] bytedata)
        {
            if (data.StartsWith("[") && data.EndsWith("]") && !data.Equals(@"[\0]"))
            {
                // Hex string
                int i = 0;
                string Tempdata = data.Substring(1, data.Length - 2);
                byte[] bytes = new byte[(Tempdata.Length + 1) / 3];
                String[] SplitStr = Tempdata.Split(' ');
                foreach (string Char in SplitStr)
                {
                    try
                    {
                        bytes[i++] = byte.Parse(Char, System.Globalization.NumberStyles.HexNumber);
                    }
                    catch
                    {
                        return false;
                    }
                }
                bytedata = bytes;
                data = Encoding.Default.GetString(bytes);
                if (data.Contains("\0"))    //此处data用于回显，需执行替换操作
                    data = data.Replace("\0", @"[\0]");
                return true;
            }
            else
            {
                return false;
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
            if (e.KeyChar == (char)1)  // Ctrl-A 相当于输入了AscII=1的控制字符
            {
                textBox.SelectAll();
                e.Handled = true;
            }
        }
        // 显示帮助窗口
        private void myTool_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                var myHelpMsgWindow = new HelpMsgWindow();
                myHelpMsgWindow.ShowDialog();
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
                            makecert -r -pe -n ""CN = www.MobileTek.Test"" -m 24 -a {SA} -sky exchange -ss my CARoot.cer -sv CARoot.pvk"
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
