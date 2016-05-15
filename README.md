## Simple Tga Decoder for C\# 

### Tga file to Bitmap object

    System.Drawing.Bitmap bmp = TgaDecoder.FromFile("sample.tga");
    bmp.Save("sample.bmp");

### Byte array to Bitmap object

    byte [] tgaImage = ...
    System.Drawing.Bitmap bmp = TgaDecoder.FromBinary(tgaImage);
    
    System.Windows.Forms.PictureBox pictureBox = new System.Windows.Forms.PictureBox();
    pictureBox.Image = bmp;
