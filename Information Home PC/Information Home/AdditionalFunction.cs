using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Information_Home_PC
{
    class AdditionalFunction
    {
        public static void Instructions(String instructions)
        {
            if (instructions.Equals("YYSB"))    // 语音识别
            {
                MyDos.ExecDosInstructions(@"%windir%\Speech\Common\sapisvr.exe -SpeechUX");
            }
            else if (instructions.Equals("FDJ"))    // 放大镜
            {
                MyDos.ExecDosInstructions(@"C:\Windows\system32\Magnify.exe");
            }
            else if (instructions.Equals("JSR"))    // 讲述人
            {
                MyDos.ExecDosInstructions(@"C:\Windows\system32\Narrator.exe");
            }
            else if (instructions.Equals("PMJP"))    // 屏幕键盘
            {
                MyDos.ExecDosInstructions(@"C:\Windows\system32\osk.exe");
            }
            else if (instructions.Equals("JSRW"))    // 结束当前活动任务
            {
                MyDos.ExecDosInstructions(@"CloseTASK.vbs");
            }
            else if (instructions.Equals("SDJSJ"))    // 锁定计算机
            {
                MyDos.ExecDosInstructions(@"%windir%\System32\rundll32.exe user32.dll,LockWorkStation");
            }
            else if (instructions.Equals("GJ"))    // 关机
            {
                MyDos.ExecDosInstructions(@"shutdown -s");
            }
            else if (instructions.Equals("CQ"))    // 快速重启
            {
                MyDos.ExecDosInstructions(@"shutdown -f");
            }
            else if (instructions.Equals("WEB"))    // 打开浏览器
            {
                MyDos.ExecDosInstructions(@"start explorer http://cn.bing.com/");
            }
            else if (instructions.Equals("HT"))    // 画图
            {
                MyDos.ExecDosInstructions(@"C:\Windows\system32\mspaint.exe");
            }
            else if (instructions.Equals("JSQ"))    // 计算器
            {
                MyDos.ExecDosInstructions(@"calc");
            }
            else if (instructions.Equals("LYJ"))    // 录音机
            {
                MyDos.ExecDosInstructions(@"SoundRecorder.exe");
            }
            else if (instructions.Equals("JSB"))    // 记事本
            {
                MyDos.ExecDosInstructions(@"notepad.exe");
            }
            else if (instructions.Equals("WMP"))    // windows media player
            {
                MyDos.ExecDosInstructions(@"C:\Program Files (x86)\Windows Media Player\wmplayer.exe");
            }
            else if (instructions.Equals("JTGJ"))    // 截图工具
            {
                MyDos.ExecDosInstructions(@"C:\Windows\system32\SnippingTool.exe");
            }
            else if (instructions.Equals("BQ"))    // 便签
            {
                MyDos.ExecDosInstructions(@"C:\Windows\system32\StikyNot.exe");
            }
            else if (instructions.Equals("BZJLQ"))    // 步骤记录器
            {
                MyDos.ExecDosInstructions(@"C:\Windows\system32\psr.exe");
            }
            else if (instructions.Equals("XZB"))    // 写字板
            {
                MyDos.ExecDosInstructions(@"C:\Program Files\Windows NT\Accessories\wordpad.exe");
            }
            else if (instructions.Equals("ZTDN"))    // 这台电脑
            {
                MyDos.ExecDosInstructions(@"explorer.exe");
            }
            else if (instructions.Equals("SXSRMB"))    // 数学输入面板
            {
                MyDos.ExecDosInstructions(@"C:\Program Files\Common Files\Microsoft Shared\Ink\mip.exe");
            }
            else if (instructions.Equals("CZHSM"))    // 传真和扫描
            {
                MyDos.ExecDosInstructions(@"C:\Windows\system32\WFS.exe");
            }
        }
    }
}
