using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;

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
        public  int KeyBoardHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KeyBoardHookStruct kbh = (KeyBoardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyBoardHookStruct));
                if (kbh.vkCode == (int)Keys.S && (int)Control.ModifierKeys == (int)Keys.Control)  // �ػ�F8
                {
                    MessageBox.Show("��ݼ�������!���ܱ���!");
                    return 1;
                }
                //if ((int)Control.ModifierKeys == (int)Keys.Delete && (int)Control.ModifierKeys == (int)Keys.Alt && (int)Control.ModifierKeys == (int)Keys.Control)
                //{
                //    MessageBox.Show("��׽��Ctrl+Alt+Delete");
                //    return 1;
                //}
                if (kbh.vkCode == (int)Keys.Y
                   && (int)Control.ModifierKeys == (int)Keys.Control + (int)Keys.Alt)  //�ػ�Ctrl+Alt+Y
                {
                    About msg = new About();
                    msg.Show();
                    //MessageBox.Show("����ȫ������!");
                    return 1;
                }
                if (kbh.vkCode == (int)Keys.A)
                {
                   // MessageBox.Show("A");
                    this.label1.Text += "A";
                }
                if (kbh.vkCode == (int)Keys.B)
                {
                   // MessageBox.Show("B");
                    this.label1.Text += "B";
                }
                if (kbh.vkCode == (int)Keys.Enter)
                {
                    this.label1.Text = "ִ�гɹ���";
                }
                if(kbh.vkCode==(int)Keys.Back)
                {
                    this.label1.Text = this.label1.Text.Remove(this.label1.Text.Length-1) ;
                }
                if (kbh.vkCode == (int)Keys.D1)
                {
                    this.label1.Text += "1";
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
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
           
        }

        void Start_BtnClick(object sender, EventArgs e)
        {
            this.Hook_Start();
        }

        void Clear_BtnClick(object sender, EventArgs e)
        {
            this.Hook_Clear();
        }
    }
}

