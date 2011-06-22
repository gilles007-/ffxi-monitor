using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.IO;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text.RegularExpressions;
using System.Xml;


namespace FFXIMonitor.Interface
{

    public partial class Form_Logs : Form
    {

        public Form_Logs()
        {    

            InitializeComponent();
        }


        public enum ScrollBarType : uint
        {
            SbHorz = 0,
            SbVert = 1,
            SbCtl = 2,
            SbBoth = 3
        }

        public enum ScrollBarCommands : uint
        {
            SB_THUMBPOSITION = 4
        }

        public RichTextBox Say
        {
            get { return rtbSay; }
        }

        public RichTextBox Tell
        {
            get { return rtbTell; }
        }

        public RichTextBox Party
        {
            get { return rtbParty; }
        }

        public RichTextBox Linkshell
        {
            get { return rtbLinkshell; }
        }

        public RichTextBox Shout
        {
            get { return rtbShout; }
        }

        public RichTextBox Other
        {
            get { return rtbOther; }
        }

        public RichTextBox All
        {
            get { return rtbAll; }
        }


        public TextBox tb
        {
            get { return textBox1; }
        }

        public Button b
        {
            get { return button1; }
        }



       
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
       



        public int SbVert = 1;

        [DllImport("user32.dll")]
        static extern bool GetScrollRange(IntPtr hWnd, int nBar, out int lpMinPos,
           out int lpMaxPos);

        bool vSay_max = false;
        public bool VSaymax
        {
            get { return vSay_max; }
        }

        bool vTell_max = false;
        public bool VTellmax
        {
            get { return vTell_max; }
        }

        bool vParty_max = false;
        public bool VPartymax
        {
            get { return vParty_max; }
        }

        bool vLinkshell_max = false;
        public bool VLinkshellmax
        {
            get { return vLinkshell_max; }
        }

        bool vShout_max = false;
        public bool VShoutmax
        {
            get { return vShout_max; }
        }

        bool vOther_max = false;
        public bool VOthermax
        {
            get { return vOther_max; }
        }

        bool vAll_max = false;
        public bool VAllmax
        {
            get { return vAll_max; }
        }

        



        [DllImport("User32.dll")]
        public extern static int GetScrollPos(IntPtr hWnd, int nBar);

        uint wSayParam;
        public uint wSay
        {
            get { return wSayParam; }
        }

        uint wTellParam;
        public uint wTell
        {
            get { return wTellParam; }
        }

        uint wPartyParam;
        public uint wParty
        {
            get { return wPartyParam; }
        }

        uint wLinkshellParam;
        public uint wLinkshell
        {
            get { return wLinkshellParam; }
        }

        uint wShoutParam;
        public uint wShout
        {
            get { return wShoutParam; }
        }

        uint wOtherParam;
        public uint wOther
        {
            get { return wOtherParam; }
        }

        uint wAllParam;
        public uint wAll
        {
            get { return wAllParam; }
        }





        private void rtbOther_VScroll(object sender, EventArgs e)
        {
            intercept(rtbOther, out wOtherParam, out vOther_max);
        }

        private void rtbShout_VScroll(object sender, EventArgs e)
        {
            intercept(rtbShout, out wShoutParam, out vShout_max);
        }

        private void rtbLinkshell_VScroll(object sender, EventArgs e)
        {
            intercept(rtbLinkshell, out wLinkshellParam, out vLinkshell_max);
        }

        private void rtbParty_VScroll(object sender, EventArgs e)
        {
            intercept(rtbParty, out wPartyParam, out vParty_max);
        }

        private void rtbTell_VScroll(object sender, EventArgs e)
        {
            intercept(rtbTell, out wTellParam, out vTell_max);
        }

        private void rtbSay_VScroll(object sender, EventArgs e)
        {
            intercept(rtbSay, out wSayParam, out vSay_max);
        }

        private void rtbAll_VScroll(object sender, EventArgs e)
        {
            intercept(rtbAll, out wAllParam, out vAll_max);
        }



        private void intercept(RichTextBox rtb, out uint wParam, out bool v_max)
        {

            v_max = false;

            int scrollMin = 0;
            int scrollMax = 0;


            int nPos = GetScrollPos(rtb.Handle, (int)ScrollBarType.SbVert);
            
            int nPos_sauv = nPos;
            nPos <<= 16;
            wParam = (uint)ScrollBarCommands.SB_THUMBPOSITION | (uint)nPos;
            
            uint wParam2 = (uint)ScrollBarCommands.SB_THUMBPOSITION | (uint)nPos_sauv;

            if (GetScrollRange(rtb.Handle, SbVert, out scrollMin, out scrollMax))
            {
                
                if (wParam2 + rtb.Height > scrollMax)
                {
                    v_max = true;
                }
                else
                {
                    v_max = false;
                }
            }
        }

        private void tabChat_DoubleClick(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {

                TextWriter tw = new StreamWriter(folderBrowserDialog1.SelectedPath + "/" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-say.txt");
                tw.Write(rtbSay.Text);
                tw.Close();

                tw = new StreamWriter(folderBrowserDialog1.SelectedPath + "/" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-tell.txt");
                tw.Write(rtbTell.Text);
                tw.Close();

                tw = new StreamWriter(folderBrowserDialog1.SelectedPath + "/" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-party.txt");
                tw.Write(rtbParty.Text);
                tw.Close();

                tw = new StreamWriter(folderBrowserDialog1.SelectedPath + "/" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-linkshell.txt");
                tw.Write(rtbLinkshell.Text);
                tw.Close();

                tw = new StreamWriter(folderBrowserDialog1.SelectedPath + "/" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-shout.txt");
                tw.Write(rtbShout.Text);
                tw.Close();

                tw = new StreamWriter(folderBrowserDialog1.SelectedPath + "/" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-other.txt");
                tw.Write(rtbOther.Text);
                tw.Close();

                tw = new StreamWriter(folderBrowserDialog1.SelectedPath + "/" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-all.txt");
                tw.Write(rtbAll.Text);
                tw.Close();

            }


        }


    }
}
