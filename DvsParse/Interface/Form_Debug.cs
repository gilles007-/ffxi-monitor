using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FFXIMonitor.Interface
{

    public partial class Form_Debug : Form
    {
        public Form_Debug()
        {
            InitializeComponent();
        }

        private void debug_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            Program._ApplicationForm._debug.Checked = false;
        }

        public TextBox tb
        {
            get { return textBox1; }
        }

    }
}
