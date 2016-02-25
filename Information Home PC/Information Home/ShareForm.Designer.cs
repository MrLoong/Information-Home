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
    partial class ShareForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        public System.Windows.Forms.PictureBox shareContent;
        ///
        /// 动画常量
        ///
        const int AW_HOR_POSITIVE = 0x0001;
        const int AW_HOR_NEGATIVE = 0x0002;
        const int AW_VER_POSITIVE = 0x0004;
        const int AW_VER_NEGATIVE = 0x0008;
        const int AW_CENTER = 0x0010;
        const int AW_HIDE = 0x10000;
        const int AW_ACTIVATE = 0x20000;
        const int AW_SLIDE = 0x40000;
        const int AW_BLEND = 0x80000;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void ShareFormdonghua_Load(object sender, EventArgs e)
        {
            HomePage.SubFormDH(this,500, AW_HOR_POSITIVE);
        }

        private void Formdonghua_FormClosing(object sender, FormClosingEventArgs e)
        {
            HomePage.SubFormDH(this, 1000, AW_BLEND | AW_HIDE | AW_VER_NEGATIVE);
        }
        public void Back_Image(Image im)
        {
            this.shareContent.Image = im;
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShareForm));
            this.shareContent = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.shareContent)).BeginInit();
            this.SuspendLayout();
            // 
            // shareContent
            // 
            this.shareContent.Image = ((System.Drawing.Image)(resources.GetObject("shareContent.Image")));
            this.shareContent.Location = new System.Drawing.Point(1, 0);
            this.shareContent.Name = "shareContent";
            this.shareContent.Size = new System.Drawing.Size(SystemInformation.PrimaryMonitorSize.Width - 13 , SystemInformation.PrimaryMonitorSize.Height - 37);
            this.shareContent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.shareContent.TabIndex = 0;
            this.shareContent.TabStop = false;
            this.shareContent.Click += new System.EventHandler(this.shareContent_Click);
            // 
            // ShareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(SystemInformation.PrimaryMonitorSize.Width - 16, SystemInformation.PrimaryMonitorSize.Height - 40);
            this.Controls.Add(this.shareContent);
            this.Name = "ShareForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShareForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Formdonghua_FormClosing);
            this.Load += new System.EventHandler(this.ShareFormdonghua_Load);
            ((System.ComponentModel.ISupportInitialize)(this.shareContent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

    }
}