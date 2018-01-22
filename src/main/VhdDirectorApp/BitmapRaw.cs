using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace VhdDirectorApp
{
    public class BitmapRaw
    {
        public int Width = 16;
        public int Height = 16;
        public byte[,] AlphaMap = new byte[,] {
            {00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00},
            {00,05,10,10,10,10,10,10,10,10,10,10,10,10,05,00},
            {00,10,99,99,99,99,99,99,99,99,99,99,99,99,10,00},
            {00,15,99,99,99,99,99,99,99,99,99,99,99,99,15,00},
            {00,15,99,99,99,99,99,99,99,99,99,99,99,99,15,00},
            {00,15,99,99,99,99,99,99,99,99,99,99,99,99,15,00},
            {00,15,99,99,99,99,99,99,99,99,99,99,99,99,15,00},
            {00,15,99,99,99,99,99,99,99,99,99,99,99,99,15,00},
            {00,15,99,99,99,99,99,99,99,99,99,99,99,99,15,00},
            {00,15,99,99,99,99,99,99,99,99,99,99,99,99,15,00},
            {00,15,99,99,99,99,99,99,99,99,99,99,99,99,15,00},
            {00,15,99,99,99,99,99,99,99,99,99,99,99,99,15,00},
            {00,15,99,99,99,99,99,99,99,99,99,99,99,99,15,00},
            {00,15,99,99,99,99,99,99,99,99,99,99,99,99,15,00},
            {00,10,35,50,50,50,50,50,50,50,50,50,50,35,10,00},
            {00,05,10,15,15,15,15,15,15,15,15,15,15,10,05,00}
        };
        public int[,] ColorMap = new int[,] {
                
            {00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00},
            {00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00},
            {00,00,99,99,99,99,99,99,99,99,99,99,99,99,00,00},
            {00,00,99,99,99,99,99,99,99,99,99,99,99,99,00,00},
            {00,00,99,99,99,99,99,99,99,99,99,99,99,99,00,00},
            {00,00,99,99,99,99,99,99,99,99,99,99,99,99,00,00},
            {00,00,99,99,99,99,99,99,99,99,99,99,99,99,00,00},
            {00,00,99,99,99,99,99,99,99,99,99,99,99,99,00,00},
            {00,00,99,99,99,99,99,99,99,99,99,99,99,99,00,00},
            {00,00,99,99,99,99,99,99,99,99,99,99,99,99,00,00},
            {00,00,99,99,99,99,99,99,99,99,99,99,99,99,00,00},
            {00,00,99,99,99,99,99,99,99,99,99,99,99,99,00,00},
            {00,00,99,99,99,99,99,99,99,99,99,99,99,99,00,00},
            {00,00,99,99,99,99,99,99,99,99,99,99,99,99,00,00},
            {00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00},
            {00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00}
        };

        public Color FillColor { get { return _FillColor; } set { _FillColor = value; Apply_FillColor(); } }
        protected Color _FillColor;
        public byte[,] AppliedAlphaMap;
        public int[,] AppliedColorMap;

        public void Apply_FillColor()
        {

   
            int bound0 = AlphaMap.GetUpperBound(0);
            int bound1 = AlphaMap.GetUpperBound(1);

            Array array = Array.CreateInstance(typeof(byte), bound0, bound1);
            AppliedAlphaMap = (byte[,])array;

            array = Array.CreateInstance(typeof(int), bound0, bound1);
            AppliedColorMap = (int[,])array;

            AppliedAlphaMap = new byte[16,16];
            AppliedColorMap = new int[16, 16];


            for (int i = 0; i < Height; i++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (AlphaMap[i, x] == 99)
                    {
                        AppliedAlphaMap[i, x] = 255;
                    }
                    else if (AlphaMap[i, x] == 0) {
                        AppliedAlphaMap[i, x] = AlphaMap[i, x];
                    }
                    else
                    {
                        AppliedAlphaMap[i, x] = (byte)(AlphaMap[i, x] * 256.0 / 100.0);
                    }
                }
            }

            int fill = _FillColor.ToArgb();

            bound0 = ColorMap.GetUpperBound(0);
            bound1 = ColorMap.GetUpperBound(1);
            for (int i = 0; i < Height; i++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (ColorMap[i, x] == 99)
                    {
                        AppliedColorMap[i, x] = fill;
                    }
                    else
                    {
                        int shade = (int)(byte)(ColorMap[i, x] * 256.0 / 100.0);
                        AppliedColorMap[i, x] = Color.FromArgb(AlphaMap[i, x], shade, shade, shade).ToArgb();
                    }
                }
            }
        }
       
        public void DrawBitmap(Bitmap TargetBitmap, int X, int Y)
        {
            int spriteWidth = AlphaMap.GetUpperBound(0);
            int spriteHeight = AlphaMap.GetUpperBound(1);

            spriteWidth = Width;
            spriteHeight = Height;

            int fill = _FillColor.ToArgb();

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    TargetBitmap.SetPixel(x, y, Color.FromArgb(AppliedColorMap[y, x]));
                }
            }

            //BitmapData bmData = TargetBitmap.LockBits(new Rectangle(X, Y, spriteWidth, spriteHeight),
            //   ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb /* Format24bppRgb */);
            //int stride = bmData.Stride;

            //System.IntPtr Scan0 = bmData.Scan0;

            //unsafe
            //{
            //    byte* p = (byte*)(void*)Scan0;
            //    int* ip = (int*)(void*)Scan0;
            //    int nOffset = stride - Width; // *4;
            //    int nWidth = Width; // *4;
            //    for (int y = 0; y < Height; ++y)
            //    {
            //        for (int x = 0; x < nWidth; ++x)
            //        {
            //            // p[0] = (byte)(255 - p[0]); // Alpha Bit
            //            // ++p;

            //            ip[0] = AppliedColorMap[y, x];
            //            ++ip;
            //        }
            //        ip += nOffset;
            //        p += nOffset;
            //    }
            //}

            //TargetBitmap.UnlockBits(bmData);

        }
    }


}
