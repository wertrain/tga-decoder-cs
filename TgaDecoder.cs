using System;
using System.IO;
using System.Drawing;

namespace TgaDecoderTest
{
    public class TgaDecoder
    {
        protected class TgaData
        {
            private const int TgaHeaderSize = 18;
            private int idFieldLength;
            private int colorMapType;
            private int imageType;
            private int colorMapIndex;
            private int colorMapLength;
            private int colorMapDepth;
            private int imageOriginX;
            private int imageOriginY;
            private int imageWidth;
            private int imageHeight;
            private int bitPerPixel;
            private int descriptor;
            private byte [] colorData;

            public TgaData(byte[] image)
            {
                this.idFieldLength = image[0];
                this.colorMapType = image[1];
                this.imageType = image[2];
                this.colorMapIndex = image[4] << 8 | image[3];
                this.colorMapLength = image[6] << 8 | image[5];
                this.colorMapDepth = image[7];
                this.imageOriginX = image[9] << 8 | image[8];
                this.imageOriginY = image[11] << 8 | image[10];
                this.imageWidth = image[13] << 8 | image[12];
                this.imageHeight = image[15] << 8 | image[14];
                this.bitPerPixel = image[16];
                this.descriptor = image[17];
                this.colorData = new byte[image.Length - TgaHeaderSize];
                Array.Copy(image, TgaHeaderSize, this.colorData, 0, this.colorData.Length);
            }

            public int Width
            {
                get { return this.imageWidth; }
            }

            public int Height
            {
                get { return this.imageHeight; }
            }

            public int GetPixel(int x, int y)
            {
                if (colorMapType == 0)
                {
                    if (this.bitPerPixel == 32)
                    {
                        int bp = 4;
                        int yy = ((this.descriptor & 0x20) == 0 ? (this.imageHeight - 1 - y) : y) * (this.imageWidth * bp);
                        int xx = ((this.descriptor & 0x10) == 0 ? x : (this.imageWidth - 1 - x)) * bp;
                        int index = yy + xx;
                        int b = this.colorData[index + 0] & 0xFF;
                        int g = this.colorData[index + 1] & 0xFF;
                        int r = this.colorData[index + 2] & 0xFF;
                        int a = this.colorData[index + 3] & 0xFF;
                        return (a << 24) | (r << 16) | (g << 8) | b;
                    }
                    else if (this.bitPerPixel == 24)
                    {
                        int bp = 3;
                        int yy = ((this.descriptor & 0x20) == 0 ? (this.imageHeight - 1 - y) : y) * (this.imageWidth * bp);
                        int xx = ((this.descriptor & 0x10) == 0 ? x : (this.imageWidth - 1 - x)) * bp;
                        int index = yy + xx;
                        int b = this.colorData[index + 0] & 0xFF;
                        int g = this.colorData[index + 1] & 0xFF;
                        int r = this.colorData[index + 2] & 0xFF;
                        return (r << 16) | (g << 8) | b;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    int colormapDataSize = this.bitPerPixel / 8 * this.colorMapLength;
                    int imageDataOffset = colormapDataSize;

                    int offset = (this.colorData[imageDataOffset] & 0xFF) - this.colorMapIndex;

                    int index = 4 * offset;
                    int b = this.colorData[index + 0] & 0xFF;
                    int g = this.colorData[index + 1] & 0xFF;
                    int r = this.colorData[index + 2] & 0xFF;
                    int a = this.colorData[index + 3] & 0xFF;

                    return (a << 24) | (r << 16) | (g << 8) | b;
                }
            }
        }

        public static Bitmap FromFile(string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    int length = (int)fs.Length;
                    byte[] buffer = new byte[length];
                    fs.Read(buffer, 0, length);
                    return decode(buffer);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        protected static Bitmap decode(byte[] image)
        {
            TgaData tga = new TgaData(image);

            Bitmap bitmap = new Bitmap(tga.Width, tga.Height);
            for (int y = 0; y < tga.Height; ++y)
            {
                for (int x = 0; x < tga.Width; ++x)
                {
                    bitmap.SetPixel(x, y, Color.FromArgb(tga.GetPixel(x, y)));
                }
            }
            return bitmap;
        }

    }
}
