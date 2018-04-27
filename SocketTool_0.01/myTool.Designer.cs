namespace SocketTool
{
    partial class myTool
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            BackupUserConfigAndData();
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(myTool));
            this.TabCtrl = new System.Windows.Forms.TabControl();
            this.Server = new System.Windows.Forms.TabPage();
            this.CTimerSpan = new System.Windows.Forms.NumericUpDown();
            this.BTimerSpan = new System.Windows.Forms.NumericUpDown();
            this.ATimerSpan = new System.Windows.Forms.NumericUpDown();
            this.ShowHexData = new System.Windows.Forms.CheckBox();
            this.SaveLogToFile = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ADatablock = new System.Windows.Forms.TextBox();
            this.BDatablock = new System.Windows.Forms.TextBox();
            this.CDatablock = new System.Windows.Forms.TextBox();
            this.TxRxCounter = new System.Windows.Forms.Label();
            this.ClearLog = new System.Windows.Forms.Button();
            this.LogTextbox = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.CSendCount = new System.Windows.Forms.NumericUpDown();
            this.BSendCount = new System.Windows.Forms.NumericUpDown();
            this.ASendCount = new System.Windows.Forms.NumericUpDown();
            this.CSendButton = new System.Windows.Forms.Button();
            this.BSendButton = new System.Windows.Forms.Button();
            this.ASendButton = new System.Windows.Forms.Button();
            this.BsendDataLable = new System.Windows.Forms.Label();
            this.CsendDataLable = new System.Windows.Forms.Label();
            this.AsendDataLable = new System.Windows.Forms.Label();
            this.CurrentConnection = new System.Windows.Forms.GroupBox();
            this.ClientListBox = new System.Windows.Forms.ListBox();
            this.DisconnectSelectedClient = new System.Windows.Forms.Button();
            this.SelectAllClient = new System.Windows.Forms.Button();
            this.sNetCfg = new System.Windows.Forms.GroupBox();
            this.UdpIpAddr = new System.Windows.Forms.ComboBox();
            this.UdpPort = new System.Windows.Forms.ComboBox();
            this.TcpPort = new System.Windows.Forms.ComboBox();
            this.TcpIpAddr = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.DTLSOnoff = new System.Windows.Forms.RadioButton();
            this.UdpOnoff = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TLSOnoff = new System.Windows.Forms.RadioButton();
            this.TcpOnoff = new System.Windows.Forms.RadioButton();
            this.StartUdpDtlsServer = new System.Windows.Forms.Button();
            this.StartTcpTlsServer = new System.Windows.Forms.Button();
            this.UdpPortLable = new System.Windows.Forms.Label();
            this.UdpAddrLable = new System.Windows.Forms.Label();
            this.TcpPortLable = new System.Windows.Forms.Label();
            this.TcpAddrLable = new System.Windows.Forms.Label();
            this.SslConfig = new System.Windows.Forms.TabPage();
            this.CertControler = new System.Windows.Forms.GroupBox();
            this.PKCS12 = new System.Windows.Forms.RadioButton();
            this.PEM_DER = new System.Windows.Forms.RadioButton();
            this.CrtAndKey = new System.Windows.Forms.GroupBox();
            this.PemShowPasswd = new System.Windows.Forms.CheckBox();
            this.PriKeyPasswd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SetPrivateKey = new System.Windows.Forms.Button();
            this.SetPubCert = new System.Windows.Forms.Button();
            this.PrvtKey = new System.Windows.Forms.TextBox();
            this.PubCert = new System.Windows.Forms.TextBox();
            this.PrivateKeyLable = new System.Windows.Forms.Label();
            this.CertPubLable = new System.Windows.Forms.Label();
            this.MobiletekInc = new System.Windows.Forms.GroupBox();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.M2M = new System.Windows.Forms.PictureBox();
            this.IgnoreCert = new System.Windows.Forms.CheckBox();
            this.TlsConfig = new System.Windows.Forms.GroupBox();
            this.SslTips = new System.Windows.Forms.TextBox();
            this.GenerateSelfSignedCert = new System.Windows.Forms.GroupBox();
            this.GssShowPasswd = new System.Windows.Forms.CheckBox();
            this.GenerateCert = new System.Windows.Forms.Button();
            this.SelfSignedPasswd = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.SignatureAlgorithm = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.ImportShowPasswd = new System.Windows.Forms.CheckBox();
            this.SelectPfxCert = new System.Windows.Forms.Button();
            this.pfxFilePath = new System.Windows.Forms.TextBox();
            this.pfxPasswd = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.TlsVersion = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.NoIgnoreCert = new System.Windows.Forms.CheckBox();
            this.NoMutualAuth = new System.Windows.Forms.CheckBox();
            this.MutualAuth = new System.Windows.Forms.CheckBox();
            this.MyToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.TabCtrl.SuspendLayout();
            this.Server.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CTimerSpan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTimerSpan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ATimerSpan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CSendCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BSendCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ASendCount)).BeginInit();
            this.CurrentConnection.SuspendLayout();
            this.sNetCfg.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SslConfig.SuspendLayout();
            this.CertControler.SuspendLayout();
            this.CrtAndKey.SuspendLayout();
            this.MobiletekInc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.M2M)).BeginInit();
            this.TlsConfig.SuspendLayout();
            this.GenerateSelfSignedCert.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabCtrl
            // 
            this.TabCtrl.Controls.Add(this.Server);
            this.TabCtrl.Controls.Add(this.SslConfig);
            this.TabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabCtrl.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TabCtrl.Location = new System.Drawing.Point(0, 0);
            this.TabCtrl.Name = "TabCtrl";
            this.TabCtrl.SelectedIndex = 0;
            this.TabCtrl.Size = new System.Drawing.Size(944, 568);
            this.TabCtrl.TabIndex = 0;
            // 
            // Server
            // 
            this.Server.Controls.Add(this.CTimerSpan);
            this.Server.Controls.Add(this.BTimerSpan);
            this.Server.Controls.Add(this.ATimerSpan);
            this.Server.Controls.Add(this.ShowHexData);
            this.Server.Controls.Add(this.SaveLogToFile);
            this.Server.Controls.Add(this.label12);
            this.Server.Controls.Add(this.label11);
            this.Server.Controls.Add(this.label9);
            this.Server.Controls.Add(this.label8);
            this.Server.Controls.Add(this.label10);
            this.Server.Controls.Add(this.label7);
            this.Server.Controls.Add(this.ADatablock);
            this.Server.Controls.Add(this.BDatablock);
            this.Server.Controls.Add(this.CDatablock);
            this.Server.Controls.Add(this.TxRxCounter);
            this.Server.Controls.Add(this.ClearLog);
            this.Server.Controls.Add(this.LogTextbox);
            this.Server.Controls.Add(this.label6);
            this.Server.Controls.Add(this.label5);
            this.Server.Controls.Add(this.CSendCount);
            this.Server.Controls.Add(this.BSendCount);
            this.Server.Controls.Add(this.ASendCount);
            this.Server.Controls.Add(this.CSendButton);
            this.Server.Controls.Add(this.BSendButton);
            this.Server.Controls.Add(this.ASendButton);
            this.Server.Controls.Add(this.BsendDataLable);
            this.Server.Controls.Add(this.CsendDataLable);
            this.Server.Controls.Add(this.AsendDataLable);
            this.Server.Controls.Add(this.CurrentConnection);
            this.Server.Controls.Add(this.sNetCfg);
            this.Server.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Server.Location = new System.Drawing.Point(4, 26);
            this.Server.Name = "Server";
            this.Server.Padding = new System.Windows.Forms.Padding(3);
            this.Server.Size = new System.Drawing.Size(936, 538);
            this.Server.TabIndex = 0;
            this.Server.Text = "服务器";
            this.Server.UseVisualStyleBackColor = true;
            // 
            // CTimerSpan
            // 
            this.CTimerSpan.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CTimerSpan.Location = new System.Drawing.Point(713, 216);
            this.CTimerSpan.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.CTimerSpan.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CTimerSpan.Name = "CTimerSpan";
            this.CTimerSpan.Size = new System.Drawing.Size(48, 21);
            this.CTimerSpan.TabIndex = 33;
            this.CTimerSpan.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.CTimerSpan.Leave += new System.EventHandler(this.numericUpDown_Leave);
            // 
            // BTimerSpan
            // 
            this.BTimerSpan.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BTimerSpan.Location = new System.Drawing.Point(404, 216);
            this.BTimerSpan.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.BTimerSpan.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BTimerSpan.Name = "BTimerSpan";
            this.BTimerSpan.Size = new System.Drawing.Size(48, 21);
            this.BTimerSpan.TabIndex = 32;
            this.BTimerSpan.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.BTimerSpan.Leave += new System.EventHandler(this.numericUpDown_Leave);
            // 
            // ATimerSpan
            // 
            this.ATimerSpan.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ATimerSpan.Location = new System.Drawing.Point(96, 216);
            this.ATimerSpan.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.ATimerSpan.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ATimerSpan.Name = "ATimerSpan";
            this.ATimerSpan.Size = new System.Drawing.Size(48, 21);
            this.ATimerSpan.TabIndex = 31;
            this.ATimerSpan.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.ATimerSpan.Leave += new System.EventHandler(this.numericUpDown_Leave);
            // 
            // ShowHexData
            // 
            this.ShowHexData.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ShowHexData.AutoSize = true;
            this.ShowHexData.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ShowHexData.Location = new System.Drawing.Point(757, 511);
            this.ShowHexData.Name = "ShowHexData";
            this.ShowHexData.Size = new System.Drawing.Size(99, 21);
            this.ShowHexData.TabIndex = 30;
            this.ShowHexData.Text = "显示十六进制";
            this.ShowHexData.UseVisualStyleBackColor = true;
            // 
            // SaveLogToFile
            // 
            this.SaveLogToFile.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.SaveLogToFile.AutoSize = true;
            this.SaveLogToFile.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SaveLogToFile.Location = new System.Drawing.Point(673, 511);
            this.SaveLogToFile.Name = "SaveLogToFile";
            this.SaveLogToFile.Size = new System.Drawing.Size(75, 21);
            this.SaveLogToFile.TabIndex = 29;
            this.SaveLogToFile.Text = "写入日志";
            this.SaveLogToFile.UseVisualStyleBackColor = true;
            this.SaveLogToFile.CheckedChanged += new System.EventHandler(this.SaveLogToFile_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(853, 221);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(18, 16);
            this.label12.TabIndex = 25;
            this.label12.Text = "次";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(543, 221);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(18, 16);
            this.label11.TabIndex = 24;
            this.label11.Text = "次";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(454, 221);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 16);
            this.label9.TabIndex = 23;
            this.label9.Text = "ms/次";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(763, 221);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 16);
            this.label8.TabIndex = 22;
            this.label8.Text = "ms/次";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(233, 221);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 16);
            this.label10.TabIndex = 21;
            this.label10.Text = "次";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(145, 221);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 16);
            this.label7.TabIndex = 21;
            this.label7.Text = "ms/次";
            // 
            // ADatablock
            // 
            this.ADatablock.AllowDrop = true;
            this.ADatablock.BackColor = System.Drawing.SystemColors.Window;
            this.ADatablock.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ADatablock.Location = new System.Drawing.Point(6, 122);
            this.ADatablock.Multiline = true;
            this.ADatablock.Name = "ADatablock";
            this.ADatablock.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ADatablock.Size = new System.Drawing.Size(303, 89);
            this.ADatablock.TabIndex = 9;
            this.ADatablock.DragDrop += new System.Windows.Forms.DragEventHandler(this.Datablock_DragDrop);
            this.ADatablock.DragEnter += new System.Windows.Forms.DragEventHandler(this.Datablock_DragEnter);
            this.ADatablock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Ctrl_A_KeyPress);
            // 
            // BDatablock
            // 
            this.BDatablock.AllowDrop = true;
            this.BDatablock.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BDatablock.BackColor = System.Drawing.SystemColors.Window;
            this.BDatablock.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BDatablock.Location = new System.Drawing.Point(315, 122);
            this.BDatablock.Multiline = true;
            this.BDatablock.Name = "BDatablock";
            this.BDatablock.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.BDatablock.Size = new System.Drawing.Size(303, 89);
            this.BDatablock.TabIndex = 12;
            this.BDatablock.DragDrop += new System.Windows.Forms.DragEventHandler(this.Datablock_DragDrop);
            this.BDatablock.DragEnter += new System.Windows.Forms.DragEventHandler(this.Datablock_DragEnter);
            this.BDatablock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Ctrl_A_KeyPress);
            // 
            // CDatablock
            // 
            this.CDatablock.AllowDrop = true;
            this.CDatablock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CDatablock.BackColor = System.Drawing.SystemColors.Window;
            this.CDatablock.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CDatablock.Location = new System.Drawing.Point(624, 122);
            this.CDatablock.Multiline = true;
            this.CDatablock.Name = "CDatablock";
            this.CDatablock.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CDatablock.Size = new System.Drawing.Size(303, 89);
            this.CDatablock.TabIndex = 15;
            this.CDatablock.DragDrop += new System.Windows.Forms.DragEventHandler(this.Datablock_DragDrop);
            this.CDatablock.DragEnter += new System.Windows.Forms.DragEventHandler(this.Datablock_DragEnter);
            this.CDatablock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Ctrl_A_KeyPress);
            // 
            // TxRxCounter
            // 
            this.TxRxCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TxRxCounter.AutoSize = true;
            this.TxRxCounter.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxRxCounter.Location = new System.Drawing.Point(55, 514);
            this.TxRxCounter.Name = "TxRxCounter";
            this.TxRxCounter.Size = new System.Drawing.Size(245, 14);
            this.TxRxCounter.TabIndex = 20;
            this.TxRxCounter.Text = "数据统计：发送 0 字节, 接收 0 字节";
            // 
            // ClearLog
            // 
            this.ClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearLog.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClearLog.Location = new System.Drawing.Point(865, 509);
            this.ClearLog.Name = "ClearLog";
            this.ClearLog.Size = new System.Drawing.Size(62, 25);
            this.ClearLog.TabIndex = 19;
            this.ClearLog.Text = "清除";
            this.ClearLog.UseVisualStyleBackColor = true;
            this.ClearLog.Click += new System.EventHandler(this.ClearLog_Click);
            // 
            // LogTextbox
            // 
            this.LogTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogTextbox.BackColor = System.Drawing.SystemColors.Window;
            this.LogTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LogTextbox.DetectUrls = false;
            this.LogTextbox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LogTextbox.HideSelection = false;
            this.LogTextbox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.LogTextbox.Location = new System.Drawing.Point(4, 244);
            this.LogTextbox.Name = "LogTextbox";
            this.LogTextbox.ReadOnly = true;
            this.LogTextbox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.LogTextbox.ShowSelectionMargin = true;
            this.LogTextbox.Size = new System.Drawing.Size(926, 263);
            this.LogTextbox.TabIndex = 18;
            this.LogTextbox.Text = "";
            this.LogTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Ctrl_A_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 244);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 17);
            this.label6.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9.07563F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(11, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 13);
            this.label5.TabIndex = 16;
            // 
            // CSendCount
            // 
            this.CSendCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CSendCount.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CSendCount.Location = new System.Drawing.Point(804, 216);
            this.CSendCount.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.CSendCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CSendCount.Name = "CSendCount";
            this.CSendCount.Size = new System.Drawing.Size(48, 21);
            this.CSendCount.TabIndex = 16;
            this.CSendCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CSendCount.Leave += new System.EventHandler(this.numericUpDown_Leave);
            // 
            // BSendCount
            // 
            this.BSendCount.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BSendCount.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BSendCount.Location = new System.Drawing.Point(492, 216);
            this.BSendCount.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.BSendCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BSendCount.Name = "BSendCount";
            this.BSendCount.Size = new System.Drawing.Size(48, 21);
            this.BSendCount.TabIndex = 13;
            this.BSendCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BSendCount.Leave += new System.EventHandler(this.numericUpDown_Leave);
            // 
            // ASendCount
            // 
            this.ASendCount.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ASendCount.Location = new System.Drawing.Point(185, 216);
            this.ASendCount.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.ASendCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ASendCount.Name = "ASendCount";
            this.ASendCount.Size = new System.Drawing.Size(48, 21);
            this.ASendCount.TabIndex = 10;
            this.ASendCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ASendCount.Leave += new System.EventHandler(this.numericUpDown_Leave);
            // 
            // CSendButton
            // 
            this.CSendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CSendButton.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CSendButton.Location = new System.Drawing.Point(873, 214);
            this.CSendButton.Name = "CSendButton";
            this.CSendButton.Size = new System.Drawing.Size(48, 25);
            this.CSendButton.TabIndex = 17;
            this.CSendButton.Text = "发送";
            this.CSendButton.UseVisualStyleBackColor = true;
            this.CSendButton.Click += new System.EventHandler(this.CSendDataReq_Click);
            // 
            // BSendButton
            // 
            this.BSendButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BSendButton.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BSendButton.Location = new System.Drawing.Point(564, 214);
            this.BSendButton.Name = "BSendButton";
            this.BSendButton.Size = new System.Drawing.Size(48, 25);
            this.BSendButton.TabIndex = 14;
            this.BSendButton.Text = "发送";
            this.BSendButton.UseVisualStyleBackColor = true;
            this.BSendButton.Click += new System.EventHandler(this.BSendDataReq_Click);
            // 
            // ASendButton
            // 
            this.ASendButton.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ASendButton.Location = new System.Drawing.Point(255, 214);
            this.ASendButton.Name = "ASendButton";
            this.ASendButton.Size = new System.Drawing.Size(48, 25);
            this.ASendButton.TabIndex = 11;
            this.ASendButton.Text = "发送";
            this.ASendButton.UseVisualStyleBackColor = true;
            this.ASendButton.Click += new System.EventHandler(this.ASendDataReq_Click);
            // 
            // BsendDataLable
            // 
            this.BsendDataLable.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BsendDataLable.AutoSize = true;
            this.BsendDataLable.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BsendDataLable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BsendDataLable.Location = new System.Drawing.Point(313, 102);
            this.BsendDataLable.Name = "BsendDataLable";
            this.BsendDataLable.Size = new System.Drawing.Size(84, 14);
            this.BsendDataLable.TabIndex = 3;
            this.BsendDataLable.Text = "发送数据Ⅱ:";
            // 
            // CsendDataLable
            // 
            this.CsendDataLable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CsendDataLable.AutoSize = true;
            this.CsendDataLable.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CsendDataLable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CsendDataLable.Location = new System.Drawing.Point(622, 102);
            this.CsendDataLable.Name = "CsendDataLable";
            this.CsendDataLable.Size = new System.Drawing.Size(84, 14);
            this.CsendDataLable.TabIndex = 3;
            this.CsendDataLable.Text = "发送数据Ⅲ:";
            // 
            // AsendDataLable
            // 
            this.AsendDataLable.AutoSize = true;
            this.AsendDataLable.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AsendDataLable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.AsendDataLable.Location = new System.Drawing.Point(4, 102);
            this.AsendDataLable.Name = "AsendDataLable";
            this.AsendDataLable.Size = new System.Drawing.Size(84, 14);
            this.AsendDataLable.TabIndex = 3;
            this.AsendDataLable.Text = "发送数据Ⅰ:";
            // 
            // CurrentConnection
            // 
            this.CurrentConnection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CurrentConnection.Controls.Add(this.ClientListBox);
            this.CurrentConnection.Controls.Add(this.DisconnectSelectedClient);
            this.CurrentConnection.Controls.Add(this.SelectAllClient);
            this.CurrentConnection.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CurrentConnection.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CurrentConnection.Location = new System.Drawing.Point(570, 6);
            this.CurrentConnection.Name = "CurrentConnection";
            this.CurrentConnection.Size = new System.Drawing.Size(357, 84);
            this.CurrentConnection.TabIndex = 0;
            this.CurrentConnection.TabStop = false;
            this.CurrentConnection.Text = "当前连接";
            // 
            // ClientListBox
            // 
            this.ClientListBox.BackColor = System.Drawing.SystemColors.Window;
            this.ClientListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ClientListBox.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClientListBox.FormattingEnabled = true;
            this.ClientListBox.ItemHeight = 17;
            this.ClientListBox.Location = new System.Drawing.Point(8, 19);
            this.ClientListBox.Name = "ClientListBox";
            this.ClientListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ClientListBox.Size = new System.Drawing.Size(256, 53);
            this.ClientListBox.TabIndex = 0;
            this.ClientListBox.SelectedIndexChanged += new System.EventHandler(this.ClientListBox_SelectedIndexChanged);
            // 
            // DisconnectSelectedClient
            // 
            this.DisconnectSelectedClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DisconnectSelectedClient.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DisconnectSelectedClient.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DisconnectSelectedClient.Location = new System.Drawing.Point(274, 50);
            this.DisconnectSelectedClient.Name = "DisconnectSelectedClient";
            this.DisconnectSelectedClient.Size = new System.Drawing.Size(77, 25);
            this.DisconnectSelectedClient.TabIndex = 8;
            this.DisconnectSelectedClient.Text = "断开";
            this.DisconnectSelectedClient.UseVisualStyleBackColor = true;
            this.DisconnectSelectedClient.Click += new System.EventHandler(this.DisconnectSelectedClient_Click);
            // 
            // SelectAllClient
            // 
            this.SelectAllClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectAllClient.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SelectAllClient.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SelectAllClient.Location = new System.Drawing.Point(274, 16);
            this.SelectAllClient.Name = "SelectAllClient";
            this.SelectAllClient.Size = new System.Drawing.Size(77, 25);
            this.SelectAllClient.TabIndex = 7;
            this.SelectAllClient.Text = "全选";
            this.SelectAllClient.UseVisualStyleBackColor = true;
            this.SelectAllClient.Click += new System.EventHandler(this.SelectAll_Click);
            // 
            // sNetCfg
            // 
            this.sNetCfg.Controls.Add(this.UdpIpAddr);
            this.sNetCfg.Controls.Add(this.UdpPort);
            this.sNetCfg.Controls.Add(this.TcpPort);
            this.sNetCfg.Controls.Add(this.TcpIpAddr);
            this.sNetCfg.Controls.Add(this.panel2);
            this.sNetCfg.Controls.Add(this.panel1);
            this.sNetCfg.Controls.Add(this.StartUdpDtlsServer);
            this.sNetCfg.Controls.Add(this.StartTcpTlsServer);
            this.sNetCfg.Controls.Add(this.UdpPortLable);
            this.sNetCfg.Controls.Add(this.UdpAddrLable);
            this.sNetCfg.Controls.Add(this.TcpPortLable);
            this.sNetCfg.Controls.Add(this.TcpAddrLable);
            this.sNetCfg.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sNetCfg.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sNetCfg.Location = new System.Drawing.Point(4, 6);
            this.sNetCfg.Name = "sNetCfg";
            this.sNetCfg.Size = new System.Drawing.Size(557, 84);
            this.sNetCfg.TabIndex = 0;
            this.sNetCfg.TabStop = false;
            this.sNetCfg.Text = "网络配置";
            // 
            // UdpIpAddr
            // 
            this.UdpIpAddr.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.UdpIpAddr.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UdpIpAddr.FormattingEnabled = true;
            this.UdpIpAddr.Location = new System.Drawing.Point(74, 50);
            this.UdpIpAddr.MaxDropDownItems = 5;
            this.UdpIpAddr.Name = "UdpIpAddr";
            this.UdpIpAddr.Size = new System.Drawing.Size(136, 25);
            this.UdpIpAddr.TabIndex = 4;
            this.UdpIpAddr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboBoxIP_KeyPress);
            // 
            // UdpPort
            // 
            this.UdpPort.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.UdpPort.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UdpPort.FormattingEnabled = true;
            this.UdpPort.Location = new System.Drawing.Point(268, 50);
            this.UdpPort.Name = "UdpPort";
            this.UdpPort.Size = new System.Drawing.Size(59, 25);
            this.UdpPort.TabIndex = 5;
            this.UdpPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboBoxPort_KeyPress);
            // 
            // TcpPort
            // 
            this.TcpPort.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.TcpPort.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.TcpPort.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.TcpPort.DisplayMember = "uint16";
            this.TcpPort.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TcpPort.FormattingEnabled = true;
            this.TcpPort.Location = new System.Drawing.Point(268, 16);
            this.TcpPort.MaxLength = 5;
            this.TcpPort.Name = "TcpPort";
            this.TcpPort.Size = new System.Drawing.Size(59, 25);
            this.TcpPort.TabIndex = 2;
            this.TcpPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboBoxPort_KeyPress);
            // 
            // TcpIpAddr
            // 
            this.TcpIpAddr.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.TcpIpAddr.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TcpIpAddr.FormattingEnabled = true;
            this.TcpIpAddr.Location = new System.Drawing.Point(74, 16);
            this.TcpIpAddr.MaxDropDownItems = 5;
            this.TcpIpAddr.MaxLength = 15;
            this.TcpIpAddr.Name = "TcpIpAddr";
            this.TcpIpAddr.Size = new System.Drawing.Size(136, 25);
            this.TcpIpAddr.TabIndex = 1;
            this.TcpIpAddr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboBoxIP_KeyPress);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.DTLSOnoff);
            this.panel2.Controls.Add(this.UdpOnoff);
            this.panel2.Location = new System.Drawing.Point(352, 43);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(111, 35);
            this.panel2.TabIndex = 0;
            // 
            // DTLSOnoff
            // 
            this.DTLSOnoff.AutoSize = true;
            this.DTLSOnoff.Dock = System.Windows.Forms.DockStyle.Right;
            this.DTLSOnoff.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTLSOnoff.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.DTLSOnoff.Location = new System.Drawing.Point(53, 0);
            this.DTLSOnoff.Name = "DTLSOnoff";
            this.DTLSOnoff.Size = new System.Drawing.Size(58, 35);
            this.DTLSOnoff.TabIndex = 0;
            this.DTLSOnoff.Text = "DTLS";
            this.DTLSOnoff.UseVisualStyleBackColor = true;
            // 
            // UdpOnoff
            // 
            this.UdpOnoff.AutoSize = true;
            this.UdpOnoff.Checked = true;
            this.UdpOnoff.Dock = System.Windows.Forms.DockStyle.Left;
            this.UdpOnoff.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UdpOnoff.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.UdpOnoff.Location = new System.Drawing.Point(0, 0);
            this.UdpOnoff.Name = "UdpOnoff";
            this.UdpOnoff.Size = new System.Drawing.Size(50, 35);
            this.UdpOnoff.TabIndex = 0;
            this.UdpOnoff.TabStop = true;
            this.UdpOnoff.Text = "UDP";
            this.UdpOnoff.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TLSOnoff);
            this.panel1.Controls.Add(this.TcpOnoff);
            this.panel1.Location = new System.Drawing.Point(352, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(103, 35);
            this.panel1.TabIndex = 0;
            // 
            // TLSOnoff
            // 
            this.TLSOnoff.AutoSize = true;
            this.TLSOnoff.Dock = System.Windows.Forms.DockStyle.Right;
            this.TLSOnoff.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TLSOnoff.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.TLSOnoff.Location = new System.Drawing.Point(53, 0);
            this.TLSOnoff.Name = "TLSOnoff";
            this.TLSOnoff.Size = new System.Drawing.Size(50, 35);
            this.TLSOnoff.TabIndex = 0;
            this.TLSOnoff.Text = "TLS";
            this.TLSOnoff.UseVisualStyleBackColor = true;
            // 
            // TcpOnoff
            // 
            this.TcpOnoff.AutoSize = true;
            this.TcpOnoff.Checked = true;
            this.TcpOnoff.Dock = System.Windows.Forms.DockStyle.Left;
            this.TcpOnoff.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TcpOnoff.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.TcpOnoff.Location = new System.Drawing.Point(0, 0);
            this.TcpOnoff.Name = "TcpOnoff";
            this.TcpOnoff.Size = new System.Drawing.Size(50, 35);
            this.TcpOnoff.TabIndex = 0;
            this.TcpOnoff.TabStop = true;
            this.TcpOnoff.Text = "TCP";
            this.TcpOnoff.UseVisualStyleBackColor = true;
            // 
            // StartUdpDtlsServer
            // 
            this.StartUdpDtlsServer.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StartUdpDtlsServer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.StartUdpDtlsServer.Location = new System.Drawing.Point(469, 50);
            this.StartUdpDtlsServer.Name = "StartUdpDtlsServer";
            this.StartUdpDtlsServer.Size = new System.Drawing.Size(77, 25);
            this.StartUdpDtlsServer.TabIndex = 6;
            this.StartUdpDtlsServer.Text = "打开";
            this.StartUdpDtlsServer.UseVisualStyleBackColor = true;
            this.StartUdpDtlsServer.Click += new System.EventHandler(this.StartUDPServer_Click);
            // 
            // StartTcpTlsServer
            // 
            this.StartTcpTlsServer.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StartTcpTlsServer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.StartTcpTlsServer.Location = new System.Drawing.Point(469, 16);
            this.StartTcpTlsServer.Name = "StartTcpTlsServer";
            this.StartTcpTlsServer.Size = new System.Drawing.Size(77, 25);
            this.StartTcpTlsServer.TabIndex = 3;
            this.StartTcpTlsServer.Text = "打开";
            this.StartTcpTlsServer.UseVisualStyleBackColor = true;
            this.StartTcpTlsServer.Click += new System.EventHandler(this.StartTCPServer_Click);
            // 
            // UdpPortLable
            // 
            this.UdpPortLable.AutoSize = true;
            this.UdpPortLable.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UdpPortLable.ForeColor = System.Drawing.SystemColors.ControlText;
            this.UdpPortLable.Location = new System.Drawing.Point(224, 55);
            this.UdpPortLable.Name = "UdpPortLable";
            this.UdpPortLable.Size = new System.Drawing.Size(42, 14);
            this.UdpPortLable.TabIndex = 1;
            this.UdpPortLable.Text = "端口:";
            // 
            // UdpAddrLable
            // 
            this.UdpAddrLable.AutoSize = true;
            this.UdpAddrLable.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UdpAddrLable.ForeColor = System.Drawing.SystemColors.ControlText;
            this.UdpAddrLable.Location = new System.Drawing.Point(10, 55);
            this.UdpAddrLable.Name = "UdpAddrLable";
            this.UdpAddrLable.Size = new System.Drawing.Size(63, 14);
            this.UdpAddrLable.TabIndex = 0;
            this.UdpAddrLable.Text = "UDP地址:";
            // 
            // TcpPortLable
            // 
            this.TcpPortLable.AutoSize = true;
            this.TcpPortLable.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TcpPortLable.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TcpPortLable.Location = new System.Drawing.Point(224, 21);
            this.TcpPortLable.Name = "TcpPortLable";
            this.TcpPortLable.Size = new System.Drawing.Size(42, 14);
            this.TcpPortLable.TabIndex = 1;
            this.TcpPortLable.Text = "端口:";
            // 
            // TcpAddrLable
            // 
            this.TcpAddrLable.AutoSize = true;
            this.TcpAddrLable.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TcpAddrLable.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TcpAddrLable.Location = new System.Drawing.Point(10, 21);
            this.TcpAddrLable.Name = "TcpAddrLable";
            this.TcpAddrLable.Size = new System.Drawing.Size(63, 14);
            this.TcpAddrLable.TabIndex = 0;
            this.TcpAddrLable.Text = "TCP地址:";
            // 
            // SslConfig
            // 
            this.SslConfig.Controls.Add(this.CertControler);
            this.SslConfig.Controls.Add(this.CrtAndKey);
            this.SslConfig.Controls.Add(this.MobiletekInc);
            this.SslConfig.Controls.Add(this.IgnoreCert);
            this.SslConfig.Controls.Add(this.TlsConfig);
            this.SslConfig.Controls.Add(this.TlsVersion);
            this.SslConfig.Controls.Add(this.label15);
            this.SslConfig.Controls.Add(this.label16);
            this.SslConfig.Controls.Add(this.label13);
            this.SslConfig.Controls.Add(this.NoIgnoreCert);
            this.SslConfig.Controls.Add(this.NoMutualAuth);
            this.SslConfig.Controls.Add(this.MutualAuth);
            this.SslConfig.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SslConfig.Location = new System.Drawing.Point(4, 26);
            this.SslConfig.Name = "SslConfig";
            this.SslConfig.Size = new System.Drawing.Size(936, 538);
            this.SslConfig.TabIndex = 2;
            this.SslConfig.Text = "SSL配置";
            this.SslConfig.UseVisualStyleBackColor = true;
            // 
            // CertControler
            // 
            this.CertControler.Controls.Add(this.PKCS12);
            this.CertControler.Controls.Add(this.PEM_DER);
            this.CertControler.Location = new System.Drawing.Point(7, 7);
            this.CertControler.Name = "CertControler";
            this.CertControler.Size = new System.Drawing.Size(192, 89);
            this.CertControler.TabIndex = 16;
            this.CertControler.TabStop = false;
            this.CertControler.Text = "证书管理";
            // 
            // PKCS12
            // 
            this.PKCS12.AutoSize = true;
            this.PKCS12.Checked = true;
            this.PKCS12.Location = new System.Drawing.Point(6, 26);
            this.PKCS12.Name = "PKCS12";
            this.PKCS12.Size = new System.Drawing.Size(165, 18);
            this.PKCS12.TabIndex = 15;
            this.PKCS12.TabStop = true;
            this.PKCS12.Text = "使用 PKCS12 证书配置";
            this.PKCS12.UseVisualStyleBackColor = true;
            // 
            // PEM_DER
            // 
            this.PEM_DER.AutoSize = true;
            this.PEM_DER.Location = new System.Drawing.Point(6, 57);
            this.PEM_DER.Name = "PEM_DER";
            this.PEM_DER.Size = new System.Drawing.Size(172, 18);
            this.PEM_DER.TabIndex = 14;
            this.PEM_DER.Text = "使用 PEM/DER 证书配置";
            this.PEM_DER.UseVisualStyleBackColor = true;
            // 
            // CrtAndKey
            // 
            this.CrtAndKey.Controls.Add(this.PemShowPasswd);
            this.CrtAndKey.Controls.Add(this.PriKeyPasswd);
            this.CrtAndKey.Controls.Add(this.label1);
            this.CrtAndKey.Controls.Add(this.SetPrivateKey);
            this.CrtAndKey.Controls.Add(this.SetPubCert);
            this.CrtAndKey.Controls.Add(this.PrvtKey);
            this.CrtAndKey.Controls.Add(this.PubCert);
            this.CrtAndKey.Controls.Add(this.PrivateKeyLable);
            this.CrtAndKey.Controls.Add(this.CertPubLable);
            this.CrtAndKey.Location = new System.Drawing.Point(428, 7);
            this.CrtAndKey.Name = "CrtAndKey";
            this.CrtAndKey.Size = new System.Drawing.Size(500, 126);
            this.CrtAndKey.TabIndex = 13;
            this.CrtAndKey.TabStop = false;
            this.CrtAndKey.Text = "PEM/DER 证书配置";
            // 
            // PemShowPasswd
            // 
            this.PemShowPasswd.AutoSize = true;
            this.PemShowPasswd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.PemShowPasswd.Location = new System.Drawing.Point(409, 92);
            this.PemShowPasswd.Name = "PemShowPasswd";
            this.PemShowPasswd.Size = new System.Drawing.Size(80, 18);
            this.PemShowPasswd.TabIndex = 12;
            this.PemShowPasswd.Text = "显示密码";
            this.PemShowPasswd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.PemShowPasswd.UseVisualStyleBackColor = true;
            this.PemShowPasswd.CheckedChanged += new System.EventHandler(this.PemShowPasswd_CheckedChanged);
            // 
            // PriKeyPasswd
            // 
            this.PriKeyPasswd.Location = new System.Drawing.Point(106, 90);
            this.PriKeyPasswd.Name = "PriKeyPasswd";
            this.PriKeyPasswd.PasswordChar = '*';
            this.PriKeyPasswd.Size = new System.Drawing.Size(298, 23);
            this.PriKeyPasswd.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 14);
            this.label1.TabIndex = 8;
            this.label1.Text = "私钥访问密码：";
            // 
            // SetPrivateKey
            // 
            this.SetPrivateKey.Location = new System.Drawing.Point(409, 55);
            this.SetPrivateKey.Name = "SetPrivateKey";
            this.SetPrivateKey.Size = new System.Drawing.Size(84, 26);
            this.SetPrivateKey.TabIndex = 7;
            this.SetPrivateKey.Text = "选择证书";
            this.SetPrivateKey.UseVisualStyleBackColor = true;
            this.SetPrivateKey.Click += new System.EventHandler(this.SetPrivateKey_Click);
            // 
            // SetPubCert
            // 
            this.SetPubCert.Location = new System.Drawing.Point(409, 22);
            this.SetPubCert.Name = "SetPubCert";
            this.SetPubCert.Size = new System.Drawing.Size(84, 26);
            this.SetPubCert.TabIndex = 6;
            this.SetPubCert.Text = "选择证书";
            this.SetPubCert.UseVisualStyleBackColor = true;
            this.SetPubCert.Click += new System.EventHandler(this.SetPubKey_Click);
            // 
            // PrvtKey
            // 
            this.PrvtKey.Location = new System.Drawing.Point(106, 57);
            this.PrvtKey.MaxLength = 2048;
            this.PrvtKey.Name = "PrvtKey";
            this.PrvtKey.Size = new System.Drawing.Size(298, 23);
            this.PrvtKey.TabIndex = 3;
            // 
            // PubCert
            // 
            this.PubCert.Location = new System.Drawing.Point(106, 24);
            this.PubCert.MaxLength = 2048;
            this.PubCert.Name = "PubCert";
            this.PubCert.Size = new System.Drawing.Size(298, 23);
            this.PubCert.TabIndex = 2;
            // 
            // PrivateKeyLable
            // 
            this.PrivateKeyLable.AutoSize = true;
            this.PrivateKeyLable.Location = new System.Drawing.Point(7, 61);
            this.PrivateKeyLable.Name = "PrivateKeyLable";
            this.PrivateKeyLable.Size = new System.Drawing.Size(105, 14);
            this.PrivateKeyLable.TabIndex = 1;
            this.PrivateKeyLable.Text = "私钥证书文件：";
            // 
            // CertPubLable
            // 
            this.CertPubLable.AutoSize = true;
            this.CertPubLable.Location = new System.Drawing.Point(7, 28);
            this.CertPubLable.Name = "CertPubLable";
            this.CertPubLable.Size = new System.Drawing.Size(105, 14);
            this.CertPubLable.TabIndex = 0;
            this.CertPubLable.Text = "公钥证书文件：";
            // 
            // MobiletekInc
            // 
            this.MobiletekInc.Controls.Add(this.Logo);
            this.MobiletekInc.Controls.Add(this.M2M);
            this.MobiletekInc.Location = new System.Drawing.Point(428, 139);
            this.MobiletekInc.Name = "MobiletekInc";
            this.MobiletekInc.Size = new System.Drawing.Size(500, 364);
            this.MobiletekInc.TabIndex = 12;
            this.MobiletekInc.TabStop = false;
            this.MobiletekInc.Text = "MobileTek Inc";
            // 
            // Logo
            // 
            this.Logo.ErrorImage = null;
            this.Logo.Image = ((System.Drawing.Image)(resources.GetObject("Logo.Image")));
            this.Logo.InitialImage = null;
            this.Logo.Location = new System.Drawing.Point(97, 36);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(307, 47);
            this.Logo.TabIndex = 1;
            this.Logo.TabStop = false;
            this.MyToolTip.SetToolTip(this.Logo, "点击进入公司主页（www.mobiletek.cn）");
            this.Logo.Click += new System.EventHandler(this.DirectToHomePage_Click);
            // 
            // M2M
            // 
            this.M2M.ErrorImage = null;
            this.M2M.Image = ((System.Drawing.Image)(resources.GetObject("M2M.Image")));
            this.M2M.InitialImage = null;
            this.M2M.Location = new System.Drawing.Point(6, 120);
            this.M2M.Name = "M2M";
            this.M2M.Size = new System.Drawing.Size(488, 238);
            this.M2M.TabIndex = 0;
            this.M2M.TabStop = false;
            this.MyToolTip.SetToolTip(this.M2M, "点击进入公司主页（www.mobiletek.cn）");
            this.M2M.Click += new System.EventHandler(this.DirectToHomePage_Click);
            // 
            // IgnoreCert
            // 
            this.IgnoreCert.AutoSize = true;
            this.IgnoreCert.Location = new System.Drawing.Point(298, 45);
            this.IgnoreCert.Name = "IgnoreCert";
            this.IgnoreCert.Size = new System.Drawing.Size(47, 18);
            this.IgnoreCert.TabIndex = 5;
            this.IgnoreCert.Text = "YES";
            this.MyToolTip.SetToolTip(this.IgnoreCert, "是否接受无效的证书");
            this.IgnoreCert.UseVisualStyleBackColor = true;
            this.IgnoreCert.Click += new System.EventHandler(this.IgnoreCert_Click);
            // 
            // TlsConfig
            // 
            this.TlsConfig.Controls.Add(this.SslTips);
            this.TlsConfig.Controls.Add(this.GenerateSelfSignedCert);
            this.TlsConfig.Controls.Add(this.ImportShowPasswd);
            this.TlsConfig.Controls.Add(this.SelectPfxCert);
            this.TlsConfig.Controls.Add(this.pfxFilePath);
            this.TlsConfig.Controls.Add(this.pfxPasswd);
            this.TlsConfig.Controls.Add(this.label17);
            this.TlsConfig.Controls.Add(this.label14);
            this.TlsConfig.Location = new System.Drawing.Point(7, 102);
            this.TlsConfig.Name = "TlsConfig";
            this.TlsConfig.Size = new System.Drawing.Size(415, 422);
            this.TlsConfig.TabIndex = 11;
            this.TlsConfig.TabStop = false;
            this.TlsConfig.Text = "PKCS12 证书配置";
            // 
            // SslTips
            // 
            this.SslTips.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SslTips.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SslTips.Location = new System.Drawing.Point(12, 194);
            this.SslTips.Multiline = true;
            this.SslTips.Name = "SslTips";
            this.SslTips.ReadOnly = true;
            this.SslTips.Size = new System.Drawing.Size(391, 199);
            this.SslTips.TabIndex = 0;
            this.SslTips.Text = resources.GetString("SslTips.Text");
            // 
            // GenerateSelfSignedCert
            // 
            this.GenerateSelfSignedCert.Controls.Add(this.GssShowPasswd);
            this.GenerateSelfSignedCert.Controls.Add(this.GenerateCert);
            this.GenerateSelfSignedCert.Controls.Add(this.SelfSignedPasswd);
            this.GenerateSelfSignedCert.Controls.Add(this.label20);
            this.GenerateSelfSignedCert.Controls.Add(this.label19);
            this.GenerateSelfSignedCert.Controls.Add(this.SignatureAlgorithm);
            this.GenerateSelfSignedCert.Controls.Add(this.label18);
            this.GenerateSelfSignedCert.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.GenerateSelfSignedCert.Location = new System.Drawing.Point(12, 21);
            this.GenerateSelfSignedCert.Name = "GenerateSelfSignedCert";
            this.GenerateSelfSignedCert.Size = new System.Drawing.Size(391, 87);
            this.GenerateSelfSignedCert.TabIndex = 12;
            this.GenerateSelfSignedCert.TabStop = false;
            this.GenerateSelfSignedCert.Text = "生成自签名证书（仅限测试使用）";
            // 
            // GssShowPasswd
            // 
            this.GssShowPasswd.AutoSize = true;
            this.GssShowPasswd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GssShowPasswd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GssShowPasswd.Location = new System.Drawing.Point(307, 24);
            this.GssShowPasswd.Name = "GssShowPasswd";
            this.GssShowPasswd.Size = new System.Drawing.Size(80, 18);
            this.GssShowPasswd.TabIndex = 12;
            this.GssShowPasswd.Text = "显示密码";
            this.GssShowPasswd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.GssShowPasswd.UseVisualStyleBackColor = true;
            this.GssShowPasswd.CheckedChanged += new System.EventHandler(this.GssShowPasswd_CheckedChanged);
            // 
            // GenerateCert
            // 
            this.GenerateCert.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GenerateCert.Location = new System.Drawing.Point(320, 53);
            this.GenerateCert.Name = "GenerateCert";
            this.GenerateCert.Size = new System.Drawing.Size(64, 26);
            this.GenerateCert.TabIndex = 5;
            this.GenerateCert.Text = "生成";
            this.GenerateCert.UseVisualStyleBackColor = true;
            this.GenerateCert.Click += new System.EventHandler(this.GenerateCert_Click);
            // 
            // SelfSignedPasswd
            // 
            this.SelfSignedPasswd.Location = new System.Drawing.Point(81, 22);
            this.SelfSignedPasswd.MaxLength = 2048;
            this.SelfSignedPasswd.Name = "SelfSignedPasswd";
            this.SelfSignedPasswd.PasswordChar = '*';
            this.SelfSignedPasswd.Size = new System.Drawing.Size(220, 23);
            this.SelfSignedPasswd.TabIndex = 4;
            this.MyToolTip.SetToolTip(this.SelfSignedPasswd, "可选项");
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label20.Location = new System.Drawing.Point(7, 26);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(77, 14);
            this.label20.TabIndex = 3;
            this.label20.Text = "证书密钥：";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(10, 64);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(0, 14);
            this.label19.TabIndex = 2;
            // 
            // SignatureAlgorithm
            // 
            this.SignatureAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SignatureAlgorithm.FormattingEnabled = true;
            this.SignatureAlgorithm.Items.AddRange(new object[] {
            "MD5",
            "SHA1",
            "SHA256",
            "SHA384",
            "SHA512"});
            this.SignatureAlgorithm.Location = new System.Drawing.Point(81, 55);
            this.SignatureAlgorithm.Name = "SignatureAlgorithm";
            this.SignatureAlgorithm.Size = new System.Drawing.Size(220, 22);
            this.SignatureAlgorithm.TabIndex = 1;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label18.Location = new System.Drawing.Point(6, 59);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(77, 14);
            this.label18.TabIndex = 0;
            this.label18.Text = "签名算法：";
            // 
            // ImportShowPasswd
            // 
            this.ImportShowPasswd.AutoSize = true;
            this.ImportShowPasswd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ImportShowPasswd.Location = new System.Drawing.Point(319, 158);
            this.ImportShowPasswd.Name = "ImportShowPasswd";
            this.ImportShowPasswd.Size = new System.Drawing.Size(80, 18);
            this.ImportShowPasswd.TabIndex = 11;
            this.ImportShowPasswd.Text = "显示密码";
            this.ImportShowPasswd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ImportShowPasswd.UseVisualStyleBackColor = true;
            this.ImportShowPasswd.CheckedChanged += new System.EventHandler(this.ImportShowPasswd_CheckedChanged);
            // 
            // SelectPfxCert
            // 
            this.SelectPfxCert.Location = new System.Drawing.Point(319, 120);
            this.SelectPfxCert.Name = "SelectPfxCert";
            this.SelectPfxCert.Size = new System.Drawing.Size(84, 26);
            this.SelectPfxCert.TabIndex = 3;
            this.SelectPfxCert.Text = "选择证书";
            this.SelectPfxCert.UseVisualStyleBackColor = true;
            this.SelectPfxCert.Click += new System.EventHandler(this.SelectPfxCert_Click);
            // 
            // pfxFilePath
            // 
            this.pfxFilePath.Location = new System.Drawing.Point(93, 122);
            this.pfxFilePath.Name = "pfxFilePath";
            this.pfxFilePath.Size = new System.Drawing.Size(218, 23);
            this.pfxFilePath.TabIndex = 2;
            // 
            // pfxPasswd
            // 
            this.pfxPasswd.Location = new System.Drawing.Point(93, 156);
            this.pfxPasswd.MaxLength = 2048;
            this.pfxPasswd.Name = "pfxPasswd";
            this.pfxPasswd.PasswordChar = '*';
            this.pfxPasswd.Size = new System.Drawing.Size(218, 23);
            this.pfxPasswd.TabIndex = 10;
            this.MyToolTip.SetToolTip(this.pfxPasswd, "若有的话，可选项");
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(18, 160);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(77, 14);
            this.label17.TabIndex = 9;
            this.label17.Text = "证书密钥：";
            this.MyToolTip.SetToolTip(this.label17, "若有的话，可选项");
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(18, 126);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 14);
            this.label14.TabIndex = 0;
            this.label14.Text = "证书文件：";
            // 
            // TlsVersion
            // 
            this.TlsVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TlsVersion.FormattingEnabled = true;
            this.TlsVersion.Items.AddRange(new object[] {
            "Default",
            "SSL v2",
            "SSL v3",
            "TLS v1.0",
            "TLS v1.1",
            "TLS v1.2"});
            this.TlsVersion.Location = new System.Drawing.Point(298, 12);
            this.TlsVersion.Name = "TlsVersion";
            this.TlsVersion.Size = new System.Drawing.Size(97, 22);
            this.TlsVersion.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(220, 47);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 14);
            this.label15.TabIndex = 4;
            this.label15.Text = "忽略证书：";
            this.MyToolTip.SetToolTip(this.label15, "是否接受无效的证书");
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(220, 78);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 14);
            this.label16.TabIndex = 6;
            this.label16.Text = "双向认证：";
            this.MyToolTip.SetToolTip(this.label16, "双向认证 SSL 协议要求服务器和用户双方都有证书，单向认证 SSL 协议不需要客户拥有CA证书");
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(220, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 14);
            this.label13.TabIndex = 0;
            this.label13.Text = "协议版本：";
            // 
            // NoIgnoreCert
            // 
            this.NoIgnoreCert.AutoSize = true;
            this.NoIgnoreCert.Checked = true;
            this.NoIgnoreCert.CheckState = System.Windows.Forms.CheckState.Checked;
            this.NoIgnoreCert.Location = new System.Drawing.Point(360, 45);
            this.NoIgnoreCert.Name = "NoIgnoreCert";
            this.NoIgnoreCert.Size = new System.Drawing.Size(47, 18);
            this.NoIgnoreCert.TabIndex = 5;
            this.NoIgnoreCert.Text = "NO ";
            this.MyToolTip.SetToolTip(this.NoIgnoreCert, "是否接受无效的证书");
            this.NoIgnoreCert.UseVisualStyleBackColor = true;
            this.NoIgnoreCert.Click += new System.EventHandler(this.NoIgnoreCert_Click);
            // 
            // NoMutualAuth
            // 
            this.NoMutualAuth.AutoSize = true;
            this.NoMutualAuth.Checked = true;
            this.NoMutualAuth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.NoMutualAuth.Location = new System.Drawing.Point(360, 76);
            this.NoMutualAuth.Name = "NoMutualAuth";
            this.NoMutualAuth.Size = new System.Drawing.Size(47, 18);
            this.NoMutualAuth.TabIndex = 7;
            this.NoMutualAuth.Text = "NO ";
            this.MyToolTip.SetToolTip(this.NoMutualAuth, "双向认证 SSL 协议要求服务器和用户双方都有证书，单向认证 SSL 协议不需要客户拥有CA证书");
            this.NoMutualAuth.UseVisualStyleBackColor = true;
            this.NoMutualAuth.Click += new System.EventHandler(this.NoMutualAuth_Click);
            // 
            // MutualAuth
            // 
            this.MutualAuth.AutoSize = true;
            this.MutualAuth.Location = new System.Drawing.Point(298, 76);
            this.MutualAuth.Name = "MutualAuth";
            this.MutualAuth.Size = new System.Drawing.Size(47, 18);
            this.MutualAuth.TabIndex = 8;
            this.MutualAuth.Text = "YES";
            this.MyToolTip.SetToolTip(this.MutualAuth, "双向认证 SSL 协议要求服务器和用户双方都有证书，单向认证 SSL 协议不需要客户拥有CA证书");
            this.MutualAuth.UseVisualStyleBackColor = true;
            this.MutualAuth.Click += new System.EventHandler(this.MutualAuth_Click);
            // 
            // myTool
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(944, 568);
            this.Controls.Add(this.TabCtrl);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(960, 1080);
            this.MinimumSize = new System.Drawing.Size(960, 38);
            this.Name = "myTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "";
            this.Text = "YKSocketTool v0.03";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.myTool_KeyDown);
            this.TabCtrl.ResumeLayout(false);
            this.Server.ResumeLayout(false);
            this.Server.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CTimerSpan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTimerSpan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ATimerSpan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CSendCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BSendCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ASendCount)).EndInit();
            this.CurrentConnection.ResumeLayout(false);
            this.sNetCfg.ResumeLayout(false);
            this.sNetCfg.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.SslConfig.ResumeLayout(false);
            this.SslConfig.PerformLayout();
            this.CertControler.ResumeLayout(false);
            this.CertControler.PerformLayout();
            this.CrtAndKey.ResumeLayout(false);
            this.CrtAndKey.PerformLayout();
            this.MobiletekInc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.M2M)).EndInit();
            this.TlsConfig.ResumeLayout(false);
            this.TlsConfig.PerformLayout();
            this.GenerateSelfSignedCert.ResumeLayout(false);
            this.GenerateSelfSignedCert.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage Server;
        private System.Windows.Forms.GroupBox sNetCfg;
        private System.Windows.Forms.Label TcpPortLable;
        private System.Windows.Forms.Label TcpAddrLable;
        private System.Windows.Forms.Button StartTcpTlsServer;
        private System.Windows.Forms.Button StartUdpDtlsServer;
        private System.Windows.Forms.Label UdpPortLable;
        private System.Windows.Forms.Label UdpAddrLable;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton DTLSOnoff;
        private System.Windows.Forms.RadioButton UdpOnoff;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton TLSOnoff;
        private System.Windows.Forms.RadioButton TcpOnoff;
        private System.Windows.Forms.GroupBox CurrentConnection;
        private System.Windows.Forms.ComboBox UdpIpAddr;
        private System.Windows.Forms.ComboBox UdpPort;
        private System.Windows.Forms.ComboBox TcpPort;
        private System.Windows.Forms.ComboBox TcpIpAddr;
        private System.Windows.Forms.Button DisconnectSelectedClient;
        private System.Windows.Forms.Button SelectAllClient;
        private System.Windows.Forms.ListBox ClientListBox;
        private System.Windows.Forms.Label BsendDataLable;
        private System.Windows.Forms.Label CsendDataLable;
        private System.Windows.Forms.TextBox CDatablock;
        private System.Windows.Forms.TextBox BDatablock;
        private System.Windows.Forms.TextBox ADatablock;
        private System.Windows.Forms.Label AsendDataLable;
        private System.Windows.Forms.NumericUpDown ASendCount;
        private System.Windows.Forms.Button CSendButton;
        private System.Windows.Forms.Button BSendButton;
        private System.Windows.Forms.Button ASendButton;
        private System.Windows.Forms.TabPage SslConfig;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown CSendCount;
        private System.Windows.Forms.NumericUpDown BSendCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox LogTextbox;
        private System.Windows.Forms.Button ClearLog;
        private System.Windows.Forms.Label TxRxCounter;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TabControl TabCtrl;
        private System.Windows.Forms.ComboBox TlsVersion;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button SelectPfxCert;
        private System.Windows.Forms.TextBox pfxFilePath;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox MutualAuth;
        private System.Windows.Forms.TextBox pfxPasswd;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox TlsConfig;
        private System.Windows.Forms.GroupBox MobiletekInc;
        private System.Windows.Forms.ToolTip MyToolTip;
        private System.Windows.Forms.CheckBox ImportShowPasswd;
        private System.Windows.Forms.GroupBox GenerateSelfSignedCert;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox SignatureAlgorithm;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button GenerateCert;
        private System.Windows.Forms.TextBox SelfSignedPasswd;
        private System.Windows.Forms.CheckBox GssShowPasswd;
        private System.Windows.Forms.PictureBox M2M;
        private System.Windows.Forms.TextBox SslTips;
        private System.Windows.Forms.GroupBox CrtAndKey;
        private System.Windows.Forms.Label CertPubLable;
        private System.Windows.Forms.Label PrivateKeyLable;
        private System.Windows.Forms.TextBox PrvtKey;
        private System.Windows.Forms.TextBox PubCert;
        private System.Windows.Forms.Button SetPrivateKey;
        private System.Windows.Forms.Button SetPubCert;
        private System.Windows.Forms.RadioButton PKCS12;
        private System.Windows.Forms.RadioButton PEM_DER;
        private System.Windows.Forms.GroupBox CertControler;
        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.CheckBox IgnoreCert;
        private System.Windows.Forms.CheckBox NoIgnoreCert;
        private System.Windows.Forms.CheckBox NoMutualAuth;
        private System.Windows.Forms.CheckBox ShowHexData;
        private System.Windows.Forms.CheckBox SaveLogToFile;
        private System.Windows.Forms.CheckBox PemShowPasswd;
        private System.Windows.Forms.TextBox PriKeyPasswd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown ATimerSpan;
        private System.Windows.Forms.NumericUpDown CTimerSpan;
        private System.Windows.Forms.NumericUpDown BTimerSpan;
    }
}

