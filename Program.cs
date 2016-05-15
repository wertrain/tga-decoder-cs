using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TgaDecoderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"../../SampleData/";
            string [] samples = { @"bakeneko32bitLU", @"bakeneko32bitLD", @"bakeneko32bitLU_RLE", @"bakeneko32bitLD_RLE" };
            for (int i = 0; i < samples.Length; ++i)
            {
                Bitmap bmp = TgaDecoder.FromFile(path + samples[i] + ".tga");
                bmp.Save(path + samples[i] + ".bmp");
            }
        }
    }
}
