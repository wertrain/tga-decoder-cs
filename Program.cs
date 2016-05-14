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
            Bitmap bmp = TgaDecoder.FromFile("bakeneko32bitLU.tga");
            bmp.Save("bakeneko32bitLU.bmp");

            bmp = TgaDecoder.FromFile("bakeneko32bitLD.tga");
            bmp.Save("bakeneko32bitLD.bmp");
        }
    }
}
