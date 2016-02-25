using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Information_Home
{
    public partial class Key_Box : PhoneApplicationPage
    {
        WP_Client Client = null;
        public Key_Box()
        {
            InitializeComponent();
            Client = Connect.Client;
        }

        private void Button_Click_0(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|ESC");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|F1");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|F3");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|F2");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|F4");
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|F5");
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|F6");
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|F7");
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|F8");
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|F9");
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|F10");
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|F11");
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|F12");
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|DELETE");
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|~");

        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|1");

        }

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|2");
        }

        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|3");
        }

        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|4");

        }

        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|5");
        }

        private void Button_Click_20(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|6");
        }

        private void Button_Click_21(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|7");
        }

        private void Button_Click_22(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|8");
        }

        private void Button_Click_23(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|9");
        }

        private void Button_Click_24(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|0");
        }

        private void Button_Click_25(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|SUBTRACT");
        }

        private void Button_Click_26(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|ADD");
        }

        private void Button_Click_27(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|BACKSPACE");
        }

        private void Button_Click_28(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|TAB");
        }

        private void Button_Click_29(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|q");
        }

        private void Button_Click_30(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|w");
        }

        private void Button_Click_31(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|e");

        }

        private void Button_Click_32(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|r");
        }

        private void Button_Click_33(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|t");
        }

        private void Button_Click_34(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|y");
        }

        private void Button_Click_35(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|u");
        }

        private void Button_Click_36(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|i");

        }

        private void Button_Click_37(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|o");

        }

        private void Button_Click_38(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|p");

        }

        private void Button_Click_39(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|[");
        }

        private void Button_Click_40(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|]");
        }

        private void Button_Click_41(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|\");
        }

        private void Button_Click_42(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|CAPSLOCK");
        }

        private void Button_Click_43(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|a");

        }

        private void Button_Click_44(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|s");
        }

        private void Button_Click_45(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|d");

        }

        private void Button_Click_46(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|f");

        }

        private void Button_Click_47(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|g");
        }

        private void Button_Click_48(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|h");
        }

        private void Button_Click_49(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|j");
        }

        private void Button_Click_50(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|k");
        }

        private void Button_Click_51(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|l");
        }

        private void Button_Click_52(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|;");
        }

        private void Button_Click_53(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|'");
        }

        private void Button_Click_54(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|ENTER");
        }

        private void Button_Click_55(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|SHIFT");
        }

        private void Button_Click_56(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|z");
        }

        private void Button_Click_57(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|x");
        }

        private void Button_Click_58(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|c");
        }

        private void Button_Click_59(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|v");

        }

        private void Button_Click_60(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|b");

        }

        private void Button_Click_61(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|n");

        }

        private void Button_Click_62(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|m");
        }

        private void Button_Click_63(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|,");
        }

        private void Button_Click_64(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|.");
        }

        private void Button_Click_65(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|/");

        }

        private void Button_Click_66(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|SHIFT");

        }

        private void Button_Click_67(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|UP");

        }

        private void Button_Click_68(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|CTRL");
        }

        private void Button_Click_69(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|win");
        }

        private void Button_Click_70(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|ALT");
        }

        private void Button_Click_71(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|kg");
        }

        private void Button_Click_72(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|ALT");
        }

        private void Button_Click_73(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|PRTSC");

        }

        private void Button_Click_74(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|CTRL");

        }

        private void Button_Click_75(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|LEFT");

        }

        private void Button_Click_76(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|DOWN");
        }

        private void Button_Click_77(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"15|RIGHT");
        }
        
    }
}