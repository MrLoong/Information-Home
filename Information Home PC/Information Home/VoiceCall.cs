using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Information_Home_PC
{
    public partial class VoiceCall : Form
    {
        public MyAudio my_Audio = new MyAudio();
        public Thread threadvoi;
        public VoiceCall()
        {
            InitializeComponent();
            my_Audio.StartRecording();
            threadvoi = new Thread(MyAudio.Send_Voice);
            threadvoi.Start();
        }

        private void Voice_Click(object sender, EventArgs e)
        {
            threadvoi.Abort();
            byte[] type = Encoding.Default.GetBytes("start");
            Server_PC.SendCommand(type);
            this.Voice.Enabled = false;
            this.label1.Text = "T语音中";
        }
        public void Change()
        {
            this.Voice.Enabled = true;
            this.label1.Text = "W语音中";
        }
    }
}
