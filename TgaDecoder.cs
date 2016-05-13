using System;
using System.IO;
using System.Drawing;

namespace TgaDecoderTest
{
    public class TgaDecoder
    {
        public static Bitmap FromFile(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    int length = (int)fs.Length;
                    byte[] buffer = new byte[length];
                    fs.Read(buffer, 0, length);
                    return decode(buffer);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        protected static Bitmap decode(byte[] image)
        {
            // ヘッダーファイルを展開
            int idFieldLength = image[0];
            int colorMapType = image[1];
            int imageType = image[2];
            switch (imageType)
            {
                case  0: break; // イメージなし
                case  1: break; // インデックスカラー（256色）
                case  2: break; // フルカラー
                case  3: break; // 白黒
                case  9: break; // インデックスカラー RLE圧縮
                case 10: break; // フルカラー RLE圧縮
                case 11: break; // 白黒 RLE圧縮
            }
            int colorMapIndex = image[4] << 8 | image[3];
            int colorMapLength = image[6] << 8 | image[5];
            int colorMapSize = image[7];
            int imageOriginX = image[9] << 8 | image[8];
            int imageOriginY = image[11] << 8 | image[10];
            int imageWidth = image[13] << 8 | image[12];
            int imageHeight = image[15] << 8 | image[14];
            int bitPerPixel = image[16];
            int descriptor = image[17];

            Bitmap bitmap = new Bitmap(imageWidth, imageHeight);
            bitmap.SetPixel();
            return bitmap;
        }
    }
}
