using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace VHD_Director
{
    public partial class TransparentControl : UserControl, ITransparentControl
    {
        public TransparentControl()
        {
            InitializeComponent();
        }
        [Description("Color of inner filler")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [CategoryAttribute("Shape")]
        public Color RectangleFillColor { get; set; }
#if !DISABLED
        protected int lastBackgroundWidth = 0;
        protected int lastBackgroundHeight = 0;
        protected Rectangle lastBackgroundRect = new Rectangle(0, 0, 0, 0);

        [Description("True Transparency")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [CategoryAttribute("Shape")]
        [DefaultValue(false)]
        public bool Transparent { get; set; }
  

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            csharp_debug.Debug.DebugWinControl(this);

            if (!Transparent)
            {
                base.OnPaintBackground(e);
                PaintOurBackground(e);
                return;
            }

            Rectangle rect = new Rectangle(Left, Top, Width, Height);
            if (rect.Equals(lastBackgroundRect))
            {
                return;
            }

            lastBackgroundRect = rect;
            csharp_debug.Debug.DebugWinControl(this);
            // base.OnPaintBackground(e);
           
            PaintParentBackground(e);
            return;

            //Graphics dc = e.Graphics;
            //dc.DrawImage(_backbuff.bitmap, 0, 0);
            //dc.Dispose();

        }


        protected Rectangle lastPaintRect = new Rectangle(0, 0, 0, 0);

        protected override void OnPaint(PaintEventArgs e)
        {
            csharp_debug.Debug.DebugWinControl(this);

            if (!Transparent)
            {
                base.OnPaint(e);
                PaintOurForeground(e);
                return;
            }

            Rectangle rect = e.ClipRectangle;
            if (rect.Equals(lastPaintRect))
            {
               // return;
            }
            lastPaintRect = rect;
            csharp_debug.Debug.DebugWinControl(this);

            Stopwatch sw = new Stopwatch();
            sw.Start();

#if DEBUG_ONPAINT
            System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
            messageBoxCS.AppendFormat("{0} = {1}", "ClipRectangle", e.ClipRectangle);
            messageBoxCS.AppendLine();
            messageBoxCS.AppendFormat("{0} = {1}", "Graphics", e.Graphics);
            messageBoxCS.AppendLine();
            System.Console.Write(messageBoxCS);
#endif
            // We're painting the invalid rectange's background, but
            // drawing the entire client's squares.  (TODO)
            // base.OnPaintBackground(e);

            //if (_backbuff == null)
            //{
            //    throw new Exception("Trying to paint buffer before it's been drawn");
            //}


            Bitmap _backBuffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            Graphics dc = Graphics.FromImage(_backBuffer);

            // Graphics dc = this.CreateGraphics();
            // Graphics dc = e.Graphics;
            PaintOurForeground(e);

            dc.Dispose();
            e.Graphics.DrawImage(_backBuffer, e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle, GraphicsUnit.Pixel);
      // Do your painting here, or call base.OnPaint
            // sw.Stop();
            // paintTime = sw.Elapsed;
        }
        
        private const int WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x00000020;

        protected override CreateParams CreateParams
        {

            get
            {
                CreateParams cp = base.CreateParams;
                if (Transparent)
                {

                    cp.ExStyle |= WS_EX_TRANSPARENT;
                    // cp.ExStyle |= WS_EX_LAYERED;
                }
                return cp;
            }
        }
        protected virtual void PaintOurBackground(PaintEventArgs e) { }

        protected virtual void PaintOurForeground(PaintEventArgs e)
        {  
        }


        // protected BackBuffer _backbuff;
        // protected Rectangle drawingArea;
        // protected Size _backbuffSize;
        // protected int _clientWidth;
        // protected int _clientHeight;
        // protected int _lineHeight;
        // protected bool doneInitLayout = false;

        public int clientWidth
        {
            get { return ClientSize.Width; }
            // set { _clientWidth = value; }
        }

        public int clientHeight
        {
            get { return ClientSize.Height; }
            // set { _clientHeight = value; }
        }

        //protected void GetBitmaps()
        //{
        //    InvalidateEx();
        //    Graphics g = _backbuff.GetGraphics();
        //    CopyParentBackground(ClientRectangle, g);
        //    _backbuff.DisposeGraphics();
        //}

        private void PaintParentBackground(PaintEventArgs e)
        {
            if (!Transparent)
            {
                // This probably shouldn't be called in non transparent mode, but we could insert a FillRectangle of bg color
                return;

            }
            if (Parent != null)
            {
                Rectangle rect = new Rectangle(Left, Top,
                                               Width, Height);

                e.Graphics.TranslateTransform(-rect.X, -rect.Y);

                try
                {
                    using (PaintEventArgs pea =
                                new PaintEventArgs(e.Graphics, rect))
                    {
                        pea.Graphics.SetClip(rect);
                        InvokePaintBackground(Parent, pea);
                        InvokePaint(Parent, pea);
                    }
                }
                finally
                {
                    e.Graphics.TranslateTransform(rect.X, rect.Y);
                }
            }
            else
            {
                e.Graphics.FillRectangle(SystemBrushes.Control,
                                         ClientRectangle);
            }
        }
        //protected void CopyParentBackground(Rectangle rect, Graphics graphics)
        //{

        //    if (Parent == null) return;

        //    Rectangle dst = new Rectangle(rect.Location, rect.Size);
        //    Point p = PointToScreen(rect.Location);
        //    Rectangle src = new Rectangle(Parent.PointToClient(p), rect.Size);

        //    Bitmap bmp = new Bitmap(rect.Width, rect.Height);
        //    DrawToBitmap(bmp, rect);
        //    graphics.DrawImageUnscaled(bmp, 0, 0);
        //    bmp.Dispose();
        //}

        private static Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return (Image)(bmpCrop);
        }



        //protected override void InitLayout()
        //{
        //    System.Console.WriteLine("InitLayout: {0}x{1}", Size.Width, Size.Height);
        //    base.InitLayout();

        //    doneInitLayout = true;
        //}


        protected bool doneOurLayout;
        protected bool doneInitLayout;

        protected override void InitLayout()
        {
            // Called after the control has been added to another container.
            // The InitLayout method is called immediately after adding a control to a container. 
            // The InitLayout method enables a control to initialize its layout state based on its container. 

            // For example, you would typically apply anchoring and docking to the control in the InitLayout method.

            // When overriding InitLayout in a derived class, be sure to call the base class's InitLayout method so that the control is displayed correctly.
        
            csharp_debug.Debug.DebugWinControl(this);
            base.InitLayout();

            if (this.Parent is TransparentControl) {
                this.BackColor = ((TransparentControl)Parent).RectangleFillColor;
            }

            if (this.RectangleFillColor.A > 0) {
                foreach (Control c in this.Controls)
                {
                    if (c is TransparentLabel)
                    {
                        c.BackColor = this.RectangleFillColor;  // Quick
                        // c.BackColor = Color.Transparent;     // Correct (but slow)
                    }
                }
            }

            doneInitLayout = true;
        }
        
        

        protected override void OnLayout(LayoutEventArgs e)
        {
            // The Layout event occurs when child controls are added or removed, when the bounds of the control changes, and when 
            // other changes occur that can affect the layout of the control.

            // The layout event can be suppressed using the 
            // SuspendLayout and ResumeLayout methods. Suspending layout enables you to perform multiple actions on a control 
            // without having to perform a layout for each change.
            // For example, if you resize and move a control, each operation would raise a Layout event.

            csharp_debug.Debug.DebugWinControl(this);

            if (doneInitLayout && !doneOurLayout)
            {
                // Stopwatch sw = new Stopwatch();
                // sw.Start();
                base.OnLayout(e);
                OurLayout();
                // sw.Stop();
                // layoutTime = sw.Elapsed;

                doneOurLayout = true;
            }
            else
            {
                base.OnLayout(e);
            }
        }


        protected virtual void OurLayout()
        {
            // csharp_debug.Debug.DebugWinControl(this);
        }

        //private TimeSpan paintTime = new TimeSpan(250);   // Time to paint, default=250ms
        //private TimeSpan resizeTime = new TimeSpan(100);  // Time to update layout, default=100ms
        //private TimeSpan layoutTime = new TimeSpan(100);  // Time to update layout, default=100ms
        //private Timer resizeTimer; // = new Timer();
        



        protected override void OnResize(EventArgs e)
        {
            csharp_debug.Debug.DebugWinControl(this);

            // The "Stop" is not redundant - it will force the timer to "reset"
            // if it is already running.
            //if (resizeTimer == null)
            //{
            //    resizeTimer = new Timer();
            //    resizeTimer.Tick += new EventHandler(resizeTimer_Tick);
            //}
            //resizeTimer.Stop();
            base.OnResize(e);
            OurLayout();   // TODO: This is bad to call OnResize, we need to differentiate OurInitLayout from OurLayout.

            //int howLong = (int)(paintTime.TotalMilliseconds + resizeTime.TotalMilliseconds + layoutTime.TotalMilliseconds);
            //// howLong = 1000;
            //resizeTimer.Interval = howLong > 1000 ? howLong : 1000;
            //resizeTimer.Start();
        }

        private void UpdateSize()
        {
            csharp_debug.Debug.DebugWinControl(this);

            // Stopwatch sw = new Stopwatch();
            // sw.Start();
            

            // sw.Stop();
            // resizeTime = sw.Elapsed;
        }

        private void resizeTimer_Tick(object sender, EventArgs e)
        {
            // resizeTimer.Stop();
            UpdateSize();
        }

   

        //protected override void OnLayout(LayoutEventArgs e)
        //{
        //    csharp_debug.Debug.DebugWinControl(this);
            
        //    System.Console.WriteLine("OnLayout: {0}x{1}", Size.Width, Size.Height);
     // base.OnLayout(e);
        //    return;

        //    if (doneInitLayout)
        //    {
        //        if (Size.Width > 0 && Size.Height > 0)
        //        {
        //            _backbuff = new BackBuffer(Size);
        //            _backbuff.size = Size;
        //            GetBitmaps();
        //        }
        //    }
       // }



        //protected void InvalidateEx()
        //{
        //    if (Parent == null)
        //        return;

        //    Rectangle rc = new Rectangle(this.Location, this.Size);
        //    Parent.Invalidate(rc, true);
        //}


        protected override void OnClientSizeChanged(EventArgs e)
        {
            csharp_debug.Debug.DebugWinControl(this);

            //    System.Console.WriteLine("OnClientSizeChange: {0}x{1}", Size.Width, Size.Height);
            base.OnClientSizeChanged(e);
        
        //    if (_backbuff != null)
        //    {
        //        _backbuff.size = Size;
        //        GetBitmaps();
        //    }
        }

#else
        protected virtual void PaintAll(Graphics dc) {}
        protected virtual void OurLayout() { }
        protected override void OnPaint(PaintEventArgs e)
        {
            csharp_debug.Debug.DebugWinControl(this);
            Rectangle rect = e.ClipRectangle;

            Bitmap _backBuffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            Graphics dc = Graphics.FromImage(_backBuffer);
            // Graphics dc = this.CreateGraphics();
            // Graphics dc = e.Graphics;
            
            PaintAll(dc);
            dc.Dispose();
            e.Graphics.DrawImage(_backBuffer, e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle, GraphicsUnit.Pixel);
            System.Console.WriteLine("e.ClipRectangle: {0}", e.ClipRectangle.ToString());
            // Do your painting here, or call base.OnPaint
        }

#endif
    }
}
