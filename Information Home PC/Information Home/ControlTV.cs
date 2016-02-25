using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Information_Home_PC
{
    class ControlTV
    {
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);  
        public static void ControlPPTPressKey(string key)
        {
            if (key.Equals("F"))    // 开始放映
            {
                //MyDos.ExecDosInstructions(@" C:\Users\Public\IHomeFiles\2014纳新宣讲\123.html");
                //System.Threading.Thread.Sleep(2000);
                SendKeys.SendWait("{F5}");
            }
            else  if (key.Equals("E"))  // 结束放映
            {
                SendKeys.SendWait("{ESC}");
            }
            else if (key.Equals("D"))    // 下一页
            {
                SendKeys.SendWait("{Down}");
            }
            else if (key.Equals("U"))    // 上一页
            {
                SendKeys.SendWait("{Up}");
            }
            else if (key.Equals("N"))
            {
                SendKeys.SendWait("^{n}");
            }
        }

        public static void ControlKeyBoxPressKey(string key)
        {

            if (key.Equals("kg"))
            {
                SendKeys.SendWait(" ");
            }
            else if (key.Equals("SHIFT"))
            {
                SendKeys.SendWait("+");
            }
            else if (key.Equals("CTRL"))
            {
                SendKeys.SendWait("^");
            }
            else if (key.Equals("ALT"))
            {
                SendKeys.SendWait("%");
            }
            else if (key.Equals("win"))
            {
                SendKeys.SendWait("^{esc}");
            }
            else if (key.Equals("CAPSLOCK"))
            {
                const int KEYEVENTF_EXTENDEDKEY = 0x1;
                const int KEYEVENTF_KEYUP = 0x2;
                keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
                keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
            }
            else
            {
                SendKeys.SendWait("{"+key+"}");
            }
                
        }
        public static void  ControlTVPressKey(string key)
        {
            Console.WriteLine(key,DateTime.Now.ToShortTimeString());
            if (key.Equals("O"))    // 打开播放器    指令实例：10|O
            {
                //MyDos.ExecDosInstructions(@"C:\Users\Public\IHomeFiles\5.flv");
                //MyDos.ExecDosInstructions(@"C:\Users\Public\IHomeFiles\4.flv");
                //MyDos.ExecDosInstructions(@"C:\Users\Public\IHomeFiles\3.flv");
                //MyDos.ExecDosInstructions(@"C:\Users\Public\IHomeFiles\2.flv");
                //MyDos.ExecDosInstructions(@"C:\Users\Public\IHomeFiles\1.flv");
                //System.Threading.Thread.Sleep(2000);
                //SendKeys.SendWait("%{Enter}");
                MyDos.ExecDosInstructions(@"PPTVAddressInfo.bat");
            }
            else if (key.Equals("C"))    // 关闭播放器      指令示例：10|C
            {
                MyDos.ExecDosInstructions(@"taskkill /f /im PPLive.exe");
            }
            else if (key.Equals("A"))    // 静音      指令示例：10|A
            {
                SendKeys.SendWait("{F7}");
            }
            else if (key.Equals("P"))    // 播放/暂停   指令实例：10|P
            {
                SendKeys.SendWait("^p");
            }
            else if (key.Equals("U"))    // 增大音量    指令实例：10|U
            {
                SendKeys.SendWait("{Up}");
            }
            else if (key.Equals("D"))    // 减小音量    指令示例：10|D
            {
                SendKeys.SendWait("{Down}");
            }
            else if (key.Equals("R"))    // 快进    指令示例：10|R
            {
                SendKeys.SendWait("{Right}");
            }
            else if (key.Equals("L"))    // 快退    指令示例：10|L
            {
                SendKeys.SendWait("{Left}");
            }
            else if (key.Equals("F"))   // 上一个节目    指令示例：10|F
            {
                SendKeys.SendWait("^{Left}");
            }
            else if (key.Equals("B"))   // 下一个节目    指令示例：~l0|B
            {
                SendKeys.SendWait("^{Right}");
            }
        }

    }
}
