namespace SocketTool
{
    partial class HelpMsgWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpMsgWindow));
            this.MsgTbox = new System.Windows.Forms.TextBox();
            this.ToolName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MsgTbox
            // 
            this.MsgTbox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.MsgTbox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MsgTbox.Location = new System.Drawing.Point(0, 33);
            this.MsgTbox.Multiline = true;
            this.MsgTbox.Name = "MsgTbox";
            this.MsgTbox.ReadOnly = true;
            this.MsgTbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MsgTbox.Size = new System.Drawing.Size(414, 488);
            this.MsgTbox.TabIndex = 0;
            this.MsgTbox.Text = resources.GetString("MsgTbox.Text");
            // 
            // ToolName
            // 
            this.ToolName.AutoSize = true;
            this.ToolName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ToolName.Location = new System.Drawing.Point(9, 6);
            this.ToolName.Name = "ToolName";
            this.ToolName.Size = new System.Drawing.Size(364, 20);
            this.ToolName.TabIndex = 1;
            this.ToolName.Text = "YKSocketTool v0.03  @2018/04/27  --按 F1 打开本窗口";
            // 
            // HelpMsgWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 521);
            this.Controls.Add(this.ToolName);
            this.Controls.Add(this.MsgTbox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HelpMsgWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User Guider";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HelpMsgWindow_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MsgTbox;
        private System.Windows.Forms.Label ToolName;
    }
}