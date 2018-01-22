using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VHD_Director.Properties;
using System.Collections;

namespace VHD_Director
{
    public partial class BlackProgressBar : UserControl, ITransparentControl
    {
        public BlackProgressBar()
        {
            InitializeComponent();
            ResizeRedraw = true;
        }

        public Color RectangleFillColor { get; set; }
        public Boolean Transparent { get; set; }
        public int Maximum { get { return _maximum; } set { _maximum = value; if (RedrawBackgroundIfRequired()) Invalidate(true); } }
        public int Value { get { return _value; } set { _value = value; if (RedrawBackgroundIfRequired()) Invalidate(true); } }
        public BitArray BitArray { get { return bitArray; } set { if (value != null) { bitArray = value; _maximum = value.Length; if (RedrawBackgroundIfRequired(true)) Invalidate(true); } } }
        public BitArray bitArray;

        protected int _maximum;
        protected int _value;
        protected Bitmap _background = null;
        protected Size _backgroundSize;
        protected Color _backgroundBackColor;
        protected int _lastLineLength;
        protected BitArrayBar babar;

        public Bitmap BitArrayBarBitmapFull
        {
            get
            {
                if (babar == null) return null;
                return babar.BitmapFull;
            }
        }

        protected virtual bool RedrawBackgroundIfRequired(bool force = false)
        {
            // Progress Bar (Inner)
            int innerMargin = 5;
            int innerLeft = innerMargin + Resources.InnerLeft.Width;
            int innerRight = innerMargin + Resources.InnerRight.Width;
            int innerStart = innerLeft;
            int innerEnd = Width - innerRight;
            int fillPixels = innerEnd - innerStart;
            double pixelsPerUnit = (_maximum - 3D) / fillPixels; // Maximum = Full (with closing corner), 0 = No inner line, 1 = opening corner. (hence, -3).
            int lineLength = (int)(fillPixels * pixelsPerUnit * _value);

            if (!force && lineLength == _lastLineLength && _background != null && this.Size.Equals(_backgroundSize) && this.BackColor.Equals(_backgroundBackColor))
            {
                return false;
            }

            Graphics dc;

            if (_background != null && this.Size.Equals(_backgroundSize) && this.BackColor.Equals(_backgroundBackColor) && (bitArray == null && lineLength >= _lastLineLength))
            {
                // We can skip redrawing the background, since the line is just getting bigger
                dc = Graphics.FromImage(_background);
            }
            else
            {
                if (_background != null)
                {
                    _background.Dispose();
                }

                _background = new Bitmap(Size.Width, Size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                dc = Graphics.FromImage(_background);

                using (Brush brushBackColor = new SolidBrush(BackColor))
                {
                    dc.FillRectangle(brushBackColor, ClientRectangle);  // NB: Using ClientRectangle, not Size. Should we change the size checks and events to match?
                }

                // Outer Border
                dc.DrawImageUnscaled(Resources.OuterLeft, 0, 0);
                for (int i = Resources.OuterLeft.Width; i < ClientRectangle.Width - Resources.OuterRight.Width; i++)
                {
                    dc.DrawImageUnscaled(Resources.OuterFill, i, 0);
                }
                dc.DrawImageUnscaled(Resources.OuterRight, ClientRectangle.Width - Resources.OuterRight.Width, 0);

            }

            // Progress bar

            // BitArray shaded progress bar
            if (bitArray != null)
            {
                // Draw opening curve?
                if (bitArray.Get(0))
                {
                    dc.DrawImageUnscaled(Resources.InnerLeft, innerMargin, 0);
                }

                // Draw the progress line using BitArrayBar
                if (babar == null)
                {
                    babar = new BitArrayBar();

                    babar.SuspendLayout();
                    babar.Group256 = true;
                    babar.Height = 6;
                    babar.FalseColor = Color.White;
                    babar.TrueColor = Color.Black;
                    babar.ba = bitArray;
                    babar.MakeBitmap();
                    this._value = babar.baValue;
                    this._maximum = babar.baMaximum;
                    babar.ResumeLayout();
                }
                babar.Width = fillPixels;

                using (Bitmap baBitmap = babar.MakeScreenBitmap())
                {
                    // this.Controls.Add(babar);
                    dc.DrawImageUnscaled(baBitmap, innerStart, 4);
                }




                // Draw closing curve?
                if (bitArray.Get(bitArray.Length - 1))
                {
                    dc.DrawImageUnscaled(Resources.InnerRight, innerEnd, 0);

                }



            }
            else
            {


                // Draw opening curve?
                if (_value > 0)
                {
                    dc.DrawImageUnscaled(Resources.InnerLeft, innerMargin, 0);
                    for (int i = innerStart; i < lineLength; i++)
                    {
                        dc.DrawImageUnscaled(Resources.InnerFill, i, 0);
                    }
                }

                // Draw closing curve?
                if (_value > 0 && _value >= _maximum)
                {
                    dc.DrawImageUnscaled(Resources.InnerRight, innerEnd, 0);
                }
            }

            dc.Dispose();

            _backgroundSize = Size;
            _backgroundBackColor = BackColor;
            _lastLineLength = lineLength;

            return true;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // base.OnPaintBackground(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            // base.OnPaint(e);
            RedrawBackgroundIfRequired();
            e.Graphics.DrawImageUnscaled(_background, 0, 0);
            base.OnPaint(e);
        }
    }
}
