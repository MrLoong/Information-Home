using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Windows;



namespace Information_Home_PC
{
    public partial class HomePage : System.Windows.Forms.Form
    {
        private Icon ico ;
        private NotifyIcon notifyIcon;
        private Information_Home_PC.VistaButton min;
        private Information_Home_PC.VistaButton close;
        private ContextMenu notifyContextMenu;
        private Information_Home_PC.VistaButton share;
        private Information_Home_PC.VistaButton sharpCorners;
        private Information_Home_PC.VistaButton roundedCorners;
        private System.Windows.Forms.Timer jianbian;
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ColumnHeader UserIP;
        private System.Windows.Forms.ColumnHeader UserName;
        [System.Runtime.InteropServices.DllImport("user32")]
        ///
        /// 动画常量
        ///
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        const int AW_HOR_POSITIVE = 0x0001;
        const int AW_HOR_NEGATIVE = 0x0002;
        const int AW_VER_POSITIVE = 0x0004;
        const int AW_VER_NEGATIVE = 0x0008;
        const int AW_CENTER = 0x0010;
        const int AW_HIDE = 0x10000;
        const int AW_ACTIVATE = 0x20000;
        const int AW_SLIDE = 0x40000;
        const int AW_BLEND = 0x80000;
        ///
        /// 窗体拖拽常量
        ///
        const int WM_NCHITTEST = 0x0084;
        const int HT_LEFT = 10;
        const int HT_RIGHT = 11;
        const int HT_TOP = 12;
        const int HT_TOPLEFT = 13;
        const int HT_TOPRIGHT = 14;
        const int HT_BOTTOM = 15;
        const int HT_BOTTOMLEFT = 16;
        const int HT_BOTTOMRIGHT = 17;
        const int HT_CAPTION = 2;
        /// <summary>
        /// 为窗体作圆角处理准备的变量
        /// </summary>
        [DllImport("gdi32.dll")]
        public static extern int CreateRoundRectRgn(int x1, int y1, int x2, int y2, int x3, int y3);
        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hwnd, int hRgn, Boolean bRedraw);
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject", CharSet = CharSet.Ansi)]
        public static extern int DeleteObject(int hObject);
        /// <summary>
        /// 为窗体阴影准备的变量
        /// </summary>
        private const int CS_DropSHADOW = 0x20000;
        private const int GCL_STYLE = (-26);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);    

        private void Formdonghua_Load(object sender, EventArgs e)
        {
            // 动画由小渐大
            AnimateWindow(this.Handle, 3000, AW_CENTER | AW_ACTIVATE);

            // 主界面渐变设置
            this.jianbian.Enabled = true;   // 让jianbian的timer值有效
            this.Opacity = 0;               // 渐显
        }

        public static void SubFormDH(Form shareForm, int time, int style)
        {
            AnimateWindow(shareForm.Handle, time, style);
        }

        private void jianbian_Tick(object sender, EventArgs e)
        {
            //让背景由0变到1
            if (this.Opacity < 1)
            {
                this.Opacity = this.Opacity + 0.05;
            }
            else
            {
                this.jianbian.Enabled = false;
            }
        }
        
        // 实现无框窗体的拖拽
        protected override void WndProc(ref Message Msg)
        {
            if (Msg.Msg == WM_NCHITTEST)
            {
                // 获取鼠标位置
                int nPosX = (Msg.LParam.ToInt32() & 65535);
                int nPosY = (Msg.LParam.ToInt32() >> 16);

                if (nPosX >= this.Right - 2)
                {
                    Msg.Result = new IntPtr(HT_RIGHT);
                    return;
                }
                else if (nPosY >= this.Bottom - 2)
                {
                    Msg.Result = new IntPtr(HT_BOTTOM);
                    return;
                }
                else if (nPosX <= this.Left + 2)
                {
                    Msg.Result = new IntPtr(HT_LEFT);
                    return;
                }
                else if (nPosY <= this.Top + 2)
                {
                    Msg.Result = new IntPtr(HT_TOP);
                    return;
                }
                else
                {
                    Msg.Result = new IntPtr(HT_CAPTION);
                    return;
                }
            }
            base.WndProc(ref Msg);
        }
       
        // 利用双缓存，解决窗体闪烁问题
        private void MainForm_Resize()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            this.Refresh();
        }

        /// 设置窗体的圆角矩形
        /// </summary>
        /// <param name="form">需要设置的窗体</param>
        /// <param name="rgnRadius">圆角矩形的半径</param>
        public static void SetFormRoundRectRgn(Form form, int rgnRadius)
        {
            int hRgn = 0;
            hRgn = CreateRoundRectRgn(0, 0, form.Width + 1, form.Height + 1, rgnRadius, rgnRadius);
            SetWindowRgn(form.Handle, hRgn, true);
            DeleteObject(hRgn);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetFormRoundRectRgn(this, 20);
        }

        private void SetShadow()
        {
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
        }

        public void ExitNotifyIcon()
        {
            this.notifyIcon.Visible = false;
            this.notifyIcon.Dispose();
        }

      
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
            

       
        #region Windows 窗体设计器生成的代码


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomePage));
            this.notifyContextMenu = new System.Windows.Forms.ContextMenu();
            this.jianbian = new System.Windows.Forms.Timer(this.components);
            this.share = new Information_Home_PC.VistaButton();
            this.roundedCorners = new Information_Home_PC.VistaButton();
            this.sharpCorners = new Information_Home_PC.VistaButton();
            this.min = new Information_Home_PC.VistaButton();
            this.close = new Information_Home_PC.VistaButton();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyiconMnu = new System.Windows.Forms.ContextMenu();
            this.listView1 = new System.Windows.Forms.ListView();
            this.UserIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UserName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ShowMess = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // jianbian
            // 
            this.jianbian.Tick += new System.EventHandler(this.jianbian_Tick);
            // 
            // share
            // 
            this.share.BackColor = System.Drawing.Color.Transparent;
            this.share.BaseColor = System.Drawing.Color.Transparent;
            this.share.ButtonColor = System.Drawing.Color.LimeGreen;
            this.share.ButtonText = "内容分享";
            this.share.ForeColor = System.Drawing.Color.DimGray;
            this.share.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(189)))), ((int)(((byte)(141)))));
            resources.ApplyResources(this.share, "share");
            this.share.Name = "share";
            this.share.Click += new System.EventHandler(this.share_Click);
            // 
            // roundedCorners
            // 
            this.roundedCorners.BackColor = System.Drawing.Color.Transparent;
            this.roundedCorners.BaseColor = System.Drawing.Color.Transparent;
            this.roundedCorners.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(211)))), ((int)(((byte)(40)))));
            this.roundedCorners.ButtonText = "Rounded Corners";
            this.roundedCorners.CornerRadius = 20;
            this.roundedCorners.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(255)))), ((int)(((byte)(189)))));
            resources.ApplyResources(this.roundedCorners, "roundedCorners");
            this.roundedCorners.Name = "roundedCorners";
            // 
            // sharpCorners
            // 
            this.sharpCorners.BackColor = System.Drawing.Color.Transparent;
            this.sharpCorners.BaseColor = System.Drawing.Color.Transparent;
            this.sharpCorners.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.sharpCorners.ButtonText = "Sharp Corners";
            this.sharpCorners.CornerRadius = 0;
            this.sharpCorners.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(141)))), ((int)(((byte)(189)))));
            resources.ApplyResources(this.sharpCorners, "sharpCorners");
            this.sharpCorners.Name = "sharpCorners";
            // 
            // min
            // 
            this.min.BackColor = System.Drawing.Color.Transparent;
            this.min.BaseColor = System.Drawing.Color.Transparent;
            this.min.ButtonColor = System.Drawing.Color.Blue;
            this.min.ButtonStyle = Information_Home_PC.VistaButton.Style.Flat;
            this.min.ButtonText = null;
            resources.ApplyResources(this.min, "min");
            this.min.Name = "min";
            this.min.Click += new System.EventHandler(this.min_Click);
            // 
            // close
            // 
            this.close.BackColor = System.Drawing.Color.Transparent;
            this.close.BaseColor = System.Drawing.Color.Transparent;
            this.close.ButtonColor = System.Drawing.Color.Red;
            this.close.ButtonStyle = Information_Home_PC.VistaButton.Style.Flat;
            this.close.ButtonText = null;
            resources.ApplyResources(this.close, "close");
            this.close.Name = "close";
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // notifyIcon
            // 
            this.ico = new Icon(@"Images/Down_ico.ico"); // 创建托盘图标对象 
            this.notifyIcon.Icon = ico;
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            this.notifyIcon.Text = "Information Home";
            this.notifyIcon.Visible = true;
            this.notifyIcon.Click += new System.EventHandler(this.notifyIcon_Click);
            MenuItem[] mnuItms = new MenuItem[3];
            mnuItms[0] = new MenuItem();
            mnuItms[0].Text = "打开软件";
            mnuItms[0].Click += new System.EventHandler(this.notifyIcon_Click);
            mnuItms[1] = new MenuItem("-");
            mnuItms[2] = new MenuItem();
            mnuItms[2].Text = "退出软件";
            mnuItms[2].Click += new System.EventHandler(this.close_Click);
            mnuItms[2].DefaultItem = true;
            ContextMenu notifyiconMnu = new ContextMenu(mnuItms);
            this.notifyIcon.ContextMenu = notifyiconMnu;
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.UserIP,
            this.UserName});
            resources.ApplyResources(this.listView1, "listView1");
            this.listView1.Name = "listView1";
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // UserIP
            // 
            resources.ApplyResources(this.UserIP, "UserIP");
            // 
            // UserName
            // 
            resources.ApplyResources(this.UserName, "UserName");
            // 
            // ShowMess
            // 
            resources.ApplyResources(this.ShowMess, "ShowMess");
            this.ShowMess.BackColor = System.Drawing.Color.Transparent;
            this.ShowMess.ForeColor = System.Drawing.Color.Transparent;
            this.ShowMess.Name = "ShowMess";
            // 
            // HomePage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.ShowMess);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.min);
            this.Controls.Add(this.close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HomePage";
            this.Icon = new Icon(@"Images/Down_ico.ico");
            this.BackgroundImage = (Image)(new Bitmap(@"Images/MainImage.png"));
            this.SetShadow();
            this.Load += new System.EventHandler(this.Formdonghua_Load);
            this.Location = new Point(SystemInformation.PrimaryMonitorSize.Width - this.Width - 50,
                            SystemInformation.PrimaryMonitorSize.Height / 5);
            this.Load += new System.EventHandler(this.HomePage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private ListView listView1;
        private ContextMenu notifyiconMnu;
        private Label ShowMess;
    }
}