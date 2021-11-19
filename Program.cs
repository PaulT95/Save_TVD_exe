﻿
using System;
using System.Diagnostics;
using System.Text;
using System.ComponentModel; //these last two are needed for pass string via CMD
using System.Collections;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Runtime.InteropServices;

// See https://aka.ms/new-console-template for more information

string filename = ""; //initialize filename as empty

//check if you insert a Line Argument after calling the .exe file
if (Environment.GetCommandLineArgs()[1] != null)
{
    filename = Environment.GetCommandLineArgs()[1]; //get argument one not zero because it corresponds to EcoSave.dll
}
//Console.WriteLine(filename);

// Create an EcoObj based on EchoWII class created below
EchoWII EcoObj = new EchoWII();

// Find windows of EchoWave II and return as a string
string EchoWins = EcoObj.FindWindows();

Console.WriteLine("EchoWave II instances open: " + EchoWins);
//Console.ReadLine(); //wait until someone press a button or click

//if (EchoWins != "0")
//    EcoObj.SendSaveTVDCommand(filename);
//else
//    Console.WriteLine("Not any Echo Wave II instances open!");
//    Console.ReadLine();

EcoObj.SendSaveTVDCommand(filename); 


//send it to "flush" the memory/cache because with frame line in
// even if it save the current video saved, it saves bigger and bigger files (probably in background is saved). 

EcoObj.SendFreezeRunCommand(); // Un armed
EcoObj.SendFreezeRunCommand(); // armed for recording via FrameLine In

Console.WriteLine("Sent the command to flush memory");
Thread.Sleep(2000); //just time for reading the sentence

// Create a Class EchoWII with DLL needed
/// <summary>
/// Class to connect and detect windows, a method SendSaveTVDCommand
/// Will save a file with the filename specified via CMD
/// Too long to explain everything, but basically it will convert the
/// command based on ASCII and based on the WMcopy of Telemed command
/// you cand find it in user manual command line
/// </summary>
public partial class EchoWII
{
    delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]

    static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]

    static extern int GetWindowTextLength(IntPtr hWnd);
    [DllImport("user32.dll")]

    static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);
    [DllImport("user32.dll")]

    static extern bool IsWindowVisible(IntPtr hWnd);

    public static ArrayList hwnd_arr;
    
    public static bool EnumTheWindows(IntPtr hWnd, IntPtr lParam)
    {
        int size = GetWindowTextLength(hWnd);
        if (size++ > 0 && IsWindowVisible(hWnd))
        {
            StringBuilder sb = new StringBuilder(size);
            GetWindowText(hWnd, sb, size);
            if (sb.ToString() == "Echo Wave II")
                hwnd_arr.Add(hWnd);
        }
        return true;
    }
    public string FindWindows()
    {
        hwnd_arr = new ArrayList();
        EnumWindows(new EnumWindowsProc(EnumTheWindows), IntPtr.Zero);
        string num_windows = (hwnd_arr.Count).ToString();        
        return(num_windows); //return num of strings
        // this.found_windows_label.Text = (hwnd_arr.Count).ToString();
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct COPYDATASTRUCT
    {
        public IntPtr dwData;
        public int cbData;
        public IntPtr lpData;

    }

    public const uint WM_COPYDATA = 0x4A;

    [DllImport("user32.dll")]
    public extern static IntPtr SendMessage(System.IntPtr hWnd,
                             uint Msg, IntPtr wParam,
                             ref COPYDATASTRUCT lParam);
    
    public void SendString(IntPtr wnd, int id, String str)
    {
        try
        {
            if (wnd == IntPtr.Zero) return;


            if (id >= 100001) // user-defined id for identification when lpData should be converted to Unicode string
            {
                IntPtr ptr = Marshal.StringToHGlobalUni(str);
                COPYDATASTRUCT cds = new COPYDATASTRUCT();

                cds.dwData = new IntPtr(id);
                cds.cbData = 2 * (str.Length + 1); // +1 - terminating zero
                cds.lpData = ptr;

                IntPtr result = SendMessage(wnd, WM_COPYDATA, wnd, ref cds);

                Marshal.FreeHGlobal(ptr);
            }
            else
            {
                IntPtr ptr = Marshal.StringToHGlobalAnsi(str);
                COPYDATASTRUCT cds = new COPYDATASTRUCT();

                cds.dwData = new IntPtr(id);
                cds.cbData = str.Length + 1; // +1 - terminating zero
                cds.lpData = ptr;

                IntPtr result = SendMessage(wnd, WM_COPYDATA, wnd, ref cds);

                Marshal.FreeHGlobal(ptr);
            }

        }
        catch (Exception)
        {
        }
    }

    public void SendSaveTVDCommand(string filename)
    {        
        if (hwnd_arr == null)
            return;
        String cmd_str;

        if (hwnd_arr.Count == 1)
        {
            cmd_str = "-save^#^" + filename + ".tvd";
            SendString((IntPtr)(hwnd_arr[0]), 100001, cmd_str);
        }

        else
        {
            for (int i1 = 0; i1 < (hwnd_arr.Count); i1++)
            {
                cmd_str = "-save^#^" + filename + "_ECO" + i1.ToString() + ".tvd";
                SendString((IntPtr)(hwnd_arr[i1]), 100001, cmd_str);

            }

        }        
      
        
        return;
    }

    public void SendFreezeRunCommand() //attention because it will do for both. That's okay after saving!
    {
        if (hwnd_arr == null)
            return;
        String cmd_str;

        cmd_str = "-freezerun";

        for (int i1 = 0; i1 < (hwnd_arr.Count); i1++)
        {
            SendString((IntPtr)(hwnd_arr[i1]), 100001, cmd_str);
        }
    }
    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}