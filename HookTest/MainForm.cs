using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Threading;
using System.Timers;
using System.Diagnostics;
using Microsoft.Win32;

namespace HookTest
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        //ί��
        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);
        static int hHook = 0;
        public const int WH_KEYBOARD_LL = 13;
        //LowLevel���̽ػ������WH_KEYBOARD��2�������ܶ�ϵͳ���̽�ȡ��Acrobat Reader�������ȡ֮ǰ��ü��̡�
        HookProc KeyBoardHookProcedure;
        //����Hook�ṹ����
        [StructLayout(LayoutKind.Sequential)]
        public class KeyBoardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
        #region DllImport
        //���ù���
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //�������
        public static extern bool UnhookWindowsHookEx(int idHook);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //������һ������
        public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);
        #endregion
        #region �Զ����¼�
        public void Hook_Start()
        {
            // ��װ���̹���
            if (hHook == 0)
            {
                KeyBoardHookProcedure = new HookProc(KeyBoardHookProc);
                hHook = SetWindowsHookEx(WH_KEYBOARD_LL,
                            KeyBoardHookProcedure,
                            Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
                //������ù���ʧ��.
                if (hHook == 0)
                {
                    Hook_Clear();
                    throw new Exception("����Hookʧ��!");
                }
            }
        }

        //ȡ�������¼�
        public void Hook_Clear()
        {
            bool retKeyboard = true;
            if (hHook != 0)
            {
                retKeyboard = UnhookWindowsHookEx(hHook);
                hHook = 0;
            }
            //���ȥ������ʧ��.
            if (!retKeyboard) throw new Exception("UnhookWindowsHookEx failed.");
        }

        //�����������Լ���Ҫ����Ϣ����
        public static  int KeyBoardHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KeyBoardHookStruct kbh = (KeyBoardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyBoardHookStruct));
                if (kbh.vkCode == (int)Keys.Escape)
                {
                    Application.Exit();
                }

            }
            return CallNextHookEx(hHook, nCode, wParam, lParam);
        }
        #endregion
        
        /*
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
        */

        public MainForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            this.Hook_Start();
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            start_Time(Convert.ToDouble(countdown_mm.Text));
        }
        System.Timers.Timer timer1 = new System.Timers.Timer();
        System.Timers.Timer timer2 = new System.Timers.Timer();
        public void start_Time(double countdown_mm)
        {
            double countdown_ss = countdown_mm * 60;
            double countdown_ms = countdown_ss * 1000;
            string Current_time = DateTime.Now.ToLongTimeString().ToString();
            lable1.Text = DateTime.Parse(Current_time).AddSeconds(countdown_ss).ToLongTimeString().ToString();
            timer1.Enabled = true;
            timer1.Interval = countdown_ms + 1;
            timer1.Start();
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(doScript);
        }

        public void doScript(object source, ElapsedEventArgs e)
        {
            Script script = new Script();
            timer2.Enabled = true;
            timer2.Interval = 600;
            timer2.Start();
            timer1.Stop();
            timer2.Elapsed += new System.Timers.ElapsedEventHandler(script.DoScript);
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://JiuyeXD.cn");
        }


    }
}

