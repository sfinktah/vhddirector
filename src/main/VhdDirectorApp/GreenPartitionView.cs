using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using VhdDirectorApp.Properties;

namespace VhdDirectorApp
{
    public partial class GreenPartitionView : UserControl, ITransparentControl, ISharePaintBuffer
    {
        public GreenPartitionView()
        {
            InitializeComponent();
            ReadPrefs();
            ResizeRedraw = true;
        }

        protected void ReadPrefs()
        {
            hue = float.Parse(App.Prefs.GetPreference("Theme.GreenPartition.Hue", "1.0"));
            saturation = float.Parse(App.Prefs.GetPreference("Theme.GreenPartition.Saturation", "1.0"));
            luminance = float.Parse(App.Prefs.GetPreference("Theme.GreenPartition.Luminance", "1.5"));
        }

        public Boolean ProgressBar { get; set; }
        public Color RectangleFillColor { get; set; }
        public Boolean Transparent { get; set; }

#if !NOBAR
        [Browsable(false)]
        public int Maximum { get { return _maximum; } set { _maximum = value; if (RedrawBackgroundIfRequired()) Invalidate(true); } }
        [Browsable(false)]
        public int Value { get { return _value; } set { _value = value; if (RedrawBackgroundIfRequired()) Invalidate(true); } }
     

        public BitArray bitArray;

        protected int _maximum;
        protected int _value;

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
#endif
        protected Bitmap _background = null;
        protected Size _backgroundSize;
        protected Color _backgroundBackColor;

        // public delegate void BufferInvalidated(object source, object eventArgument);
        public event SharePaintBuffer.BufferInvalidated bufferInvalidated;

        public SharePaintBuffer.TryGetBufferResults TryGetBuffer(ref Bitmap bmp, Rectangle rectangle)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            
            RedrawBackgroundIfRequired();
            using (Graphics dc = Graphics.FromImage(bmp))
            {
                Rectangle dstRect = rectangle;
                dstRect.X = 0;
                dstRect.Y = 0;
                dc.DrawImage(_background, dstRect, rectangle, GraphicsUnit.Pixel);
            }

            sw.Stop();
            TimeSpan layoutTime = sw.Elapsed;
            System.Console.WriteLine("TryGetBuffer took: {0} ms", layoutTime.Milliseconds);
            return SharePaintBuffer.TryGetBufferResults.Ok;
        }

        protected float lastSaturation = 1F;
        protected float lastLuminance = 1F;
        protected float lastHue = 1F;

        protected Bitmap GreenPartition_Left;
        protected Bitmap GreenPartition_Right; 
        protected Bitmap GreenPartition_Fill_Unused;
                    
                    //    _background.Dispose();
                    //    _background = _bmp;
                    //    _bmp = null;
                    //}
        protected virtual bool RedrawBackgroundIfRequired(bool force = false)
        {

            //if (!force && lineLength == _lastLineLength && _background != null && this.Size.Equals(_backgroundSize) && this.BackColor.Equals(_backgroundBackColor))
            //{
            //    return false;
            //}

            Graphics dc;


            if (!redraw && _background != null && this.Size.Equals(_backgroundSize) && this.BackColor.Equals(_backgroundBackColor))
            {
                return false;
                // We can skip redrawing the background, since the line is just getting bigger
                // dc = Graphics.FromImage(_background);
            }
            else
            {
                int ShadowSkipLeft = (ShadowLeft ? 0 : 6);
                int ShadowSkipRight = (ShadowRight ? 0 : 6);

                if (_background != null)
                {
                    _background.Dispose();
                }

                if (this.GreenPartition_Left == null || hue != lastHue || saturation != lastSaturation || luminance != lastLuminance)
                {
                    lastHue = hue;
                    lastLuminance = luminance;
                    lastSaturation = saturation;

                    GreenPartition_Left = CSharpFilters.ColorSpace.Hue(VhdDirectorApp.Properties.Resources.GreenPartition_Left, hue, saturation, luminance);
                    GreenPartition_Right = CSharpFilters.ColorSpace.Hue(VhdDirectorApp.Properties.Resources.GreenPartition_Right, hue, saturation, luminance);
                    GreenPartition_Fill_Unused = CSharpFilters.ColorSpace.Hue(VhdDirectorApp.Properties.Resources.GreenPartition_Fill_Unused, hue, saturation, luminance);

                }

                _background = new Bitmap(Size.Width, Size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                dc = Graphics.FromImage(_background);
                {

                    // Draw Partition

                    using (Brush brushBackColor = new SolidBrush(BackColor))
                    {
                        dc.FillRectangle(brushBackColor, ClientRectangle);  // NB: Using ClientRectangle, not Size. Should we change the size checks and events to match?
                    }

                    // Clipping - http://codeidol.com/csharp/windows-forms-programming/Drawing-Basics/Images/
                    // Clip the image to the destination rectangle

                    //Rectangle srcRect = new Rectangle(...);
                    //Rectangle destRect = srcRect;
                    //g.DrawImage(bmp, destRect, srcRect, g.PageUnit);

                    int x = 0;

                    // PartitionLeft
                    {
                        Rectangle srcRect = new Rectangle(new Point(0, 0), this.GreenPartition_Left.Size);
                        Rectangle dstRect = srcRect;
                        if (ShadowSkipLeft > 0)
                        {
                            srcRect.X += ShadowSkipLeft;
                            srcRect.Width -= ShadowSkipLeft;
                            dstRect.Width -= ShadowSkipLeft;
                        }


                        dc.DrawImage(this.GreenPartition_Left, dstRect, srcRect, GraphicsUnit.Pixel);
                        x += this.GreenPartition_Left.Width - ShadowSkipLeft;
                    }

                    // PartitionFill

                    int RightWidth = this.GreenPartition_Right.Width - ShadowSkipRight;
                    for (int i = x; i < ClientRectangle.Width - RightWidth; i += this.GreenPartition_Fill_Unused.Width)
                    {

                        {
                            if (i + this.GreenPartition_Fill_Unused.Width < ClientRectangle.Width - RightWidth)
                            {
                                dc.DrawImageUnscaled(this.GreenPartition_Fill_Unused, i, 0);
                            }
                            else
                            {
                                dc.DrawImageUnscaledAndClipped(this.GreenPartition_Fill_Unused, new Rectangle(i, 0, ClientRectangle.Width - RightWidth - i, this.GreenPartition_Fill_Unused.Height));
                            }
                        }
                    }

                    // PartitionRight

                    // dc.DrawImageUnscaled(this.GreenPartition_Left, 0, 0);
                    // Outer Border
                    dc.DrawImageUnscaled(this.GreenPartition_Right, ClientRectangle.Width - RightWidth, 0);



                    //}

                    //// 0.25F = Burnt Orange
                    //// 0.40F = Yellow
                    //// 2.80F = Upside-down Sunrise
                    //// 0.6 Lime
                    //// 0.4 Gold
                    //// 0.3 Bronze
                    //// 0.2 Orange
                    //// 0.1 Red
                    //// 1.4 Blue
                    //// 1.6 Indigo


                    redraw = false;

                    // Draw white progress hole

                    if (ProgressBar)
                    {
                        // using (Graphics dc = Graphics.FromImage(_background))
                        {

                            // Outer Border
                            // dc.DrawImageUnscaled(Resources.PartitionBar_Left, 0, 0);

                            {
                                Rectangle srcRect = new Rectangle(new Point(0, 0), Resources.PartitionBar_Left.Size);
                                Rectangle dstRect = srcRect;
                                if (ShadowSkipLeft > 0)
                                {
                                    srcRect.X += ShadowSkipLeft;
                                    srcRect.Width -= ShadowSkipLeft;
                                    dstRect.Width -= ShadowSkipLeft;
                                }


                                dc.DrawImage(Resources.PartitionBar_Left, dstRect, srcRect, GraphicsUnit.Pixel);
                                // x += this.GreenPartition_Left.Width - ShadowSkipLeft;
                            }

                            int startX = Resources.PartitionBar_Left.Width - ShadowSkipLeft;
                            int endX = ClientRectangle.Width - Resources.PartitionBar_Right.Width + ShadowSkipRight;

                            for (int i = startX; i < endX; i += Resources.PartitionBar_Fill_Unused.Width)
                            {
                                if (i + Resources.PartitionBar_Fill_Unused.Width < endX)
                                {
                                    dc.DrawImageUnscaled(Resources.PartitionBar_Fill_Unused, i, 0);
                                }
                                else
                                {
                                    dc.DrawImageUnscaledAndClipped(Resources.PartitionBar_Fill_Unused, new Rectangle(i, 0, endX - i, Resources.PartitionBar_Fill_Unused.Height));
                                }
                            }
                            dc.DrawImageUnscaled(Resources.PartitionBar_Right, endX, 0);

                        }
                    }

                    // return false;

                }
#if true
                if (ProgressBar)
                {

                    // Progress Bar (Inner)
                    // int startX = Resources.InnerLeft.Width - ShadowSkipLeft;
                    // int endX = ClientRectangle.Width - Resources.InnerRight.Width + ShadowSkipLeft + ShadowSkipRight;

                    int innerMargin = 16; // 16 pixels to start or end of where the black line is drawn (13 pixels to start/end of white space itself)
                    int innerLeftPadding = innerMargin + Resources.InnerLeft.Width - ShadowSkipLeft;
                    int innerRightPadding = innerMargin + Resources.InnerRight.Width - ShadowSkipRight;
                    int innerStart = innerLeftPadding;
                    int innerEnd = Width - innerRightPadding;
                    int fillPixels = innerEnd - innerStart;
                    double pixelsPerUnit = (_maximum - 3D) / fillPixels; // Maximum = Full (with closing corner), 0 = No inner line, 1 = opening corner. (hence, -3).
                    int lineLength = (int)(fillPixels * pixelsPerUnit * _value); // +6;

                    // Progress bar
                    // using (Graphics dc = Graphics.FromImage(_background))
                    {

                        // BitArray shaded progress bar
                        if (bitArray != null)
                        {

                            // Draw opening curve?
                            if (bitArray.Get(0))
                            {
                                dc.DrawImageUnscaled(Resources.InnerLeft, innerStart - Resources.InnerLeft.Width, 56);
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
                                // dc.DrawImageUnscaled(baBitmap, innerStart, 4);
                                dc.DrawImageUnscaled(baBitmap, innerStart, 60);
                            }




                            // Draw closing curve?
#if false
                        if (bitArray.Get(bitArray.Length - 1))
                        {
                            dc.DrawImageUnscaled(Resources.InnerRight, innerEnd, 0);

                        }
#endif


                        }
                        else
                        {
                            // Standard linear progress bar

                            // Draw opening curve?
                            if (_value > 0)
                            {
                                dc.DrawImageUnscaled(Resources.InnerLeft, innerMargin, 56);
                                for (int i = innerStart; i < lineLength; i++)
                                {
                                    dc.DrawImageUnscaled(Resources.InnerFill, i, 56);
                                }
                            }

                            // Draw closing curve?
                            if (_value > 0 && _value >= _maximum)
                            {
                                dc.DrawImageUnscaled(Resources.InnerRight, innerEnd, 56);
                            }
                        }
                    }

                    // _lastLineLength = lineLength;
#endif
                }
            }
            dc.Dispose();

            if (bufferInvalidated != null)
            {
                bufferInvalidated(this, _background);
            }
            _backgroundSize = Size;
            _backgroundBackColor = BackColor;
            return true;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // base.OnPaintBackground(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            // base.OnPaint(e);
            using (new csharp_debug.SectionStopWatch("GreenPartitionView::OnPaint"))
            {
                RedrawBackgroundIfRequired();
                e.Graphics.DrawImageUnscaled(_background, 0, 0);
                base.OnPaint(e);
            }
        }

        protected void redraw_lines()
        {
            label1.Text = _line1 + Environment.NewLine + _line2;
        }

        public bool redraw = false;
        public void InvalidateBuffer(bool invalidateChildren = false)
        {
            redraw = true;
            foreach (var c in Controls)
            {
                if (c is ISharePaintBuffer)
                {
                    (c as ISharePaintBuffer).InvalidateBuffer(invalidateChildren);
                }
            }
        }

        protected float hue = 1F;
        protected float saturation = 1F;
        protected float luminance = 1F;

        // [Description("ShadowLeft")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [CategoryAttribute("Appearance")]
        [DefaultValue(1F)]
        public float Hue { get { return hue; } set { hue = value; } }
        [RefreshProperties(RefreshProperties.Repaint)]
        [CategoryAttribute("Appearance")]
        [DefaultValue(1F)]
        public float Saturation { get { return saturation; } set { saturation = value; } }
        [RefreshProperties(RefreshProperties.Repaint)]
        [CategoryAttribute("Appearance")]
        [DefaultValue(1F)]
        public float Luminance { get { return luminance; } set { luminance = value; } }

        public bool AdjustHue { get; set; }

        protected string _line1 = string.Empty;
        protected string _line2 = string.Empty;
        protected string _line3 = string.Empty;

        [Browsable(false)]
        public string line1 { get { return _line1; } set { _line1 = value; redraw_lines(); } }

        [Browsable(false)]
        public string line2 { get { return _line2; } set { _line2 = value; redraw_lines(); } }
        
        [Browsable(false)]
        public string line3 { get { return _line3; } set { _line3 = value; redraw_lines(); } }

        [Description("ShadowLeft")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [CategoryAttribute("Shadow")]
        [DefaultValue(true)]
        public bool ShadowLeft { get; set; }

        [Description("ShadowRight")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [CategoryAttribute("Shadow")]
        [DefaultValue(true)]
        public bool ShadowRight { get; set; }

        [Browsable(false)]
        public BitArray ClusterUsage { get { return BitArray; } set { BitArray = value; } }

        [Browsable(false)]
        public BitArray BitArray { get { return bitArray; } set { if (value != null) { bitArray = value; _maximum = value.Length; if (RedrawBackgroundIfRequired(true)) Invalidate(true); } } }

        private void GreenPartitionView_Load(object sender, EventArgs e)
        {
            label1.Location = new Point(label1.Location.X + (ShadowLeft ? 6 : 0), label1.Location.Y);
        }

    }

}

