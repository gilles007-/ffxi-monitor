using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
using System.Runtime.InteropServices;
using System.Drawing;

using FFXIMonitor.Interface;

namespace FFXIMonitor
{

	public static class Program
	{

		public static Form_FFXI_Monitor _ApplicationForm = null;
        private static DvsChatmon.MonitorThread _Monitor = null;

        public static Form_Logs secondForm = null;
        public static Form_Debug debugForm = null;
        public static Form_Icons fourthform = null;

        public static System.Media.SoundPlayer myPlayer;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
        static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Initialize();
            
            Application.Run(_ApplicationForm);
		}


		private static void Initialize()
		{
			//create & initialize the main application window
			_ApplicationForm = new Form_FFXI_Monitor();

            myPlayer = new System.Media.SoundPlayer();
		}

		static void _Monitor_OnLinesIncoming(DvsChatmon.ChatLineInfo[] NewLines)
		{
			if (_ApplicationForm == null)
				return;

            if (_ApplicationForm._debug.Checked == true)
            {
                debugForm.tb.Text = DateTime.Now + " chat -----------------" + Environment.NewLine + debugForm.tb.Text;
            }

            Form_FFXI_Monitor.t = DateTime.Now;
            
            _ApplicationForm.BeginInvoke(
				(EventHandler)delegate
				{
                    foreach (DvsChatmon.ChatLineInfo Line in NewLines)
                    {

                        string newtext = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString() + ": " + Line.Text + "\r\n";

                        newtext = newtext.Replace("", "");
                        newtext = newtext.Replace("", "");

                        switch (Line.ChatCode)
                        {
                            case 1:              //say sent
                                SetText(newtext, secondForm.Say, secondForm.wSay, false, secondForm.VSaymax);
                                break;

                            case 2:              //shout sent
                                SetText(newtext, secondForm.Shout, secondForm.wShout, false, secondForm.VShoutmax);
                                break;

                            case 4:                //tell sent
                                SetText(newtext, secondForm.Tell, secondForm.wTell, false, secondForm.VTellmax);
                                break;

                            case 5:                //party sent
                                SetText(newtext, secondForm.Party, secondForm.wParty, false, secondForm.VPartymax);
                                break;

                            case 6:                //ls sent
                                SetText(newtext, secondForm.Linkshell, secondForm.wLinkshell, false, secondForm.VLinkshellmax);
                                break;

                            case 10:              //shout receive
                                SetText(newtext, secondForm.Shout, secondForm.wShout, false, secondForm.VShoutmax);
                                if (_ApplicationForm._checkBoxShout.Checked == true)
                                {
                                    if (_ApplicationForm._checkBoxLoop.Checked == true)
                                    {
                                        myPlayer.SoundLocation = "alarm0.wav";
                                        myPlayer.PlayLooping();
                                    }
                                    else
                                    {
                                        myPlayer.SoundLocation = "alarm0.wav";
                                        myPlayer.Play();
                                    }
                                }
                                if (_ApplicationForm._cbTranslate.Text != "")
                                {
                                    string y = "";
                                    if (_ApplicationForm._cbTranslate.Text == "Babelfish")
                                    {
                                        y = getjap_babelfish(Line.Text);
                                    }
                                    if (_ApplicationForm._cbTranslate.Text == "Google")
                                    {
                                        y = getjap_google(Line.Text);
                                    }
                                    if (y != "")
                                    {
                                        SetText(y + "\r\n", secondForm.Shout, secondForm.wShout, true, secondForm.VShoutmax);
                                        Form_FFXI_Monitor._FFACE.Windower.SendString("/echo " + y);
                                    }
                                }
                                break;

                            case 9:              //say receive
                                SetText(newtext, secondForm.Say, secondForm.wSay, false, secondForm.VSaymax);
                                if (_ApplicationForm._checkBoxSay.Checked == true)
                                {
                                    if (_ApplicationForm._checkBoxLoop.Checked == true)
                                    {
                                        myPlayer.SoundLocation = "alarm0.wav";
                                        myPlayer.PlayLooping();
                                    }
                                    else
                                    {
                                        myPlayer.SoundLocation = "alarm0.wav";
                                        myPlayer.Play();
                                    }
                                }
                                if (_ApplicationForm._cbTranslate.Text != "")
                                {
                                    string y = "";
                                    if (_ApplicationForm._cbTranslate.Text == "Babelfish")
                                    {
                                        y = getjap_babelfish(Line.Text);
                                    }
                                    if (_ApplicationForm._cbTranslate.Text == "Google")
                                    {
                                        y = getjap_google(Line.Text);
                                    }
                                    if (y != "")
                                    {
                                        SetText(y + "\r\n", secondForm.Say, secondForm.wSay, true, secondForm.VSaymax);
                                        Form_FFXI_Monitor._FFACE.Windower.SendString("/echo " + y);
                                    }
                                }
                                break;

                            case 11:              //yell receive
                                SetText(newtext, secondForm.Yell, secondForm.wYell, false, secondForm.VYellmax);
                                if (_ApplicationForm._checkBoxYell.Checked == true)
                                {
                                    if (_ApplicationForm._checkBoxLoop.Checked == true)
                                    {
                                        myPlayer.SoundLocation = "alarm0.wav";
                                        myPlayer.PlayLooping();
                                    }
                                    else
                                    {
                                        myPlayer.SoundLocation = "alarm0.wav";
                                        myPlayer.Play();
                                    }
                                }
                                if (_ApplicationForm._cbTranslate.Text != "")
                                {
                                    string y = "";
                                    if (_ApplicationForm._cbTranslate.Text == "Babelfish")
                                    {
                                        y = getjap_babelfish(Line.Text);
                                    }
                                    if (_ApplicationForm._cbTranslate.Text == "Google")
                                    {
                                        y = getjap_google(Line.Text);
                                    }
                                    if (y != "")
                                    {
                                        SetText(y + "\r\n", secondForm.Yell, secondForm.wYell, true, secondForm.VYellmax);
                                        Form_FFXI_Monitor._FFACE.Windower.SendString("/echo " + y);
                                    }
                                }
                                break;

                            case 12:              //tell receive
                                SetText(newtext, secondForm.Tell, secondForm.wTell, false, secondForm.VTellmax);
                                if (_ApplicationForm._checkBoxTell.Checked == true)
                                {
                                    if (_ApplicationForm._checkBoxLoop.Checked == true)
                                    {
                                        myPlayer.SoundLocation = "alarm0.wav";
                                        myPlayer.PlayLooping();
                                    }
                                    else
                                    {
                                        myPlayer.SoundLocation = "alarm0.wav";
                                        myPlayer.Play();
                                    }
                                }
                                if (_ApplicationForm._cbTranslate.Text != "")
                                {
                                    string y = "";
                                    if (_ApplicationForm._cbTranslate.Text == "Babelfish")
                                    {
                                        y = getjap_babelfish(Line.Text);
                                    }
                                    if (_ApplicationForm._cbTranslate.Text == "Google")
                                    {
                                        y = getjap_google(Line.Text);
                                    }
                                    if (y != "")
                                    {
                                        SetText(y + "\r\n", secondForm.Tell, secondForm.wTell, true, secondForm.VTellmax);
                                        Form_FFXI_Monitor._FFACE.Windower.SendString("/echo " + y);
                                    }
                                }
                                break;

                            case 14:                //ls receive
                                SetText(newtext, secondForm.Linkshell, secondForm.wLinkshell, false, secondForm.VLinkshellmax);
                                if (_ApplicationForm._checkBoxLS.Checked == true)
                                {
                                    if (_ApplicationForm._checkBoxLoop.Checked == true)
                                    {
                                        myPlayer.SoundLocation = "alarm0.wav";
                                        myPlayer.PlayLooping();
                                    }
                                    else
                                    {
                                        myPlayer.SoundLocation = "alarm0.wav";
                                        myPlayer.Play();
                                    }
                                }
                                if (_ApplicationForm._cbTranslate.Text != "")
                                {
                                    string y = "";
                                    if (_ApplicationForm._cbTranslate.Text == "Babelfish")
                                    {
                                        y = getjap_babelfish(Line.Text);
                                    }
                                    if (_ApplicationForm._cbTranslate.Text == "Google")
                                    {
                                        y = getjap_google(Line.Text);
                                    }
                                    if (y != "")
                                    {
                                        SetText(y + "\r\n", secondForm.Linkshell, secondForm.wLinkshell, true, secondForm.VLinkshellmax);
                                        Form_FFXI_Monitor._FFACE.Windower.SendString("/echo " + y);
                                    }
                                }
                                break;

                            case 13:                //party receive
                                SetText(newtext, secondForm.Party, secondForm.wParty, false, secondForm.VPartymax);
                                if (_ApplicationForm._checkBoxParty.Checked == true)
                                {
                                    if (_ApplicationForm._checkBoxLoop.Checked == true)
                                    {
                                        myPlayer.SoundLocation = "alarm0.wav";
                                        myPlayer.PlayLooping();
                                    }
                                    else
                                    {
                                        myPlayer.SoundLocation = "alarm0.wav";
                                        myPlayer.Play();
                                    }
                                }
                                if (_ApplicationForm._cbTranslate.Text != "")
                                {
                                    string y = "";
                                    if (_ApplicationForm._cbTranslate.Text == "Babelfish")
                                    {
                                        y = getjap_babelfish(Line.Text);
                                    }
                                    if (_ApplicationForm._cbTranslate.Text == "Google")
                                    {
                                        y = getjap_google(Line.Text);
                                    }
                                    if (y != "")
                                    {
                                        SetText(y + "\r\n", secondForm.Party, secondForm.wParty, true, secondForm.VPartymax);
                                        Form_FFXI_Monitor._FFACE.Windower.SendString("/echo " + y);
                                    }
                                }
                                break;

                            default:
                                if (_ApplicationForm._debug.Checked == true)
                                {
                                    SetText(newtext + Line.ChatCode.ToString(), secondForm.Other, secondForm.wOther, false, secondForm.VOthermax);
                                }
                                else
                                {
                                    SetText(newtext, secondForm.Other, secondForm.wOther, false, secondForm.VOthermax);
                                }
                                break;
                        } //end switch

                        SetText(newtext, secondForm.All, secondForm.wAll, false, secondForm.VAllmax);

                        if (_ApplicationForm._checkBoxA1.Checked == true)
                        {
                            char[] separateur = new Char[] { ';' };
                            string[] alerts = _ApplicationForm._tbA1.Text.ToLower().Split(separateur, 20);
                            
                            foreach (string item in alerts)
                            {

                                char[] separateur2 = new Char[] { ' ' };
                                string[] elements = item.Split(separateur, 20);
                                int flag_element = 1;
                                foreach (string element in elements)
                                {
                                    if (Line.Text.ToLower().IndexOf(element) == -1)
                                    {
                                        flag_element = 0;
                                    }
                                }

                                if (flag_element == 1)
                                {
                                    if (_ApplicationForm._checkBoxLoop.Checked == true)
                                    {
                                        myPlayer.SoundLocation = "alarm1.wav";
                                        myPlayer.PlayLooping();
                                    }
                                    else
                                    {
                                        myPlayer.SoundLocation = "alarm1.wav";
                                        myPlayer.Play();
                                    }
                                }

                            }
                        }

                        if (_ApplicationForm._checkBoxA2.Checked == true)
                        {
                            char[] separateur = new Char[] { ';' };
                            string[] alerts = _ApplicationForm._tbA2.Text.ToLower().Split(separateur, 20);

                            foreach (string item in alerts)
                            {

                                char[] separateur2 = new Char[] { ' ' };
                                string[] elements = item.Split(separateur, 20);
                                int flag_element = 1;
                                foreach (string element in elements)
                                {
                                    if (Line.Text.ToLower().IndexOf(element) == -1)
                                    {
                                        flag_element = 0;
                                    }
                                }

                                if (flag_element == 1)
                                {
                                    if (_ApplicationForm._checkBoxLoop.Checked == true)
                                    {
                                        myPlayer.SoundLocation = "alarm2.wav";
                                        myPlayer.PlayLooping();
                                    }
                                    else
                                    {
                                        myPlayer.SoundLocation = "alarm2.wav";
                                        myPlayer.Play();
                                    }
                                }

                            }
                        }

                        if (_ApplicationForm._checkBoxA3.Checked == true)
                        {
                            char[] separateur = new Char[] { ';' };
                            string[] alerts = _ApplicationForm._tbA3.Text.ToLower().Split(separateur, 20);

                            foreach (string item in alerts)
                            {

                                char[] separateur2 = new Char[] { ' ' };
                                string[] elements = item.Split(separateur, 20);
                                int flag_element = 1;
                                foreach (string element in elements)
                                {
                                    if (Line.Text.ToLower().IndexOf(element) == -1)
                                    {
                                        flag_element = 0;
                                    }
                                }

                                if (flag_element == 1)
                                {
                                    if (_ApplicationForm._checkBoxLoop.Checked == true)
                                    {
                                        myPlayer.SoundLocation = "alarm3.wav";
                                        myPlayer.PlayLooping();
                                    }
                                    else
                                    {
                                        myPlayer.SoundLocation = "alarm3.wav";
                                        myPlayer.Play();
                                    }
                                }

                            }
                        }

                        if (_ApplicationForm._checkBoxA4.Checked == true)
                        {
                            char[] separateur = new Char[] { ';' };
                            string[] alerts = _ApplicationForm._tbA4.Text.ToLower().Split(separateur, 20);

                            foreach (string item in alerts)
                            {

                                char[] separateur2 = new Char[] { ' ' };
                                string[] elements = item.Split(separateur, 20);
                                int flag_element = 1;
                                foreach (string element in elements)
                                {
                                    if (Line.Text.ToLower().IndexOf(element) == -1)
                                    {
                                        flag_element = 0;
                                    }
                                }

                                if (flag_element == 1)
                                {
                                    if (_ApplicationForm._checkBoxLoop.Checked == true)
                                    {
                                        myPlayer.SoundLocation = "alarm4.wav";
                                        myPlayer.PlayLooping();
                                    }
                                    else
                                    {
                                        myPlayer.SoundLocation = "alarm4.wav";
                                        myPlayer.Play();
                                    }
                                }

                            }
                        }

                        if (_ApplicationForm._checkBoxA5.Checked == true)
                        {
                            char[] separateur = new Char[] { ';' };
                            string[] alerts = _ApplicationForm._tbA5.Text.ToLower().Split(separateur, 20);

                            foreach (string item in alerts)
                            {

                                char[] separateur2 = new Char[] { ' ' };
                                string[] elements = item.Split(separateur, 20);
                                int flag_element = 1;
                                foreach (string element in elements)
                                {
                                    if (Line.Text.ToLower().IndexOf(element) == -1)
                                    {
                                        flag_element = 0;
                                    }
                                }

                                if (flag_element == 1)
                                {
                                    if (_ApplicationForm._checkBoxLoop.Checked == true)
                                    {
                                        myPlayer.SoundLocation = "alarm5.wav";
                                        myPlayer.PlayLooping();
                                    }
                                    else
                                    {
                                        myPlayer.SoundLocation = "alarm5.wav";
                                        myPlayer.Play();
                                    }
                                }

                            }
                        }

                        if (_ApplicationForm._checkBoxA6.Checked == true)
                        {
                            char[] separateur = new Char[] { ';' };
                            string[] alerts = _ApplicationForm._tbA6.Text.ToLower().Split(separateur, 20);

                            foreach (string item in alerts)
                            {

                                char[] separateur2 = new Char[] { ' ' };
                                string[] elements = item.Split(separateur, 20);
                                int flag_element = 1;
                                foreach (string element in elements)
                                {
                                    if (Line.Text.ToLower().IndexOf(element) == -1)
                                    {
                                        flag_element = 0;
                                    }
                                }

                                if (flag_element == 1)
                                {
                                    if (_ApplicationForm._checkBoxLoop.Checked == true)
                                    {
                                        myPlayer.SoundLocation = "alarm6.wav";
                                        myPlayer.PlayLooping();
                                    }
                                    else
                                    {
                                        myPlayer.SoundLocation = "alarm6.wav";
                                        myPlayer.Play();
                                    }
                                }

                            }
                        }

                        if (_ApplicationForm._checkBoxA7.Checked == true)
                        {
                            char[] separateur = new Char[] { ';' };
                            string[] alerts = _ApplicationForm._tbA7.Text.ToLower().Split(separateur, 20);

                            foreach (string item in alerts)
                            {

                                char[] separateur2 = new Char[] { ' ' };
                                string[] elements = item.Split(separateur, 20);
                                int flag_element = 1;
                                foreach (string element in elements)
                                {
                                    if (Line.Text.ToLower().IndexOf(element) == -1)
                                    {
                                        flag_element = 0;
                                    }
                                }

                                if (flag_element == 1)
                                {
                                    if (_ApplicationForm._checkBoxLoop.Checked == true)
                                    {
                                        myPlayer.SoundLocation = "alarm7.wav";
                                        myPlayer.PlayLooping();
                                    }
                                    else
                                    {
                                        myPlayer.SoundLocation = "alarm7.wav";
                                        myPlayer.Play();
                                    }
                                }

                            }
                        }

                        _ApplicationForm._toolStripStatusLabel2.Text = Form_FFXI_Monitor.t.ToString();





                        for (int i = 1; i < 31; i++)
                        {
                            if (Form_FFXI_Monitor.ar[i] != "")
                            {
                                char[] separateur = new Char[] { '=' };

                                string text = Form_FFXI_Monitor.ar[i].Split(separateur)[0];

                                int pos3 = Line.Text.ToLower().IndexOf(text);
                                if (pos3 != -1)
                                {
                                    //MessageBox.Show(text + "#" + actions);
                                    char[] separateur2 = new Char[] { ';' };
                                    string[] actions = Form_FFXI_Monitor.ar[i].Split(separateur)[1].Split(separateur2);
                                    foreach (string action in actions)
                                    {
                                        //MessageBox.Show(text + "##" + action);
                                        char[] separateur3 = new Char[] { ':' };
                                        string timer = action.Split(separateur3)[0];
                                        string act = action.Split(separateur3)[1];
                                        //MessageBox.Show(timer + "#" + act);
                                        Form_FFXI_Monitor.sleep(int.Parse(timer) * 1000);
                                        Form_FFXI_Monitor._FFACE.Windower.SendString(act);

                                    }

                                }
                            }
                        }




                        //???????????????????????? pourquoi chercher un espace ??????????
                        //int pos = Line.Text.ToLower().IndexOf(" ");
                        //if (pos != -1)
                        //{




                        string result = "";
                        int pos;
                        int pos2;
                        



                        if (Line.Text.ToLower().Contains(" gains the effect of "))
                        {
                            result = Line.Text.Substring(0, Line.Text.ToLower().IndexOf(" gains the effect of ")).ToLower();
                            //MessageBox.Show("#" + result + "#");
                            pos2 = Form_FFXI_Monitor.cherche_pers(result);
                            if (pos2 != -1)
                            {
                                //party
                                for (int i = 1; i < 31; i++)
                                {
                                    if ((Line.Text.ToLower().Contains(Form_FFXI_Monitor.buffs[i])) & (Form_FFXI_Monitor.buffs[i] != ""))
                                    {
                                        if (i == 1) Form_FFXI_Monitor.arrayB1[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 2) Form_FFXI_Monitor.arrayB2[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 3) Form_FFXI_Monitor.arrayB3[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 4) Form_FFXI_Monitor.arrayB4[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 5) Form_FFXI_Monitor.arrayB5[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 6) Form_FFXI_Monitor.arrayB6[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 7) Form_FFXI_Monitor.arrayB7[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 8) Form_FFXI_Monitor.arrayB8[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 9) Form_FFXI_Monitor.arrayB9[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 10) Form_FFXI_Monitor.arrayB10[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 11) Form_FFXI_Monitor.arrayB11[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 12) Form_FFXI_Monitor.arrayB12[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 13) Form_FFXI_Monitor.arrayB13[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 14) Form_FFXI_Monitor.arrayB14[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 15) Form_FFXI_Monitor.arrayB15[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 16) Form_FFXI_Monitor.arrayB16[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 17) Form_FFXI_Monitor.arrayB17[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 18) Form_FFXI_Monitor.arrayB18[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 19) Form_FFXI_Monitor.arrayB19[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 20) Form_FFXI_Monitor.arrayB20[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 21) Form_FFXI_Monitor.arrayB21[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 22) Form_FFXI_Monitor.arrayB22[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 23) Form_FFXI_Monitor.arrayB23[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 24) Form_FFXI_Monitor.arrayB24[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 25) Form_FFXI_Monitor.arrayB25[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 26) Form_FFXI_Monitor.arrayB26[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 27) Form_FFXI_Monitor.arrayB27[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 28) Form_FFXI_Monitor.arrayB28[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 29) Form_FFXI_Monitor.arrayB29[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 30) Form_FFXI_Monitor.arrayB30[pos2] = Form_FFXI_Monitor.t;
                                        goto EndBDB;        //we can quit
                                    }
                                }
                            }
                            else
                            {
                                //mob


                                
                                for (int i = 1; i < 31; i++)
                                {
                                    if ((Line.Text.ToLower().Contains(Form_FFXI_Monitor.buffs[i])) & (Form_FFXI_Monitor.buffs[i] != ""))
                                    {

                                        if (result.Contains("the "))
                                        {
                                            result = result.Substring(4, result.Length - 4);
                                        }

                                        pos2 = Form_FFXI_Monitor.cherche_mob(result);
                                        if (pos2 == -1)
                                        {
                                            pos2 = Form_FFXI_Monitor.add_mob(result);
                                        }

                                        if (i == 1) Form_FFXI_Monitor.arrayMB1[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 2) Form_FFXI_Monitor.arrayMB2[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 3) Form_FFXI_Monitor.arrayMB3[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 4) Form_FFXI_Monitor.arrayMB4[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 5) Form_FFXI_Monitor.arrayMB5[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 6) Form_FFXI_Monitor.arrayMB6[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 7) Form_FFXI_Monitor.arrayMB7[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 8) Form_FFXI_Monitor.arrayMB8[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 9) Form_FFXI_Monitor.arrayMB9[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 10) Form_FFXI_Monitor.arrayMB10[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 11) Form_FFXI_Monitor.arrayMB11[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 12) Form_FFXI_Monitor.arrayMB12[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 13) Form_FFXI_Monitor.arrayMB13[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 14) Form_FFXI_Monitor.arrayMB14[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 15) Form_FFXI_Monitor.arrayMB15[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 16) Form_FFXI_Monitor.arrayMB16[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 17) Form_FFXI_Monitor.arrayMB17[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 18) Form_FFXI_Monitor.arrayMB18[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 19) Form_FFXI_Monitor.arrayMB19[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 20) Form_FFXI_Monitor.arrayMB20[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 21) Form_FFXI_Monitor.arrayMB21[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 22) Form_FFXI_Monitor.arrayMB22[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 23) Form_FFXI_Monitor.arrayMB23[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 24) Form_FFXI_Monitor.arrayMB24[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 25) Form_FFXI_Monitor.arrayMB25[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 26) Form_FFXI_Monitor.arrayMB26[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 27) Form_FFXI_Monitor.arrayMB27[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 28) Form_FFXI_Monitor.arrayMB28[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 29) Form_FFXI_Monitor.arrayMB29[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 30) Form_FFXI_Monitor.arrayMB30[pos2] = Form_FFXI_Monitor.t;
                                        goto EndBDB;        //we can quit
                                    }
                                }






                            }
                        }





                        if ((Line.Text.ToLower().Contains("'s ")) & (Line.Text.ToLower().Contains(" effect wears off")))
                        {
                            result = Line.Text.Substring(0, Line.Text.ToLower().IndexOf("'s ")).ToLower();
                            //MessageBox.Show("#" + result + "#");
                            pos2 = Form_FFXI_Monitor.cherche_pers(result);
                            if (pos2 != -1)
                            {
                                //party
                                for (int i = 1; i < 31; i++) 
                                {
                                    if ((Line.Text.ToLower().Contains(Form_FFXI_Monitor.buffs[i])) & (Form_FFXI_Monitor.buffs[i] != ""))
                                    {
                                        if (i == 1) Form_FFXI_Monitor.arrayB1[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 2) Form_FFXI_Monitor.arrayB2[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 3) Form_FFXI_Monitor.arrayB3[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 4) Form_FFXI_Monitor.arrayB4[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 5) Form_FFXI_Monitor.arrayB5[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 6) Form_FFXI_Monitor.arrayB6[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 7) Form_FFXI_Monitor.arrayB7[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 8) Form_FFXI_Monitor.arrayB8[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 9) Form_FFXI_Monitor.arrayB9[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 10) Form_FFXI_Monitor.arrayB10[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 11) Form_FFXI_Monitor.arrayB11[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 12) Form_FFXI_Monitor.arrayB12[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 13) Form_FFXI_Monitor.arrayB13[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 14) Form_FFXI_Monitor.arrayB14[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 15) Form_FFXI_Monitor.arrayB15[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 16) Form_FFXI_Monitor.arrayB16[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 17) Form_FFXI_Monitor.arrayB17[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 18) Form_FFXI_Monitor.arrayB18[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 19) Form_FFXI_Monitor.arrayB19[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 20) Form_FFXI_Monitor.arrayB20[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 21) Form_FFXI_Monitor.arrayB21[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 22) Form_FFXI_Monitor.arrayB22[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 23) Form_FFXI_Monitor.arrayB23[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 24) Form_FFXI_Monitor.arrayB24[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 25) Form_FFXI_Monitor.arrayB25[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 26) Form_FFXI_Monitor.arrayB26[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 27) Form_FFXI_Monitor.arrayB27[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 28) Form_FFXI_Monitor.arrayB28[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 29) Form_FFXI_Monitor.arrayB29[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 30) Form_FFXI_Monitor.arrayB30[pos2] = new DateTime(1975, 1, 18);
                                        goto EndBDB;        //we can quit
                                    }
                                }
                            }
                            else
                            {
                                //mob


                                

                                for (int i = 1; i < 31; i++)
                                {
                                    if ((Line.Text.ToLower().Contains(Form_FFXI_Monitor.buffs[i])) & (Form_FFXI_Monitor.buffs[i] != ""))
                                    {

                                        if (result.Contains("the "))
                                        {
                                            result = result.Substring(4, result.Length - 4);
                                        }

                                        pos2 = Form_FFXI_Monitor.cherche_mob(result);
                                        if (pos2 == -1)
                                        {
                                            pos2 = Form_FFXI_Monitor.add_mob(result);
                                        }

                                        if (i == 1) Form_FFXI_Monitor.arrayMB1[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 2) Form_FFXI_Monitor.arrayMB2[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 3) Form_FFXI_Monitor.arrayMB3[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 4) Form_FFXI_Monitor.arrayMB4[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 5) Form_FFXI_Monitor.arrayMB5[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 6) Form_FFXI_Monitor.arrayMB6[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 7) Form_FFXI_Monitor.arrayMB7[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 8) Form_FFXI_Monitor.arrayMB8[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 9) Form_FFXI_Monitor.arrayMB9[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 10) Form_FFXI_Monitor.arrayMB10[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 11) Form_FFXI_Monitor.arrayMB11[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 12) Form_FFXI_Monitor.arrayMB12[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 13) Form_FFXI_Monitor.arrayMB13[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 14) Form_FFXI_Monitor.arrayMB14[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 15) Form_FFXI_Monitor.arrayMB15[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 16) Form_FFXI_Monitor.arrayMB16[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 17) Form_FFXI_Monitor.arrayMB17[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 18) Form_FFXI_Monitor.arrayMB18[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 19) Form_FFXI_Monitor.arrayMB19[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 20) Form_FFXI_Monitor.arrayMB20[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 21) Form_FFXI_Monitor.arrayMB21[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 22) Form_FFXI_Monitor.arrayMB22[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 23) Form_FFXI_Monitor.arrayMB23[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 24) Form_FFXI_Monitor.arrayMB24[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 25) Form_FFXI_Monitor.arrayMB25[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 26) Form_FFXI_Monitor.arrayMB26[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 27) Form_FFXI_Monitor.arrayMB27[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 28) Form_FFXI_Monitor.arrayMB28[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 29) Form_FFXI_Monitor.arrayMB29[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 30) Form_FFXI_Monitor.arrayMB30[pos2] = new DateTime(1975, 1, 18);
                                        goto EndBDB;        //we can quit
                                    }
                                }







                            }
                        }


                        //============================== DEBUFFS ====================================


                        if ((Line.Text.ToLower().Contains(" receives the effect of ")) |
                            ((Line.Text.ToLower().Contains(" is ")) & !(Line.Text.ToLower().Contains(" is no longer "))))
                        {

                            pos = Line.Text.ToLower().IndexOf(" receives the effect of ");
                            if (pos > 0)
                            {
                                result = Line.Text.Substring(0, pos).ToLower();
                            }
                            else
                            {
                                pos2 = Line.Text.ToLower().IndexOf(" is ");
                                if (pos2 > 0)
                                {
                                    result = Line.Text.Substring(0, pos2).ToLower();
                                }
                            }
                            
                            pos2 = Form_FFXI_Monitor.cherche_pers(result);
                            if (pos2 != -1)
                            {
                                //party
                                for (int i = 1; i < 31; i++)
                                {
                                    if ((Line.Text.ToLower().Contains(Form_FFXI_Monitor.debuffs[i])) & (Form_FFXI_Monitor.debuffs[i] != ""))
                                    {
                                        if (i == 1) Form_FFXI_Monitor.arrayDB1[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 2) Form_FFXI_Monitor.arrayDB2[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 3) Form_FFXI_Monitor.arrayDB3[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 4) Form_FFXI_Monitor.arrayDB4[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 5) Form_FFXI_Monitor.arrayDB5[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 6) Form_FFXI_Monitor.arrayDB6[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 7) Form_FFXI_Monitor.arrayDB7[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 8) Form_FFXI_Monitor.arrayDB8[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 9) Form_FFXI_Monitor.arrayDB9[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 10) Form_FFXI_Monitor.arrayDB10[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 11) Form_FFXI_Monitor.arrayDB11[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 12) Form_FFXI_Monitor.arrayDB12[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 13) Form_FFXI_Monitor.arrayDB13[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 14) Form_FFXI_Monitor.arrayDB14[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 15) Form_FFXI_Monitor.arrayDB15[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 16) Form_FFXI_Monitor.arrayDB16[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 17) Form_FFXI_Monitor.arrayDB17[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 18) Form_FFXI_Monitor.arrayDB18[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 19) Form_FFXI_Monitor.arrayDB19[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 20) Form_FFXI_Monitor.arrayDB20[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 21) Form_FFXI_Monitor.arrayDB21[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 22) Form_FFXI_Monitor.arrayDB22[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 23) Form_FFXI_Monitor.arrayDB23[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 24) Form_FFXI_Monitor.arrayDB24[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 25) Form_FFXI_Monitor.arrayDB25[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 26) Form_FFXI_Monitor.arrayDB26[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 27) Form_FFXI_Monitor.arrayDB27[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 28) Form_FFXI_Monitor.arrayDB28[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 29) Form_FFXI_Monitor.arrayDB29[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 30) Form_FFXI_Monitor.arrayDB30[pos2] = Form_FFXI_Monitor.t;
                                        goto EndBDB;        //we can quit                   
                                    }
                                }
                            }
                            else
                            {
                                //mob



                                for (int i = 1; i < 31; i++)
                                {
                                    if ((Line.Text.ToLower().Contains(Form_FFXI_Monitor.debuffs[i])) & (Form_FFXI_Monitor.debuffs[i] != ""))
                                    {

                                        if (result.Contains("the "))
                                        {
                                            result = result.Substring(4, result.Length - 4);
                                        }

                                        pos2 = Form_FFXI_Monitor.cherche_mob(result);
                                        if (pos2 == -1)
                                        {
                                            pos2 = Form_FFXI_Monitor.add_mob(result);
                                        }

                                        if (i == 1) Form_FFXI_Monitor.arrayMDB1[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 2) Form_FFXI_Monitor.arrayMDB2[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 3) Form_FFXI_Monitor.arrayMDB3[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 4) Form_FFXI_Monitor.arrayMDB4[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 5) Form_FFXI_Monitor.arrayMDB5[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 6) Form_FFXI_Monitor.arrayMDB6[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 7) Form_FFXI_Monitor.arrayMDB7[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 8) Form_FFXI_Monitor.arrayMDB8[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 9) Form_FFXI_Monitor.arrayMDB9[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 10) Form_FFXI_Monitor.arrayMDB10[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 11) Form_FFXI_Monitor.arrayMDB11[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 12) Form_FFXI_Monitor.arrayMDB12[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 13) Form_FFXI_Monitor.arrayMDB13[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 14) Form_FFXI_Monitor.arrayMDB14[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 15) Form_FFXI_Monitor.arrayMDB15[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 16) Form_FFXI_Monitor.arrayMDB16[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 17) Form_FFXI_Monitor.arrayMDB17[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 18) Form_FFXI_Monitor.arrayMDB18[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 19) Form_FFXI_Monitor.arrayMDB19[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 20) Form_FFXI_Monitor.arrayMDB20[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 21) Form_FFXI_Monitor.arrayMDB21[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 22) Form_FFXI_Monitor.arrayMDB22[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 23) Form_FFXI_Monitor.arrayMDB23[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 24) Form_FFXI_Monitor.arrayMDB24[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 25) Form_FFXI_Monitor.arrayMDB25[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 26) Form_FFXI_Monitor.arrayMDB26[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 27) Form_FFXI_Monitor.arrayMDB27[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 28) Form_FFXI_Monitor.arrayMDB28[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 29) Form_FFXI_Monitor.arrayMDB29[pos2] = Form_FFXI_Monitor.t;
                                        if (i == 30) Form_FFXI_Monitor.arrayMDB30[pos2] = Form_FFXI_Monitor.t;
                                        goto EndBDB;        //we can quit
                                    }
                                }




                            }
                        }







                        if (((Line.Text.ToLower().Contains("'s ")) & (Line.Text.ToLower().Contains(" effect wears off"))) |
                            (Line.Text.ToLower().Contains(" is no longer ")))
                        {

                            pos = Line.Text.ToLower().IndexOf("'s  ");
                            if (pos > 0)
                            {
                                result = Line.Text.Substring(0, pos).ToLower();
                            }
                            else
                            {
                                pos2 = Line.Text.ToLower().IndexOf(" is no longer ");
                                if (pos2 > 0)
                                {
                                    result = Line.Text.Substring(0, pos2).ToLower();
                                }
                            }
                            
                            pos2 = Form_FFXI_Monitor.cherche_pers(result);
                            if (pos2 != -1)
                            {
                                //party
                                for (int i = 1; i < 31; i++)
                                {
                                    if ((Line.Text.ToLower().Contains(Form_FFXI_Monitor.debuffs[i])) & (Form_FFXI_Monitor.debuffs[i] != ""))
                                    {
                                        if (i == 1) Form_FFXI_Monitor.arrayDB1[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 2) Form_FFXI_Monitor.arrayDB2[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 3) Form_FFXI_Monitor.arrayDB3[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 4) Form_FFXI_Monitor.arrayDB4[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 5) Form_FFXI_Monitor.arrayDB5[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 6) Form_FFXI_Monitor.arrayDB6[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 7) Form_FFXI_Monitor.arrayDB7[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 8) Form_FFXI_Monitor.arrayDB8[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 9) Form_FFXI_Monitor.arrayDB9[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 10) Form_FFXI_Monitor.arrayDB10[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 11) Form_FFXI_Monitor.arrayDB11[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 12) Form_FFXI_Monitor.arrayDB12[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 13) Form_FFXI_Monitor.arrayDB13[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 14) Form_FFXI_Monitor.arrayDB14[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 15) Form_FFXI_Monitor.arrayDB15[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 16) Form_FFXI_Monitor.arrayDB16[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 17) Form_FFXI_Monitor.arrayDB17[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 18) Form_FFXI_Monitor.arrayDB18[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 19) Form_FFXI_Monitor.arrayDB19[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 20) Form_FFXI_Monitor.arrayDB20[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 21) Form_FFXI_Monitor.arrayDB21[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 22) Form_FFXI_Monitor.arrayDB22[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 23) Form_FFXI_Monitor.arrayDB23[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 24) Form_FFXI_Monitor.arrayDB24[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 25) Form_FFXI_Monitor.arrayDB25[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 26) Form_FFXI_Monitor.arrayDB26[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 27) Form_FFXI_Monitor.arrayDB27[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 28) Form_FFXI_Monitor.arrayDB28[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 29) Form_FFXI_Monitor.arrayDB29[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 30) Form_FFXI_Monitor.arrayDB30[pos2] = new DateTime(1975, 1, 18);
                                        goto EndBDB;        //we can quit
                                    }
                                }
                            }
                            else
                            {
                                //mob



                                
                                for (int i = 1; i < 31; i++)
                                {
                                    if ((Line.Text.ToLower().Contains(Form_FFXI_Monitor.debuffs[i])) & (Form_FFXI_Monitor.debuffs[i] != ""))
                                    {

                                        if (result.Contains("the "))
                                        {
                                            result = result.Substring(4, result.Length - 4);
                                        }

                                        pos2 = Form_FFXI_Monitor.cherche_mob(result);
                                        if (pos2 == -1)
                                        {
                                            pos2 = Form_FFXI_Monitor.add_mob(result);
                                        }

                                        if (i == 1) Form_FFXI_Monitor.arrayMDB1[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 2) Form_FFXI_Monitor.arrayMDB2[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 3) Form_FFXI_Monitor.arrayMDB3[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 4) Form_FFXI_Monitor.arrayMDB4[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 5) Form_FFXI_Monitor.arrayMDB5[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 6) Form_FFXI_Monitor.arrayMDB6[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 7) Form_FFXI_Monitor.arrayMDB7[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 8) Form_FFXI_Monitor.arrayMDB8[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 9) Form_FFXI_Monitor.arrayMDB9[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 10) Form_FFXI_Monitor.arrayMDB10[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 11) Form_FFXI_Monitor.arrayMDB11[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 12) Form_FFXI_Monitor.arrayMDB12[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 13) Form_FFXI_Monitor.arrayMDB13[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 14) Form_FFXI_Monitor.arrayMDB14[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 15) Form_FFXI_Monitor.arrayMDB15[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 16) Form_FFXI_Monitor.arrayMDB16[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 17) Form_FFXI_Monitor.arrayMDB17[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 18) Form_FFXI_Monitor.arrayMDB18[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 19) Form_FFXI_Monitor.arrayMDB19[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 20) Form_FFXI_Monitor.arrayMDB20[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 21) Form_FFXI_Monitor.arrayMDB21[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 22) Form_FFXI_Monitor.arrayMDB22[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 23) Form_FFXI_Monitor.arrayMDB23[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 24) Form_FFXI_Monitor.arrayMDB24[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 25) Form_FFXI_Monitor.arrayMDB25[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 26) Form_FFXI_Monitor.arrayMDB26[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 27) Form_FFXI_Monitor.arrayMDB27[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 28) Form_FFXI_Monitor.arrayMDB28[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 29) Form_FFXI_Monitor.arrayMDB29[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 30) Form_FFXI_Monitor.arrayMDB30[pos2] = new DateTime(1975, 1, 18);
                                        goto EndBDB;        //we can quit
                                    }
                                }




                            }
                        }




                        pos = Line.Text.ToLower().IndexOf(" recovers ");
                        if (pos > 0)
                        {
                            result = Line.Text.Substring(0, pos).ToLower();
                            //MessageBox.Show("#" + result + "#");
                            pos2 = Form_FFXI_Monitor.cherche_pers(result);
                            if (pos2 != -1)
                            {
                                //party
                                for (int i = 1; i < 31; i++)
                                {
                                    if (Form_FFXI_Monitor.debuffs[i] == "sleep")
                                    {
                                        if (i == 1) Form_FFXI_Monitor.arrayDB1[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 2) Form_FFXI_Monitor.arrayDB2[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 3) Form_FFXI_Monitor.arrayDB3[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 4) Form_FFXI_Monitor.arrayDB4[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 5) Form_FFXI_Monitor.arrayDB5[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 6) Form_FFXI_Monitor.arrayDB6[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 7) Form_FFXI_Monitor.arrayDB7[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 8) Form_FFXI_Monitor.arrayDB8[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 9) Form_FFXI_Monitor.arrayDB9[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 10) Form_FFXI_Monitor.arrayDB10[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 11) Form_FFXI_Monitor.arrayDB11[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 12) Form_FFXI_Monitor.arrayDB12[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 13) Form_FFXI_Monitor.arrayDB13[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 14) Form_FFXI_Monitor.arrayDB14[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 15) Form_FFXI_Monitor.arrayDB15[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 16) Form_FFXI_Monitor.arrayDB16[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 17) Form_FFXI_Monitor.arrayDB17[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 18) Form_FFXI_Monitor.arrayDB18[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 19) Form_FFXI_Monitor.arrayDB19[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 20) Form_FFXI_Monitor.arrayDB20[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 21) Form_FFXI_Monitor.arrayDB21[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 22) Form_FFXI_Monitor.arrayDB22[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 23) Form_FFXI_Monitor.arrayDB23[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 24) Form_FFXI_Monitor.arrayDB24[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 25) Form_FFXI_Monitor.arrayDB25[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 26) Form_FFXI_Monitor.arrayDB26[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 27) Form_FFXI_Monitor.arrayDB27[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 28) Form_FFXI_Monitor.arrayDB28[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 29) Form_FFXI_Monitor.arrayDB29[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 30) Form_FFXI_Monitor.arrayDB30[pos2] = new DateTime(1975, 1, 18);
                                        goto EndBDB;        //we can quit
                                    }
                                }
                            }
                            else
                            {
                                //mob



                                for (int i = 1; i < 31; i++)
                                {
                                    if (Form_FFXI_Monitor.debuffs[i] == "sleep")
                                    {

                                        if (result.Contains("the "))
                                        {
                                            result = result.Substring(4, result.Length - 4);
                                        }

                                        pos2 = Form_FFXI_Monitor.cherche_mob(result);
                                        if (pos2 == -1)
                                        {
                                            pos2 = Form_FFXI_Monitor.add_mob(result);
                                        }

                                        if (i == 1) Form_FFXI_Monitor.arrayMDB1[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 2) Form_FFXI_Monitor.arrayMDB2[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 3) Form_FFXI_Monitor.arrayMDB3[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 4) Form_FFXI_Monitor.arrayMDB4[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 5) Form_FFXI_Monitor.arrayMDB5[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 6) Form_FFXI_Monitor.arrayMDB6[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 7) Form_FFXI_Monitor.arrayMDB7[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 8) Form_FFXI_Monitor.arrayMDB8[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 9) Form_FFXI_Monitor.arrayMDB9[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 10) Form_FFXI_Monitor.arrayMDB10[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 11) Form_FFXI_Monitor.arrayMDB11[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 12) Form_FFXI_Monitor.arrayMDB12[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 13) Form_FFXI_Monitor.arrayMDB13[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 14) Form_FFXI_Monitor.arrayMDB14[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 15) Form_FFXI_Monitor.arrayMDB15[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 16) Form_FFXI_Monitor.arrayMDB16[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 17) Form_FFXI_Monitor.arrayMDB17[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 18) Form_FFXI_Monitor.arrayMDB18[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 19) Form_FFXI_Monitor.arrayMDB19[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 20) Form_FFXI_Monitor.arrayMDB20[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 21) Form_FFXI_Monitor.arrayMDB21[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 22) Form_FFXI_Monitor.arrayMDB22[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 23) Form_FFXI_Monitor.arrayMDB23[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 24) Form_FFXI_Monitor.arrayMDB24[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 25) Form_FFXI_Monitor.arrayMDB25[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 26) Form_FFXI_Monitor.arrayMDB26[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 27) Form_FFXI_Monitor.arrayMDB27[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 28) Form_FFXI_Monitor.arrayMDB28[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 29) Form_FFXI_Monitor.arrayMDB29[pos2] = new DateTime(1975, 1, 18);
                                        if (i == 30) Form_FFXI_Monitor.arrayMDB30[pos2] = new DateTime(1975, 1, 18);
                                        goto EndBDB;        //we can quit
                                    }
                                }



                            }
                        }



















                        for (int i = 1; i < 31; i++)
                        {
                            pos = Line.Text.ToLower().IndexOf(" successfully removes ");
                            if (pos > 0)
                            {
                                if (Form_FFXI_Monitor.debuffs[i] != "")
                                {
                                    pos2 = Line.Text.ToLower().IndexOf(Form_FFXI_Monitor.debuffs[i]);
                                    if (pos2 > 0)
                                    {
                                        result = Line.Text.Substring(pos + 22, pos2 - pos - 22 - 3).ToLower();
                                        //MessageBox.Show("#" + result + "#");
                                        int pos3 = Form_FFXI_Monitor.cherche_pers(result);
                                        if (pos3 != -1)
                                        {
                                            if (i == 1) Form_FFXI_Monitor.arrayDB1[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 2) Form_FFXI_Monitor.arrayDB2[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 3) Form_FFXI_Monitor.arrayDB3[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 4) Form_FFXI_Monitor.arrayDB4[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 5) Form_FFXI_Monitor.arrayDB5[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 6) Form_FFXI_Monitor.arrayDB6[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 7) Form_FFXI_Monitor.arrayDB7[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 8) Form_FFXI_Monitor.arrayDB8[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 9) Form_FFXI_Monitor.arrayDB9[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 10) Form_FFXI_Monitor.arrayDB10[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 11) Form_FFXI_Monitor.arrayDB11[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 12) Form_FFXI_Monitor.arrayDB12[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 13) Form_FFXI_Monitor.arrayDB13[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 14) Form_FFXI_Monitor.arrayDB14[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 15) Form_FFXI_Monitor.arrayDB15[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 16) Form_FFXI_Monitor.arrayDB16[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 17) Form_FFXI_Monitor.arrayDB17[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 18) Form_FFXI_Monitor.arrayDB18[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 19) Form_FFXI_Monitor.arrayDB19[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 20) Form_FFXI_Monitor.arrayDB20[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 21) Form_FFXI_Monitor.arrayDB21[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 22) Form_FFXI_Monitor.arrayDB22[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 23) Form_FFXI_Monitor.arrayDB23[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 24) Form_FFXI_Monitor.arrayDB24[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 25) Form_FFXI_Monitor.arrayDB25[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 26) Form_FFXI_Monitor.arrayDB26[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 27) Form_FFXI_Monitor.arrayDB27[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 28) Form_FFXI_Monitor.arrayDB28[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 29) Form_FFXI_Monitor.arrayDB29[pos3] = new DateTime(1975, 1, 18);
                                            if (i == 30) Form_FFXI_Monitor.arrayDB30[pos3] = new DateTime(1975, 1, 18);
                                            goto EndBDB;
                                        }
                                    }
                                }
                            }
                        }







                        /*

                                        pos = Line.Text.ToLower().IndexOf(" is a" + Form_FFXI_Monitor.debuffs[i]);
                                        if (pos > 0)
                                        {
                                            if (i == 1) Form_FFXI_Monitor.arrayDB1[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 2) Form_FFXI_Monitor.arrayDB2[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 3) Form_FFXI_Monitor.arrayDB3[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 4) Form_FFXI_Monitor.arrayDB4[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 5) Form_FFXI_Monitor.arrayDB5[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 6) Form_FFXI_Monitor.arrayDB6[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 7) Form_FFXI_Monitor.arrayDB7[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 8) Form_FFXI_Monitor.arrayDB8[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 9) Form_FFXI_Monitor.arrayDB9[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 10) Form_FFXI_Monitor.arrayDB10[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 11) Form_FFXI_Monitor.arrayDB11[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 12) Form_FFXI_Monitor.arrayDB12[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 13) Form_FFXI_Monitor.arrayDB13[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 14) Form_FFXI_Monitor.arrayDB14[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 15) Form_FFXI_Monitor.arrayDB15[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 16) Form_FFXI_Monitor.arrayDB16[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 17) Form_FFXI_Monitor.arrayDB17[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 18) Form_FFXI_Monitor.arrayDB18[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 19) Form_FFXI_Monitor.arrayDB19[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 20) Form_FFXI_Monitor.arrayDB20[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 21) Form_FFXI_Monitor.arrayDB21[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 22) Form_FFXI_Monitor.arrayDB22[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 23) Form_FFXI_Monitor.arrayDB23[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 24) Form_FFXI_Monitor.arrayDB24[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 25) Form_FFXI_Monitor.arrayDB25[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 26) Form_FFXI_Monitor.arrayDB26[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 27) Form_FFXI_Monitor.arrayDB27[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 28) Form_FFXI_Monitor.arrayDB28[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 29) Form_FFXI_Monitor.arrayDB29[pos2] = Form_FFXI_Monitor.t;
                                            if (i == 30) Form_FFXI_Monitor.arrayDB30[pos2] = Form_FFXI_Monitor.t;
                                            goto EndBDB;
                                        }

                                       

                                        

                                        pos = Line.Text.ToLower().IndexOf(" is no longer a" + Form_FFXI_Monitor.debuffs[i]);
                                        if (pos > 0)
                                        {
                                            if (i == 1) Form_FFXI_Monitor.arrayDB1[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 2) Form_FFXI_Monitor.arrayDB2[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 3) Form_FFXI_Monitor.arrayDB3[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 4) Form_FFXI_Monitor.arrayDB4[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 5) Form_FFXI_Monitor.arrayDB5[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 6) Form_FFXI_Monitor.arrayDB6[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 7) Form_FFXI_Monitor.arrayDB7[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 8) Form_FFXI_Monitor.arrayDB8[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 9) Form_FFXI_Monitor.arrayDB9[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 10) Form_FFXI_Monitor.arrayDB10[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 11) Form_FFXI_Monitor.arrayDB11[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 12) Form_FFXI_Monitor.arrayDB12[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 13) Form_FFXI_Monitor.arrayDB13[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 14) Form_FFXI_Monitor.arrayDB14[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 15) Form_FFXI_Monitor.arrayDB15[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 16) Form_FFXI_Monitor.arrayDB16[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 17) Form_FFXI_Monitor.arrayDB17[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 18) Form_FFXI_Monitor.arrayDB18[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 19) Form_FFXI_Monitor.arrayDB19[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 20) Form_FFXI_Monitor.arrayDB20[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 21) Form_FFXI_Monitor.arrayDB21[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 22) Form_FFXI_Monitor.arrayDB22[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 23) Form_FFXI_Monitor.arrayDB23[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 24) Form_FFXI_Monitor.arrayDB24[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 25) Form_FFXI_Monitor.arrayDB25[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 26) Form_FFXI_Monitor.arrayDB26[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 27) Form_FFXI_Monitor.arrayDB27[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 28) Form_FFXI_Monitor.arrayDB28[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 29) Form_FFXI_Monitor.arrayDB29[pos2] = new DateTime(1975, 1, 18);
                                            if (i == 30) Form_FFXI_Monitor.arrayDB30[pos2] = new DateTime(1975, 1, 18);
                                            goto EndBDB;
                                        }

                        */



                            EndBDB:
                                Form_FFXI_Monitor.sleep(1);

                        //}

                        

                    } //end foreach NewLines

                }
            );      
                    
		
		}


        

		public static void Start(string param1)
		{

			Stop();

            _Monitor = new DvsChatmon.MonitorThread();
            _Monitor.OnLinesIncoming += new DvsChatmon.MonitorThread.LinesIncomingDelegate(_Monitor_OnLinesIncoming);
            _Monitor.Start(param1, Form_FFXI_Monitor.memloc_chatlog);

		}




		public static void Stop()
		{

			if (_Monitor != null)
			{
				_Monitor.PauseMonitoring();
				_Monitor.ShutdownThread();
				_Monitor = null;
			}

		}


		
        

        public enum Message : uint
        {
            WM_VSCROLL = 0x0115
        }


        [DllImport("User32.dll")]
        public extern static int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, Int32 lParam);
        
        const int WM_USER = 0x400;
        const int EM_HIDESELECTION = WM_USER + 63;

        delegate void SetTextCallback(string text, RichTextBox rtb, uint w, bool b, bool max);

        public static void SetText(string text, RichTextBox obj, uint w2, bool bold, bool Vmax)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (obj.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                _ApplicationForm.Invoke(d, new object[] { text, obj, w2, bold, Vmax });
            }
            else
            {



                int length_save = obj.Text.Length;

                bool focused = obj.Focused;
                //backup initial selection
                int selection = obj.SelectionStart;
                int length = obj.SelectionLength;
                //allow autoscroll if selection is at end of text
                //bool autoscroll = (selection == secondForm.rtb.Text.Length);
                bool autoscroll = Vmax;

                if (!autoscroll)
                {
                    //shift focus from RichTextBox to some other control
                    if (focused) secondForm.b.Focus();
                    //hide selection
                    SendMessage(obj.Handle, EM_HIDESELECTION, 1, 0);
                }

                secondForm.tb.Text = text;

                obj.SelectionFont = new Font(obj.Font, FontStyle.Regular);
                if (bold == true)
                {
                    obj.SelectionFont = new Font(obj.Font, FontStyle.Bold);
                }

                obj.AppendText(secondForm.tb.Text);

                //obj.SelectAll();
                obj.SelectionStart = length_save;
                obj.SelectionLength = length_save + text.Length;

                obj.SelectionFont = new Font(obj.Font, FontStyle.Regular);
                obj.SelectionStart = obj.Text.Length;
                obj.SelectionLength = 0;

                if (!autoscroll)
                {
                    //restore initial selection
                    obj.SelectionStart = selection;
                    obj.SelectionLength = length;
                    //unhide selection
                    SendMessage(obj.Handle, EM_HIDESELECTION, 0, 0);
                    //restore focus to RichTextBox
                    if (focused) obj.Focus();
                    SendMessage(obj.Handle, (int)Message.WM_VSCROLL, new IntPtr(w2), new IntPtr(0));
                }


            }

        }




        public static string getjap_babelfish(string tmp)
        {

            string[] hiragana = new string[] { "あ", "い", "う", "え", "お", "か", "き", "く", "け", "こ", "さ", "し", "す", "せ", "そ", "た", "ち", "つ", "て", "と", "な", "に", "ぬ", "ね", "の", "は", "ひ", "ふ", "へ", "ほ", "ま", "み", "む", "め", "も", "ら", "り", "る", "れ", "ろ", "や", "ゆ", "よ", "わ", "を", "ん" };
            //あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもらりるれろやゆよわをん

            string[] katakana = new string[] { "ア", "イ", "ウ", "エ", "オ", "カ", "キ", "ク", "ケ", "コ", "サ", "シ", "ス", "セ", "ソ", "タ", "チ", "ツ", "テ", "ト", "ナ", "ニ", "ヌ", "ネ", "ノ", "ハ", "ヒ", "フ", "ヘ", "ホ", "マ", "ミ", "ム", "メ", "モ", "ラ", "リ", "ル", "レ", "ロ", "ヤ", "ユ", "ヨ", "ワ", "ヲ", "ン" };

            string flag = "0";
            int pos = 0;
            for (int j = 0; j < 46; j++)
            {
                pos = tmp.IndexOf(hiragana[j]);
                if (pos > 0)
                {
                    flag = "1";
                }
                pos = tmp.IndexOf(katakana[j]);
                if (pos > 0)
                {
                    flag = "1";
                }
            }

            string y = "";
            if (flag == "1")
            {

                WebClient client = new WebClient();
                string result = client.DownloadString("http://fr.babelfish.yahoo.com/translate_txt?ei=UTF-8&doit=done&fr=bf-res&intl=1&tt=urltext&lp=ja_" + _ApplicationForm._cbL.Text + "&trtext=" + tmp);
                result = result.Replace("\"", "*");    //pour remplacer ", on met \"
                int pos2 = result.IndexOf("<input type=*hidden* name=*p* value=*");
                if (pos2 == 0)
                {
                    y = "";
                }
                else
                {
                    string msg2 = result.Substring(pos2 + 37, result.Length - pos2 - 37);
                    int pos3 = msg2.IndexOf("*>");
                    if (pos3 == 0)
                    {
                        y = "";
                    }
                    else
                    {
                        y = msg2.Substring(0, pos3);
                    }

                }

            }

            return y;


        } // @ private string getjap()


        public static string getjap_google(string word)
        {

            string y = "";

            try
            {
                string[] hiragana = new string[] { "あ", "い", "う", "え", "お", "か", "き", "く", "け", "こ", "さ", "し", "す", "せ", "そ", "た", "ち", "つ", "て", "と", "な", "に", "ぬ", "ね", "の", "は", "ひ", "ふ", "へ", "ほ", "ま", "み", "む", "め", "も", "ら", "り", "る", "れ", "ろ", "や", "ゆ", "よ", "わ", "を", "ん" };
                //あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもらりるれろやゆよわをん

                string[] katakana = new string[] { "ア", "イ", "ウ", "エ", "オ", "カ", "キ", "ク", "ケ", "コ", "サ", "シ", "ス", "セ", "ソ", "タ", "チ", "ツ", "テ", "ト", "ナ", "ニ", "ヌ", "ネ", "ノ", "ハ", "ヒ", "フ", "ヘ", "ホ", "マ", "ミ", "ム", "メ", "モ", "ラ", "リ", "ル", "レ", "ロ", "ヤ", "ユ", "ヨ", "ワ", "ヲ", "ン" };

                string flag = "0";
                int pos = 0;
                for (int j = 0; j < 46; j++)
                {
                    pos = word.IndexOf(hiragana[j]);
                    if (pos > 0)
                    {
                        flag = "1";
                    }
                    pos = word.IndexOf(katakana[j]);
                    if (pos > 0)
                    {
                        flag = "1";
                    }
                }

                if (flag == "1")
                {

                    //var word = "こんにちは";

                    var fromLanguage = "ja";
                    var toLanguage = Program._ApplicationForm._cbL.Text;

                    // create the url for making web request
                    var apiUrl = "http://ajax.googleapis.com/ajax/services/language/translate?v=1.0&q={0}&langpair={1}|{2}";
                    var url = String.Format(apiUrl, word, fromLanguage, toLanguage);

                    // get translated text using google API.
                    var data = Translate(url);

                    string result = data.Replace("\"", "[*]");    //pour remplacer ", on met \"

                    int pos2 = result.IndexOf("[*]responseData: {[*]translatedText[*]:[*]");
                    if (pos2 == 0)
                    {
                        y = "";
                    }
                    else
                    {
                        string msg2 = result.Substring(46, result.Length - 46);

                        int pos3 = msg2.IndexOf("[*]},");
                        if (pos3 == 0)
                        {
                            y = "";
                        }
                        else
                        {
                            y = msg2.Substring(0, pos3);

                        }

                    }

                }
                
            }
            catch (Exception e)
            {
                if (_ApplicationForm._debug.Checked == true)
                {
                    debugForm.tb.Text = DateTime.Now + " error in translate with google (main)" + Environment.NewLine + debugForm.tb.Text;
                }
            }
            return y;
        }


        /// 
        /// Translates the text into specied language using google API.
        /// 
        public static string Translate(string url)
        {
            string text = string.Empty;

            try
            {
                // create the web request
                WebRequest req = HttpWebRequest.Create(url);

                // set the request method
                req.Method = "GET";

                // get the response
                WebResponse res = req.GetResponse();

                // read response stream
                // you must specify the encoding as UTF8 
                // because google returns the response in UTF8 format
                using (StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8))
                {
                    // read text from response stream
                    text = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                if (_ApplicationForm._debug.Checked == true)
                {
                    debugForm.tb.Text = DateTime.Now + " error in translate with google (api)" + Environment.NewLine + debugForm.tb.Text;
                }
            }

            return text;
        }




        
	}
}
