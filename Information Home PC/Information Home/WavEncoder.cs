using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Server_pc
{
    public class WavEncoder
    {
        // Some consts. Refer to http://www.sonicspot.com/guide/wavefiles.html.
        public int SamplesPerSecond { get; set; }
        public int NumChannels { get; set; }
        public int BitsPerSample { get; set; }
        public int BlockAlign { get; set; }
        public int AverageBytesPerSecond { get; set; }

        public WavEncoder(int samplesPerSecond = 16000)
        {
            this.SamplesPerSecond = samplesPerSecond;
            this.NumChannels = 1;
            this.BitsPerSample = 16;
            this.BlockAlign = this.BitsPerSample / 8 * this.NumChannels;
            this.AverageBytesPerSecond = this.SamplesPerSecond * this.BlockAlign;
        }

        public Stream Encode(Stream inputStream)
        {
            if (inputStream == null || !inputStream.CanRead)
            {
                throw new ArgumentNullException("inputStream");
            }

            if (inputStream.Length == 0)
            {
                throw new ArgumentOutOfRangeException("Length", "The stream is empty.");
            }

            if (inputStream.CanSeek)
            {
                inputStream.Position = 0;
            }
            Encoding utf8 = Encoding.UTF8;
            MemoryStream wavStream = new MemoryStream();

            // Capture the length of the raw audio stream.
            int dataLength = (int)inputStream.Length;

            // RIFF
            wavStream.Write(utf8.GetBytes("RIFF"), 0, 4);

            // We can't obtain the file size now. So we use a place holder, and skip to WAVE.
            wavStream.Write(new byte[4], 0, 4);
            wavStream.Write(utf8.GetBytes("WAVE"), 0, 4);

            // Header chunk ID (always "fmt")
            wavStream.Write(utf8.GetBytes("fmt "), 0, 4);

            // Chunk data size (always 16 in this case)
            wavStream.Write(BitConverter.GetBytes(16), 0, 4);

            // Compression code (1 for PCM)
            wavStream.Write(BitConverter.GetBytes(1), 0, 2);

            // Number of channels (1 since we recorded with a single microphone)
            wavStream.Write(BitConverter.GetBytes(NumChannels), 0, 2);

            // Samples per second
            wavStream.Write(BitConverter.GetBytes(this.SamplesPerSecond), 0, 4);

            // Average bytes per second
            wavStream.Write(BitConverter.GetBytes(AverageBytesPerSecond), 0, 4);

            // Block align
            wavStream.Write(BitConverter.GetBytes(BlockAlign), 0, 2);

            // Bits per sample
            wavStream.Write(BitConverter.GetBytes(this.BitsPerSample), 0, 2);


            // Extra format bytes don't exist for PCM. So we don't write it.

            // Data chunk ID (always "data")
            wavStream.Write(utf8.GetBytes("data"), 0, 4);

            // Data chunk size (the raw audio stream's length)
            wavStream.Write(BitConverter.GetBytes(dataLength), 0, 4);


            inputStream.CopyTo(wavStream);

            // Finally, populate the RIFF chunk data size, which begins from 4.
            // The value is file size - 8.
            wavStream.Position = 4;
            wavStream.Write(BitConverter.GetBytes((int)inputStream.Length - 8), 0, 4);

            wavStream.Position = 0;
            return wavStream;
        }
    }
}
