
using System;
using NAudio.Wave;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

namespace Information_Home_PC
{
    public class MyAudio
    {
        public IWaveIn waveIn;
        public WaveFileWriter writer;
        public static int i = 0;
        public static byte[] voice;

        // 开始录音的外部接口
        public void StartRecording()
        {
            waveIn = new WaveIn { WaveFormat = new WaveFormat(7900, 1) };   // 将码率设置为最佳
            writer = new WaveFileWriter("test"+i+".wav", waveIn.WaveFormat);
            waveIn.DataAvailable += waveIn_DataAvailable;
            waveIn.RecordingStopped += OnRecordingStopped;
            waveIn.StartRecording();
            if(i == 0)
            {
                i++;
            }
            else
            {
                i--;
            }
        }
        // 停止录音的外部接口
        public void StopRecording()
        {
            waveIn.StopRecording();
            waveIn.Dispose();
            waveIn = null;
            writer.Dispose();
            writer = null;
        }
        // 录音时的操作
        private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            writer.Write(e.Buffer, 0, e.BytesRecorded);
            voice = e.Buffer;
        }
        // 停止录音
        void OnRecordingStopped(object sender, StoppedEventArgs e)
        {
            if (writer != null)
            {
                writer.Dispose();
                writer = null;
            }
        }
        public static void Send_Voice()
        {
            while(true){
                Thread.Sleep(40); 
                Server_PC.SendCommand(voice);
            }
        }
    }
}
