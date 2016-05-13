using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TgaDecoderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TgaDecoder.FromFile("bakeneko32bit.tga");
        }
    }
}
