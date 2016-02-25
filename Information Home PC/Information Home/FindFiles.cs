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
    public class FindFiles
    {
        private string result = "";  // 保存搜索结果
        private DriveInfo[] d = DriveInfo.GetDrives();


        // 该函数实现获取文件内容
        public byte[] GetFileContent(string fileAddress)
        {
            byte[] reads = File.ReadAllBytes(fileAddress);
            return reads;
        }



        // 该函数实现更新文件内容
        public Boolean UpdateFileContent(string oldFileAddress,byte[] fileContent)
        {
            File.WriteAllBytes(oldFileAddress, fileContent);    
            return true;
        }



        // 该函数实现返回当前目录的子目录
        public string GetSubFiles(string address = "")
        {
            result = "";   // 清空结果集

            if (String.IsNullOrEmpty(address))
            {
                foreach (DriveInfo d1 in d)
                {
                    if (d1.DriveType == DriveType.Fixed)
                    {
                        result += d1.Name + "|";
                    }
                }
            }
            else   // 如果路径不为空就检索具体路径
            {
                try
                {
                    DirectoryInfo dinfo = new DirectoryInfo(address);
                    FileSystemInfo[] fsinfos = dinfo.GetFileSystemInfos();

                    // 获取指定目录下的所有子目录及文件类型
                    foreach (FileSystemInfo fsinfo in fsinfos)
                    {
                        // 获取当前文件属性
                        FileAttributes MyAttributes = File.GetAttributes(fsinfo.FullName);
                        string MyFileType = MyAttributes.ToString();

                        // 判断文件是否为只读，系统文件，隐藏文件，存档，临时文件
                        /* if (MyFileType.LastIndexOf("Readingly") != -1 || MyFileType.LastIndexOf("System") != -1 ||
                            MyFileType.LastIndexOf("Hidden") != -1 || MyFileType.LastIndexOf("Archive") != -1 ||
                            MyFileType.LastIndexOf("Temporary") != -1)      
                        {
                            continue;
                        }*/
                        if (MyFileType.LastIndexOf("System") != -1 || MyFileType.LastIndexOf("Hidden") != -1)    // 判断文件是否为系统文件，能否访问
                        {
                            continue;
                        }
                        if (fsinfo is DirectoryInfo)    // 判断是否是文件夹
                        {
                            DirectoryInfo dirinfo = new DirectoryInfo(fsinfo.FullName);
                            result += dirinfo.Name + "*1|";   // 0 代表文件，1 代表文件夹
                            //    content.Text += "《" + num + "》" + dirinfo.Name + " 文件夹 " + " " +
                            //      dirinfo.FullName + " " + dirinfo.CreationTime.ToShortDateString() + "\r\n";
                        }
                        else
                        {
                            FileInfo finfo = new FileInfo(fsinfo.FullName);
                            result += finfo.Name + "*0|";
                            //  content.Text += "《" + num + "》" + finfo.Name + " 文件 " + " " + finfo.Length + " " +
                            //     finfo.FullName + " " + finfo.CreationTime.ToShortDateString() + "\r\n";
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                    //MessageBox.Show(ex.Message + "\r\n\n路径错误或不存在,请核对后重新输入！", "I.Pigeon专用");
                }
            }

            return result;
        }



        // 该函数实现文件模糊搜索
        public string Find_File(string fileAddress,string fileName)
        {
            result = "";
                GetFileName(fileAddress, fileName);
                return result;
        }

        
        // 该函数实现根据路径，文件名检索文件
        void GetFileName(string DirName, string FileName)
        {
            //Console.WriteLine(DirName + FileName,DateTime.Now.ToLongTimeString());
            DirectoryInfo dir = new DirectoryInfo(DirName);

            // 如果非根路径且是系统文件夹则跳过
            if (null != dir.Parent && dir.Attributes.ToString().IndexOf("System") > -1)
            {
                return;
            }

            // 以下文件夹的访问被拒绝,一个个找出来的，我也是滴血了。呵呵！
            if(dir.FullName.Equals(@"C:\Program Files\WindowsApps")||
                dir.FullName.Equals(@"C:\Program Files (x86)\Google\CrashReports")||
                dir.FullName.Equals(@"C:\Users\lurenyi\AppData\Local\ElevatedDiagnostics")||
                dir.FullName.Equals(@"C:\Windows")||
                dir.FullName.Equals(@"C:\Windows\LiveKernelReports")||
                dir.FullName.Equals(@"C:\Windows\Logs\HomeGroup"))
            {
                return ;
            }



            // 取得所有文件
            FileInfo[] finfo = dir.GetFiles();
            string fname = string.Empty;
            string fname1 = "";
            string last = "";
            for (int i = 0; i < finfo.Length; i++)
            {
                fname = finfo[i].Name;
                if (fname.IndexOf(".") >= 0)
                {
                    string[] type = fname.Split('.');
                    last = type[type.Length - 1];             //后缀名
                    fname1 = Get_Last(fname,"."+last);
                }
                else
                {
                    fname1 = fname;
                }  
                //string[] f = fname.Split('.');
                //string ff = f[f.Length - 1];
                
                // 判断文件名是否包含查询名,模糊搜索，返回第一次匹配时的下标
                //if (fname.IndexOf(FileName) > -1)
                //{
                //    result += finfo[i].FullName + "|";
                //}
                if (fname1 == FileName)
                {
                    result += finfo[i].FullName + "|";
                }
            }
            // 取得所有子文件夹
            DirectoryInfo[] dinfo = dir.GetDirectories();
            for (int i = 0; i < dinfo.Length; i++)
            {
                GetFileName(dinfo[i].FullName, FileName);
            }
        }

        public string Get_Last(string a, string b)                              //字符串相减
        {
            char[] chara = a.ToCharArray();
            char[] charb = b.ToCharArray();
            for (int i = 1; i <= charb.Length; i++)
            {
                if (chara[chara.Length - i] == charb[charb.Length - i])
                {
                    chara[chara.Length - i] = ' ';
                }
            }
            string newstr = new string(chara);
            newstr = newstr.Replace(" ", "");
            return newstr;
        }
        
        
    }
}
