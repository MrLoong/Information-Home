using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
namespace Information_Home_PC
{
    class MyInstruction
    {

        private FindFiles findFilesInfo = new FindFiles();
        private string findContent;
        private AlterFileInfo alter = new AlterFileInfo();
        public static byte[] bt;
        
        public string Exec(string ReceiveContent)
        {
            bt = null;
            string[] message = ReceiveContent.Split('|');
            switch (message[0])
            {
                case "0":   // 检索所有的盘符
                    findContent = findFilesInfo.GetSubFiles();
                    //this.BeginInvoke(new Action<string>(this.SendCommand), r);     //根据指令调用命令
                    break;
                case "1":   // 检索当前路径的子目录
                    findContent = findFilesInfo.GetSubFiles(message[1]);
                    if (String.IsNullOrEmpty(findContent))
                    {
                        findContent = "|||***|||";                                 //空处理
                    }
                    break;
                case "2":   // 检索当前路径下符合条件的文件，当无路径是检索全部盘符
                    findContent = findFilesInfo.Find_File(message[1],message[2]);
                    Console.WriteLine("findContent" + findContent, DateTime.Now.ToShortTimeString());
                    if (String.IsNullOrEmpty(findContent))
                    {
                        findContent = "|||***|||";                                 //空处理
                    }
                    break;
                case "3":   // 获取当前路径文件的内容，txt预览
                    bt = findFilesInfo.GetFileContent(message[1]);
                    findContent = System.Text.Encoding.Default.GetString(bt);
                    if (String.IsNullOrEmpty(findContent))
                    {
                        findContent = "|||***|||";                                 //空处理
                    }
                    break;
                case "4":   // 获取当前路径文件的内容，保存
                    bt = findFilesInfo.GetFileContent(message[1]);
                   // findContent = Convert.ToBase64String(bt);
                    if (bt.Length == 0 || null == bt)                              //空处理
                    {
                        string StringMessage = "|||***|||";
                        System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
                        bt = UTF8.GetBytes(StringMessage);
                    }
                    findContent = "HQNR";
                    break;
                case "5":   // 修改指定文件夹的文件名
                    alter.AlterFilesName(message[1], message[2]);
                    findContent = "wcz";
                    break;
                case "6":   // 新建文件夹
                    if (!Directory.Exists(message[1]))
                    {
                        Directory.CreateDirectory(message[1]);
                    }
                    findContent = "wcz";
                    break;
                case "7":   // 修改指定文件的文件名
                    alter.AlterFileName(message[1], message[2]);
                    findContent = "wcz";
                    break;
                case "8":   // 删除文件夹及子目录中的文件夹 
                    Directory.Delete(message[1], true);
                    findContent = "wcz";
                    break;
                case "9":   // 删除文件
                    File.Delete(message[1]);
                    findContent = "wcz";
                    break;
                case "10":   // 控制电视
                    ControlTV.ControlTVPressKey(message[1]);
                    findContent = "wcz";
                    break;
                case "11":   // 分享链接

                    findContent = "wcz";
                    break;
                case "12":  // 屏幕推送
                    bt = CutScreen.Cut_Screen();
                    findContent = "PMTS";
                    break;
                case "13":  // 控制PPT
                    ControlTV.ControlPPTPressKey(message[1]);
                    findContent = "wcz";
                    break;
                case "14":
                    AdditionalFunction.Instructions(message[1]);
                    findContent = "wcz";
                    break;
                case "15":
                    ControlTV.ControlKeyBoxPressKey(message[1]);
                    findContent = "wcz";
                    break;

            }
            return findContent;
        }

        public byte[] Get_Byte()
        {
            return bt;
        }
    }
}
