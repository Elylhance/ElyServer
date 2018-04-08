using MySocketServer;
using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySocketTool
{
    public partial class myTool : Form
    {
        #region private-variables
        private int TxBytes = 0, RxBytes = 0;
        private bool IsTCPListening;
        private bool IsUDPListening;
        private bool IsSending;
        private CancellationTokenSource CancellTS;
        private ListBox.SelectedObjectCollection selectedClientList;
        private ElyTCPServer elyTcpServer;
        private ElyTCPServer elyTlsServer;
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
            sIpaddr1.Text = LocalIP;
            sIpaddr2.Text = LocalIP;
        }
        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return string.Empty;
        }
        #endregion

        #region ConstrutorCallBack
        delegate bool delegateCallBack(string newClient);
        delegate bool delegateCallBackWithData(string Sender, byte[] Data);

        private async Task<int> SendDataToClient(string data, decimal SendCount, CancellationToken Token)
        {
            int SentSize = 0;
            while (selectedClientList.Count > 0 && SendCount-- > 0)
            {
                if (Token.IsCancellationRequested)
                {
                    break;
                }
                for (int i = 0; i < selectedClientList.Count; i++)
                {
                    if (Token.IsCancellationRequested)
                    {
                        break;
                    }
                    object Client = selectedClientList[i];
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
            if (sConnectionlistBox.InvokeRequired)
            {
                delegateCallBack d = new delegateCallBack(ConnectedFDback);
                this.Invoke(d, new object[] { newClient });
            }
            else
            {
                this.sConnectionlistBox.Items.Add(newClient);
                if (this.selectedClientList == null || this.selectedClientList.Count == 0)
                    sConnectionlistBox.SetSelected(0, true);
            } 
            return true;
        }
        private bool DisconnectedFDback(string ConnectedClient)
        {
            if (sConnectionlistBox.InvokeRequired)
            {
                delegateCallBack d = new delegateCallBack(DisconnectedFDback);
                this.Invoke(d, new object[] { ConnectedClient });
            }
            else
            {
                this.sConnectionlistBox.Items.Remove(ConnectedClient);
                if (this.selectedClientList.Count == 0 && this.sConnectionlistBox.Items.Count > 0)
                    sConnectionlistBox.SetSelected(0, true);
                this.sConnectionlistBox.Update();
            }
            return true;
        }
        private bool DataReceivedFDback(string Sender, byte[] Data)
        {
            if (sLogTextbox.InvokeRequired)
            {
                delegateCallBackWithData d = new delegateCallBackWithData(DataReceivedFDback);
                this.Invoke(d, new object[] { Sender,Data });
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
            string Str = string.Empty;
            if (SequenceNum != 0)
            {
                Str = Environment.NewLine
                        + GetSequenceNumStr()
                        + $"  [{DateTime.Now.ToString("HH:mm:ss.fff")}] "
                        + msginfo;
            }
            else
            {
                Str =  GetSequenceNumStr()
                        + $"  [{DateTime.Now.ToString("HH:mm:ss.fff")}] "
                        + msginfo;
            }
            sLogTextbox.AppendText(Str);
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
        /// <summary>
        /// Start TCP/DTLS Server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sListen1_Click(object sender, EventArgs e)
        {
            string ListenerIp = sIpaddr1.Text;
            string ListenerPort = sPort1.Text;
            if (!IsTCPListening)
            {
                try
                {
                    // 开启后不允许修改服务器参数
                    sIpaddr1.Enabled = false;
                    sPort1.Enabled = false;
                    sTcpOnoff.Enabled = false;
                    sTLSOnoff.Enabled = false;

                    if (sTcpOnoff.Checked)
                    {
                        elyTcpServer = new ElyTCPServer(ListenerIp, ListenerPort, ConnectedFDback, DisconnectedFDback,
                                                        DataReceivedFDback, false);
                        MessageOutPutHandler($"Start TCP Server[{ListenerIp}:{ListenerPort}] Successfully!");
                    }
                    else if (sTLSOnoff.Checked)
                    {
                    }
                    IsTCPListening = true;
                    sListen1.Text = "已开启";
                    sListen1.BackColor = Color.LightGreen;
                }
                catch (Exception ex)
                {
                    if (elyTcpServer != null)
                    {
                        elyTcpServer.Dispose();
                    }
                    MessageOutPutHandler($"{ex.Message}");
                    sListen1.Text = "打开";
                    sListen1.BackColor = TransparencyKey;
                    sIpaddr1.Enabled = true;
                    sPort1.Enabled = true;
                    sTcpOnoff.Enabled = true;
                    sTLSOnoff.Enabled = true;
                }
            }
            else
            {
                if (elyTcpServer != null)
                {
                    elyTcpServer.Dispose();
                }
                // 取消控件未执行的委托

                MessageOutPutHandler($"Stop TCP Server[{ListenerIp}:{ListenerPort}] Successfully!");

                sIpaddr1.Enabled = true;
                sPort1.Enabled = true;
                sTcpOnoff.Enabled = true;
                sTLSOnoff.Enabled = true;
                sListen1.Text = "打开";
                sListen1.BackColor = TransparencyKey;

                IsTCPListening = false;
                return;
            }
        }

        private void sListen2_Click(object sender, EventArgs e)
        {
            string ListenerIp = sIpaddr2.Text;
            string ListenerPort = sPort2.Text;
            if (!IsUDPListening)
            {
                try
                {
                    // 开启后不允许修改服务器参数
                    sIpaddr2.Enabled = false;
                    sPort2.Enabled = false;
                    sUdpOnoff.Enabled = false;
                    sDTLSOnoff.Enabled = false;

                    if (sUdpOnoff.Checked)
                    {
                        elyUdpServer = new ElyUDP(ListenerIp, ListenerPort, ConnectedFDback, DisconnectedFDback,
                                                        DataReceivedFDback, false);
                        MessageOutPutHandler($"Start UDP Server[{ListenerIp}:{ListenerPort}] Successfully!");
                    }
                    else if (sDTLSOnoff.Checked)
                    {
                    }
                    IsUDPListening = true;
                    sListen2.Text = "已开启";
                    sListen2.BackColor = Color.LightGreen;
                }
                catch (Exception ex)
                {
                    if (elyUdpServer != null)
                    {
                        elyUdpServer.Dispose();
                    }

                    MessageOutPutHandler($"{ex.Message}");
                    sListen2.Text = "打开";
                    sListen2.BackColor = TransparencyKey;

                    sIpaddr2.Enabled = true;
                    sPort2.Enabled = true;
                    sUdpOnoff.Enabled = true;
                    sDTLSOnoff.Enabled = true;   

                    IsUDPListening = false;
                }
            }
            else
            {
                if (elyUdpServer != null)
                {
                    elyUdpServer.Dispose();
                }
                MessageOutPutHandler($"Stop UDP Server[{ListenerIp}:{ListenerPort}] Successfully!");

                sIpaddr2.Enabled = true;
                sPort2.Enabled = true;
                sUdpOnoff.Enabled = true;
                sDTLSOnoff.Enabled = true;
                sListen2.Text = "打开";
                sListen2.BackColor = TransparencyKey;

                IsUDPListening = false;
                return;
            }
        }

        private void ClearLog_Click(object sender, EventArgs e)
        {
            sLogTextbox.Text = string.Empty;
            RxBytes = TxBytes = 0;
            SequenceNum = 0;
            TxRxCounter.Text = $"数据统计：发送 {TxBytes} 字节, 接收 {RxBytes} 字节";
        }

        private void sConnectionlistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedClientList = sConnectionlistBox.SelectedItems;
        }

        private async void sSendButton1_Click(object sender, EventArgs e)
        {
            if (!IsSending)
            {
                if (selectedClientList == null || selectedClientList.Count == 0)
                    return;
                if (Datablock_1.Text == string.Empty)
                    return;
                string InputData = Datablock_1.Text;
                SendTimerSpanMs = Convert.ToInt32(TimerSpan1.Text);
                if (SendTimerSpanMs == 0 && numericUpDown1.Value > 1)
                {
                    TimerSpan1.Focus();
                    return;
                }
                try
                {
                    IsSending = true;
                    /// 禁止修改一些参数及触发按钮
                    sSendButton2.Enabled = false;
                    sSendButton3.Enabled = false;

                    sSendButton1.BackColor = Color.SkyBlue;
                    TimerSpan1.Enabled = false;
                    Datablock_1.Enabled = false;
                    numericUpDown1.Enabled = false;

                    CancellTS = new CancellationTokenSource();
                    int unUse = await SendDataToClient(InputData, numericUpDown1.Value, CancellTS.Token);
                }
                finally
                {
                    sSendButton2.Enabled = true;
                    sSendButton3.Enabled = true;

                    TimerSpan1.Enabled = true;
                    Datablock_1.Enabled = true;
                    numericUpDown1.Enabled = true;
                    sSendButton1.BackColor = TransparencyKey;
                    IsSending = false;
                }
            }
            else
            {
                if (CancellTS != null)
                {
                    CancellTS.Cancel();
                }     
                sSendButton2.Enabled = true;
                sSendButton3.Enabled = true;

                TimerSpan1.Enabled = true;
                Datablock_1.Enabled = true;
                numericUpDown1.Enabled = true;
                sSendButton1.BackColor = TransparencyKey;
                IsSending = false;
            }

        }

        private async void sSendButton2_Click(object sender, EventArgs e)
        {
            if (!IsSending)
            {
                if (selectedClientList == null || selectedClientList.Count == 0)
                    return;
                if (Datablock_2.Text == string.Empty)
                    return;

                string InputData = Datablock_2.Text;
                SendTimerSpanMs = Convert.ToInt32(TimerSpan2.Text);
                if (SendTimerSpanMs == 0 && numericUpDown2.Value > 1)
                {
                    TimerSpan2.Focus();
                    return;
                }
                try
                {
                    IsSending = true;
                    // 开启互斥
                    sSendButton1.Enabled = false;
                    sSendButton3.Enabled = false;

                    sSendButton2.BackColor = Color.SkyBlue;
                    TimerSpan2.Enabled = false;
                    Datablock_2.Enabled = false;
                    numericUpDown2.Enabled = false;

                    CancellTS = new CancellationTokenSource();
                    int unUse = await SendDataToClient(InputData, numericUpDown2.Value, CancellTS.Token);
                }
                finally
                {
                    // 取消互斥
                    sSendButton1.Enabled = true;
                    sSendButton3.Enabled = true;
                    // 反转状态
                    TimerSpan2.Enabled = true;
                    Datablock_2.Enabled = true;
                    numericUpDown2.Enabled = true;
                    sSendButton2.BackColor = TransparencyKey;
                    IsSending = false;
                }
            }
            else
            {
                if (CancellTS != null)
                {
                    CancellTS.Cancel();
                }
                sSendButton1.Enabled = true;
                sSendButton3.Enabled = true;

                TimerSpan2.Enabled = true;
                Datablock_2.Enabled = true;
                numericUpDown2.Enabled = true;
                sSendButton2.BackColor = TransparencyKey;
                IsSending = false;
            }
        }

        private async void sSendButton3_Click(object sender, EventArgs e)
        {
            if (!IsSending)
            {
                if (selectedClientList == null || selectedClientList.Count == 0)
                    return;
                if (Datablock_3.Text == string.Empty)
                    return;

                string InputData = Datablock_3.Text;
                SendTimerSpanMs = Convert.ToInt32(TimerSpan3.Text);
                if (SendTimerSpanMs == 0 && numericUpDown3.Value > 1)
                {
                    TimerSpan3.Focus();
                    return;
                }
                try
                {
                    IsSending = true;

                    sSendButton1.Enabled = false;
                    sSendButton2.Enabled = false;

                    sSendButton3.BackColor = Color.SkyBlue;
                    Datablock_3.Enabled = false;
                    TimerSpan3.Enabled = false;
                    numericUpDown3.Enabled = false;

                    CancellTS = new CancellationTokenSource();
                    int unUse = await SendDataToClient(InputData, numericUpDown3.Value, CancellTS.Token);
                }
                finally
                {
                    sSendButton1.Enabled = true;
                    sSendButton2.Enabled = true;

                    TimerSpan3.Enabled = true;
                    Datablock_3.Enabled = true;
                    numericUpDown3.Enabled = true;
                    sSendButton3.BackColor = TransparencyKey;
                    IsSending = false;
                }
            }
            else
            {
                if (CancellTS != null)
                {
                    CancellTS.Cancel();
                }
                sSendButton1.Enabled = true;
                sSendButton2.Enabled = true;
       
                TimerSpan3.Enabled = true;
                Datablock_3.Enabled = true;
                numericUpDown3.Enabled = true;
                sSendButton3.BackColor = TransparencyKey;
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
            NumericUpDown Num = sender as NumericUpDown;
            if (Num != null)
            {
                /// NumericUpDown 类型的Text属性不可见，转为其基类的Text来判断是否为空
                if (string.IsNullOrEmpty(((UpDownBase)Num).Text))
                {
                    ((UpDownBase)Num).Text = Num.Value.ToString();
                }
            }
        }

        private void sAllSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < sConnectionlistBox.Items.Count; i++)
            {
                sConnectionlistBox.SetSelected(i, true);
            }
        }

        private void sDisconnectCurrentConnection_Click(object sender, EventArgs e)
        {
            if (selectedClientList != null)
            {
                if (CancellTS != null)
                {
                    CancellTS.Cancel();
                }
                Disconnect();
            }
        }

        private void Disconnect()
        {
            while (selectedClientList.Count > 0)
            {
                if (selectedClientList[0].ToString() != string.Empty)
                {
                    if (elyTcpServer != null && selectedClientList[0].ToString().StartsWith("TCP"))
                        elyTcpServer.Disconnect(selectedClientList[0].ToString());
                    else if (elyTlsServer != null && selectedClientList[0].ToString().StartsWith("TLS"))
                        elyTlsServer.Disconnect(selectedClientList[0].ToString());
                    else if (elyUdpServer != null && selectedClientList[0].ToString().StartsWith("UDP"))
                        elyUdpServer.Disconnect(selectedClientList[0].ToString());
                    else if (elyDtlsServer != null && selectedClientList[0].ToString().StartsWith("DTLS"))
                        elyDtlsServer.Disconnect(selectedClientList[0].ToString());
                    
                    //删除的是选中的，所以 selectedClientList.Count 会自减 1
                    sConnectionlistBox.Items.Remove(selectedClientList[0]); 
                }
            }
        }
        #endregion

        #region KeyPressEvent
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

    }
}
