using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Kids.Utility
{
    public class WaveProcessor
    {
        // Public Fields can be used for various operations
        private int Length;
        private short Channels;
        private int SampleRate;
        private int DataLength;
        private short BitsPerSample;
        //public ushort MaxAudioLevel;

        private static WaveProcessor WaveHeaderIN(char Digit)
        {

            String Path = HttpContext.Current.Server.MapPath(String.Format("~/Epayment/DigitVoice/{0}.WAV", Digit));
            FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read);

            BinaryReader br = new BinaryReader(fs);
            try
            {
                WaveProcessor Header = new WaveProcessor {Length = (int) fs.Length - 8};

                fs.Position = 22;

                Header.Channels = br.ReadInt16(); //1
                fs.Position = 24;

                Header.SampleRate = br.ReadInt32(); //8000
                fs.Position = 34;

                Header.BitsPerSample = br.ReadInt16(); //16

                Header.DataLength = (int)fs.Length - 44;
                byte[] arrfile = new byte[fs.Length - 44];
                fs.Position = 44;
                fs.Read(arrfile, 0, arrfile.Length);
                return Header;

            }
            finally
            {
                br.Close();
                fs.Close();
            }

        }

        private static IEnumerable<byte> MakeWaveHeader(WaveProcessor Wave)
        {
            MemoryStream fs = new MemoryStream();

            BinaryWriter bw = new BinaryWriter(fs);

            fs.Position = 0;
            bw.Write(new[] { 'R', 'I', 'F', 'F' });

            bw.Write(Wave.Length);

            bw.Write(new[] { 'W', 'A', 'V', 'E', 'f', 'm', 't', ' ' });

            bw.Write(16);

            bw.Write((short)1);
            bw.Write(Wave.Channels);

            bw.Write(Wave.SampleRate);

            bw.Write((Wave.SampleRate * ((Wave.BitsPerSample * Wave.Channels) / 8)));

            bw.Write((short)((Wave.BitsPerSample * Wave.Channels) / 8));

            bw.Write(Wave.BitsPerSample);

            bw.Write(new[] { 'd', 'a', 't', 'a' });
            bw.Write(Wave.DataLength);

            bw.Close();
            Byte[] Result = fs.ToArray();
            fs.Close();

            return Result;
        }


        public static List<Byte> Merge(char[] VoiceString)
        {
            List<Byte> Result = new List<byte>();

            //Gather header data
            WaveProcessor wav = WaveHeaderIN(VoiceString[0]);
            wav.DataLength = 0;
            wav.Length = 0;



            //Write data - modified code

            foreach (char d in VoiceString)
            {
                WaveProcessor wa = WaveHeaderIN(d);
                wav.Length += wa.Length;
                wav.DataLength += wa.DataLength;
                Result.AddRange(GetWAVEData(d));
            }
            Result.InsertRange(0, MakeWaveHeader(wav));


            return Result;
        }


        private static IEnumerable<byte> GetWAVEData(char Digit)
        {
            if (HttpContext.Current.Cache["Voice_" + Digit] == null)
            {
                String Path = HttpContext.Current.Server.MapPath(string.Format("~/Epayment/DigitVoice/{0}.WAV", Digit));
                FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read);
                byte[] arrfile = new byte[fs.Length - 44];
                fs.Position = 44;
                fs.Read(arrfile, 0, arrfile.Length);
                fs.Close();
                HttpContext.Current.Cache["Voice_" + Digit] = arrfile;

            }
            return HttpContext.Current.Cache["Voice_" + Digit] as byte[];

        }




    }
}
