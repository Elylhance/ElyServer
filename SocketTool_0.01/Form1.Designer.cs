﻿namespace MySocketTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(myTool));
            this.TabCtrl = new System.Windows.Forms.TabControl();
            this.Server = new System.Windows.Forms.TabPage();
            this.TimerSpan3 = new System.Windows.Forms.TextBox();
            this.TimerSpan2 = new System.Windows.Forms.TextBox();
            this.TimerSpan1 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Datablock_1 = new System.Windows.Forms.TextBox();
            this.Datablock_2 = new System.Windows.Forms.TextBox();
            this.Datablock_3 = new System.Windows.Forms.TextBox();
            this.TxRxCounter = new System.Windows.Forms.Label();
            this.ClearLog = new System.Windows.Forms.Button();
            this.sLogTextbox = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.sSendButton3 = new System.Windows.Forms.Button();
            this.sSendButton2 = new System.Windows.Forms.Button();
            this.sSendButton1 = new System.Windows.Forms.Button();
            this.sSDlable2 = new System.Windows.Forms.Label();
            this.sSDlable3 = new System.Windows.Forms.Label();
            this.sSDlable1 = new System.Windows.Forms.Label();
            this.sCurrentConnection = new System.Windows.Forms.GroupBox();
            this.sConnectionlistBox = new System.Windows.Forms.ListBox();
            this.sDisconnectCurrentConnection = new System.Windows.Forms.Button();
            this.sAllSelect = new System.Windows.Forms.Button();
            this.sNetCfg = new System.Windows.Forms.GroupBox();
            this.sIpaddr2 = new System.Windows.Forms.ComboBox();
            this.sPort2 = new System.Windows.Forms.ComboBox();
            this.sPort1 = new System.Windows.Forms.ComboBox();
            this.sIpaddr1 = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.sDTLSOnoff = new System.Windows.Forms.RadioButton();
            this.sUdpOnoff = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sTLSOnoff = new System.Windows.Forms.RadioButton();
            this.sTcpOnoff = new System.Windows.Forms.RadioButton();
            this.sListen2 = new System.Windows.Forms.Button();
            this.sListen1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Client = new System.Windows.Forms.TabPage();
            this.SSLCfg = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.TabCtrl.SuspendLayout();
            this.Server.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.sCurrentConnection.SuspendLayout();
            this.sNetCfg.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabCtrl
            // 
            this.TabCtrl.Controls.Add(this.Server);
            this.TabCtrl.Controls.Add(this.Client);
            this.TabCtrl.Controls.Add(this.SSLCfg);
            this.TabCtrl.Controls.Add(this.tabPage1);
            this.TabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabCtrl.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TabCtrl.Location = new System.Drawing.Point(0, 0);
            this.TabCtrl.Name = "TabCtrl";
            this.TabCtrl.SelectedIndex = 0;
            this.TabCtrl.Size = new System.Drawing.Size(944, 568);
            this.TabCtrl.TabIndex = 0;
            // 
            // Server
            // 
            this.Server.Controls.Add(this.TimerSpan3);
            this.Server.Controls.Add(this.TimerSpan2);
            this.Server.Controls.Add(this.TimerSpan1);
            this.Server.Controls.Add(this.label12);
            this.Server.Controls.Add(this.label11);
            this.Server.Controls.Add(this.label9);
            this.Server.Controls.Add(this.label8);
            this.Server.Controls.Add(this.label10);
            this.Server.Controls.Add(this.label7);
            this.Server.Controls.Add(this.Datablock_1);
            this.Server.Controls.Add(this.Datablock_2);
            this.Server.Controls.Add(this.Datablock_3);
            this.Server.Controls.Add(this.TxRxCounter);
            this.Server.Controls.Add(this.ClearLog);
            this.Server.Controls.Add(this.sLogTextbox);
            this.Server.Controls.Add(this.label6);
            this.Server.Controls.Add(this.label5);
            this.Server.Controls.Add(this.numericUpDown3);
            this.Server.Controls.Add(this.numericUpDown2);
            this.Server.Controls.Add(this.numericUpDown1);
            this.Server.Controls.Add(this.sSendButton3);
            this.Server.Controls.Add(this.sSendButton2);
            this.Server.Controls.Add(this.sSendButton1);
            this.Server.Controls.Add(this.sSDlable2);
            this.Server.Controls.Add(this.sSDlable3);
            this.Server.Controls.Add(this.sSDlable1);
            this.Server.Controls.Add(this.sCurrentConnection);
            this.Server.Controls.Add(this.sNetCfg);
            this.Server.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Server.Location = new System.Drawing.Point(4, 29);
            this.Server.Name = "Server";
            this.Server.Padding = new System.Windows.Forms.Padding(3);
            this.Server.Size = new System.Drawing.Size(936, 535);
            this.Server.TabIndex = 0;
            this.Server.Text = "服务器";
            this.Server.UseVisualStyleBackColor = true;
            // 
            // TimerSpan3
            // 
            this.TimerSpan3.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TimerSpan3.Location = new System.Drawing.Point(732, 216);
            this.TimerSpan3.MaxLength = 7;
            this.TimerSpan3.Name = "TimerSpan3";
            this.TimerSpan3.Size = new System.Drawing.Size(30, 21);
            this.TimerSpan3.TabIndex = 28;
            this.TimerSpan3.Text = "500";
            this.TimerSpan3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TimerSpan_KeyPress);
            this.TimerSpan3.Leave += new System.EventHandler(this.TimerSpan_Leave);
            // 
            // TimerSpan2
            // 
            this.TimerSpan2.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TimerSpan2.Location = new System.Drawing.Point(423, 216);
            this.TimerSpan2.MaxLength = 7;
            this.TimerSpan2.Name = "TimerSpan2";
            this.TimerSpan2.Size = new System.Drawing.Size(30, 21);
            this.TimerSpan2.TabIndex = 27;
            this.TimerSpan2.Text = "500";
            this.TimerSpan2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TimerSpan_KeyPress);
            this.TimerSpan2.Leave += new System.EventHandler(this.TimerSpan_Leave);
            // 
            // TimerSpan1
            // 
            this.TimerSpan1.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TimerSpan1.Location = new System.Drawing.Point(114, 216);
            this.TimerSpan1.MaxLength = 7;
            this.TimerSpan1.Name = "TimerSpan1";
            this.TimerSpan1.Size = new System.Drawing.Size(30, 21);
            this.TimerSpan1.TabIndex = 26;
            this.TimerSpan1.Text = "500";
            this.TimerSpan1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TimerSpan_KeyPress);
            this.TimerSpan1.Leave += new System.EventHandler(this.TimerSpan_Leave);
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
            // Datablock_1
            // 
            this.Datablock_1.BackColor = System.Drawing.SystemColors.Window;
            this.Datablock_1.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Datablock_1.Location = new System.Drawing.Point(6, 122);
            this.Datablock_1.Multiline = true;
            this.Datablock_1.Name = "Datablock_1";
            this.Datablock_1.Size = new System.Drawing.Size(303, 89);
            this.Datablock_1.TabIndex = 9;
            this.Datablock_1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Ctrl_A_KeyPress);
            // 
            // Datablock_2
            // 
            this.Datablock_2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Datablock_2.BackColor = System.Drawing.SystemColors.Window;
            this.Datablock_2.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Datablock_2.Location = new System.Drawing.Point(315, 122);
            this.Datablock_2.Multiline = true;
            this.Datablock_2.Name = "Datablock_2";
            this.Datablock_2.Size = new System.Drawing.Size(303, 89);
            this.Datablock_2.TabIndex = 12;
            this.Datablock_2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Ctrl_A_KeyPress);
            // 
            // Datablock_3
            // 
            this.Datablock_3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Datablock_3.BackColor = System.Drawing.SystemColors.Window;
            this.Datablock_3.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Datablock_3.Location = new System.Drawing.Point(624, 122);
            this.Datablock_3.Multiline = true;
            this.Datablock_3.Name = "Datablock_3";
            this.Datablock_3.Size = new System.Drawing.Size(303, 89);
            this.Datablock_3.TabIndex = 15;
            this.Datablock_3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Ctrl_A_KeyPress);
            // 
            // TxRxCounter
            // 
            this.TxRxCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TxRxCounter.AutoSize = true;
            this.TxRxCounter.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxRxCounter.Location = new System.Drawing.Point(55, 515);
            this.TxRxCounter.Name = "TxRxCounter";
            this.TxRxCounter.Size = new System.Drawing.Size(245, 14);
            this.TxRxCounter.TabIndex = 20;
            this.TxRxCounter.Text = "数据统计：发送 0 字节, 接收 0 字节";
            // 
            // ClearLog
            // 
            this.ClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearLog.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClearLog.Location = new System.Drawing.Point(856, 510);
            this.ClearLog.Name = "ClearLog";
            this.ClearLog.Size = new System.Drawing.Size(69, 25);
            this.ClearLog.TabIndex = 19;
            this.ClearLog.Text = "清除";
            this.ClearLog.UseVisualStyleBackColor = true;
            this.ClearLog.Click += new System.EventHandler(this.ClearLog_Click);
            // 
            // sLogTextbox
            // 
            this.sLogTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sLogTextbox.BackColor = System.Drawing.SystemColors.Window;
            this.sLogTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sLogTextbox.DetectUrls = false;
            this.sLogTextbox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sLogTextbox.HideSelection = false;
            this.sLogTextbox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sLogTextbox.Location = new System.Drawing.Point(4, 244);
            this.sLogTextbox.Name = "sLogTextbox";
            this.sLogTextbox.ReadOnly = true;
            this.sLogTextbox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.sLogTextbox.ShowSelectionMargin = true;
            this.sLogTextbox.Size = new System.Drawing.Size(926, 260);
            this.sLogTextbox.TabIndex = 18;
            this.sLogTextbox.Text = "";
            this.sLogTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Ctrl_A_KeyPress);
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
            // numericUpDown3
            // 
            this.numericUpDown3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numericUpDown3.Location = new System.Drawing.Point(804, 216);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDown3.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(48, 21);
            this.numericUpDown3.TabIndex = 16;
            this.numericUpDown3.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown3.Leave += new System.EventHandler(this.numericUpDown_Leave);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericUpDown2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numericUpDown2.Location = new System.Drawing.Point(492, 216);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(48, 21);
            this.numericUpDown2.TabIndex = 13;
            this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Leave += new System.EventHandler(this.numericUpDown_Leave);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numericUpDown1.Location = new System.Drawing.Point(185, 216);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(48, 21);
            this.numericUpDown1.TabIndex = 10;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Leave += new System.EventHandler(this.numericUpDown_Leave);
            // 
            // sSendButton3
            // 
            this.sSendButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sSendButton3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sSendButton3.Location = new System.Drawing.Point(879, 214);
            this.sSendButton3.Name = "sSendButton3";
            this.sSendButton3.Size = new System.Drawing.Size(48, 25);
            this.sSendButton3.TabIndex = 17;
            this.sSendButton3.Text = "发送";
            this.sSendButton3.UseVisualStyleBackColor = true;
            this.sSendButton3.Click += new System.EventHandler(this.sSendButton3_Click);
            // 
            // sSendButton2
            // 
            this.sSendButton2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.sSendButton2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sSendButton2.Location = new System.Drawing.Point(570, 214);
            this.sSendButton2.Name = "sSendButton2";
            this.sSendButton2.Size = new System.Drawing.Size(48, 25);
            this.sSendButton2.TabIndex = 14;
            this.sSendButton2.Text = "发送";
            this.sSendButton2.UseVisualStyleBackColor = true;
            this.sSendButton2.Click += new System.EventHandler(this.sSendButton2_Click);
            // 
            // sSendButton1
            // 
            this.sSendButton1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sSendButton1.Location = new System.Drawing.Point(261, 214);
            this.sSendButton1.Name = "sSendButton1";
            this.sSendButton1.Size = new System.Drawing.Size(48, 25);
            this.sSendButton1.TabIndex = 11;
            this.sSendButton1.Text = "发送";
            this.sSendButton1.UseVisualStyleBackColor = true;
            this.sSendButton1.Click += new System.EventHandler(this.sSendButton1_Click);
            // 
            // sSDlable2
            // 
            this.sSDlable2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.sSDlable2.AutoSize = true;
            this.sSDlable2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sSDlable2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.sSDlable2.Location = new System.Drawing.Point(313, 102);
            this.sSDlable2.Name = "sSDlable2";
            this.sSDlable2.Size = new System.Drawing.Size(77, 14);
            this.sSDlable2.TabIndex = 3;
            this.sSDlable2.Text = "发送数据2:";
            // 
            // sSDlable3
            // 
            this.sSDlable3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sSDlable3.AutoSize = true;
            this.sSDlable3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sSDlable3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.sSDlable3.Location = new System.Drawing.Point(622, 102);
            this.sSDlable3.Name = "sSDlable3";
            this.sSDlable3.Size = new System.Drawing.Size(77, 14);
            this.sSDlable3.TabIndex = 3;
            this.sSDlable3.Text = "发送数据3:";
            // 
            // sSDlable1
            // 
            this.sSDlable1.AutoSize = true;
            this.sSDlable1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sSDlable1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.sSDlable1.Location = new System.Drawing.Point(4, 102);
            this.sSDlable1.Name = "sSDlable1";
            this.sSDlable1.Size = new System.Drawing.Size(77, 14);
            this.sSDlable1.TabIndex = 3;
            this.sSDlable1.Text = "发送数据1:";
            // 
            // sCurrentConnection
            // 
            this.sCurrentConnection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sCurrentConnection.Controls.Add(this.sConnectionlistBox);
            this.sCurrentConnection.Controls.Add(this.sDisconnectCurrentConnection);
            this.sCurrentConnection.Controls.Add(this.sAllSelect);
            this.sCurrentConnection.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sCurrentConnection.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.sCurrentConnection.Location = new System.Drawing.Point(584, 6);
            this.sCurrentConnection.Name = "sCurrentConnection";
            this.sCurrentConnection.Size = new System.Drawing.Size(346, 84);
            this.sCurrentConnection.TabIndex = 0;
            this.sCurrentConnection.TabStop = false;
            this.sCurrentConnection.Text = "当前连接";
            // 
            // sConnectionlistBox
            // 
            this.sConnectionlistBox.BackColor = System.Drawing.SystemColors.Window;
            this.sConnectionlistBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sConnectionlistBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.sConnectionlistBox.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sConnectionlistBox.FormattingEnabled = true;
            this.sConnectionlistBox.ItemHeight = 17;
            this.sConnectionlistBox.Location = new System.Drawing.Point(3, 19);
            this.sConnectionlistBox.Name = "sConnectionlistBox";
            this.sConnectionlistBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.sConnectionlistBox.Size = new System.Drawing.Size(246, 62);
            this.sConnectionlistBox.TabIndex = 0;
            this.sConnectionlistBox.SelectedIndexChanged += new System.EventHandler(this.sConnectionlistBox_SelectedIndexChanged);
            // 
            // sDisconnectCurrentConnection
            // 
            this.sDisconnectCurrentConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sDisconnectCurrentConnection.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sDisconnectCurrentConnection.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sDisconnectCurrentConnection.Location = new System.Drawing.Point(261, 50);
            this.sDisconnectCurrentConnection.Name = "sDisconnectCurrentConnection";
            this.sDisconnectCurrentConnection.Size = new System.Drawing.Size(77, 25);
            this.sDisconnectCurrentConnection.TabIndex = 8;
            this.sDisconnectCurrentConnection.Text = "断开";
            this.sDisconnectCurrentConnection.UseVisualStyleBackColor = true;
            this.sDisconnectCurrentConnection.Click += new System.EventHandler(this.sDisconnectCurrentConnection_Click);
            // 
            // sAllSelect
            // 
            this.sAllSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sAllSelect.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sAllSelect.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sAllSelect.Location = new System.Drawing.Point(261, 16);
            this.sAllSelect.Name = "sAllSelect";
            this.sAllSelect.Size = new System.Drawing.Size(77, 25);
            this.sAllSelect.TabIndex = 7;
            this.sAllSelect.Text = "全选";
            this.sAllSelect.UseVisualStyleBackColor = true;
            this.sAllSelect.Click += new System.EventHandler(this.sAllSelect_Click);
            // 
            // sNetCfg
            // 
            this.sNetCfg.Controls.Add(this.sIpaddr2);
            this.sNetCfg.Controls.Add(this.sPort2);
            this.sNetCfg.Controls.Add(this.sPort1);
            this.sNetCfg.Controls.Add(this.sIpaddr1);
            this.sNetCfg.Controls.Add(this.panel2);
            this.sNetCfg.Controls.Add(this.panel1);
            this.sNetCfg.Controls.Add(this.sListen2);
            this.sNetCfg.Controls.Add(this.sListen1);
            this.sNetCfg.Controls.Add(this.label4);
            this.sNetCfg.Controls.Add(this.label3);
            this.sNetCfg.Controls.Add(this.label2);
            this.sNetCfg.Controls.Add(this.label1);
            this.sNetCfg.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sNetCfg.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.sNetCfg.Location = new System.Drawing.Point(4, 6);
            this.sNetCfg.Name = "sNetCfg";
            this.sNetCfg.Size = new System.Drawing.Size(560, 84);
            this.sNetCfg.TabIndex = 0;
            this.sNetCfg.TabStop = false;
            this.sNetCfg.Text = "网络配置";
            // 
            // sIpaddr2
            // 
            this.sIpaddr2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.sIpaddr2.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sIpaddr2.FormattingEnabled = true;
            this.sIpaddr2.Location = new System.Drawing.Point(74, 50);
            this.sIpaddr2.MaxDropDownItems = 5;
            this.sIpaddr2.Name = "sIpaddr2";
            this.sIpaddr2.Size = new System.Drawing.Size(136, 25);
            this.sIpaddr2.TabIndex = 4;
            this.sIpaddr2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboBoxIP_KeyPress);
            // 
            // sPort2
            // 
            this.sPort2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.sPort2.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sPort2.FormattingEnabled = true;
            this.sPort2.Location = new System.Drawing.Point(268, 50);
            this.sPort2.Name = "sPort2";
            this.sPort2.Size = new System.Drawing.Size(59, 25);
            this.sPort2.TabIndex = 5;
            this.sPort2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboBoxPort_KeyPress);
            // 
            // sPort1
            // 
            this.sPort1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.sPort1.DisplayMember = "uint16";
            this.sPort1.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sPort1.FormattingEnabled = true;
            this.sPort1.Location = new System.Drawing.Point(268, 16);
            this.sPort1.MaxLength = 5;
            this.sPort1.Name = "sPort1";
            this.sPort1.Size = new System.Drawing.Size(59, 25);
            this.sPort1.TabIndex = 2;
            this.sPort1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboBoxPort_KeyPress);
            // 
            // sIpaddr1
            // 
            this.sIpaddr1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.sIpaddr1.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sIpaddr1.FormattingEnabled = true;
            this.sIpaddr1.Location = new System.Drawing.Point(74, 16);
            this.sIpaddr1.MaxDropDownItems = 5;
            this.sIpaddr1.MaxLength = 15;
            this.sIpaddr1.Name = "sIpaddr1";
            this.sIpaddr1.Size = new System.Drawing.Size(136, 25);
            this.sIpaddr1.TabIndex = 1;
            this.sIpaddr1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboBoxIP_KeyPress);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.sDTLSOnoff);
            this.panel2.Controls.Add(this.sUdpOnoff);
            this.panel2.Location = new System.Drawing.Point(338, 43);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(111, 35);
            this.panel2.TabIndex = 0;
            // 
            // sDTLSOnoff
            // 
            this.sDTLSOnoff.AutoSize = true;
            this.sDTLSOnoff.Dock = System.Windows.Forms.DockStyle.Right;
            this.sDTLSOnoff.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sDTLSOnoff.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.sDTLSOnoff.Location = new System.Drawing.Point(53, 0);
            this.sDTLSOnoff.Name = "sDTLSOnoff";
            this.sDTLSOnoff.Size = new System.Drawing.Size(58, 35);
            this.sDTLSOnoff.TabIndex = 0;
            this.sDTLSOnoff.Text = "DTLS";
            this.sDTLSOnoff.UseVisualStyleBackColor = true;
            // 
            // sUdpOnoff
            // 
            this.sUdpOnoff.AutoSize = true;
            this.sUdpOnoff.Checked = true;
            this.sUdpOnoff.Dock = System.Windows.Forms.DockStyle.Left;
            this.sUdpOnoff.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sUdpOnoff.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.sUdpOnoff.Location = new System.Drawing.Point(0, 0);
            this.sUdpOnoff.Name = "sUdpOnoff";
            this.sUdpOnoff.Size = new System.Drawing.Size(50, 35);
            this.sUdpOnoff.TabIndex = 0;
            this.sUdpOnoff.TabStop = true;
            this.sUdpOnoff.Text = "UDP";
            this.sUdpOnoff.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.sTLSOnoff);
            this.panel1.Controls.Add(this.sTcpOnoff);
            this.panel1.Location = new System.Drawing.Point(338, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(103, 35);
            this.panel1.TabIndex = 0;
            // 
            // sTLSOnoff
            // 
            this.sTLSOnoff.AutoSize = true;
            this.sTLSOnoff.Dock = System.Windows.Forms.DockStyle.Right;
            this.sTLSOnoff.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sTLSOnoff.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.sTLSOnoff.Location = new System.Drawing.Point(53, 0);
            this.sTLSOnoff.Name = "sTLSOnoff";
            this.sTLSOnoff.Size = new System.Drawing.Size(50, 35);
            this.sTLSOnoff.TabIndex = 0;
            this.sTLSOnoff.Text = "TLS";
            this.sTLSOnoff.UseVisualStyleBackColor = true;
            // 
            // sTcpOnoff
            // 
            this.sTcpOnoff.AutoSize = true;
            this.sTcpOnoff.Checked = true;
            this.sTcpOnoff.Dock = System.Windows.Forms.DockStyle.Left;
            this.sTcpOnoff.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sTcpOnoff.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.sTcpOnoff.Location = new System.Drawing.Point(0, 0);
            this.sTcpOnoff.Name = "sTcpOnoff";
            this.sTcpOnoff.Size = new System.Drawing.Size(50, 35);
            this.sTcpOnoff.TabIndex = 0;
            this.sTcpOnoff.TabStop = true;
            this.sTcpOnoff.Text = "TCP";
            this.sTcpOnoff.UseVisualStyleBackColor = true;
            // 
            // sListen2
            // 
            this.sListen2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sListen2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sListen2.Location = new System.Drawing.Point(465, 50);
            this.sListen2.Name = "sListen2";
            this.sListen2.Size = new System.Drawing.Size(77, 25);
            this.sListen2.TabIndex = 6;
            this.sListen2.Text = "打开";
            this.sListen2.UseVisualStyleBackColor = true;
            this.sListen2.Click += new System.EventHandler(this.sListen2_Click);
            // 
            // sListen1
            // 
            this.sListen1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sListen1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sListen1.Location = new System.Drawing.Point(465, 16);
            this.sListen1.Name = "sListen1";
            this.sListen1.Size = new System.Drawing.Size(77, 25);
            this.sListen1.TabIndex = 3;
            this.sListen1.Text = "打开";
            this.sListen1.UseVisualStyleBackColor = true;
            this.sListen1.Click += new System.EventHandler(this.sListen1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label4.Location = new System.Drawing.Point(224, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 14);
            this.label4.TabIndex = 1;
            this.label4.Text = "端口:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label3.Location = new System.Drawing.Point(10, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "UDP地址:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label2.Location = new System.Drawing.Point(224, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "端口:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label1.Location = new System.Drawing.Point(10, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "TCP地址:";
            // 
            // Client
            // 
            this.Client.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Client.Location = new System.Drawing.Point(4, 29);
            this.Client.Name = "Client";
            this.Client.Padding = new System.Windows.Forms.Padding(3);
            this.Client.Size = new System.Drawing.Size(936, 535);
            this.Client.TabIndex = 1;
            this.Client.Text = "客户端";
            this.Client.UseVisualStyleBackColor = true;
            // 
            // SSLCfg
            // 
            this.SSLCfg.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SSLCfg.Location = new System.Drawing.Point(4, 29);
            this.SSLCfg.Name = "SSLCfg";
            this.SSLCfg.Size = new System.Drawing.Size(936, 535);
            this.SSLCfg.TabIndex = 2;
            this.SSLCfg.Text = "SSL配置";
            this.SSLCfg.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(936, 535);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "PingTool";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // myTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(944, 568);
            this.Controls.Add(this.TabCtrl);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(960, 960);
            this.MinimumSize = new System.Drawing.Size(960, 38);
            this.Name = "myTool";
            this.Tag = "";
            this.Text = "ElySocketTool";
            this.TabCtrl.ResumeLayout(false);
            this.Server.ResumeLayout(false);
            this.Server.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.sCurrentConnection.ResumeLayout(false);
            this.sNetCfg.ResumeLayout(false);
            this.sNetCfg.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage Server;
        private System.Windows.Forms.GroupBox sNetCfg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage Client;
        private System.Windows.Forms.Button sListen1;
        private System.Windows.Forms.Button sListen2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton sDTLSOnoff;
        private System.Windows.Forms.RadioButton sUdpOnoff;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton sTLSOnoff;
        private System.Windows.Forms.RadioButton sTcpOnoff;
        private System.Windows.Forms.GroupBox sCurrentConnection;
        private System.Windows.Forms.ComboBox sIpaddr2;
        private System.Windows.Forms.ComboBox sPort2;
        private System.Windows.Forms.ComboBox sPort1;
        private System.Windows.Forms.ComboBox sIpaddr1;
        private System.Windows.Forms.Button sDisconnectCurrentConnection;
        private System.Windows.Forms.Button sAllSelect;
        private System.Windows.Forms.ListBox sConnectionlistBox;
        private System.Windows.Forms.Label sSDlable2;
        private System.Windows.Forms.Label sSDlable3;
        private System.Windows.Forms.TextBox Datablock_3;
        private System.Windows.Forms.TextBox Datablock_2;
        private System.Windows.Forms.TextBox Datablock_1;
        private System.Windows.Forms.Label sSDlable1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button sSendButton3;
        private System.Windows.Forms.Button sSendButton2;
        private System.Windows.Forms.Button sSendButton1;
        private System.Windows.Forms.TabPage SSLCfg;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox sLogTextbox;
        private System.Windows.Forms.Button ClearLog;
        private System.Windows.Forms.Label TxRxCounter;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TimerSpan1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TimerSpan3;
        private System.Windows.Forms.TextBox TimerSpan2;
        private System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.TabControl TabCtrl;
    }
}
