﻿#pragma checksum "D:\profession\Information\Information Home\Information Home WP\Information Home\Scenic_Sharing.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A8B54915D4EE2C36CD60867C8AB4A076"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.34014
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Information_Home {
    
    
    public partial class Scenic_Sharing : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Canvas LayoutRoot;
        
        internal System.Windows.Shapes.Rectangle Re_video;
        
        internal System.Windows.Media.VideoBrush TheVideoBrush;
        
        internal System.Windows.Controls.Image imgCapture;
        
        internal System.Windows.Controls.Button Share;
        
        internal System.Windows.Controls.Button closeShare;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Information%20Home;component/Scenic_Sharing.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Canvas)(this.FindName("LayoutRoot")));
            this.Re_video = ((System.Windows.Shapes.Rectangle)(this.FindName("Re_video")));
            this.TheVideoBrush = ((System.Windows.Media.VideoBrush)(this.FindName("TheVideoBrush")));
            this.imgCapture = ((System.Windows.Controls.Image)(this.FindName("imgCapture")));
            this.Share = ((System.Windows.Controls.Button)(this.FindName("Share")));
            this.closeShare = ((System.Windows.Controls.Button)(this.FindName("closeShare")));
        }
    }
}

