using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Information_Home_PC
{
    class MyDos
    {
        private static Process p = new Process();

        //  用指定程序执行命令
        public static void Exec(string openfile ,string filename)
        {
            p.StartInfo.FileName = openfile;
            p.StartInfo.Arguments = @"/c" + filename;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
        }

        //  执行Dos命令
        public static void ExecDosInstructions(string instructions)
        {
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = @"/c" + instructions;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
        }
    }
}
