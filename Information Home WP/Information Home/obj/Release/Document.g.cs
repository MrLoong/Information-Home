﻿#pragma checksum "D:\profession\Information\Information Home\Information Home WP\Information Home\Document.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "67513E6E513EECFBF05AF776956DA917"
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
    
    
    public partial class Document : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.TextBlock top_Text;
        
        internal System.Windows.Controls.ListBox List_Documents;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Information%20Home;component/Document.xaml", System.UriKind.Relative));
            this.top_Text = ((System.Windows.Controls.TextBlock)(this.FindName("top_Text")));
            this.List_Documents = ((System.Windows.Controls.ListBox)(this.FindName("List_Documents")));
        }
    }
}

