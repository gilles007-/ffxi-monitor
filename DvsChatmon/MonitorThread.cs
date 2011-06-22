using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.IO;


namespace DvsChatmon
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct ChatLogBasicInfoStruct
    {
        [FieldOffset(0)]
        public byte NumberOfLines;
        [FieldOffset(4)]
        public IntPtr NewChatLogPtr;
        [FieldOffset(8)]
        public IntPtr OldChatLogPtr;
        [FieldOffset(12)]
        public int ChatLogBytes;
        [FieldOffset(16)]
        public short FinalOffset;
    }

    internal class ChatLogDetails
    {
        public short[] OldLogOffsets = new short[50];
        public short[] NewLogOffsets = new short[50];

        public ChatLogBasicInfoStruct Info;

        public override bool Equals(object obj)
        {
            ChatLogDetails rhs = obj as ChatLogDetails;
            if (rhs == null)
                return false;
            if (Info.NumberOfLines != rhs.Info.NumberOfLines)
                return false;
            if (Info.NewChatLogPtr != rhs.Info.NewChatLogPtr)
                return false;
            if (Info.OldChatLogPtr != rhs.Info.OldChatLogPtr)
                return false;
            if (Info.FinalOffset != rhs.Info.FinalOffset)
                return false;
            if (Info.ChatLogBytes != rhs.Info.ChatLogBytes)
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            return Info.NewChatLogPtr.GetHashCode() ^ Info.OldChatLogPtr.GetHashCode();
        }
    }

    internal class ChatLogMetaInfo
    {
        public IntPtr PointerToLineOffsetArray;
    }

    public class ChatLineInfo
    {
        public DateTime Time;
        public string HeaderString;

        public byte[] Header = null;
        public byte[] Center = null;
        public byte[] Footer = null;

        public byte ChatCode = 0;
        public string Text = null;
        public uint LineNumber = 0;
        public uint ConversationThread = 0;
    }

    public class MonitorThread
    {
        //private static readonly int FFXiMainStaticOffset = 0x4C34D0;
        //private static readonly int FFXiMainStaticOffset = 0x4BB1D8;
        //private static readonly int FFXiMainStaticOffset = 0x4C1B10;
        //private static readonly int FFXiMainStaticOffset = 0x4C1A90;
        //private static readonly int FFXiMainStaticOffset = 0x557A90;
        //private static readonly int FFXiMainStaticOffset = 0x55AEC8;
        //private static readonly int FFXiMainStaticOffset = 0x563098;



        // from kparser
        // Base address before patch on 2008-03-10: 0x0056A788
        // Base address after patch  on 2008-03-10: 0x0056DA48
        // Base address after update on 2008-06-09: 0x00575968
        // Base address after update on 2008-09-08: 0x00576D58
        // Base address after update on 2008-12-08: 0x0057A2C8
        // Base address after update on 2009-04-08: 0x0057d768
        // Base address after patch  on 2009-04-22: 0x0057d7e8
        // Base address after update on 2009-07-20: 0x0057da98
        // Base address after update on 2009-11-09: 0x005801d8
        // Base address after update on 2010-03-22: 0x00581518
        // Base address after update on 2010-06-21: 0x005827d8
        // Base address after update on 2010-09-08: 0x00582958
        // Base address after update on 2010-12-06: 0x00583948
        // Base address after update on 2011-02-14: 0x005839C8
        // Base address after update on 2011-05-xx: 0x00584E68


        //par exemple, quand c'est décalé de 4 bytes
        //on passe de 0x583948 à 0x5839C8 (5781832 -> 5781960 en dec)
        //différence de 128 en dec, 128/4=32, donc 1 byte décalerait de 32 en dec
        //différence de 80 en dec, 80/4=20, donc 1 byte décalerait de 20 en hex




        private static int FFXiMainStaticOffset = 0x0;
        private static readonly int ChatLogHeaderLength = 53;

        private bool _Monitoring = true;
        private bool _Shutdown = false;
        private object _MonitorLock = new object();

        public delegate void LinesIncomingDelegate(ChatLineInfo[] NewLines);

        public event LinesIncomingDelegate OnLinesIncoming;

        private class PolProcessInfo
        {
            private Process _PolProcess;
            private IntPtr _BaseAddress;

            public IntPtr Handle { get { return _PolProcess.Handle; } }
            public IntPtr BaseAddress { get { return _BaseAddress; } }

            public PolProcessInfo(Process Proc, IntPtr BaseAddress)
            {
                _PolProcess = Proc;
                _BaseAddress = BaseAddress;
            }
        }
        private PolProcessInfo _PolProcess = null;

        public MonitorThread()
        {
        }

        private bool IsMonitoring
        {
            get { lock (_MonitorLock) { return _Monitoring; } }
            set { lock (_MonitorLock) { _Monitoring = value; } }
        }

        private bool Shutdown
        {
            set { lock (_MonitorLock) { _Shutdown = true; } }
            get { lock (_MonitorLock) { return _Shutdown; } }
        }

        string id = "";
        string memloc = "";

        public void Start(string param1, string param2)
        {

            Trace.WriteLine("# " + id.ToString() + memloc.ToString());
            id = param1;
            memloc = param2;

            System.Threading.Thread ThreadMonitor = new System.Threading.Thread(ThreadStart);
            ThreadMonitor.Priority = ThreadPriority.Normal;
            ThreadMonitor.Start();
        }

        private IntPtr IncrementPtr(IntPtr Ptr, int NumBytes)
        {
            return (IntPtr)((int)Ptr + NumBytes);
        }

        private IntPtr FollowPointer(IntPtr PointerToFollow)
        {
            IntPtr BufferForPointerValue = PInvoke.ReadProcessMemory(_PolProcess.Handle, PointerToFollow, (uint)IntPtr.Size);
            if (BufferForPointerValue == IntPtr.Zero)
                return IntPtr.Zero;

            try
            {
                IntPtr PointerValue = Marshal.ReadIntPtr(BufferForPointerValue);
                return PointerValue;
            }
            finally
            {
                Marshal.FreeHGlobal(BufferForPointerValue);
            }
        }

        private string[] ReadLines(IntPtr BufferStart, int MaxLinesToRead, int BufferSize)
        {
            if (MaxLinesToRead == 0)
                return new string[] { };
            MaxLinesToRead = System.Math.Min(MaxLinesToRead, 50);

            IntPtr LinesBuffer = IntPtr.Zero;
            try
            {
                LinesBuffer = PInvoke.ReadProcessMemory(_PolProcess.Handle, BufferStart, (uint)BufferSize);
                if (LinesBuffer == IntPtr.Zero)
                {
                    Trace.WriteLine(String.Format(Thread.CurrentThread.Name + ": WARNING: ReadProcessMemory returned 0 with error code {0}.", Marshal.GetLastWin32Error()));
                    return new string[] { };
                }
                string NullDelimintedChatLines = Marshal.PtrToStringAnsi(LinesBuffer, (int)BufferSize);

                //Read 1 extra line so that it throws the rest of the crap onto a new line, so we can easily
                //chop it off.
                string[] AllLinesPlusOne = NullDelimintedChatLines.Split(new char[] { '\0' }, MaxLinesToRead + 1);
                string[] AllLines = new string[MaxLinesToRead];
                Array.ConstrainedCopy(AllLinesPlusOne, 0, AllLines, 0, MaxLinesToRead);
                return AllLines;
            }
            finally
            {
                if (LinesBuffer != IntPtr.Zero)
                    Marshal.FreeHGlobal(LinesBuffer);
            }
        }

        private int GetLineEndingOffset(ChatLogDetails CurrentDetails, uint LineIndex)
        {
            //If this is the very last line index (i.e. the chat log JUST filled up)
            //make sure we don't step over the bounds.  Instead just set the next
            //line offset to one byte after the end of the chat log.
            if (LineIndex == CurrentDetails.Info.NumberOfLines - 1)
                return (int)CurrentDetails.Info.FinalOffset;
            else
                return (int)CurrentDetails.NewLogOffsets[LineIndex + 1];
        }

        private string TrimSpecialCharacters(string Text, out byte[] Header, out byte[] Footer)
        {
            Header = Footer = null;
            if (Text.Length < 2)
                return Text;
            if ((Text[0] == '\x1F' && (Text[1] == '\x8A' || Text[1] == '\xD0' || Text[1] == '\x79' || Text[1] == '\x7F' || Text[1] == '\x7B' || Text[1] == 'ˆ')) || (Text[0] == '\x1E' && Text[1] == '\x01'))
            {
                Header = new byte[] { (byte)Text[0], (byte)Text[1] };
                Text = Text.Substring(2);
            }
            int Last = Text.Length - 1;
            int NextToLast = Text.Length - 2;
            if (Text[NextToLast] == '\x7F' && Text[Last] == '\x31')
            {
                Footer = new byte[] { (byte)Text[NextToLast], (byte)Text[Last] };
                Text = Text.Substring(0, NextToLast);
            }
            return Text;
        }

        private ChatLineInfo StringToChatLineInfo(string Line)
        {
            ChatLineInfo Info = new ChatLineInfo();

            string CenterEncloser = Line.Substring(ChatLogHeaderLength, 2);
            int SecondIndex = Line.IndexOf(CenterEncloser, ChatLogHeaderLength + 2);
            Info.HeaderString = Line.Substring(0, ChatLogHeaderLength);
            Info.Center = Array.ConvertAll<char, byte>(Line.Substring(ChatLogHeaderLength, SecondIndex + 2 - ChatLogHeaderLength).ToCharArray(), delegate(char c) { return (byte)c; });            
            Info.Text = TrimSpecialCharacters(Line.Substring(SecondIndex + 2), out Info.Header, out Info.Footer);            
            Info.Text = AdjustInputEncoding(Info.Text);

            Info.Time = DateTime.Now;
            Info.ChatCode = byte.Parse(Info.HeaderString.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            Info.ConversationThread = uint.Parse(Info.HeaderString.Substring(18, 8), System.Globalization.NumberStyles.AllowHexSpecifier);
            Info.LineNumber = uint.Parse(Info.HeaderString.Substring(27, 8), System.Globalization.NumberStyles.AllowHexSpecifier);
            return Info;
        }

        private string AdjustInputEncoding(string Text)
        {
            byte[] OriginalBytes = UnicodeEncoding.Default.GetBytes(Text);
            //Windower Timestamp v2.0 detection
            //If first two bytes are unicode values 30 then 250, shift by 15 to get the correct input
            // In Timestamp v2.1 the first 2 bytes are unicode 30 and 252
            if (OriginalBytes[0] == 30 && (OriginalBytes[1] == 250 || OriginalBytes[1] == 252))
            {
                Text = Text.Substring(15);  
                // Special characters are not removed by TrimSpecialCharacters() with Timestamp enabled, remove them here
                // TrimSpecialCharacters() has 7 cases, however, only the 5 cases that I've seen in game are below
                if (Text[0] == '\x1F' && (Text[1] == 'y' || Text[1] == '{' || Text[1] == '\x7F' || Text[1] == '\xD0' || Text[1] == 'ˆ'))
                    Text = Text.Substring(2);              
                
                OriginalBytes = UnicodeEncoding.Default.GetBytes(Text);
            }

            // With Japanese language set for non-Unicode programs, the first two bytes are 30 and 129
            if (OriginalBytes[0] == 30 && (OriginalBytes[1] == 129))
            {
                Text = Text.Substring(14);

                // Special characters are not removed by TrimSpecialCharacters() with Timestamp enabled, remove them here
                // TrimSpecialCharacters() has 7 cases, however, only the 5 cases that I've seen in game are below
                if (Text[0] == '\x1F' && (Text[1] == 'y' || Text[1] == '{' || Text[1] == '\x7F' || Text[1] == '\xD0' || Text[1] == 'ˆ'))
                    Text = Text.Substring(2);

                OriginalBytes = UnicodeEncoding.Default.GetBytes(Text);
            }
            byte[] RecodedBytes = Encoding.Convert(Encoding.GetEncoding("Shift-JIS"), Encoding.Unicode, OriginalBytes);
            return Encoding.Unicode.GetString(RecodedBytes).Trim();
        }

        private void ThreadStart()
        {
            uint HighestLineProcessed = 0;

            Thread.CurrentThread.Name = "DvsChatmon_" + Thread.CurrentThread.GetHashCode().ToString();

            Trace.WriteLine(Thread.CurrentThread.Name + ": Monitor thread started.");

            ChatLogMetaInfo MetaInfo = null;
            try
            {
                _PolProcess = FindPolProcess();
                if (_PolProcess == null)
                {
                    Trace.WriteLine(Thread.CurrentThread.Name + ": An error occured while searcing for the Found Final Fantasy XI Process!");
                    return;
                }
                Trace.WriteLine(Thread.CurrentThread.Name + ": Success attaching to Final Fantasy XI process!");

                //Find the address of chat log meta-structures in the memory space of the FFXI process.
                MetaInfo = ReadMetaInfo();
            }
            catch (Exception e)
            {
                Trace.WriteLine(String.Format(Thread.CurrentThread.Name + ": ERROR: Exception encountered while connecting to Final Fantasy XI chat log.  Exception={0}", e.ToString()));
            }

            Trace.WriteLine(Thread.CurrentThread.Name + ": Monitoring...");
            ChatLogDetails OldDetails = null;
            bool Successful = true;
            while (!Shutdown)
            {
                IntPtr NewChatLogStart = IntPtr.Zero;
                try
                {
                    if (!IsMonitoring)
                        continue;

                    //Fetch details such as how many lines are in the chat log, pointers to
                    //the memory containing the actual text, etc.  If every single field is
                    //the same as it was the last time we checked, we use an optimization which
                    //basically "assumes" that the chat log hasn't changed.  It's an assumption
                    //because it's theoretically possible you could have exactly the same number 
                    //of lines, number of bytes, etc but have had lines change.  e.g. if it got
                    //full and cycled around.  But it's so rare, and even when it does happen we
                    //can just catch the lines the next time around.  It's far more common
                    //that the chat log actually had no new lines added.
                    ChatLogDetails CurrentDetails = ReadChatLogDetails(MetaInfo);
                    if (CurrentDetails.Equals(OldDetails))
                        continue;

                    //If there are zero lines in the NEW chat log, they are not logged into
                    //the game (e.g. at the character selection screen).  The log doesn't fill
                    //up and get copied over to the old log until there is one too many lines.  
                    //And then this log will still contain 1 line, the new one.  Obviously let's
                    //not do anything if they aren't logged in ;-)
                    if (CurrentDetails.Info.NumberOfLines <= 0 || CurrentDetails.Info.NumberOfLines > 50)
                    {
                        HighestLineProcessed = 0;
                        OldDetails = null;
                        continue;
                    }

                    //Read the entire NEW chat log from memory, start to finish.
                    NewChatLogStart = PInvoke.ReadProcessMemory(_PolProcess.Handle, CurrentDetails.Info.NewChatLogPtr, (uint)CurrentDetails.Info.FinalOffset);
                    if (NewChatLogStart == IntPtr.Zero)
                    {
                        Trace.WriteLine(String.Format(Thread.CurrentThread.Name + ": ERROR: Unable to read Chat Log start from memory.  Error Code = {0}", Marshal.GetLastWin32Error()));
                        continue;
                    }

                    //Parse the first line by hand, so we can get the line number to see where we are
                    //with respect to where the last line we processed was.
                    int FirstOffset = 0;
                    int SecondOffset = GetLineEndingOffset(CurrentDetails, 0);
                    int LineBytes = SecondOffset - FirstOffset;
                    string[] AllMissedLines = null;
                    string[] AllNewLines = ReadLines(CurrentDetails.Info.NewChatLogPtr, CurrentDetails.Info.NumberOfLines, CurrentDetails.Info.FinalOffset);
                    uint FirstLineNumber = uint.Parse(AllNewLines[0].Substring(27, 8), System.Globalization.NumberStyles.AllowHexSpecifier);
                    if (OldDetails == null)
                    {
                        //This is our first pass through the loop (i.e. parser just started)
                        //Set our counter to the last line currently in memory, so that next
                        //time through the loop, we start with the first line that wasn't there
                        //when the parser started.
                        OldDetails = CurrentDetails;
                        HighestLineProcessed = FirstLineNumber + (uint)CurrentDetails.Info.NumberOfLines - 1;
                        continue;
                    }

                    string[] LinesToProcess = null;
                    //It's not our first pass through the loop.  Check if lines were missed
                    int DistanceSincePreviousLine = (int)(FirstLineNumber - HighestLineProcessed);
                    if (DistanceSincePreviousLine > 1)
                    {
                        //If we're here, that means lines were missed, and we need to get 
                        //them from the OLD log.
                        uint NumberOfLinesMissed = (uint)System.Math.Max(0, (int)(FirstLineNumber - HighestLineProcessed - 1));

                        //Since each chat log only holds a max of 50 lines, this is how it's done.
                        uint IndexOfFirstMissedLine = (uint)System.Math.Max(0, (int)(50 - NumberOfLinesMissed));
                        IntPtr StartOfFirstMissedLine = IncrementPtr(CurrentDetails.Info.OldChatLogPtr, CurrentDetails.OldLogOffsets[IndexOfFirstMissedLine]);
                        uint NumberOfBytesUntilStartOfLastMissedLine = (uint)(CurrentDetails.OldLogOffsets[49] - CurrentDetails.OldLogOffsets[IndexOfFirstMissedLine]);

                        //There is a field "FinalOffset" in the main Chat log meta struct that tells
                        //us where the last byte of actual chat log text is for the NEW log.  Unfortunately
                        //there is no analogous field for the OLD log.  Because of this, the best
                        //we can do is overestimate.  This has the nasty side effect of leading to 
                        //rare ReadProcessMemory() errors where we attempt to read past the end
                        //of a memory region, and into a block with different protection settings.
                        //Fortunately, even though ReadProcessMemory() returns an error in this case
                        //we can sleep well knowing we got everything we needed.
                        uint TotalBytesToRead = NumberOfBytesUntilStartOfLastMissedLine + 250;
                        AllMissedLines = ReadLines(StartOfFirstMissedLine, (int)NumberOfLinesMissed, (int)TotalBytesToRead);
                        //We know we missed lines or we wouldn't be here.  On the other hand, when 
                        //we tried to read them we got back nothing.  Log a warning and continue.
                        if (AllMissedLines.Length == 0)
                            Trace.WriteLine(String.Format(Thread.CurrentThread.Name + ": Chat monitor missed line(s) [{0}, {1}]", IndexOfFirstMissedLine, IndexOfFirstMissedLine + NumberOfLinesMissed));

                        //Make an array containing all missed lines and all new lines, which we'll
                        //process later on.
                        LinesToProcess = new string[AllMissedLines.Length + CurrentDetails.Info.NumberOfLines];
                        Array.ConstrainedCopy(AllMissedLines, 0, LinesToProcess, 0, AllMissedLines.Length);
                        Array.ConstrainedCopy(AllNewLines, 0, LinesToProcess, AllMissedLines.Length, AllNewLines.Length);
                    }
                    else
                    {
                        //Some of the items in the array we processed last time.  So the only ones we want to 
                        //copy to the array of items to process are ones with a line number higher
                        //that the highest line number we've seen.
                        uint FirstLineIndexForBatchProcess = HighestLineProcessed - FirstLineNumber + 1;
                        int NumActualNewLines = (int)(CurrentDetails.Info.NumberOfLines - FirstLineIndexForBatchProcess);
                        LinesToProcess = new string[NumActualNewLines];
                        Array.ConstrainedCopy(AllNewLines, (int)FirstLineIndexForBatchProcess, LinesToProcess, 0, NumActualNewLines);
                    }

                    List<ChatLineInfo> ChatLines = new List<ChatLineInfo>();
                    foreach (string Line in LinesToProcess)
                    {
                        try
                        {
                            ChatLineInfo ProcessedLine = StringToChatLineInfo(Line);
                            ChatLines.Add(ProcessedLine);
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(String.Format("ERROR - An Exception occured while processing a chat line.  Line={0}, Message={1}", (Line == null) ? String.Empty : Line, e.Message));
                            HighestLineProcessed++;
                        }
                    }

                    HighestLineProcessed = ChatLines[ChatLines.Count - 1].LineNumber;

                    //Notify whoever is listening that we detected new lines.
                    if (OnLinesIncoming != null && (OldDetails != null))
                        OnLinesIncoming(ChatLines.ToArray());

                    OldDetails = CurrentDetails;
                    Successful = true;
                }
                catch (Exception e)
                {
                    if (Successful)
                        Trace.WriteLine(String.Format(Thread.CurrentThread.Name + ": ERROR: Exception encountered while processing lines from chat log.  Exception={0}", e.ToString()));
                    Successful = false;
                }
                finally
                {
                    if (NewChatLogStart != IntPtr.Zero)
                        Marshal.FreeHGlobal(NewChatLogStart);
                    //Make sure that there is a .7 second pause between every scan of the
                    //chat log.  This ensures CPU usage is kept to a minimum, if we put this
                    //in a tight loop the system would get hammered.
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        private int AsciiToInt(string val)
        {
            int ret = 0;

            int numBase = val.StartsWith("0x") ? 16 : 10;
            int i = val.Length - 1;
            int multiplier = 1;

            while (val[i] != 'x' && i >= 0)
            {
                int digit;

                if (val[i] >= 'A' && val[i] <= 'F')
                {
                    digit = val[i] + 0xa - 'A';
                }
                else if (val[i] >= 'f' && val[i] <= 'f')
                {
                    digit = val[i] + 0xa - 'a';
                }
                else if (val[i] >= '0' && val[i] <= '9')
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

        private ChatLogMetaInfo ReadMetaInfo()
        {
            while (true)
            {
                Trace.WriteLine("Attempting to read chatlog meta info...");

                //Set the FFXiMainStaticOffset from the value found in file memloc.txt
                //FFXiMainStaticOffset = ReadMemLocFile();
                FFXiMainStaticOffset = AsciiToInt(memloc);

                //FFXIMainStaticOffset is the offset to the address that contains the
                //first pointer in the hierarchy of FFXI's chat log data structures.  Obviously,
                //it is an offset from the base address that FFXIMain.dll is loaded at.
                IntPtr RootAddress = IncrementPtr(_PolProcess.BaseAddress, FFXiMainStaticOffset);

                //Follow that pointer inside the address space of the FFXI process and return
                //the pointer that it points to.
                Trace.WriteLine(Thread.CurrentThread.Name + ": Dereferencing chat pointer.");
                IntPtr FirstDereference = FollowPointer(RootAddress);


                if (FirstDereference == IntPtr.Zero)
                {
                    Trace.WriteLine("Error dereferencing first pointer.");
                    System.Threading.Thread.Sleep(700);
                    continue;
                }

                //This is just the way it is (discovered through trial and error).  4 bytes from
                //where the first pointer takes us is where the second pointer of interest lives.
                IntPtr OffsetFromFirstDereference = IncrementPtr(FirstDereference, 4);

                //Follow the second pointer inside the address space of the FFXI process.
                Trace.WriteLine(Thread.CurrentThread.Name + ": Dereferencing chat pointer.");
                IntPtr SecondDereference = FollowPointer(OffsetFromFirstDereference);
                if (SecondDereference == IntPtr.Zero)
                {
                    Trace.WriteLine("Error dereferencing second pointer.");
                    System.Threading.Thread.Sleep(700);
                    continue;
                }

                ChatLogMetaInfo MetaInfo = new ChatLogMetaInfo();

                //Finally, we've arrived at the address of the "line offsets arrays".  
                //Save this, as we'll read the Line Offsets arrays later, and also use it
                //to get to other interesting chat log related information.
                MetaInfo.PointerToLineOffsetArray = SecondDereference;

                return MetaInfo;
            }
        }

        private void _PolProcess_Exited(object sender, EventArgs e)
        {
            _PolProcess = null;

            PauseMonitoring();
        }

        private ChatLogDetails ReadChatLogDetails(ChatLogMetaInfo MetaInfo)
        {
            IntPtr LineOffsetsBuffer = IntPtr.Zero;
            try
            {
                uint TotalBytes = (uint)(200 + Marshal.SizeOf(typeof(ChatLogBasicInfoStruct)));
                ChatLogDetails Details = new ChatLogDetails();
                LineOffsetsBuffer = PInvoke.ReadProcessMemory(_PolProcess.Handle, MetaInfo.PointerToLineOffsetArray, TotalBytes);
                Marshal.Copy(LineOffsetsBuffer, Details.NewLogOffsets, 0, 50);
                Marshal.Copy(IncrementPtr(LineOffsetsBuffer, 100), Details.OldLogOffsets, 0, 50);
                Details.Info = (ChatLogBasicInfoStruct)Marshal.PtrToStructure(IncrementPtr(LineOffsetsBuffer, 200), typeof(ChatLogBasicInfoStruct));
                return Details;
            }
            finally
            {
                if (LineOffsetsBuffer != IntPtr.Zero)
                    Marshal.FreeHGlobal(LineOffsetsBuffer);
            }
        }

        private PolProcessInfo FindPolProcess()
        {
            while (true)
            {
                try
                {
                    Trace.WriteLine(Thread.CurrentThread.Name + ": Attempting to connect to Final Fantasy.");

                    Process ProcessObject = Process.GetProcessById(int.Parse(id));
                    for (int i = 0; i < ProcessObject.Modules.Count; ++i)
                    {
                        ProcessModule Module = ProcessObject.Modules[i];
                        if (!Module.ModuleName.Equals("ffximain.dll", StringComparison.CurrentCultureIgnoreCase))
                            continue;

                        Trace.WriteLine(String.Format(Thread.CurrentThread.Name + ": Module {0} Base Address = {1}", Module.ModuleName, Module.BaseAddress.ToString("X")));
                        ProcessObject.Exited += new EventHandler(_PolProcess_Exited);
                        return new PolProcessInfo(ProcessObject, Module.BaseAddress);
                    }

                    System.Threading.Thread.Sleep(5000);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(String.Format(Thread.CurrentThread.Name + ": ERROR: An exception occured while trying to connect to Final Fantasy.  Message = {0}", e.Message));
                    System.Threading.Thread.Sleep(5000);
                }
            }
        }

        public void PauseMonitoring()
        {
            IsMonitoring = false;
        }

        public void ResumeMonitoring()
        {
            IsMonitoring = true;
        }

        public void ShutdownThread()
        {
            PauseMonitoring();
            Shutdown = true;
        }
    }
}
