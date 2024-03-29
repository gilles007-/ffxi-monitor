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


using FFACETools;


enum ProcessAccessFlags
{
    PROCESS_ALL_ACCESS = 0x1F0FFF,
    PROCESS_CREATE_THREAD = 0x2,
    PROCESS_DUP_HANDLE = 0x40,
    PROCESS_QUERY_INFORMATION = 0x400,
    PROCESS_SET_INFORMATION = 0x200,
    PROCESS_TERMINATE = 0x1,
    PROCESS_VM_OPERATION = 0x8,
    PROCESS_VM_READ = 0x10,
    PROCESS_VM_WRITE = 0x20,
    SYNCHRONIZE = 0x100000
}


namespace FFXIMonitor.Interface
{
	public partial class Form_FFXI_Monitor : Form
	{
        
        public static DateTime t;

        IntPtr hWnd;
        public static FFACE _FFACE { get; set; }
        Process myProcesses;
        int FFXIBase = 0;

        public static string[] buffs = new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        public static string[] debuffs = new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

        public static string[] ar = new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

        public static string ffxi = "";
        public static int update_display = 0;
        public static int x_pos = 0;
        public static int x_separator = 0;
        public static int party0_y_pos = 0;
        public static int y0_separator = 0;
        public static int icon_size0 = 0;
        public static int party1_y_pos = 0;
        public static int party2_y_pos = 0;
        public static int y12_separator = 0;
        public static int icon_size12 = 0;
        public static string npc_name = "";
        public static int npc_y = 0;
        public static int npc_x_pos = 0;
        public static string memloc_chatlog_lines = "";
        public static string memloc_chatlog = "";
        public static int[] durationsB = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static int[] durationsDB = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static DateTime[] arrayB1 = new DateTime[200];
        public static DateTime[] arrayB2 = new DateTime[200];
        public static DateTime[] arrayB3 = new DateTime[200];
        public static DateTime[] arrayB4 = new DateTime[200];
        public static DateTime[] arrayB5 = new DateTime[200];
        public static DateTime[] arrayB6 = new DateTime[200];
        public static DateTime[] arrayB7 = new DateTime[200];
        public static DateTime[] arrayB8 = new DateTime[200];
        public static DateTime[] arrayB9 = new DateTime[200];
        public static DateTime[] arrayB10 = new DateTime[200];
        public static DateTime[] arrayB11 = new DateTime[200];
        public static DateTime[] arrayB12 = new DateTime[200];
        public static DateTime[] arrayB13 = new DateTime[200];
        public static DateTime[] arrayB14 = new DateTime[200];
        public static DateTime[] arrayB15 = new DateTime[200];
        public static DateTime[] arrayB16 = new DateTime[200];
        public static DateTime[] arrayB17 = new DateTime[200];
        public static DateTime[] arrayB18 = new DateTime[200];
        public static DateTime[] arrayB19 = new DateTime[200];
        public static DateTime[] arrayB20 = new DateTime[200];
        public static DateTime[] arrayB21 = new DateTime[200];
        public static DateTime[] arrayB22 = new DateTime[200];
        public static DateTime[] arrayB23 = new DateTime[200];
        public static DateTime[] arrayB24 = new DateTime[200];
        public static DateTime[] arrayB25 = new DateTime[200];
        public static DateTime[] arrayB26 = new DateTime[200];
        public static DateTime[] arrayB27 = new DateTime[200];
        public static DateTime[] arrayB28 = new DateTime[200];
        public static DateTime[] arrayB29 = new DateTime[200];
        public static DateTime[] arrayB30 = new DateTime[200];
        public static string[] party_2 = new string[200];
        public static DateTime[] arrayDB1 = new DateTime[200];
        public static DateTime[] arrayDB2 = new DateTime[200];
        public static DateTime[] arrayDB3 = new DateTime[200];
        public static DateTime[] arrayDB4 = new DateTime[200];
        public static DateTime[] arrayDB5 = new DateTime[200];
        public static DateTime[] arrayDB6 = new DateTime[200];
        public static DateTime[] arrayDB7 = new DateTime[200];
        public static DateTime[] arrayDB8 = new DateTime[200];
        public static DateTime[] arrayDB9 = new DateTime[200];
        public static DateTime[] arrayDB10 = new DateTime[200];
        public static DateTime[] arrayDB11 = new DateTime[200];
        public static DateTime[] arrayDB12 = new DateTime[200];
        public static DateTime[] arrayDB13 = new DateTime[200];
        public static DateTime[] arrayDB14 = new DateTime[200];
        public static DateTime[] arrayDB15 = new DateTime[200];
        public static DateTime[] arrayDB16 = new DateTime[200];
        public static DateTime[] arrayDB17 = new DateTime[200];
        public static DateTime[] arrayDB18 = new DateTime[200];
        public static DateTime[] arrayDB19 = new DateTime[200];
        public static DateTime[] arrayDB20 = new DateTime[200];
        public static DateTime[] arrayDB21 = new DateTime[200];
        public static DateTime[] arrayDB22 = new DateTime[200];
        public static DateTime[] arrayDB23 = new DateTime[200];
        public static DateTime[] arrayDB24 = new DateTime[200];
        public static DateTime[] arrayDB25 = new DateTime[200];
        public static DateTime[] arrayDB26 = new DateTime[200];
        public static DateTime[] arrayDB27 = new DateTime[200];
        public static DateTime[] arrayDB28 = new DateTime[200];
        public static DateTime[] arrayDB29 = new DateTime[200];
        public static DateTime[] arrayDB30 = new DateTime[200];
        public static int[] y0pos = new int[] { -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static int[] y1pos = new int[] { -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static int[] y2pos = new int[] { -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static string limit = "";
        public static int[] npc_y_pos = new int[] { -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int wl = 0;
        int wt = 0;
        int wb = 0;
        int wr = 0;
        int dbgp0 = 0;
        int dbgp1 = 0;
        int dbgp2 = 0;
        int total = 0;
        int total_precedent = 0;
        int pre_x = 0;
        int prec_y = 0;
        bool updateencours = false;
        bool pause = false;
        string ffxi_status = "";
        DateTime pause_time;
        string[] jobs = new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        public static string main_character = "";
        public static string alarm1 = "";
        public static string alarm2 = "";
        public static string alarm3 = "";
        public static string alarm4 = "";
        public static string alarm5 = "";
        public static string alarm6 = "";
        public static string alarm7 = "";
        public static string first_instance = "";
        public static string minimized_start = "";









        
        public static DateTime[] arrayMB1 = new DateTime[200];
        public static DateTime[] arrayMB2 = new DateTime[200];
        public static DateTime[] arrayMB3 = new DateTime[200];
        public static DateTime[] arrayMB4 = new DateTime[200];
        public static DateTime[] arrayMB5 = new DateTime[200];
        public static DateTime[] arrayMB6 = new DateTime[200];
        public static DateTime[] arrayMB7 = new DateTime[200];
        public static DateTime[] arrayMB8 = new DateTime[200];
        public static DateTime[] arrayMB9 = new DateTime[200];
        public static DateTime[] arrayMB10 = new DateTime[200];
        public static DateTime[] arrayMB11 = new DateTime[200];
        public static DateTime[] arrayMB12 = new DateTime[200];
        public static DateTime[] arrayMB13 = new DateTime[200];
        public static DateTime[] arrayMB14 = new DateTime[200];
        public static DateTime[] arrayMB15 = new DateTime[200];
        public static DateTime[] arrayMB16 = new DateTime[200];
        public static DateTime[] arrayMB17 = new DateTime[200];
        public static DateTime[] arrayMB18 = new DateTime[200];
        public static DateTime[] arrayMB19 = new DateTime[200];
        public static DateTime[] arrayMB20 = new DateTime[200];
        public static DateTime[] arrayMB21 = new DateTime[200];
        public static DateTime[] arrayMB22 = new DateTime[200];
        public static DateTime[] arrayMB23 = new DateTime[200];
        public static DateTime[] arrayMB24 = new DateTime[200];
        public static DateTime[] arrayMB25 = new DateTime[200];
        public static DateTime[] arrayMB26 = new DateTime[200];
        public static DateTime[] arrayMB27 = new DateTime[200];
        public static DateTime[] arrayMB28 = new DateTime[200];
        public static DateTime[] arrayMB29 = new DateTime[200];
        public static DateTime[] arrayMB30 = new DateTime[200];

        public static DateTime[] arrayMDB1 = new DateTime[200];
        public static DateTime[] arrayMDB2 = new DateTime[200];
        public static DateTime[] arrayMDB3 = new DateTime[200];
        public static DateTime[] arrayMDB4 = new DateTime[200];
        public static DateTime[] arrayMDB5 = new DateTime[200];
        public static DateTime[] arrayMDB6 = new DateTime[200];
        public static DateTime[] arrayMDB7 = new DateTime[200];
        public static DateTime[] arrayMDB8 = new DateTime[200];
        public static DateTime[] arrayMDB9 = new DateTime[200];
        public static DateTime[] arrayMDB10 = new DateTime[200];
        public static DateTime[] arrayMDB11 = new DateTime[200];
        public static DateTime[] arrayMDB12 = new DateTime[200];
        public static DateTime[] arrayMDB13 = new DateTime[200];
        public static DateTime[] arrayMDB14 = new DateTime[200];
        public static DateTime[] arrayMDB15 = new DateTime[200];
        public static DateTime[] arrayMDB16 = new DateTime[200];
        public static DateTime[] arrayMDB17 = new DateTime[200];
        public static DateTime[] arrayMDB18 = new DateTime[200];
        public static DateTime[] arrayMDB19 = new DateTime[200];
        public static DateTime[] arrayMDB20 = new DateTime[200];
        public static DateTime[] arrayMDB21 = new DateTime[200];
        public static DateTime[] arrayMDB22 = new DateTime[200];
        public static DateTime[] arrayMDB23 = new DateTime[200];
        public static DateTime[] arrayMDB24 = new DateTime[200];
        public static DateTime[] arrayMDB25 = new DateTime[200];
        public static DateTime[] arrayMDB26 = new DateTime[200];
        public static DateTime[] arrayMDB27 = new DateTime[200];
        public static DateTime[] arrayMDB28 = new DateTime[200];
        public static DateTime[] arrayMDB29 = new DateTime[200];
        public static DateTime[] arrayMDB30 = new DateTime[200];

        public static string[] mob_2 = new string[200];
        public static int mob_y_pos = 0;
        //public static int[] ympos = new int[] { -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        
        






        [DllImport("kernel32.dll")]
        static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadProcessMemory(IntPtr hProcess, uint lpBaseAddress, ref uint lpBuffer, int dwSize, int lpNumberOfBytesRead);
        



        [DllImport("FFACE.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern void DeleteInstance(int instanceID);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, WindowShowStyle nCmdShow);

        private enum WindowShowStyle : uint
        {
            /// <summary>Hides the window and activates another window.</summary>
            /// <remarks>See SW_HIDE</remarks>
            Hide = 0,
            /// <summary>Activates and displays a window. If the window is minimized
            /// or maximized, the system restores it to its original size and
            /// position. An application should specify this flag when displaying
            /// the window for the first time.</summary>
            /// <remarks>See SW_SHOWNORMAL</remarks>
            ShowNormal = 1,
            /// <summary>Activates the window and displays it as a minimized window.</summary>
            /// <remarks>See SW_SHOWMINIMIZED</remarks>
            ShowMinimized = 2,
            /// <summary>Activates the window and displays it as a maximized window.</summary>
            /// <remarks>See SW_SHOWMAXIMIZED</remarks>
            ShowMaximized = 3,
            /// <summary>Maximizes the specified window.</summary>
            /// <remarks>See SW_MAXIMIZE</remarks>
            Maximize = 3,
            /// <summary>Displays a window in its most recent size and position.
            /// This value is similar to "ShowNormal", except the window is not
            /// actived.</summary>
            /// <remarks>See SW_SHOWNOACTIVATE</remarks>
            ShowNormalNoActivate = 4,
            /// <summary>Activates the window and displays it in its current size
            /// and position.</summary>
            /// <remarks>See SW_SHOW</remarks>
            Show = 5,
            /// <summary>Minimizes the specified window and activates the next
            /// top-level window in the Z order.</summary>
            /// <remarks>See SW_MINIMIZE</remarks>
            Minimize = 6,
            /// <summary>Displays the window as a minimized window. This value is
            /// similar to "ShowMinimized", except the window is not activated.</summary>
            /// <remarks>See SW_SHOWMINNOACTIVE</remarks>
            ShowMinNoActivate = 7,
            /// <summary>Displays the window in its current size and position. This
            /// value is similar to "Show", except the window is not activated.</summary>
            /// <remarks>See SW_SHOWNA</remarks>
            ShowNoActivate = 8,
            /// <summary>Activates and displays the window. If the window is
            /// minimized or maximized, the system restores it to its original size
            /// and position. An application should specify this flag when restoring
            /// a minimized window.</summary>
            /// <remarks>See SW_RESTORE</remarks>
            Restore = 9,
            /// <summary>Sets the show state based on the SW_ value specified in the
            /// STARTUPINFO structure passed to the CreateProcess function by the
            /// program that started the application.</summary>
            /// <remarks>See SW_SHOWDEFAULT</remarks>
            ShowDefault = 10,
            /// <summary>Windows 2000/XP: Minimizes a window, even if the thread
            /// that owns the window is hung. This flag should only be used when
            /// minimizing windows from a different thread.</summary>
            /// <remarks>See SW_FORCEMINIMIZE</remarks>
            ForceMinimized = 11
        }

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            ffxi = comboBox1.Text;
        }

        private void buttonS_Click(object sender, EventArgs e)
        {

                ffxi_status = "Started";

                button7.Enabled = true;

                char[] separateur = new Char[] { '~' };
                string[] aaa = ffxi.Split(separateur, 10);

                hWnd = (IntPtr)Convert.ToInt32(aaa[2].Trim());

                ShowWindow(hWnd, WindowShowStyle.Restore);

                int FFXIPID = 0;
                FFXIPID = Convert.ToInt32(Convert.ToInt32(aaa[1].Trim()));

                _FFACE = new FFACE(FFXIPID);

                myProcesses = Process.GetProcessById(FFXIPID);
                ProcessModuleCollection modules = myProcesses.Modules;
                foreach (ProcessModule i in modules)
                {
                    if (i.ModuleName.ToLower() == "ffximain.dll")
                        FFXIBase = i.BaseAddress.ToInt32();
                }

                move();

                init();

                comboBox1.Enabled = false;
                //timer2.Enabled = false;

                toolStripStatusLabel2.Text = "Started";
    
                timer1.Interval = update_display;
                timer1.Enabled = true;

                adddebugpb();

                Program.Start(aaa[1].Trim());

                buttonStart.Visible = false;
                buttonStop.Visible = true;

        }

        private void buttonStop_Click(object sender, EventArgs e)
        {

            this.Text = "FFXI Monitor";

            toolStripStatusLabel2.Text = "Idle";

            timer1.Enabled = false;

            button7.Enabled = false;

            comboBox1.Enabled = true;
            //timer2.Enabled = true;

            Program.fourthform.Controls.Clear();
            Program.fourthform.Hide();

            Program.Stop();
            
            DeleteInstance(_FFACE._InstanceID);

            buttonStart.Visible = true;
            buttonStop.Visible = false;

            ffxi_status = "Stopped";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "Idle";
        }



        private int program_exit()
        {


            //timer2.Enabled = false;
            //timer2.Stop();

            Program.secondForm.Close();
            Program.secondForm.Dispose();
            Program.fourthform.Close();
            Program.fourthform.Dispose();
            Program.debugForm.Close();
            Program.debugForm.Dispose();

            notifyIcon1.Dispose();

            Program.myPlayer.Dispose();

            //trying to quit without leaving a process in tasklist
            try
            {
                Program.Stop();
            }
            catch (Exception ee)
            {
                // Code exécuté en cas d'exception 
            }

            try
            {
                DeleteInstance(_FFACE._InstanceID);
            }
            catch (Exception ee)
            {
                // Code exécuté en cas d'exception 
            }

            Process.GetCurrentProcess().Kill();

            return 0;

        }

        private void ParseDisplayForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            program_exit();

        }

        private void textBoxA1_TextChanged(object sender, EventArgs e)
        {
            checkBoxA1.Checked = true;
            if (_tbA1.Text == "")
            {
                checkBoxA1.Checked = false;
            }
        }

        private void textBoxA2_TextChanged(object sender, EventArgs e)
        {
            checkBoxA2.Checked = true;
            if (textBoxA2.Text == "")
            {
                checkBoxA2.Checked = false;
            }
        }

        private void textBoxA3_TextChanged(object sender, EventArgs e)
        {
            checkBoxA3.Checked = true;
            if (textBoxA3.Text == "")
            {
                checkBoxA3.Checked = false;
            }
        }

        private void textBoxA4_TextChanged(object sender, EventArgs e)
        {
            checkBoxA4.Checked = true;
            if (textBoxA4.Text == "")
            {
                checkBoxA4.Checked = false;
            }
        }

        private void textBoxA5_TextChanged(object sender, EventArgs e)
        {
            checkBoxA5.Checked = true;
            if (textBoxA5.Text == "")
            {
                checkBoxA5.Checked = false;
            }
        }

        private void textBoxA6_TextChanged(object sender, EventArgs e)
        {
            checkBoxA6.Checked = true;
            if (textBoxA6.Text == "")
            {
                checkBoxA6.Checked = false;
            }
        }

        private void textBoxA7_TextChanged(object sender, EventArgs e)
        {
            checkBoxA7.Checked = true;
            if (textBoxA7.Text == "")
            {
                checkBoxA7.Checked = false;
            }
        }


        public Form_FFXI_Monitor()
        {
            InitializeComponent();
        }


        public static void lectureFichierBuffs(string fichier)
        {
            int i = 1;

            try
            {
                // Création d'une instance de StreamReader pour permettre la lecture de notre fichier 
                StreamReader monStreamReader = new StreamReader(fichier);
                string ligne = monStreamReader.ReadLine();     //first line will be skipped

                ligne = monStreamReader.ReadLine();
                while ((ligne != null) & (i < 30))
                {
                    buffs[i] = ligne;
                    i = i + 1;
                    ligne = monStreamReader.ReadLine();
                }
                // Fermeture du StreamReader (attention très important) 
                monStreamReader.Close();
            }
            catch (Exception e)
            {
                if (Program._ApplicationForm._debug.Checked == true)
                {
                    Program.debugForm.tb.Text = DateTime.Now + " error reading buffs.ini" + Environment.NewLine + Program.debugForm.tb.Text;
                }
            }

        }


        public static void lectureFichierdeBuffs(string fichier)
        {
            int i = 1;

            try
            {
                // Création d'une instance de StreamReader pour permettre la lecture de notre fichier 
                StreamReader monStreamReader = new StreamReader(fichier);
                string ligne = monStreamReader.ReadLine();     //first line will be skipped

                ligne = monStreamReader.ReadLine();
                while ((ligne != null) & (i < 30))
                {
                    debuffs[i] = ligne;
                    i = i + 1;
                    ligne = monStreamReader.ReadLine();
                }
                // Fermeture du StreamReader (attention très important) 
                monStreamReader.Close();
            }
            catch (Exception e)
            {
                if (Program._ApplicationForm._debug.Checked == true)
                {
                    Program.debugForm.tb.Text = DateTime.Now + " error reading debuffs.ini" + Environment.NewLine + Program.debugForm.tb.Text;
                }
            }

        }


        public static void lectureFichierSettings(string fichier)
        {

            try
            {
                // Création d'une instance de StreamReader pour permettre la lecture de notre fichier 
                StreamReader monStreamReader = new StreamReader(fichier);
                string ligne = monStreamReader.ReadLine();     //first line will be skipped

                char[] separateur = new Char[] { '=' };
                char[] separateur2 = new Char[] { ' ' };

                first_instance = monStreamReader.ReadLine().ToString().Replace(" ", "").Split(separateur)[1];
                minimized_start = monStreamReader.ReadLine().ToString().Replace(" ", "").Split(separateur)[1];
                memloc_chatlog = monStreamReader.ReadLine().ToString().Replace(" ", "").Split(separateur)[1];
                alarm1 = monStreamReader.ReadLine().ToString().Split(separateur)[1].TrimStart(separateur2).TrimEnd(separateur2);
                alarm2 = monStreamReader.ReadLine().ToString().Split(separateur)[1].TrimStart(separateur2).TrimEnd(separateur2);
                alarm3 = monStreamReader.ReadLine().ToString().Split(separateur)[1].TrimStart(separateur2).TrimEnd(separateur2);
                alarm4 = monStreamReader.ReadLine().ToString().Split(separateur)[1].TrimStart(separateur2).TrimEnd(separateur2);
                alarm5 = monStreamReader.ReadLine().ToString().Split(separateur)[1].TrimStart(separateur2).TrimEnd(separateur2);
                alarm6 = monStreamReader.ReadLine().ToString().Split(separateur)[1].TrimStart(separateur2).TrimEnd(separateur2);
                alarm7 = monStreamReader.ReadLine().ToString().Split(separateur)[1].TrimStart(separateur2).TrimEnd(separateur2);
                main_character = monStreamReader.ReadLine().ToString().Replace(" ", "").Split(separateur)[1];
                update_display = int.Parse(monStreamReader.ReadLine().ToString().Replace(" ", "").Split(separateur)[1]);
                limit = monStreamReader.ReadLine().ToString().Split(separateur)[1].TrimStart(separateur2).TrimEnd(separateur2);
                x_pos = int.Parse(monStreamReader.ReadLine().ToString().Replace(" ", "").Split(separateur)[1]);
                x_separator = int.Parse(monStreamReader.ReadLine().ToString().Replace(" ", "").Split(separateur)[1]);

                ligne = monStreamReader.ReadLine();     //skip this line
                ligne = monStreamReader.ReadLine();     //skip this line

                party0_y_pos = int.Parse(monStreamReader.ReadLine().ToString().Replace(" ", "").Split(separateur)[1]);
                y0_separator = int.Parse(monStreamReader.ReadLine().ToString().Replace(" ", "").Split(separateur)[1]);
                icon_size0 = int.Parse(monStreamReader.ReadLine().ToString().Replace(" ", "").Split(separateur)[1]);

                ligne = monStreamReader.ReadLine();     //skip this line
                ligne = monStreamReader.ReadLine();     //skip this line

                if (limit == "yes")
                {
                    party1_y_pos = int.Parse(monStreamReader.ReadLine().ToString().Replace(" ", "").Split(separateur)[1]);
                    party2_y_pos = int.Parse(monStreamReader.ReadLine().ToString().Replace(" ", "").Split(separateur)[1]);
                    y12_separator = int.Parse(monStreamReader.ReadLine().ToString().Replace(" ", "").Split(separateur)[1]);
                    icon_size12 = int.Parse(monStreamReader.ReadLine().ToString().Replace(" ", "").Split(separateur)[1]);
                }
                else
                {
                    ligne = monStreamReader.ReadLine();     //skip this line
                    ligne = monStreamReader.ReadLine();     //skip this line
                    ligne = monStreamReader.ReadLine();     //skip this line
                    ligne = monStreamReader.ReadLine();     //skip this line
                }

                ligne = monStreamReader.ReadLine();     //skip this line
                ligne = monStreamReader.ReadLine();     //skip this line

                npc_name = monStreamReader.ReadLine().ToString().Replace(" ", "").Split(separateur)[1];
                npc_y = int.Parse(monStreamReader.ReadLine().ToString().Replace(" ", "").Split(separateur)[1]);
                npc_x_pos = int.Parse(monStreamReader.ReadLine().ToString().Replace(" ", "").Split(separateur)[1]);
                memloc_chatlog_lines = monStreamReader.ReadLine().ToString().Replace(" ", "").Split(separateur)[1];

                ligne = monStreamReader.ReadLine();     //skip this line
                ligne = monStreamReader.ReadLine();     //skip this line

                mob_y_pos = int.Parse(monStreamReader.ReadLine().ToString().Replace(" ", "").Split(separateur)[1]);
                
                // Fermeture du StreamReader (attention très important)
                monStreamReader.Close();
            }
            catch (Exception e)
            {
                if (Program._ApplicationForm._debug.Checked == true)
                {
                    Program.debugForm.tb.Text = DateTime.Now + " error reading settings.ini" + Environment.NewLine + Program.debugForm.tb.Text;
                }
            }

        }


        public static void lectureFichierDurations(string fichier)
        {
            
            try
            {
                // Création d'une instance de StreamReader pour permettre la lecture de notre fichier 
                StreamReader monStreamReader = new StreamReader(fichier);
                string ligne = monStreamReader.ReadLine();     //first line will be skipped

                char[] separateur = new Char[] { '=' };
                char[] separateur2 = new Char[] { ' ' };

                string name = "";
                string value = "";
                ligne = monStreamReader.ReadLine();
                while (ligne != null)
                {

                    name = ligne.Split(separateur)[0].TrimStart(separateur2);
                    name = ligne.Split(separateur)[0].TrimEnd(separateur2);

                    value = ligne.ToString().Replace(" ", "").Split(separateur)[1];
                    for (int i = 1; i < 31; i++)
                    {
                        if (buffs[i] == name)
                        {
                            durationsB[i] = int.Parse(value);
                            break;
                        }
                        if (debuffs[i] == name)
                        {
                            durationsDB[i] = int.Parse(value);
                            break;
                        }
                    }

                    ligne = monStreamReader.ReadLine();
                }
                // Fermeture du StreamReader (attention très important) 
                monStreamReader.Close();
            }
            catch (Exception e)
            {
                if (Program._ApplicationForm._debug.Checked == true)
                {
                    Program.debugForm.tb.Text = DateTime.Now + " error reading durations.ini" + Environment.NewLine + Program.debugForm.tb.Text;
                }
            }

        }

        
        public static void lectureFichierAR(string fichier)
        {

            int i = 1;

            try
            {
                // Création d'une instance de StreamReader pour permettre la lecture de notre fichier 
                StreamReader monStreamReader = new StreamReader(fichier);
                string ligne = monStreamReader.ReadLine();     //first line will be skipped

                ligne = monStreamReader.ReadLine();
                while ((ligne != null) & (i < 30))
                {
                    ar[i] = ligne;
                    i = i + 1;
                    ligne = monStreamReader.ReadLine();
                }
                // Fermeture du StreamReader (attention très important) 
                monStreamReader.Close();
            }
            catch (Exception e)
            {
                if (Program._ApplicationForm._debug.Checked == true)
                {
                    Program.debugForm.tb.Text = DateTime.Now + " error reading a-r.ini" + Environment.NewLine + Program.debugForm.tb.Text;
                }
            }

        }

        public CheckBox _checkBoxSay
        {
            get { return checkBoxSay; }
        }

        public CheckBox _checkBoxTell
        {
            get { return checkBoxTell; }
        }

        public CheckBox _checkBoxParty
        {
            get { return checkBoxParty; }
        }

        public CheckBox _checkBoxLS
        {
            get { return checkBoxLS; }
        }

        public CheckBox _checkBoxShout
        {
            get { return checkBoxShout; }
        }

        public CheckBox _checkBoxYell
        {
            get { return checkBoxYell; }
        }

        public CheckBox _checkBoxLoop
        {
            get { return checkBoxLoop; }
        }

        

        public CheckBox _checkBoxA1
        {
            get { return checkBoxA1; }
        }
        
        public CheckBox _checkBoxA2
        {
            get { return checkBoxA2; }
        }
        
        public CheckBox _checkBoxA3
        {
            get { return checkBoxA3; }
        }
        
        public CheckBox _checkBoxA4
        {
            get { return checkBoxA4; }
        }

        public CheckBox _checkBoxA5
        {
            get { return checkBoxA5; }
        }

        public CheckBox _checkBoxA6
        {
            get { return checkBoxA6; }
        }

        public CheckBox _checkBoxA7
        {
            get { return checkBoxA7; }
        }

        public TextBox _tbA1
        {
            get { return textBoxA1; }
        }

        public TextBox _tbA2
        {
            get { return textBoxA2; }
        }

        public TextBox _tbA3
        {
            get { return textBoxA3; }
        }

        public TextBox _tbA4
        {
            get { return textBoxA4; }
        }

        public TextBox _tbA5
        {
            get { return textBoxA5; }
        }

        public TextBox _tbA6
        {
            get { return textBoxA6; }
        }
        public TextBox _tbA7
        {
            get { return textBoxA7; }
        }

        public CheckBox _debug
        {
            get { return debug; }
        }

        public ComboBox _cbTranslate
        {
            get { return comboBoxTranslate; }
        }

        public ComboBox _cbL
        {
            get { return comboBoxL; }
        }

        public ToolStripStatusLabel _toolStripStatusLabel2
        {
            get { return toolStripStatusLabel2; }
        }

        public CheckBox _checkBox3
        {
            get { return checkBox3; }
        }

        public static int cherche_pers(string pers)
        {

            int flag = 0;
            int j = 0;

            foreach (string p in party_2)
            {
                if (p == pers)
                {
                    flag = 1;
                    break;
                }
                j = j + 1;
            }

            if (flag == 1)
            {
                return j;
            }
            else
            {
                return -1;
            }

        }          //end cherche_pers

        public static int cherche_mob(string pers)
        {

            int flag = 0;
            int j = 0;

            foreach (string p in mob_2)
            {
                if (p == pers)
                {
                    flag = 1;
                    break;
                }
                j = j + 1;
            }

            if (flag == 1)
            {
                return j;
            }
            else
            {
                return -1;
            }

        }          //end cherche_mob

        public static int add_mob(string pers)
        {

            int j = 0;
            foreach (string p in mob_2)
            {
                if (p == null)
                {
                    break;
                }
                j = j + 1;
            }

            if (j == 150)
            {
                MessageBox.Show("warning : names limit (150/200)");
            }

            if (j == 198)
            {
                MessageBox.Show("names limit (200), please restart program before it crash");
            }

            mob_2[j] = pers;
            return j;

        }          //end add_mob

        private int init()
        {

            Program.fourthform.Show();

            lectureFichierBuffs("./buffs.ini");
            lectureFichierdeBuffs("./debuffs.ini");
            lectureFichierDurations("./durations.ini");

            for (int d = 0; d < 6; d++)
            {
                y0pos[d] = Program.fourthform.Height - party0_y_pos + (d - 6) * y0_separator;
            }


            if (limit == "yes")
            {

                for (int d = 0; d < 6; d++)
                {
                    y1pos[d] = Program.fourthform.Height - party1_y_pos + (d - 6) * y12_separator;
                }

                for (int d = 0; d < 6; d++)
                {
                    y2pos[d] = Program.fourthform.Height - party2_y_pos + (d - 6) * y12_separator;
                }

            }

            for (int d = 0; d < 15; d++)
            {
                npc_y_pos[d] = Program.fourthform.Height - npc_y - icon_size12 * (d + 1);
            }


            if (npc_name != "")
            {
                party_2[0] = npc_name;
            }

            /*
            for (int d = 0; d < 15; d++)
            {
                ympos[d] = Program.fourthform.Height - mob_y_pos;
            }
            */
            
            Program.fourthform.Controls.Clear();

            PictureBox pict = null;

            ToolTip tt = new ToolTip();

            for (int j = 0; j < 6; j++)
            {

                for (int i = 0; i < 20; i++)
                {
                    pict = new PictureBox();
                    pict.Location = new System.Drawing.Point(Program.fourthform.Width - x_pos - i * icon_size0 - i * x_separator, y0pos[j]);
                    pict.Name = "pictureBox0-" + j.ToString() + "-" + i.ToString();
                    pict.Size = new System.Drawing.Size(icon_size0, icon_size0);
                    pict.Load("./images/0/none.bmp");
                    pict.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    tt.SetToolTip(pict, pict.Name);
                    Program.fourthform.Controls.Add(pict);
                }

                if (limit == "yes")
                {

                    for (int i = 0; i < 20; i++)
                    {
                        pict = new PictureBox();
                        pict.Location = new System.Drawing.Point(Program.fourthform.Width - x_pos - i * icon_size12 - i * x_separator, y1pos[j]);
                        pict.Name = "pictureBox1-" + j.ToString() + "-" + i.ToString();
                        pict.Size = new System.Drawing.Size(icon_size12, icon_size12);
                        pict.Load("./images/12/none.bmp");
                        pict.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                        tt.SetToolTip(pict, pict.Name);
                        Program.fourthform.Controls.Add(pict);
                    }

                    for (int i = 0; i < 20; i++)
                    {
                        pict = new PictureBox();
                        pict.Location = new System.Drawing.Point(Program.fourthform.Width - x_pos - i * icon_size12 - i * x_separator, y2pos[j]);
                        pict.Name = "pictureBox2-" + j.ToString() + "-" + i.ToString();
                        pict.Size = new System.Drawing.Size(icon_size12, icon_size12);
                        pict.Load("./images/12/none.bmp");
                        pict.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                        tt.SetToolTip(pict, pict.Name);
                        Program.fourthform.Controls.Add(pict);
                    }

                }

            }


            for (int i = 0; i < 20; i++)
            {
                pict = new PictureBox();
                pict.Location = new System.Drawing.Point(Program.fourthform.Width - npc_x_pos - i * icon_size12 - i * x_separator, npc_y_pos[0]);
                pict.Name = "pictureBoxNPC-" + i.ToString();
                pict.Size = new System.Drawing.Size(icon_size12, icon_size12);
                pict.Load("./images/12/none.bmp");
                pict.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                tt.SetToolTip(pict, pict.Name);
                Program.fourthform.Controls.Add(pict);
                pict.BringToFront();
            }


            //mob
            for (int i = 0; i < 20; i++)
            {
                pict = new PictureBox();
                pict.Location = new System.Drawing.Point(Program.fourthform.Width - x_pos - i * icon_size0 - i * x_separator, 100);    //y=100 is arbitrary
                pict.Name = "pictureBoxM-" + i.ToString();
                pict.Size = new System.Drawing.Size(icon_size0, icon_size0);
                pict.Load("./images/0/none.bmp");
                pict.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                tt.SetToolTip(pict, pict.Name);
                Program.fourthform.Controls.Add(pict);
                pict.BringToFront();
            }



            for (int i = 0; i < arrayB1.Length; i++)         //we suppose all array have same length
            {
                arrayB1[i] = new DateTime(1975, 1, 18);
                arrayB2[i] = new DateTime(1975, 1, 18);
                arrayB3[i] = new DateTime(1975, 1, 18);
                arrayB4[i] = new DateTime(1975, 1, 18);
                arrayB5[i] = new DateTime(1975, 1, 18);
                arrayB6[i] = new DateTime(1975, 1, 18);
                arrayB7[i] = new DateTime(1975, 1, 18);
                arrayB8[i] = new DateTime(1975, 1, 18);
                arrayB9[i] = new DateTime(1975, 1, 18);
                arrayB10[i] = new DateTime(1975, 1, 18);
                arrayB11[i] = new DateTime(1975, 1, 18);
                arrayB12[i] = new DateTime(1975, 1, 18);
                arrayB13[i] = new DateTime(1975, 1, 18);
                arrayB14[i] = new DateTime(1975, 1, 18);
                arrayB15[i] = new DateTime(1975, 1, 18);
                arrayB16[i] = new DateTime(1975, 1, 18);
                arrayB17[i] = new DateTime(1975, 1, 18);
                arrayB18[i] = new DateTime(1975, 1, 18);
                arrayB19[i] = new DateTime(1975, 1, 18);
                arrayB20[i] = new DateTime(1975, 1, 18);
                arrayB21[i] = new DateTime(1975, 1, 18);
                arrayB22[i] = new DateTime(1975, 1, 18);
                arrayB23[i] = new DateTime(1975, 1, 18);
                arrayB24[i] = new DateTime(1975, 1, 18);
                arrayB25[i] = new DateTime(1975, 1, 18);
                arrayB26[i] = new DateTime(1975, 1, 18);
                arrayB27[i] = new DateTime(1975, 1, 18);
                arrayB28[i] = new DateTime(1975, 1, 18);
                arrayB29[i] = new DateTime(1975, 1, 18);
                arrayB30[i] = new DateTime(1975, 1, 18);

                arrayDB1[i] = new DateTime(1975, 1, 18);
                arrayDB2[i] = new DateTime(1975, 1, 18);
                arrayDB3[i] = new DateTime(1975, 1, 18);
                arrayDB4[i] = new DateTime(1975, 1, 18);
                arrayDB5[i] = new DateTime(1975, 1, 18);
                arrayDB6[i] = new DateTime(1975, 1, 18);
                arrayDB7[i] = new DateTime(1975, 1, 18);
                arrayDB8[i] = new DateTime(1975, 1, 18);
                arrayDB9[i] = new DateTime(1975, 1, 18);
                arrayDB10[i] = new DateTime(1975, 1, 18);
                arrayDB11[i] = new DateTime(1975, 1, 18);
                arrayDB12[i] = new DateTime(1975, 1, 18);
                arrayDB13[i] = new DateTime(1975, 1, 18);
                arrayDB14[i] = new DateTime(1975, 1, 18);
                arrayDB15[i] = new DateTime(1975, 1, 18);
                arrayDB16[i] = new DateTime(1975, 1, 18);
                arrayDB17[i] = new DateTime(1975, 1, 18);
                arrayDB18[i] = new DateTime(1975, 1, 18);
                arrayDB19[i] = new DateTime(1975, 1, 18);
                arrayDB20[i] = new DateTime(1975, 1, 18);
                arrayDB21[i] = new DateTime(1975, 1, 18);
                arrayDB22[i] = new DateTime(1975, 1, 18);
                arrayDB23[i] = new DateTime(1975, 1, 18);
                arrayDB24[i] = new DateTime(1975, 1, 18);
                arrayDB25[i] = new DateTime(1975, 1, 18);
                arrayDB26[i] = new DateTime(1975, 1, 18);
                arrayDB27[i] = new DateTime(1975, 1, 18);
                arrayDB28[i] = new DateTime(1975, 1, 18);
                arrayDB29[i] = new DateTime(1975, 1, 18);
                arrayDB30[i] = new DateTime(1975, 1, 18);

                arrayMB1[i] = new DateTime(1975, 1, 18);
                arrayMB2[i] = new DateTime(1975, 1, 18);
                arrayMB3[i] = new DateTime(1975, 1, 18);
                arrayMB4[i] = new DateTime(1975, 1, 18);
                arrayMB5[i] = new DateTime(1975, 1, 18);
                arrayMB6[i] = new DateTime(1975, 1, 18);
                arrayMB7[i] = new DateTime(1975, 1, 18);
                arrayMB8[i] = new DateTime(1975, 1, 18);
                arrayMB9[i] = new DateTime(1975, 1, 18);
                arrayMB10[i] = new DateTime(1975, 1, 18);
                arrayMB11[i] = new DateTime(1975, 1, 18);
                arrayMB12[i] = new DateTime(1975, 1, 18);
                arrayMB13[i] = new DateTime(1975, 1, 18);
                arrayMB14[i] = new DateTime(1975, 1, 18);
                arrayMB15[i] = new DateTime(1975, 1, 18);
                arrayMB16[i] = new DateTime(1975, 1, 18);
                arrayMB17[i] = new DateTime(1975, 1, 18);
                arrayMB18[i] = new DateTime(1975, 1, 18);
                arrayMB19[i] = new DateTime(1975, 1, 18);
                arrayMB20[i] = new DateTime(1975, 1, 18);
                arrayMB21[i] = new DateTime(1975, 1, 18);
                arrayMB22[i] = new DateTime(1975, 1, 18);
                arrayMB23[i] = new DateTime(1975, 1, 18);
                arrayMB24[i] = new DateTime(1975, 1, 18);
                arrayMB25[i] = new DateTime(1975, 1, 18);
                arrayMB26[i] = new DateTime(1975, 1, 18);
                arrayMB27[i] = new DateTime(1975, 1, 18);
                arrayMB28[i] = new DateTime(1975, 1, 18);
                arrayMB29[i] = new DateTime(1975, 1, 18);
                arrayMB30[i] = new DateTime(1975, 1, 18);

                arrayMDB1[i] = new DateTime(1975, 1, 18);
                arrayMDB2[i] = new DateTime(1975, 1, 18);
                arrayMDB3[i] = new DateTime(1975, 1, 18);
                arrayMDB4[i] = new DateTime(1975, 1, 18);
                arrayMDB5[i] = new DateTime(1975, 1, 18);
                arrayMDB6[i] = new DateTime(1975, 1, 18);
                arrayMDB7[i] = new DateTime(1975, 1, 18);
                arrayMDB8[i] = new DateTime(1975, 1, 18);
                arrayMDB9[i] = new DateTime(1975, 1, 18);
                arrayMDB10[i] = new DateTime(1975, 1, 18);
                arrayMDB11[i] = new DateTime(1975, 1, 18);
                arrayMDB12[i] = new DateTime(1975, 1, 18);
                arrayMDB13[i] = new DateTime(1975, 1, 18);
                arrayMDB14[i] = new DateTime(1975, 1, 18);
                arrayMDB15[i] = new DateTime(1975, 1, 18);
                arrayMDB16[i] = new DateTime(1975, 1, 18);
                arrayMDB17[i] = new DateTime(1975, 1, 18);
                arrayMDB18[i] = new DateTime(1975, 1, 18);
                arrayMDB19[i] = new DateTime(1975, 1, 18);
                arrayMDB20[i] = new DateTime(1975, 1, 18);
                arrayMDB21[i] = new DateTime(1975, 1, 18);
                arrayMDB22[i] = new DateTime(1975, 1, 18);
                arrayMDB23[i] = new DateTime(1975, 1, 18);
                arrayMDB24[i] = new DateTime(1975, 1, 18);
                arrayMDB25[i] = new DateTime(1975, 1, 18);
                arrayMDB26[i] = new DateTime(1975, 1, 18);
                arrayMDB27[i] = new DateTime(1975, 1, 18);
                arrayMDB28[i] = new DateTime(1975, 1, 18);
                arrayMDB29[i] = new DateTime(1975, 1, 18);
                arrayMDB30[i] = new DateTime(1975, 1, 18);
                        
            }

            return 0;

        }

        private void comboBoxTranslate_TextChanged(object sender, EventArgs e)
        {

            if (comboBoxTranslate.Text == "Babelfish")
            {
                comboBoxL.Text = "en";
                comboBoxL.Enabled = false;
            }

            if (comboBoxTranslate.Text == "Google")
            {
                if (comboBoxL.Text == "")
                {
                    comboBoxL.Text = "en";
                }
                comboBoxL.Enabled = true;
            }

            if (comboBoxTranslate.Text == "")
            {
                comboBoxL.Enabled = false;
            }

        }

        private void ParseDisplayForm_Load(object sender, EventArgs e)
        {
            Program.secondForm = new Form_Logs();
            Program.secondForm.Show();
            Program.secondForm.Hide();

            Program.debugForm = new Form_Debug();
            Program.debugForm.Show();
            Program.debugForm.Hide();

            Program.fourthform = new Form_Icons();
            Program.fourthform.Show();
            Program.fourthform.Hide();

            lectureFichierSettings("./settings.ini");
            if (alarm1 != "")
            {
                textBoxA1.Text = alarm1;
                checkBoxA1.Checked = true;
            }
            if (alarm2 != "")
            {
                textBoxA2.Text = alarm2;
                checkBoxA2.Checked = true;
            }
            if (alarm3 != "")
            {
                textBoxA3.Text = alarm3;
                checkBoxA3.Checked = true;
            }
            if (alarm4 != "")
            {
                textBoxA4.Text = alarm4;
                checkBoxA4.Checked = true;
            }
            if (alarm5 != "")
            {
                textBoxA5.Text = alarm5;
                checkBoxA5.Checked = true;
            }
            if (alarm6 != "")
            {
                textBoxA6.Text = alarm6;
                checkBoxA6.Checked = true;
            }
            if (alarm7 != "")
            {
                textBoxA7.Text = alarm7;
                checkBoxA7.Checked = true;
            }



            int flag = 0;
            comboBox1.Items.Clear();

            comboBox1.Items.Add("---Pol");

            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);

            Process[] procs = Process.GetProcessesByName("pol");

            for (int i = 0; i < procs.Length; i++)
            {

                GetWindowText(procs[i].MainWindowHandle, Buff, nChars);

                comboBox1.Items.Add(Buff.ToString() + " ~ " + procs[i].Id.ToString() + " ~ " + procs[i].MainWindowHandle.ToString());

                if (comboBox1.Text == "")
                {
                    comboBox1.Text = Buff.ToString() + " ~ " + procs[i].Id.ToString() + " ~ " + procs[i].MainWindowHandle.ToString();
                }

                flag = 1;
            }

            comboBox1.Items.Add("---Meteor");
            procs = Process.GetProcessesByName("mXI-7.1-EU");

            for (int i = 0; i < procs.Length; i++)
            {

                GetWindowText(procs[i].MainWindowHandle, Buff, nChars);

                comboBox1.Items.Add(Buff.ToString() + " ~ " + procs[i].Id.ToString() + " ~ " + procs[i].MainWindowHandle.ToString());

                if (comboBox1.Text == "")
                {
                    comboBox1.Text = Buff.ToString() + " ~ " + procs[i].Id.ToString() + " ~ " + procs[i].MainWindowHandle.ToString();
                }

                flag = 1;
            }



            procs = Process.GetProcessesByName("meteorXI-0.8.5");

            for (int i = 0; i < procs.Length; i++)
            {

                GetWindowText(procs[i].MainWindowHandle, Buff, nChars);

                comboBox1.Items.Add(Buff.ToString() + " ~ " + procs[i].Id.ToString() + " ~ " + procs[i].MainWindowHandle.ToString());

                if (comboBox1.Text == "")
                {
                    comboBox1.Text = Buff.ToString() + " ~ " + procs[i].Id.ToString() + " ~ " + procs[i].MainWindowHandle.ToString();
                }

                flag = 1;

            }

            if (flag == 1)
            {
                buttonStart.Enabled = true;

                if (minimized_start == "yes")
                {
                    this.WindowState = FormWindowState.Minimized;
                }

                if (first_instance == "yes")
                {
                    buttonStart.PerformClick();
                }
            }
            else
            {
                buttonStart.Enabled = false;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.secondForm.Visible == false)
            {
                Program.secondForm.Show();
            }
            else
            {
                Program.secondForm.Hide();
            }
        }

        private void debug_CheckStateChanged(object sender, EventArgs e)
        {
            if (debug.Checked == true)
            {
                Program.fourthform.FormBorderStyle = FormBorderStyle.FixedSingle;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = true;

                Program.debugForm.Show();
            }

            if (debug.Checked == false)
            {
                Program.fourthform.FormBorderStyle = FormBorderStyle.None;
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                button6.Visible = false;

                Program.debugForm.Hide();
            }

        }



        private string move()
        {
            if (checkBox3.Checked == true)
            {

                RECT rct;
                if (!GetWindowRect(hWnd, out rct))
                {

                    this.Text = "FFXI Monitor";

                    toolStripStatusLabel2.Text = "Idle";

                    timer1.Enabled = false;

                    buttonStart.Visible = true;
                    buttonStop.Visible = false;
                    button7.Enabled = false;
                    comboBox1.Enabled = true;
                    //timer2.Enabled = true;

                    Program.fourthform.Controls.Clear();
                    Program.fourthform.Hide();

                    Program.Stop();

                    DeleteInstance(_FFACE._InstanceID);
                    buttonStart.Visible = true;
                    buttonStop.Visible = false;
                    
                    if (debug.Checked == true)
                    {
                        Program.debugForm.tb.Text = DateTime.Now + " error in move function" + Environment.NewLine + Program.debugForm.tb.Text;
                    }

                }
                else
                {
                    wl = rct.Left;
                    wt = rct.Top;
                    wr = rct.Right;
                    wb = rct.Bottom;
                }

                Program.fourthform.Width = (wr - wl) / 2 + 50;
                Program.fourthform.Height = (wb - wt) - 50;
                Program.fourthform.Location = new Point(wr - Program.fourthform.Width, wb - Program.fourthform.Height);

            }
            return "";

        }


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        private void checkBox3_CheckStateChanged(object sender, EventArgs e)
        {

            if (buttonStart.Visible == false)
            {
            
            ShowWindow(hWnd, WindowShowStyle.Restore);

            move();
            init();

            timer1.Interval = update_display;
            timer1.Enabled = true;
            }
        }



        private void adddebugpb()
        {
            PictureBox pict = null;

            pict = new PictureBox();
            pict.Location = new System.Drawing.Point(5, y0pos[0]);
            pict.Name = "pictureBoxA1";
            pict.Size = new System.Drawing.Size(icon_size0, icon_size0);
            pict.Load("./images/debug/none.bmp");
            pict.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            Program.fourthform.Controls.Add(pict);

            pict = new PictureBox();
            pict.Location = new System.Drawing.Point(5, y0pos[3]);
            pict.Name = "pictureBoxB1";
            pict.Size = new System.Drawing.Size(icon_size0, icon_size0);
            pict.Load("./images/debug/none.bmp");
            pict.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            Program.fourthform.Controls.Add(pict);

            pict = new PictureBox();
            pict.Location = new System.Drawing.Point(5, y0pos[5]);
            pict.Name = "pictureBoxC1";
            pict.Size = new System.Drawing.Size(icon_size0, icon_size0);
            pict.Load("./images/debug/none.bmp");
            pict.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            Program.fourthform.Controls.Add(pict);

            if (limit == "yes")
            {
                pict = new PictureBox();
                pict.Location = new System.Drawing.Point(5, y1pos[0]);
                pict.Name = "pictureBoxA2";
                pict.Size = new System.Drawing.Size(icon_size12, icon_size12);
                pict.Load("./images/debug/none.bmp");
                pict.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                Program.fourthform.Controls.Add(pict);

                pict = new PictureBox();
                pict.Location = new System.Drawing.Point(5, y1pos[3]);
                pict.Name = "pictureBoxB2";
                pict.Size = new System.Drawing.Size(icon_size12, icon_size12);
                pict.Load("./images/debug/none.bmp");
                pict.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                Program.fourthform.Controls.Add(pict);

                pict = new PictureBox();
                pict.Location = new System.Drawing.Point(5, y1pos[5]);
                pict.Name = "pictureBoxC2";
                pict.Size = new System.Drawing.Size(icon_size12, icon_size12);
                pict.Load("./images/debug/none.bmp");
                pict.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                Program.fourthform.Controls.Add(pict);



                pict = new PictureBox();
                pict.Location = new System.Drawing.Point(5, y2pos[0]);
                pict.Name = "pictureBoxA3";
                pict.Size = new System.Drawing.Size(icon_size12, icon_size12);
                pict.Load("./images/debug/none.bmp");
                pict.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                Program.fourthform.Controls.Add(pict);

                pict = new PictureBox();
                pict.Location = new System.Drawing.Point(5, y2pos[3]);
                pict.Name = "pictureBoxB3";
                pict.Size = new System.Drawing.Size(icon_size12, icon_size12);
                pict.Load("./images/debug/none.bmp");
                pict.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                Program.fourthform.Controls.Add(pict);

                pict = new PictureBox();
                pict.Location = new System.Drawing.Point(5, y2pos[5]);
                pict.Name = "pictureBoxC3";
                pict.Size = new System.Drawing.Size(icon_size12, icon_size12);
                pict.Load("./images/debug/none.bmp");
                pict.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                Program.fourthform.Controls.Add(pict);
            }
        }




        private void button3_Click(object sender, EventArgs e)
        {
            if (buttonStop.Visible == true)
            {

                string text = "index/number name : ";
                
                if (npc_name != "")
                {
                    text = text + Environment.NewLine + "?/? " + npc_name;
                }

                for (int i = 0; i < 18; i++)
                {
                    if (_FFACE.PartyMember[(byte)i].Active == true)
                    {
                        text = text + Environment.NewLine + _FFACE.PartyMember[(byte)i].Index.ToString() + "/" + _FFACE.PartyMember[(byte)i].MemberNumber.ToString() + " " + _FFACE.PartyMember[(byte)i].Name;
                    }
                }



                //Add Reference/.NET/Microsoft.VisualBasic
                string input = Microsoft.VisualBasic.Interaction.InputBox(text, "party members", "select index", 0, 0);

                int pos = cherche_pers(input);

                if (pos != -1)
                {
                    if (arrayB1[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff1 on");
                    }

                    if (arrayB2[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff2 on");
                    }

                    if (arrayB3[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff3 on");
                    }

                    if (arrayB4[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff4 on");
                    }

                    if (arrayB5[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff5 on");
                    }

                    if (arrayB6[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff6 on");
                    }

                    if (arrayB7[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff7 on");
                    }

                    if (arrayB8[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff8 on");
                    }

                    if (arrayB9[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff9 on");
                    }

                    if (arrayB10[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff10 on");
                    }

                    if (arrayB11[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff11 on");
                    }

                    if (arrayB12[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff12 on");
                    }

                    if (arrayB13[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff13 on");
                    }

                    if (arrayB14[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff14 on");
                    }

                    if (arrayB15[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff15 on");
                    }

                    if (arrayB16[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff16 on");
                    }

                    if (arrayB17[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff17 on");
                    }

                    if (arrayB18[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff18 on");
                    }

                    if (arrayB19[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff19 on");
                    }

                    if (arrayB20[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff20 on");
                    }

                    if (arrayB21[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff21 on");
                    }

                    if (arrayB22[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff22 on");
                    }

                    if (arrayB23[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff23 on");
                    }

                    if (arrayB24[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff24 on");
                    }

                    if (arrayB25[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff25 on");
                    }

                    if (arrayB26[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff26 on");
                    }

                    if (arrayB27[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff27 on");
                    }

                    if (arrayB28[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff28 on");
                    }

                    if (arrayB29[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff29 on");
                    }

                    if (arrayB30[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff30 on");
                    }

                    if (arrayDB1[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff1 on");
                    }

                    if (arrayDB2[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff2 on");
                    }

                    if (arrayDB3[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff3 on");
                    }

                    if (arrayDB4[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff4 on");
                    }

                    if (arrayDB5[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff5 on");
                    }

                    if (arrayDB6[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff6 on");
                    }

                    if (arrayDB7[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff7 on");
                    }

                    if (arrayDB8[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff8 on");
                    }

                    if (arrayDB9[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff9 on");
                    }

                    if (arrayDB10[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff10 on");
                    }

                    if (arrayDB11[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff11 on");
                    }

                    if (arrayDB12[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff12 on");
                    }

                    if (arrayDB13[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff13 on");
                    }

                    if (arrayDB14[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff14 on");
                    }

                    if (arrayDB15[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff15 on");
                    }

                    if (arrayDB16[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff16 on");
                    }

                    if (arrayDB17[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff17 on");
                    }

                    if (arrayDB18[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff18 on");
                    }

                    if (arrayDB19[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff19 on");
                    }

                    if (arrayDB20[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff20 on");
                    }

                    if (arrayDB21[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff21 on");
                    }

                    if (arrayDB22[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff22 on");
                    }

                    if (arrayDB23[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff23 on");
                    }

                    if (arrayDB24[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff24 on");
                    }

                    if (arrayDB25[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff25 on");
                    }

                    if (arrayDB26[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff26 on");
                    }

                    if (arrayDB27[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff27 on");
                    }

                    if (arrayDB28[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff28 on");
                    }

                    if (arrayDB29[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff29 on");
                    }

                    if (arrayDB30[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff30 on");
                    }
                }
                else
                {
                    MessageBox.Show("not found");
                }


            }
        }







        private void button2_Click(object sender, EventArgs e)
        {

            if (buttonStop.Visible == true)
            {

                string text = "? name : ";
                for (int i = 0; i < 18; i++)
                {
                    if (mob_2[i] != "")
                    {
                        text = text + Environment.NewLine + i.ToString() + " " + mob_2[i];
                    }
                }



                //Add Reference/.NET/Microsoft.VisualBasic
                string input = Microsoft.VisualBasic.Interaction.InputBox(text, "mobs", "select index", 0, 0);

                int pos = int.Parse(input);
                
                if (pos != -1)
                {
                    if (arrayMB1[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff1 on");
                    }

                    if (arrayMB2[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff2 on");
                    }

                    if (arrayMB3[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff3 on");
                    }

                    if (arrayMB4[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff4 on");
                    }

                    if (arrayMB5[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff5 on");
                    }

                    if (arrayMB6[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff6 on");
                    }

                    if (arrayMB7[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff7 on");
                    }

                    if (arrayMB8[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff8 on");
                    }

                    if (arrayMB9[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff9 on");
                    }

                    if (arrayMB10[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff10 on");
                    }

                    if (arrayMB11[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff11 on");
                    }

                    if (arrayMB12[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff12 on");
                    }

                    if (arrayMB13[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff13 on");
                    }

                    if (arrayMB14[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff14 on");
                    }

                    if (arrayMB15[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff15 on");
                    }

                    if (arrayMB16[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff16 on");
                    }

                    if (arrayMB17[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff17 on");
                    }

                    if (arrayMB18[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff18 on");
                    }

                    if (arrayMB19[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff19 on");
                    }

                    if (arrayMB20[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff20 on");
                    }

                    if (arrayMB21[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff21 on");
                    }

                    if (arrayMB22[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff22 on");
                    }

                    if (arrayMB23[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff23 on");
                    }

                    if (arrayMB24[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff24 on");
                    }

                    if (arrayMB25[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff25 on");
                    }

                    if (arrayMB26[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff26 on");
                    }

                    if (arrayMB27[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff27 on");
                    }

                    if (arrayMB28[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff28 on");
                    }

                    if (arrayMB29[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff29 on");
                    }

                    if (arrayMB30[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("buff30 on");
                    }

                    if (arrayMDB1[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff1 on");
                    }

                    if (arrayMDB2[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff2 on");
                    }

                    if (arrayMDB3[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff3 on");
                    }

                    if (arrayMDB4[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff4 on");
                    }

                    if (arrayMDB5[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff5 on");
                    }

                    if (arrayMDB6[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff6 on");
                    }

                    if (arrayMDB7[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff7 on");
                    }

                    if (arrayMDB8[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff8 on");
                    }

                    if (arrayMDB9[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff9 on");
                    }

                    if (arrayMDB10[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff10 on");
                    }

                    if (arrayMDB11[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff11 on");
                    }

                    if (arrayMDB12[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff12 on");
                    }

                    if (arrayMDB13[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff13 on");
                    }

                    if (arrayMDB14[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff14 on");
                    }

                    if (arrayMDB15[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff15 on");
                    }

                    if (arrayMDB16[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff16 on");
                    }

                    if (arrayMDB17[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff17 on");
                    }

                    if (arrayMDB18[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff18 on");
                    }

                    if (arrayMDB19[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff19 on");
                    }

                    if (arrayMDB20[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff20 on");
                    }

                    if (arrayMDB21[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff21 on");
                    }

                    if (arrayMDB22[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff22 on");
                    }

                    if (arrayMDB23[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff23 on");
                    }

                    if (arrayMDB24[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff24 on");
                    }

                    if (arrayMDB25[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff25 on");
                    }

                    if (arrayMDB26[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff26 on");
                    }

                    if (arrayMDB27[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff27 on");
                    }

                    if (arrayMDB28[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff28 on");
                    }

                    if (arrayMDB29[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff29 on");
                    }

                    if (arrayMDB30[pos] != new DateTime(1975, 1, 18))
                    {
                        MessageBox.Show("debuff30 on");
                    }
                }
                else
                {
                    MessageBox.Show("not found");
                }


            }

        }




        private void button4_Click(object sender, EventArgs e)
        {

            try
            {

                comboBox1.Enabled = false;
                //timer2.Enabled = false;

                // Création d'une instance de StreamReader pour permettre la lecture de notre fichier 
                StreamReader monStreamReader = new StreamReader("debug.ini");

                string ligne = monStreamReader.ReadLine();
                char[] separateur = new Char[] { '-' };
                string[] val = ligne.Split(separateur, 10);
                dbgp0 = int.Parse(val[0]);
                dbgp1 = int.Parse(val[1]);
                dbgp2 = int.Parse(val[2]);

                int i = 0;
                ligne = monStreamReader.ReadLine();
                while (ligne != null)
                {
                    party_2[i] = ligne;
                    i = i + 1;
                    ligne = monStreamReader.ReadLine();
                }
                // Fermeture du StreamReader (attention très important) 
                monStreamReader.Close();

                string text = "pos : " + dbgp0.ToString() + "-" + dbgp1.ToString() + "-" + dbgp2.ToString();
                for (int j = 0; j < 18; j++)
                {

                    if (j == dbgp0)
                    {
                        text = text + Environment.NewLine + "-----------";
                    }

                    if (j == dbgp0 + dbgp1)
                    {
                        text = text + Environment.NewLine + "-----------";
                    }

                    if (j == dbgp0 + dbgp1 + dbgp2)
                    {
                        text = text + Environment.NewLine + "-----------";
                    }

                    if (party_2[j] != "")
                    {
                        text = text + Environment.NewLine + j.ToString() + " " + party_2[j];
                    }

                }
                total = i;

                MessageBox.Show(text);

                separateur = new Char[] { '-' };
                string[] aaa = ffxi.Split(separateur, 10);

                hWnd = (IntPtr)Convert.ToInt32(aaa[2].Trim());

                _FFACE = new FFACE(Convert.ToInt32(aaa[1].Trim()));

                move();
                init();

                timer1.Interval = update_display;
                timer1.Enabled = true;

                adddebugpb();

            }
            catch (Exception ee)
            {
                // Code exécuté en cas d'exception 
            }


        }


        private void button5_Click(object sender, EventArgs e)
        {

            string text = "name : ";
            for (int i = 0; i < 18; i++)
            {

                if (party_2[i] != "")
                {
                    text = text + Environment.NewLine + party_2[i];
                }

            }

            //Add Reference/.NET/Microsoft.VisualBasic
            string name = Microsoft.VisualBasic.Interaction.InputBox(text, "party members", "select name", 0, 0);

            int pos = cherche_pers(name);

            string input = Microsoft.VisualBasic.Interaction.InputBox("selection des buffs à activer", "buffs", "1-0-1-0", 0, 0);

            char[] separateur = new Char[] { '-' };
            string[] B = input.Split(separateur, 10);

            int j = 1;
            foreach (string p in B)
            {

                DateTime tmp = new DateTime(1975, 1, 18);
                if (p == "1")
                {
                    tmp = DateTime.Now;
                }

                if (j == 1) arrayB1[pos] = tmp;
                if (j == 2) arrayB2[pos] = tmp;
                if (j == 3) arrayB3[pos] = tmp;
                if (j == 4) arrayB4[pos] = tmp;
                if (j == 5) arrayB5[pos] = tmp;
                if (j == 6) arrayB6[pos] = tmp;
                if (j == 7) arrayB7[pos] = tmp;
                if (j == 8) arrayB8[pos] = tmp;
                if (j == 9) arrayB9[pos] = tmp;
                if (j == 10) arrayB10[pos] = tmp;
                if (j == 11) arrayB11[pos] = tmp;
                if (j == 12) arrayB12[pos] = tmp;
                if (j == 13) arrayB13[pos] = tmp;
                if (j == 14) arrayB14[pos] = tmp;
                if (j == 15) arrayB15[pos] = tmp;
                if (j == 16) arrayB16[pos] = tmp;
                if (j == 17) arrayB17[pos] = tmp;
                if (j == 18) arrayB18[pos] = tmp;
                if (j == 19) arrayB19[pos] = tmp;
                if (j == 20) arrayB20[pos] = tmp;
                if (j == 21) arrayB21[pos] = tmp;
                if (j == 22) arrayB22[pos] = tmp;
                if (j == 23) arrayB23[pos] = tmp;
                if (j == 24) arrayB24[pos] = tmp;
                if (j == 25) arrayB25[pos] = tmp;
                if (j == 26) arrayB26[pos] = tmp;
                if (j == 27) arrayB27[pos] = tmp;
                if (j == 28) arrayB28[pos] = tmp;
                if (j == 29) arrayB29[pos] = tmp;
                if (j == 30) arrayB30[pos] = tmp;

                j = j + 1;
            }


            input = Microsoft.VisualBasic.Interaction.InputBox("selection des debuffs à activer", "debuffs", "1-0-1-0", 0, 0);

            separateur = new Char[] { '-' };
            string[] DB = input.Split(separateur, 10);

            j = 1;
            foreach (string p in DB)
            {

                DateTime tmp = new DateTime(1975, 1, 18);
                if (p == "1")
                {
                    tmp = DateTime.Now;
                }

                if (j == 1) arrayDB1[pos] = tmp;
                if (j == 2) arrayDB2[pos] = tmp;
                if (j == 3) arrayDB3[pos] = tmp;
                if (j == 4) arrayDB4[pos] = tmp;
                if (j == 5) arrayDB5[pos] = tmp;
                if (j == 6) arrayDB6[pos] = tmp;
                if (j == 7) arrayDB7[pos] = tmp;
                if (j == 8) arrayDB8[pos] = tmp;
                if (j == 9) arrayDB9[pos] = tmp;
                if (j == 10) arrayDB10[pos] = tmp;
                if (j == 11) arrayDB11[pos] = tmp;
                if (j == 12) arrayDB12[pos] = tmp;
                if (j == 13) arrayDB13[pos] = tmp;
                if (j == 14) arrayDB14[pos] = tmp;
                if (j == 15) arrayDB15[pos] = tmp;
                if (j == 16) arrayDB16[pos] = tmp;
                if (j == 17) arrayDB17[pos] = tmp;
                if (j == 18) arrayDB18[pos] = tmp;
                if (j == 19) arrayDB19[pos] = tmp;
                if (j == 20) arrayDB20[pos] = tmp;
                if (j == 21) arrayDB21[pos] = tmp;
                if (j == 22) arrayDB22[pos] = tmp;
                if (j == 23) arrayDB23[pos] = tmp;
                if (j == 24) arrayDB24[pos] = tmp;
                if (j == 25) arrayDB25[pos] = tmp;
                if (j == 26) arrayDB26[pos] = tmp;
                if (j == 27) arrayDB27[pos] = tmp;
                if (j == 28) arrayDB28[pos] = tmp;
                if (j == 29) arrayDB29[pos] = tmp;
                if (j == 30) arrayDB30[pos] = tmp;

                j = j + 1;
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {

            move();

            int cur = 0;

            for (int i = 0; i < 18; i++)
            {

                if (party_2[i] != "")
                {

                    int pos = cherche_pers(party_2[i]);

                    cur = cur + 1;

                    int flag = 0;
                    int idx = 0;


                    if ((cur <= dbgp0) & (flag == 0))
                    {
                        idx = 6 - dbgp0 + cur - 1;
                        flag = 1;
                        doBDB(pos, y0pos, idx, "0/", "0");
                    }


                    if ((cur <= dbgp0 + dbgp1) & (flag == 0))
                    {
                        idx = cur - dbgp0 - 1;
                        flag = 1;
                        if (limit == "yes")
                        {
                            doBDB(pos, y1pos, idx, "12/", "1");
                        }
                    }

                    if ((cur <= dbgp0 + dbgp1 + dbgp2) & (flag == 0))
                    {
                        idx = cur - dbgp1 - dbgp0 - 1;
                        flag = 1;
                        if (limit == "yes")
                        {
                            doBDB(pos, y2pos, idx, "12/", "2");
                        }
                    }


                    if (debug.Checked == true)
                    {
                        foreach (Control c in Program.fourthform.Controls)
                        {
                            if (c is PictureBox)
                            {
                                if (((PictureBox)c).Name.Contains("pictureBoxA"))
                                {
                                    ((PictureBox)c).Load("./images/debug/protect.bmp");
                                }
                                if (((PictureBox)c).Name.Contains("pictureBoxB"))
                                {
                                    ((PictureBox)c).Load("./images/debug/shell.bmp");
                                }
                                if (((PictureBox)c).Name.Contains("pictureBoxC"))
                                {
                                    ((PictureBox)c).Load("./images/debug/haste.bmp");
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (Control c in Program.fourthform.Controls)
                        {
                            if (c is PictureBox)
                            {
                                if (((PictureBox)c).Name.Contains("pictureBoxA"))
                                {
                                    ((PictureBox)c).Load("./images/debug/none.bmp");
                                }
                                if (((PictureBox)c).Name.Contains("pictureBoxB"))
                                {
                                    ((PictureBox)c).Load("./images/debug/none.bmp");
                                }
                                if (((PictureBox)c).Name.Contains("pictureBoxC"))
                                {
                                    ((PictureBox)c).Load("./images/debug/none.bmp");
                                }
                            }
                        }
                    }



                }

            }

        }


        private int doBDB(int pos, int[] ypos, int idx, string p, string p2)
        {

            if (debug.Checked == true)
            {
                Program.debugForm.tb.Text = DateTime.Now + " @0 pos=" + pos.ToString() + " ypos=" + ypos.ToString() + " idx=" + idx.ToString() + " p=" + p + " p2=" + p2 + Environment.NewLine + Program.debugForm.tb.Text;
            }

            try
            {

                int nbbuffs = 0;

                if (arrayB1[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[1]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB2[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[2]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB3[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[3]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB4[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[4]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB5[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[5]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB6[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[6]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB7[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[7]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB8[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[8]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB9[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[9]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB10[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[10]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB11[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[11]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB12[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[12]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB13[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[13]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB14[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[14]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB15[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[15]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB16[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[16]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB17[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[17]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB18[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[18]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB19[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[19]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB20[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[20]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB21[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[21]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB22[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[22]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB23[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[23]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB24[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[24]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB25[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[25]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB26[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[26]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB27[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[27]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB28[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[28]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB29[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[29]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayB30[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, buffs[30]);
                    nbbuffs = nbbuffs + 1;
                }




                if (debug.Checked == true)
                {
                    Program.debugForm.tb.Text = DateTime.Now + " @1" + Environment.NewLine + Program.debugForm.tb.Text;
                }

                string x = DateTime.Now.Second.ToString();

                if ((x.Substring(x.Length - 1, 1) == "0") |
                    (x.Substring(x.Length - 1, 1) == "2") |
                    (x.Substring(x.Length - 1, 1) == "4") |
                    (x.Substring(x.Length - 1, 1) == "6") |
                    (x.Substring(x.Length - 1, 1) == "8"))
                {
                    
                    if (arrayDB1[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[1]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB2[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[2]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB3[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[3]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB4[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[4]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB5[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[5]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB6[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[6]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB7[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[7]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB8[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[8]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB9[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[9]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB10[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[10]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB11[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[11]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB12[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[12]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB13[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[13]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB14[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[14]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB15[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[15]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB16[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[16]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB17[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[17]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB18[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[18]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB19[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[19]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB20[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[20]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB21[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[21]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB22[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[22]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB23[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[23]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB24[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[24]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB25[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[25]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB26[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[26]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB27[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[27]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB28[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[28]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB29[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[29]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayDB30[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, debuffs[30]);
                        nbbuffs = nbbuffs + 1;
                    }
                }

                if (debug.Checked == true)
                {
                    Program.debugForm.tb.Text = DateTime.Now + " @2" + Environment.NewLine + Program.debugForm.tb.Text;
                }

                if ((p2 == "0") | (p2 == "1") | (p2 == "2"))
                {
                    for (int j = nbbuffs; j < 30; j++)
                    {
                        foreach (Control c in Program.fourthform.Controls)
                        {
                            if (c is PictureBox)
                            {
                                if (((PictureBox)c).Name == "pictureBox" + p2 + "-" + idx.ToString() + "-" + j.ToString())
                                {
                                    ((PictureBox)c).Load("./images/" + p + "none.bmp");
                                }
                            }
                        }
                    }
                }

                if (p2 == "npc")
                {
                    for (int j = nbbuffs; j < 30; j++)
                    {
                        foreach (Control c in Program.fourthform.Controls)
                        {
                            if (c is PictureBox)
                            {
                                if (((PictureBox)c).Name == "pictureBoxNPC-" + j.ToString())
                                {
                                    ((PictureBox)c).Load("./images/" + p + "none.bmp");
                                }
                            }
                        }
                    }
                }




            }
            catch (Exception e)
            {
                if (debug.Checked == true)
                {
                    Program.debugForm.tb.Text = DateTime.Now + " ERROR in doBDB function pos=" + pos.ToString() + " idx=" + idx.ToString() + " x_pox=" + x_pos.ToString() + " p=" + p + " p2=" + p2 + Environment.NewLine + Program.debugForm.tb.Text;
                }
            }

            return 0;

        }    //end doBDB







        private int doBDBM(int pos, int[] ypos, int idx, string p, string p2)
        {

            if (debug.Checked == true)
            {
                Program.debugForm.tb.Text = DateTime.Now + " @0 pos=" + pos.ToString() + " ypos=" + ypos.ToString() + " idx=" + idx.ToString() + " p=" + p + " p2=" + p2 + Environment.NewLine + Program.debugForm.tb.Text;
            }

            try
            {

                int nbbuffs = 0;

                if (arrayMDB1[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[1]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB2[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[2]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB3[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[3]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB4[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[4]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB5[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[5]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB6[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[6]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB7[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[7]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB8[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[8]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB9[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[9]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB10[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[10]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB11[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[11]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB12[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[12]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB13[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[13]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB14[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[14]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB15[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[15]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB16[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[16]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB17[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[17]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB18[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[18]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB19[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[19]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB20[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[20]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB21[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[21]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB22[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[22]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB23[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[23]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB24[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[24]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB25[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[25]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB26[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[26]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB27[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[27]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB28[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[28]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB29[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[29]);
                    nbbuffs = nbbuffs + 1;
                }

                if (arrayMDB30[pos] != new DateTime(1975, 1, 18))
                {
                    xxx(p, p2, idx, nbbuffs, debuffs[30]);
                    nbbuffs = nbbuffs + 1;
                }




                if (debug.Checked == true)
                {
                    Program.debugForm.tb.Text = DateTime.Now + " @1" + Environment.NewLine + Program.debugForm.tb.Text;
                }

                string x = DateTime.Now.Second.ToString();

                if ((x.Substring(x.Length - 1, 1) == "0") |
                    (x.Substring(x.Length - 1, 1) == "2") |
                    (x.Substring(x.Length - 1, 1) == "4") |
                    (x.Substring(x.Length - 1, 1) == "6") |
                    (x.Substring(x.Length - 1, 1) == "8"))
                {

                    if (arrayMB1[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[1]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB2[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[2]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB3[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[3]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB4[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[4]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB5[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[5]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB6[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[6]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB7[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[7]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB8[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[8]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB9[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[9]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB10[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[10]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB11[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[11]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB12[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[12]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB13[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[13]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB14[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[14]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB15[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[15]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB16[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[16]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB17[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[17]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB18[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[18]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB19[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[19]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB20[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[20]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB21[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[21]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB22[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[22]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB23[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[23]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB24[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[24]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB25[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[25]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB26[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[26]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB27[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[27]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB28[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[28]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB29[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[29]);
                        nbbuffs = nbbuffs + 1;
                    }

                    if (arrayMB30[pos] != new DateTime(1975, 1, 18))
                    {
                        xxx(p, p2, idx, nbbuffs, buffs[30]);
                        nbbuffs = nbbuffs + 1;
                    }
                    
                }

                if (debug.Checked == true)
                {
                    Program.debugForm.tb.Text = DateTime.Now + " @2" + Environment.NewLine + Program.debugForm.tb.Text;
                }

                


                if (p2 == "mob")
                {
                    for (int j = nbbuffs; j < 30; j++)
                    {
                        foreach (Control c in Program.fourthform.Controls)
                        {
                            if (c is PictureBox)
                            {
                                if (((PictureBox)c).Name == "pictureBoxM-" + j.ToString())
                                {
                                    ((PictureBox)c).Load("./images/" + p + "none.bmp");
                                }
                            }
                        }
                    }
                }




            }
            catch (Exception e)
            {
                if (debug.Checked == true)
                {
                    Program.debugForm.tb.Text = DateTime.Now + " ERROR in doBDBM function pos=" + pos.ToString() + " idx=" + idx.ToString() + " x_pox=" + x_pos.ToString() + " p=" + p + " p2=" + p2 + Environment.NewLine + Program.debugForm.tb.Text;
                }
            }

            return 0;

        }    //end doBDBM





        private int xxx(string p, string p2, int idx, int nbbuffs, string BDB)
        {
            if (BDB == "")
            {
                BDB = "error";
            }

            if ((p2 == "0") | (p2 == "1") | (p2 == "2"))
            {
                foreach (Control c in Program.fourthform.Controls)
                {
                    if (c is PictureBox)
                    {
                        if (((PictureBox)c).Name == "pictureBox" + p2 + "-" + idx.ToString() + "-" + nbbuffs.ToString())
                        {
                            ((PictureBox)c).Load("./images/" + p + BDB + ".bmp");
                        }
                    }
                }
            }

            if (p2 == "npc")
            {
                foreach (Control c in Program.fourthform.Controls)
                {
                    if (c is PictureBox)
                    {
                        if (((PictureBox)c).Name == "pictureBoxNPC-" + nbbuffs.ToString())
                        {
                            ((PictureBox)c).Load("./images/" + p + BDB + ".bmp");
                            ((PictureBox)c).Location = new System.Drawing.Point(Program.fourthform.Width - npc_x_pos - nbbuffs * icon_size12 - nbbuffs * x_separator, npc_y_pos[idx]); //.Load("./images/" + p + BDB + ".bmp");
                        }
                    }
                }
            }

            if (p2 == "mob")
            {
                foreach (Control c in Program.fourthform.Controls)
                {
                    if (c is PictureBox)
                    {
                        if (((PictureBox)c).Name == "pictureBoxM-" + nbbuffs.ToString())
                        {
                            ((PictureBox)c).Load("./images/" + p + BDB + ".bmp");
                            ((PictureBox)c).Location = new System.Drawing.Point(Program.fourthform.Width - x_pos - nbbuffs * icon_size0 - nbbuffs * x_separator, y0pos[6-_FFACE.Party.Party0Count] - mob_y_pos); //.Load("./images/" + p + BDB + ".bmp");
                        }
                    }
                }
            }

            return 0;
        }



        private void update()
        {

            updateencours = true;

            try
            {

                if (debug.Checked == true)
                {
                    Program.debugForm.tb.Text = DateTime.Now + " timer1 +++++++++++++++" + Environment.NewLine + Program.debugForm.tb.Text;
                }


                int px = System.Windows.Forms.Cursor.Position.X;
                int py = System.Windows.Forms.Cursor.Position.Y;

                if ((px != pre_x) & (checkBoxLoop.Checked))
                {
                    Program.myPlayer.Stop();
                }

                pre_x = px;
                prec_y = py;



                if ((pause == true) & (DateTime.Now < pause_time))
                {
                    if (debug.Checked == true)
                    {
                        Program.debugForm.tb.Text = DateTime.Now + " exit" + Environment.NewLine + Program.debugForm.tb.Text;
                    }
                    updateencours = false;
                    return;
                }



                const int nChars = 256;
                StringBuilder Buff = new StringBuilder(nChars);

                GetWindowText(hWnd, Buff, nChars);

                if ((Buff.ToString().ToLower() == "final fantasy xi") | (Buff.ToString().ToLower().Contains("playonline")))
                {

                    if (ffxi_status == "Running")
                    {
                        notifyIcon1.Text = "FFXI Monitor";
                        notifyIcon1.ShowBalloonTip(10000, "FFXI Monitor" , "Goodbye, " + _FFACE.Player.Name, ToolTipIcon.Info);
                        ffxi_status = "Paused";
                        sleep(4000);
                        notifyIcon1.Visible = false;
                        notifyIcon1.Visible = true;
                    }
                    
                    this.Text = "FFXI Monitor (login screen)";

                    toolStripStatusLabel2.Text = "Idle";

                    Program.Stop();

                    button7.Enabled = false;
                    button7.Text = "Add jobs";

                    Program.fourthform.Controls.Clear();
                    total = 0;
                    

                    if (debug.Checked == true)
                    {
                        Program.debugForm.tb.Text = DateTime.Now + " login screen detected" + Environment.NewLine + Program.debugForm.tb.Text;
                    }
                    pause = true;
                    pause_time = DateTime.Now.AddSeconds(10);

//DeleteInstance(_FFACE._InstanceID);
                    updateencours = false;
                    return;

                }
                else
                {

                    if (pause == true)
                    {

                        if (debug.Checked == true)
                        {
                            Program.debugForm.tb.Text = DateTime.Now + " now login" + Environment.NewLine + Program.debugForm.tb.Text;
                        }

                        pause = false;

                        button3.Enabled = true;

                        toolStripStatusLabel2.Text = "Started";

//move();

                        init();

                        adddebugpb();

//timer1.Enabled = true;

                        button7.Enabled = true;

                        char[] separateur = new Char[] { '~' };
                        string[] aaa = ffxi.Split(separateur, 10);

                        Program.Start(aaa[1].Trim());

                        updateencours = false;
                        return;


                    }

                }


                if (debug.Checked == true)
                {
                    Program.debugForm.tb.Text = DateTime.Now + " ##1" + Environment.NewLine + Program.debugForm.tb.Text;
                }


                this.Text = "FFXI Monitor (" + _FFACE.Player.Name + ")";

                if ((ffxi_status == "Started") | (ffxi_status == "Paused"))
                {
                    notifyIcon1.Text = "FFXI Monitor - " + _FFACE.Player.Name;
                    notifyIcon1.ShowBalloonTip(10000, "FFXI Monitor", "Welcome, " + _FFACE.Player.Name, ToolTipIcon.Info);
                    ffxi_status = "Running";
                    sleep(4000);
                    notifyIcon1.Visible = false;
                    notifyIcon1.Visible = true;
                }


                move();

                if (debug.Checked == true)
                {
                    Program.debugForm.tb.Text = DateTime.Now + " ##2" + Environment.NewLine + Program.debugForm.tb.Text;
                }

                if (total != total_precedent)
                {
                    foreach (Control c in Program.fourthform.Controls)
                    {
                        if (c is PictureBox)
                        {
                            if (((PictureBox)c).Name.Contains("pictureBox0"))
                            {
                                ((PictureBox)c).Load("./images/0/none.bmp");
                            }
                            if (((PictureBox)c).Name.Contains("pictureBox1"))
                            {
                                ((PictureBox)c).Load("./images/12/none.bmp");
                            }
                            if (((PictureBox)c).Name.Contains("pictureBox2"))
                            {
                                ((PictureBox)c).Load("./images/12/none.bmp");
                            }
                            if (((PictureBox)c).Name.Contains("pictureBoxM"))
                            {
                                ((PictureBox)c).Load("./images/0/none.bmp");
                            }
                        }
                    }

                    delete_jobs();
                    //add_jobs();

                    total = total_precedent;
                }


                if (debug.Checked == true)
                {
                    Program.debugForm.tb.Text = DateTime.Now + " ##3" + Environment.NewLine + Program.debugForm.tb.Text;
                }

                int pos = 0;
                int cur = 0;
                int total_tmp = 0;
                for (int i = 0; i < 18; i++)
                {

                    if (_FFACE.PartyMember[(byte)i].Active == true)
                    {

                        total_tmp = total_tmp + 1;

                        string name = _FFACE.PartyMember[(byte)i].Name.ToLower();
                        pos = cherche_pers(name);
                        if (pos == -1)
                        {

                            int j = 0;
                            foreach (string p in party_2)
                            {
                                if (p == null)
                                {
                                    break;
                                }
                                j = j + 1;
                            }

                            if (j == 150)
                            {
                                MessageBox.Show("warning : names limit (150/200)");
                            }

                            if (j == 198)
                            {
                                MessageBox.Show("names limit (200), please restart program before it crash");
                            }

                            party_2[j] = _FFACE.PartyMember[(byte)i].Name.ToLower();
                            pos = cherche_pers(name);
                        }

                        cur = cur + 1;

                        int flag = 0;
                        int idx = 0;


                        if ((cur <= _FFACE.Party.Party0Count) & (flag == 0))
                        {
                            idx = 6 - _FFACE.Party.Party0Count + cur - 1;
                            flag = 1;
                            if (((_FFACE.PartyMember[(byte)i].Name.ToLower() == _FFACE.Player.Name.ToLower()) & (main_character == "yes")) |
                                 (_FFACE.PartyMember[(byte)i].Name.ToLower() != _FFACE.Player.Name.ToLower()))
                            {
                                    doBDB(pos, y0pos, idx, "0/", "0");
                            }
                        }

                        if ((cur <= _FFACE.Party.Party0Count + _FFACE.Party.Party1Count) & (flag == 0))
                        {
                            idx = cur - _FFACE.Party.Party0Count - 1;
                            flag = 1;
                            if (limit == "yes")
                            {
                                doBDB(pos, y1pos, idx, "12/", "1");
                            }
                        }

                        if ((cur <= _FFACE.Party.Party0Count + _FFACE.Party.Party1Count + _FFACE.Party.Party2Count) & (flag == 0))
                        {
                            idx = cur - _FFACE.Party.Party1Count - _FFACE.Party.Party0Count - 1;
                            flag = 1;
                            if (limit == "yes")
                            {
                                doBDB(pos, y2pos, idx, "12/", "2");
                            }
                        }

                    }
                }


                total_precedent = total_tmp;
                if (total == 0)
                {
                    total = total_precedent;
                }

             
                if (npc_name != "")
                {
                    IntPtr hProc = IntPtr.Zero;
                    hProc = OpenProcess(ProcessAccessFlags.PROCESS_VM_READ, false, (uint)myProcesses.Id);
                    uint invPointer = 0;
                    int tmp = AsciiToInt(memloc_chatlog_lines);
                    uint loc = Convert.ToUInt32(FFXIBase + tmp);
                    ReadProcessMemory(hProc, loc, ref invPointer, 4, 0);

                    uint lines = 0;
                    ReadProcessMemory(hProc, invPointer + 43, ref lines, 2, 0);

                    if (debug.Checked == true)
                    {
                        Program.debugForm.tb.Text = DateTime.Now + " ##4 npc " + lines.ToString() + Environment.NewLine + Program.debugForm.tb.Text;
                    }

                    pos = cherche_pers(npc_name);

                    doBDB(pos, npc_y_pos, (int)lines, "12/", "npc");
                    }


                Boolean clean = true;
                if (_FFACE.Target.Name != "")
                {
                    if (debug.Checked == true)
                    {
                        Program.debugForm.tb.Text = DateTime.Now + " ##4 mob" + Environment.NewLine + Program.debugForm.tb.Text;
                    }

                    pos = cherche_mob(_FFACE.Target.Name.ToLower());
                    if (pos != -1)
                    {
                        clean = false;
                        //int[] tmp = new int[] { 0, 0, 0 };
                        doBDBM(pos, null, 1, "0/", "mob");         //just need a int[], ympos
                    }
                }
                if (clean == true)
                {

                    for (int j = 0; j < 30; j++)
                    {
                        foreach (Control c in Program.fourthform.Controls)
                        {
                            if (c is PictureBox)
                            {
                                if (((PictureBox)c).Name == "pictureBoxM-" + j.ToString())
                                {
                                    ((PictureBox)c).Load("./images/0/none.bmp");
                                }
                            }
                        }
                    }

                }











                if (debug.Checked == true)
                {
                    Program.debugForm.tb.Text = DateTime.Now + " ##5" + Environment.NewLine + Program.debugForm.tb.Text;
                }


                if (debug.Checked == true)
                {
                    foreach (Control c in Program.fourthform.Controls)
                    {
                        if (c is PictureBox)
                        {
                            if (((PictureBox)c).Name.Contains("pictureBoxA"))
                            {
                                ((PictureBox)c).Load("./images/debug/protect.bmp");
                            }
                            if (((PictureBox)c).Name.Contains("pictureBoxB"))
                            {
                                ((PictureBox)c).Load("./images/debug/shell.bmp");
                            }
                            if (((PictureBox)c).Name.Contains("pictureBoxC"))
                            {
                                ((PictureBox)c).Load("./images/debug/haste.bmp");
                            }
                        }
                    }
                }
                else
                {
                    foreach (Control c in Program.fourthform.Controls)
                    {
                        if (c is PictureBox)
                        {
                            if (((PictureBox)c).Name.Contains("pictureBoxA"))
                            {
                                ((PictureBox)c).Load("./images/debug/none.bmp");
                            }
                            if (((PictureBox)c).Name.Contains("pictureBoxB"))
                            {
                                ((PictureBox)c).Load("./images/debug/none.bmp");
                            }
                            if (((PictureBox)c).Name.Contains("pictureBoxC"))
                            {
                                ((PictureBox)c).Load("./images/debug/none.bmp");
                            }
                        }
                    }
                }

                t = DateTime.Now;

                for (int j = 0; j < 200; j++)
                {
                    if ((t > arrayB1[j].AddSeconds(durationsB[1])) & (arrayB1[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB1[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB2[j].AddSeconds(durationsB[2])) & (arrayB2[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB2[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB3[j].AddSeconds(durationsB[3])) & (arrayB3[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB3[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB4[j].AddSeconds(durationsB[4])) & (arrayB4[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB4[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB5[j].AddSeconds(durationsB[5])) & (arrayB5[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB5[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB6[j].AddSeconds(durationsB[6])) & (arrayB6[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB6[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB7[j].AddSeconds(durationsB[7])) & (arrayB7[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB7[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB8[j].AddSeconds(durationsB[8])) & (arrayB8[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB8[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB9[j].AddSeconds(durationsB[9])) & (arrayB9[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB9[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB10[j].AddSeconds(durationsB[10])) & (arrayB10[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB10[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB11[j].AddSeconds(durationsB[11])) & (arrayB11[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB11[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB12[j].AddSeconds(durationsB[12])) & (arrayB12[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB12[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB13[j].AddSeconds(durationsB[13])) & (arrayB13[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB13[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB14[j].AddSeconds(durationsB[14])) & (arrayB14[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB14[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB15[j].AddSeconds(durationsB[15])) & (arrayB15[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB15[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB16[j].AddSeconds(durationsB[16])) & (arrayB16[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB16[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB17[j].AddSeconds(durationsB[17])) & (arrayB17[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB17[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB18[j].AddSeconds(durationsB[18])) & (arrayB18[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB18[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB19[j].AddSeconds(durationsB[19])) & (arrayB19[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB19[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB20[j].AddSeconds(durationsB[20])) & (arrayB20[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB20[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB21[j].AddSeconds(durationsB[21])) & (arrayB21[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB1[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB22[j].AddSeconds(durationsB[22])) & (arrayB22[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB22[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB23[j].AddSeconds(durationsB[23])) & (arrayB23[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB23[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB24[j].AddSeconds(durationsB[24])) & (arrayB24[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB24[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB25[j].AddSeconds(durationsB[25])) & (arrayB25[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB25[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB26[j].AddSeconds(durationsB[26])) & (arrayB26[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB26[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB27[j].AddSeconds(durationsB[27])) & (arrayB27[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB27[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB28[j].AddSeconds(durationsB[28])) & (arrayB28[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB28[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB29[j].AddSeconds(durationsB[29])) & (arrayB29[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB29[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayB30[j].AddSeconds(durationsB[30])) & (arrayB30[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayB30[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB1[j].AddSeconds(durationsDB[1])) & (arrayDB1[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB1[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB2[j].AddSeconds(durationsDB[2])) & (arrayDB2[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB2[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB3[j].AddSeconds(durationsDB[3])) & (arrayDB3[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB3[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB4[j].AddSeconds(durationsDB[4])) & (arrayDB4[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB4[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB5[j].AddSeconds(durationsDB[5])) & (arrayDB5[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB5[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB6[j].AddSeconds(durationsDB[6])) & (arrayDB6[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB6[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB7[j].AddSeconds(durationsDB[7])) & (arrayDB7[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB7[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB8[j].AddSeconds(durationsDB[8])) & (arrayDB8[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB8[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB9[j].AddSeconds(durationsDB[9])) & (arrayDB9[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB9[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB10[j].AddSeconds(durationsDB[10])) & (arrayDB10[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB10[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB11[j].AddSeconds(durationsDB[11])) & (arrayDB11[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB11[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB12[j].AddSeconds(durationsDB[12])) & (arrayDB12[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB12[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB13[j].AddSeconds(durationsDB[13])) & (arrayDB13[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB13[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB14[j].AddSeconds(durationsDB[14])) & (arrayDB14[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB14[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB15[j].AddSeconds(durationsDB[15])) & (arrayDB15[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB15[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB16[j].AddSeconds(durationsDB[16])) & (arrayDB16[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB16[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB17[j].AddSeconds(durationsDB[17])) & (arrayDB17[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB17[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB18[j].AddSeconds(durationsDB[18])) & (arrayDB18[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB18[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB19[j].AddSeconds(durationsDB[19])) & (arrayDB19[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB19[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB20[j].AddSeconds(durationsDB[20])) & (arrayDB20[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB20[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB21[j].AddSeconds(durationsDB[21])) & (arrayDB21[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB21[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB22[j].AddSeconds(durationsDB[22])) & (arrayDB22[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB22[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB23[j].AddSeconds(durationsDB[23])) & (arrayDB23[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB23[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB24[j].AddSeconds(durationsDB[24])) & (arrayDB24[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB24[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB25[j].AddSeconds(durationsDB[25])) & (arrayDB25[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB25[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB26[j].AddSeconds(durationsDB[26])) & (arrayDB26[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB26[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB27[j].AddSeconds(durationsDB[27])) & (arrayDB27[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB27[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB28[j].AddSeconds(durationsDB[28])) & (arrayDB28[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB28[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB29[j].AddSeconds(durationsDB[29])) & (arrayDB29[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB29[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayDB30[j].AddSeconds(durationsDB[30])) & (arrayDB30[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayDB30[j] = new DateTime(1975, 1, 18);
                    }





                    if ((t > arrayMB1[j].AddSeconds(durationsB[1])) & (arrayMB1[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB1[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB2[j].AddSeconds(durationsB[2])) & (arrayMB2[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB2[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB3[j].AddSeconds(durationsB[3])) & (arrayMB3[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB3[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB4[j].AddSeconds(durationsB[4])) & (arrayMB4[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB4[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB5[j].AddSeconds(durationsB[5])) & (arrayMB5[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB5[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB6[j].AddSeconds(durationsB[6])) & (arrayMB6[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB6[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB7[j].AddSeconds(durationsB[7])) & (arrayMB7[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB7[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB8[j].AddSeconds(durationsB[8])) & (arrayMB8[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB8[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB9[j].AddSeconds(durationsB[9])) & (arrayMB9[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB9[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB10[j].AddSeconds(durationsB[10])) & (arrayMB10[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB10[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB11[j].AddSeconds(durationsB[11])) & (arrayMB11[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB11[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB12[j].AddSeconds(durationsB[12])) & (arrayMB12[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB12[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB13[j].AddSeconds(durationsB[13])) & (arrayMB13[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB13[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB14[j].AddSeconds(durationsB[14])) & (arrayMB14[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB14[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB15[j].AddSeconds(durationsB[15])) & (arrayMB15[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB15[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB16[j].AddSeconds(durationsB[16])) & (arrayMB16[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB16[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB17[j].AddSeconds(durationsB[17])) & (arrayMB17[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB17[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB18[j].AddSeconds(durationsB[18])) & (arrayMB18[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB18[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB19[j].AddSeconds(durationsB[19])) & (arrayMB19[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB19[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB20[j].AddSeconds(durationsB[20])) & (arrayMB20[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB20[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB21[j].AddSeconds(durationsB[21])) & (arrayMB21[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB1[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB22[j].AddSeconds(durationsB[22])) & (arrayMB22[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB22[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB23[j].AddSeconds(durationsB[23])) & (arrayMB23[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB23[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB24[j].AddSeconds(durationsB[24])) & (arrayMB24[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB24[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB25[j].AddSeconds(durationsB[25])) & (arrayMB25[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB25[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB26[j].AddSeconds(durationsB[26])) & (arrayMB26[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB26[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB27[j].AddSeconds(durationsB[27])) & (arrayMB27[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB27[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB28[j].AddSeconds(durationsB[28])) & (arrayMB28[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB28[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB29[j].AddSeconds(durationsB[29])) & (arrayMB29[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB29[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMB30[j].AddSeconds(durationsB[30])) & (arrayMB30[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMB30[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB1[j].AddSeconds(durationsDB[1])) & (arrayMDB1[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB1[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB2[j].AddSeconds(durationsDB[2])) & (arrayMDB2[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB2[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB3[j].AddSeconds(durationsDB[3])) & (arrayMDB3[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB3[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB4[j].AddSeconds(durationsDB[4])) & (arrayMDB4[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB4[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB5[j].AddSeconds(durationsDB[5])) & (arrayMDB5[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB5[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB6[j].AddSeconds(durationsDB[6])) & (arrayMDB6[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB6[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB7[j].AddSeconds(durationsDB[7])) & (arrayMDB7[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB7[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB8[j].AddSeconds(durationsDB[8])) & (arrayMDB8[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB8[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB9[j].AddSeconds(durationsDB[9])) & (arrayMDB9[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB9[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB10[j].AddSeconds(durationsDB[10])) & (arrayMDB10[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB10[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB11[j].AddSeconds(durationsDB[11])) & (arrayMDB11[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB11[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB12[j].AddSeconds(durationsDB[12])) & (arrayMDB12[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB12[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB13[j].AddSeconds(durationsDB[13])) & (arrayMDB13[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB13[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB14[j].AddSeconds(durationsDB[14])) & (arrayMDB14[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB14[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB15[j].AddSeconds(durationsDB[15])) & (arrayMDB15[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB15[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB16[j].AddSeconds(durationsDB[16])) & (arrayMDB16[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB16[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB17[j].AddSeconds(durationsDB[17])) & (arrayMDB17[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB17[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB18[j].AddSeconds(durationsDB[18])) & (arrayMDB18[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB18[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB19[j].AddSeconds(durationsDB[19])) & (arrayMDB19[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB19[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB20[j].AddSeconds(durationsDB[20])) & (arrayMDB20[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB20[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB21[j].AddSeconds(durationsDB[21])) & (arrayMDB21[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB21[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB22[j].AddSeconds(durationsDB[22])) & (arrayMDB22[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB22[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB23[j].AddSeconds(durationsDB[23])) & (arrayMDB23[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB23[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB24[j].AddSeconds(durationsDB[24])) & (arrayMDB24[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB24[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB25[j].AddSeconds(durationsDB[25])) & (arrayMDB25[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB25[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB26[j].AddSeconds(durationsDB[26])) & (arrayMDB26[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB26[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB27[j].AddSeconds(durationsDB[27])) & (arrayMDB27[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB27[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB28[j].AddSeconds(durationsDB[28])) & (arrayMDB28[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB28[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB29[j].AddSeconds(durationsDB[29])) & (arrayMDB29[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB29[j] = new DateTime(1975, 1, 18);
                    }
                    if ((t > arrayMDB30[j].AddSeconds(durationsDB[30])) & (arrayMDB30[j] != new DateTime(1975, 1, 18)))
                    {
                        arrayMDB30[j] = new DateTime(1975, 1, 18);
                    }



                }         //

            }
            catch (Exception e)
            {
                if (debug.Checked == true)
                {
                    Program.debugForm.tb.Text = DateTime.Now + " error in update function" + Environment.NewLine + Program.debugForm.tb.Text;
                }
            }

            updateencours = false;

        }         //end private string update()



        private int AsciiToInt(string val)
        {
            int ret = 0;

            int numBase = val.StartsWith("0x") ? 16 : 10;
            int i = val.Length - 1;
            int multiplier = 1;

            while (val[i] != 'x' & i >= 0)
            {
                int digit;

                if (val[i] >= 'A' & val[i] <= 'F')
                {
                    digit = val[i] + 0xa - 'A';
                }
                else if (val[i] >= 'f' & val[i] <= 'f')
                {
                    digit = val[i] + 0xa - 'a';
                }
                else if (val[i] >= '0' & val[i] <= '9')
                {
                    digit = val[i] - '0';
                }
                else
                {
                    /* this is an error case unless more bases 
                     * are supported later. */
                    ret = -1;
                    break;
                }

                ret += digit * multiplier;
                multiplier *= numBase;
                i--;
            }

            return ret;
        }



        private void timer1_Tick(object sender, EventArgs e)
        {

            if (updateencours == false)
            {
                update();
            }

        }



        private void button7_Click(object sender, EventArgs e)
        {
            if (button7.Text == "Add jobs")
            {
                add_jobs();
                button7.Text = "Delete jobs";
            }
            else
            {
                delete_jobs();
                button7.Text = "Add jobs";
            }
        }

        private void delete_jobs()
        {
            foreach (Control c in Program.fourthform.Controls)
            {
                if ((c is PictureBox) & (((PictureBox)c).Name.Contains("PictureBoxJob-")))
                {
                    ((PictureBox)c).Dispose();
                }
            }

            for (int i = 0; i < 19; i++)
            {
                jobs[i] = "";
            }

        }



        private void add_jobs()
        {

            for (int i = -1; i < _FFACE.Search.TotalCount - 1; i++)
            {
                int pos2 = cherche_pers(_FFACE.Search.Name((short)i).ToLower());
                if (pos2 != -1)
                {
                    jobs[pos2] = _FFACE.Search.MainJob((short)i).ToString();
                }
            }

            int pos = 0;
            int cur = 0;
            for (int i = 0; i < 18; i++)
            {

                if (_FFACE.PartyMember[(byte)i].Active == true)
                {

                    string name = _FFACE.PartyMember[(byte)i].Name.ToLower();
                    pos = cherche_pers(name);

                    if (pos != -1)
                    {
                        cur = cur + 1;

                        int flag = 0;
                        int idx = 0;

                        if ((cur <= _FFACE.Party.Party0Count) & (flag == 0) & (jobs[pos] != ""))
                        {
                            idx = 6 - _FFACE.Party.Party0Count + cur - 1;
                            flag = 1;
                            do_jobs(pos, y0pos[idx], "0");
                        }

                        if ((cur <= _FFACE.Party.Party0Count + _FFACE.Party.Party1Count) & (flag == 0) & (jobs[pos] != ""))
                        {
                            idx = cur - _FFACE.Party.Party0Count - 1;
                            flag = 1;
                            if (limit == "yes")
                            {
                                do_jobs(pos, y1pos[idx], "1");
                            }
                        }

                        if ((cur <= _FFACE.Party.Party0Count + _FFACE.Party.Party1Count + _FFACE.Party.Party2Count) & (flag == 0) & (jobs[pos] != ""))
                        {
                            idx = cur - _FFACE.Party.Party1Count - _FFACE.Party.Party0Count - 1;
                            flag = 1;
                            if (limit == "yes")
                            {
                                do_jobs(pos, y2pos[idx], "2");
                            }
                        }

                    }
                }
            }
        }

        private void do_jobs(int pos, int y, string p2)
        {
            int d = 0;
            int size = icon_size0;
            if ((p2 == "1") | (p2 == "2"))
            {
                size = icon_size12;
                d = 7;
            }

            PictureBox pict = null;

            pict = new PictureBox();
            pict.Location = new System.Drawing.Point(Program.fourthform.Width - 30 + d, y);
            pict.Name = "PictureBoxJob-" + y.ToString();
            pict.Size = new System.Drawing.Size(size, size);
            pict.Load("./images/jobs/" + jobs[pos] + ".png");
            pict.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            Program.fourthform.Controls.Add(pict);
            pict.BringToFront();

        }

        private void Form_FFXI_Monitor_Resize(object sender, EventArgs e)
        {

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
            }
            else
            {
                this.ShowInTaskbar = true;
            }

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                //this.TopMost = true;
                //sleep(1000);
                //this.TopMost = false;
                //this.Focus();
                //this.BringToFront();
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
            }

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void loadArFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                lectureFichierAR(openFileDialog1.FileName.ToString());
            }


        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            program_exit();
        }


        public static void sleep(int val)
        {
            val = val / 1000;

            DateTime x = DateTime.Now;
            x = x.AddSeconds(val);

            while (DateTime.Now < x)
            {
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(10);
                System.Windows.Forms.Application.DoEvents();
            }

        }







    }
}