using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace VHD_Director
{
    public partial class BitArrayBar : UserControl
    {
        public Bitmap BitmapFull { get { return _bitmapFullsize; } }
        protected Bitmap _bitmapFullsize;
        protected Bitmap _bitmap;
        public BitArray ba;
        public Boolean Group256 { get; set; }
        protected int _baTrueCount = 0;
        public int baValue { get { return _baTrueCount; } }
        public int baMaximum { get { return (ba == null) ? 0 : ba.Length; } }
        protected byte[] grouped;
        public BitArrayBar()
        {
            InitializeComponent();
            ResizeRedraw = true;
        }

        //public BitArrayBar(BitArray ba) : this()
        //{
        //    this.ba = ba;
        //}

        public void MakeBitmap()
        {
            if (this.ba == null)
            {
                return;
            }

            if (this._bitmapFullsize != null)
            {
                return;
            }
            if (!Group256)
            {
                this._bitmapFullsize = new Bitmap(ba.Count, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                for (int x = 0; x < ba.Count; x++)
                {
                    this._bitmapFullsize.SetPixel(x, 0, ba.Get(x) ? TrueColor : FalseColor);
                }
                return;
            }

            if (Group256)
            {
                if (grouped == null)
                {
                    _baTrueCount = 0;
                    byte counter = 0;
                    int pos = 0;
                    grouped = new byte[1 + (ba.Count / 255)];
                    for (int x = 0; x < ba.Count; x++)
                    {
                        if (ba.Get(x))
                        {
                            _baTrueCount++;
                            grouped[pos]++;
                        }
                        if (++counter == 255)
                        {
                            counter = 0;
                            pos++;
                        }
                    }
                    this._bitmapFullsize = new Bitmap(grouped.Length, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                    for (int x = 0; x < grouped.Length; x++)
                    {
                        // 

                        // this._bitmapFullsize.SetPixel(x, 0, Color.FromArgb(grouped[x], TrueColor));
                        if (grouped[x] > 0)
                        {
                            this._bitmapFullsize.SetPixel(x, 0, Color.FromArgb(grouped[x], TrueColor));
                        } else {
                            // this._bitmapFullsize.SetPixel(x, 0, FalseColor);
                        }
                    }
                }
                return;
            }


        }


        public Bitmap MakeScreenBitmap()
        {
            _bitmap = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (Graphics dc = Graphics.FromImage(_bitmap))
            {
                dc.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                for (int y = 0; y < Height; y++)
                {
                    dc.DrawImage(_bitmapFullsize, 0, y, Width, 1);
                }
            }
            return _bitmap;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_bitmapFullsize == null)
            {
                return;
            }
            // base.OnPaint(e);
            if (_bitmap == null || _bitmap.Width != Width)
            {
                if (_bitmap != null)
                {
                    _bitmap.Dispose();
                }

                MakeScreenBitmap();
            }

            e.Graphics.DrawImageUnscaled(_bitmap, 0, 0);
        }
            
        public Color TrueColor { get; set; }
        public Color FalseColor { get; set; }
        public BitArray bitArray { get { return ba; } set { ba = value; MakeBitmap(); } }
    }
}
