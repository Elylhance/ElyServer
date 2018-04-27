using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketTool
{
    public partial class HelpMsgWindow : Form
    {
        public HelpMsgWindow()
        {
            InitializeComponent();
            MsgTbox.SelectionStart = MsgTbox.TextLength;
        }

        private void HelpMsgWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                this.Close();
            }
        }
    }
}
