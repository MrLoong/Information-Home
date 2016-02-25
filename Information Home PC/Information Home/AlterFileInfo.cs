using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace Information_Home_PC
{
    public class AlterFileInfo
    {
        private Boolean flag ;

        public Boolean AlterFilesName(string oldFilesFullName,string newFilesName)
        {
            flag = false;
            try
            {
                DirectoryInfo DInfo = new DirectoryInfo(oldFilesFullName);
                DInfo.MoveTo(oldFilesFullName.Substring(0, oldFilesFullName.LastIndexOf("\\") + 1) + newFilesName);
                flag = true;
            }
            catch(Exception ex)
            {
               
                //MessageBox.Show(ex.Message);
            }
            return flag;
        }
        public Boolean AlterFileName(string oldFileFullName, string newFileName)
        {
            flag = false;
            string fileSuffix = "";
            // 从路径当中分离出文件名
            string[] name = oldFileFullName.Split('\\');
            //string fileName = oldFileFullName.Substring(oldFileFullName.LastIndexOf("\\") + 1, (oldFileFullName.LastIndexOf(".") - oldFileFullName.LastIndexOf("\\") - 1));
            string fileName = name[name.Length - 1];
            // 从路径当中分离出文件后缀
            if (fileName.IndexOf(".") >= 0)
            {
                fileSuffix = oldFileFullName.Substring(oldFileFullName.LastIndexOf(".") + 1, (oldFileFullName.Length - oldFileFullName.LastIndexOf(".") - 1));
            }
            try
            {
                Console.WriteLine(fileSuffix + "+" + oldFileFullName, DateTime.Now.ToShortTimeString());
                FileInfo FInfo = new FileInfo(oldFileFullName);
                FInfo.MoveTo(oldFileFullName.Substring(0, oldFileFullName.LastIndexOf("\\") + 1) + newFileName + "." + fileSuffix);
                flag = true;
            }
            catch (Exception ex)
            {
                
                //MessageBox.Show(ex.Message);
            }
            return flag;
        }
    }
}