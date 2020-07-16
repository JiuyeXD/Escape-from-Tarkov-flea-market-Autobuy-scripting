using System;
using System.IO;
using System.Management;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace HookTest
{
    class Script
    {
        public class KeyBoard
        {
            public const byte vKeyLButton = 0x1;    // 鼠标左键
            public const byte vKeyRButton = 0x2;    // 鼠标右键
            public const byte vKeyCancel = 0x3;     // CANCEL 键
            public const byte vKeyMButton = 0x4;    // 鼠标中键
            public const byte vKeyBack = 0x8;       // BACKSPACE 键
            public const byte vKeyTab = 0x9;        // TAB 键
            public const byte vKeyClear = 0xC;      // CLEAR 键
            public const byte vKeyReturn = 0xD;     // ENTER 键
            public const byte vKeyShift = 0x10;     // SHIFT 键
            public const byte vKeyControl = 0x11;   // CTRL 键
            public const byte vKeyAlt = 18;         // Alt 键  (键码18)
            public const byte vKeyMenu = 0x12;      // MENU 键
            public const byte vKeyPause = 0x13;     // PAUSE 键
            public const byte vKeyCapital = 0x14;   // CAPS LOCK 键
            public const byte vKeyEscape = 0x1B;    // ESC 键
            public const byte vKeySpace = 0x20;     // SPACEBAR 键
            public const byte vKeyPageUp = 0x21;    // PAGE UP 键
            public const byte vKeyEnd = 0x23;       // End 键
            public const byte vKeyHome = 0x24;      // HOME 键
            public const byte vKeyLeft = 0x25;      // LEFT ARROW 键
            public const byte vKeyUp = 0x26;        // UP ARROW 键
            public const byte vKeyRight = 0x27;     // RIGHT ARROW 键
            public const byte vKeyDown = 0x28;      // DOWN ARROW 键
            public const byte vKeySelect = 0x29;    // Select 键
            public const byte vKeyPrint = 0x2A;     // PRINT SCREEN 键
            public const byte vKeyExecute = 0x2B;   // EXECUTE 键
            public const byte vKeySnapshot = 0x2C;  // SNAPSHOT 键
            public const byte vKeyDelete = 0x2E;    // Delete 键
            public const byte vKeyHelp = 0x2F;      // HELP 键
            public const byte vKeyNumlock = 0x90;   // NUM LOCK 键

            //常用键 字母键A到Z
            public const byte vKeyA = 65;
            public const byte vKeyB = 66;
            public const byte vKeyC = 67;
            public const byte vKeyD = 68;
            public const byte vKeyE = 69;
            public const byte vKeyF = 70;
            public const byte vKeyG = 71;
            public const byte vKeyH = 72;
            public const byte vKeyI = 73;
            public const byte vKeyJ = 74;
            public const byte vKeyK = 75;
            public const byte vKeyL = 76;
            public const byte vKeyM = 77;
            public const byte vKeyN = 78;
            public const byte vKeyO = 79;
            public const byte vKeyP = 80;
            public const byte vKeyQ = 81;
            public const byte vKeyR = 82;
            public const byte vKeyS = 83;
            public const byte vKeyT = 84;
            public const byte vKeyU = 85;
            public const byte vKeyV = 86;
            public const byte vKeyW = 87;
            public const byte vKeyX = 88;
            public const byte vKeyY = 89;
            public const byte vKeyZ = 90;

            //数字键盘0到9
            public const byte vKey0 = 48;    // 0 键
            public const byte vKey1 = 49;    // 1 键
            public const byte vKey2 = 50;    // 2 键
            public const byte vKey3 = 51;    // 3 键
            public const byte vKey4 = 52;    // 4 键
            public const byte vKey5 = 53;    // 5 键
            public const byte vKey6 = 54;    // 6 键
            public const byte vKey7 = 55;    // 7 键
            public const byte vKey8 = 56;    // 8 键
            public const byte vKey9 = 57;    // 9 键


            public const byte vKeyNumpad0 = 0x60;    //0 键
            public const byte vKeyNumpad1 = 0x61;    //1 键
            public const byte vKeyNumpad2 = 0x62;    //2 键
            public const byte vKeyNumpad3 = 0x63;    //3 键
            public const byte vKeyNumpad4 = 0x64;    //4 键
            public const byte vKeyNumpad5 = 0x65;    //5 键
            public const byte vKeyNumpad6 = 0x66;    //6 键
            public const byte vKeyNumpad7 = 0x67;    //7 键
            public const byte vKeyNumpad8 = 0x68;    //8 键
            public const byte vKeyNumpad9 = 0x69;    //9 键
            public const byte vKeyMultiply = 0x6A;   // MULTIPLICATIONSIGN(*)键
            public const byte vKeyAdd = 0x6B;        // PLUS SIGN(+) 键
            public const byte vKeySeparator = 0x6C;  // ENTER 键
            public const byte vKeySubtract = 0x6D;   // MINUS SIGN(-) 键
            public const byte vKeyDecimal = 0x6E;    // DECIMAL POINT(.) 键
            public const byte vKeyDivide = 0x6F;     // DIVISION SIGN(/) 键


            //F1到F12按键
            public const byte vKeyF1 = 0x70;   //F1 键
            public const byte vKeyF2 = 0x71;   //F2 键
            public const byte vKeyF3 = 0x72;   //F3 键
            public const byte vKeyF4 = 0x73;   //F4 键
            public const byte vKeyF5 = 0x74;   //F5 键
            public const byte vKeyF6 = 0x75;   //F6 键
            public const byte vKeyF7 = 0x76;   //F7 键
            public const byte vKeyF8 = 0x77;   //F8 键
            public const byte vKeyF9 = 0x78;   //F9 键
            public const byte vKeyF10 = 0x79;  //F10 键
            public const byte vKeyF11 = 0x7A;  //F11 键
            public const byte vKeyF12 = 0x7B;  //F12 键

            [DllImport("user32.dll")]
            public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

            public static void keyPress(byte keyName)
            {
                KeyBoard.keybd_event(keyName, 0, 0, 0);
                KeyBoard.keybd_event(keyName, 0, 2, 0);
            }
        }

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        const int MOUSEEVENTF_MOVE = 0x0001;//移动 
        


        const int MOUSEEVENTF_LEFTDOWN = 0x0002; //左键按下 
        const int MOUSEEVENTF_LEFTUP = 0x0004; //左键抬起 
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008; //右键按下 
        const int MOUSEEVENTF_RIGHTUP = 0x0010; //右键抬起 
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020; //中键按下 
        const int MOUSEEVENTF_MIDDLEUP = 0x0040; //中键抬起 
        const int MOUSEEVENTF_ABSOLUTE = 0x8000; //是否采用绝对坐标 

        static void click(int x, int y)
        {
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, x * 65535 / 3440, y * 65535 / 1440, 0, 0);//分割
            Thread.Sleep(20);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        internal bool Auth()
        {
            var request = WebRequest.Create("https://JiuyeXD.cn/Tarkov/" + ToMD5(Register.GetMachineCodeString()) + ".txt");
            request.Method = "GET";
            try
            {
                var response = request.GetResponse();
                using (var stream = new StreamReader(response.GetResponseStream()))
                {
                    int content = Convert.ToInt32(stream.ReadToEnd());
                    if (content == 1)
                    {
                        return true;
                    }
                    else if (content == 0)
                    {
                        MessageBox.Show("You has been banned.");
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("未注册!\n识别码已复制到剪切板.");
                Clipboard.SetData(DataFormats.Text, ToMD5(Register.GetMachineCodeString()));//复制内容到剪切板
                return false;
            }
            return false;
        }

        static void code()
        {
            var request = WebRequest.Create("https://JiuyeXD.cn/Tarkov/Script.txt");
            request.Method = "GET";
            var response = request.GetResponse();
            using (var stream = new System.IO.StreamReader(response.GetResponseStream()))
            {
                string content = stream.ReadToEnd();
            }
        }

        public void DoScript(object source, ElapsedEventArgs e)
        {
            Thread.Sleep(100);
            KeyBoard.keyPress(KeyBoard.vKeyY);

            click(1079, 111);
            Thread.Sleep(100);

            KeyBoard.keyPress(KeyBoard.vKeyY);
            click(1255, 569);
            Thread.Sleep(500);

            KeyBoard.keyPress(KeyBoard.vKeyY);
            click(2790, 266);
            Thread.Sleep(100);

            KeyBoard.keyPress(KeyBoard.vKeyY);
            click(1760, 759);
            Thread.Sleep(100);
        }
        public static string ToMD5(string strs)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(strs);
            byte[] encryptdata = md5.ComputeHash(bytes);
            return Convert.ToBase64String(encryptdata);
        }
    }



    //机器码
    public class Register
    {
        static Register machineCode;

        public static string GetMachineCodeString()
        {
            string machineCodeString = string.Empty;
            if (machineCode == null)
            {
                machineCode = new Register();
            }
            machineCodeString = machineCode.GetCpuInfo() +
                                machineCode.GetHDid();
            return machineCodeString;
        }
        ///   获取cpu序列号     
        public string GetCpuInfo()
        {
            string cpuInfo = "";
            try
            {
                using (ManagementClass cimobject = new ManagementClass("Win32_Processor"))
                {
                    ManagementObjectCollection moc = cimobject.GetInstances();

                    foreach (ManagementObject mo in moc)
                    {
                        cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                        mo.Dispose();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return cpuInfo.ToString();
        }
        ///   <summary> 
        ///   获取硬盘ID     
        ///   </summary> 
        ///   <returns> string </returns> 
        public string GetHDid()
        {
            string HDid = "";
            try
            {
                using (ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive"))
                {
                    ManagementObjectCollection moc1 = cimobject1.GetInstances();
                    foreach (ManagementObject mo in moc1)
                    {
                        HDid = (string)mo.Properties["Model"].Value;
                        mo.Dispose();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return HDid.ToString();
        }
    }
}

