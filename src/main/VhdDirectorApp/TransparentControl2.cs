using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace VhdDirectorApp
{
    public partial class TransparentControl2 : UserControl, ITransparentControl
    {
        public TransparentControl2()
        {
            InitializeComponent();
        }

        [Description("Color of inner filler")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [CategoryAttribute("Shape")]
        public Color RectangleFillColor { get; set; }

        [Description("True Transparency")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [CategoryAttribute("Shape")]
        [DefaultValue(false)]
        public bool Transparent { get; set; }

        [Browsable(false)]
        public int clientWidth
        {
            get { return ClientSize.Width; }
            // set { _clientWidth = value; }
        }

        [Browsable(false)]
        public int clientHeight
        {
            get { return ClientSize.Height; }
            // set { _clientHeight = value; }
        }

        
        // http://msdn.microsoft.com/en-us/library/a19191fh.aspx
        // Attributes and Design-Time Support
        // Attribute applied to a property.

#if false
        [Description("Corner rounding (px)")]
        [DefaultValue(10)]
        [CategoryAttribute("Shape")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public int CornerRadius { get; set; }

        [CategoryAttribute("Shape")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public Color StrokeColor { get { return _strokeColor; } set { _strokePen = new Pen(_strokeColor = value); } }

        [CategoryAttribute("Shape")]
        [Browsable(false)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public Pen StrokePen { get { return _strokePen; } set { _strokePen = value; } }

        [CategoryAttribute("Shape")]
        [DefaultValue(1)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public int StrokeMarginY { get { return _strokeMarginY; } set { _strokeMarginY = value; } }

        [CategoryAttribute("Shape")]
        [DefaultValue(1)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public int StrokeMarginX { get { return _strokeMarginX; } set { _strokeMarginX = value; } }

        [CategoryAttribute("Shape")]
        [DefaultValue(1.5)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public float StrokeWidth { get; set; }


        private Pen _strokePen = new Pen(Color.FromArgb(70, Color.Black));
        private Color _strokeColor = Color.FromArgb(70, Color.Black);
        private int _strokeMarginX;
        private int _strokeMarginY;

#endif


        protected Bitmap _background = null;
        protected string lastHash = string.Empty;
        
        protected virtual bool IsRedrawBackgroundRequired()
        {
            string hash = ComputePropertyHash();
            if (_background == null || hash != lastHash)
            {
                return true;
            }

            return false;
        }

        protected virtual void RedrawBackgroundIfRequired(PaintEventArgs e)
        {
            if (!IsRedrawBackgroundRequired())
            {
                return;
            }

            if (_background != null)
            {
                _background.Dispose();
            }

            _background = GenerateBackgroundImage(Size);
            // PaintOurBackground(e);
        }
        protected Bitmap GenerateBackgroundImage(Size size)
        {
            Bitmap _bmp = NewBackgroundBitmap(size);
            BackgroundImageFill(ref _bmp);
            BackgroundImagePaint(ref _bmp);
            return _bmp;
        }

        protected Bitmap NewBackgroundBitmap(Size size)
        {
            return new Bitmap(size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }
        
        protected void BackgroundImageFill(ref Bitmap _bmp) {
            using (Graphics dc = Graphics.FromImage(_bmp))
            {
                // Paint Background
                using (Brush brushBackColor = new SolidBrush(BackColor))
                {
                    dc.FillRectangle(brushBackColor, 0, 0, _bmp.Width, _bmp.Height);  // NB: Using ClientRectangle, not ClientSize. Should we change the ClientSize checks and events to match?
                }
            }
        }

        protected virtual void BackgroundImagePaint(ref Bitmap _bmp)
        {
        }


        protected string ComputePropertyHash()
        {

            // foreach (System.Reflection.FieldInfo field in typeof(Footer).GetFields())
            // foreach (PropertyDescriptor field in Footer)
            // foreach (PropertyInfo pi in Footers)

            string hash = String.Empty;
            object value;
            foreach (System.Reflection.PropertyInfo pi in this.GetType().GetProperties())
            {
                switch (pi.PropertyType.FullName)
                {
                    case "System.Drawing.Image" : 
                    case "System.Drawing.Bitmap" :
                    case "System.Windows.Forms.Cursor" :
                        continue;
                }
                value = null;
                try
                {
                    value = pi.GetValue(this, null);
                }
                catch {
                    continue;
                }
                if (value == null)
                {
                    continue;
                }
                // pi.PropertyType.FullName = System.Windows.Form.Control | System.Int32 | System.String

                if (pi.PropertyType.FullName == "System.String" || pi.PropertyType.IsPrimitive && pi.PropertyType.IsPublic)
                //  && pi.PropertyType.IsValueType) // IsPrimitive
                // pi.DeclaringType is ITransparentControl
                {
                    hash += pi.Name;
                    hash += "=";
                    hash += value.ToString();
                    hash += ";";
                }
                else if (pi.PropertyType.IsSerializable && pi.PropertyType.FullName != "System.Windows.Forms.Cursor")
                {
                    hash += pi.Name;
                    hash += "=";
                    hash += value.ToString();
                    hash += ";";
                }
                else
                {
                    // hash += pi.Name + ";";
                }
            }

            return hash;
        }


        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // base.OnPaintBackground(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            csharp_debug.Debug.DebugWinControl(this);

            // base.OnPaint(e);

            // Background 
            RedrawBackgroundIfRequired(e);
            e.Graphics.DrawImageUnscaled(_background, 0, 0);

            // Foreground
        }


        protected virtual void PaintOurBackground(PaintEventArgs e) { }
        protected virtual void PaintOurForeground(PaintEventArgs e) { }

 
        public static Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpCrop;
            using (Bitmap bmpImage = new Bitmap(img))
            {
                bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            }
            return bmpCrop;
        }



        //protected override void InitLayout()
        //{
        //    System.Console.WriteLine("InitLayout: {0}x{1}", Size.Width, Size.Height);
        //    base.InitLayout();

        //    doneInitLayout = true;
        //}


        // Called after the control has been added to another container.
        // The InitLayout method is called immediately after adding a control to a container. 
        // The InitLayout method enables a control to initialize its layout state based on its container. 

        // For example, you would typically apply anchoring and docking to the control in the InitLayout method.

        // When overriding InitLayout in a derived class, be sure to call the base class's InitLayout method so that the control is displayed correctly.


        // The Layout event occurs when child controls are added or removed, when the bounds of the control changes, and when 
        // other changes occur that can affect the layout of the control.

        // The layout event can be suppressed using the 
        // SuspendLayout and ResumeLayout methods. Suspending layout enables you to perform multiple actions on a control 
        // without having to perform a layout for each change.
        // For example, if you resize and move a control, each operation would raise a Layout event.

        protected virtual void PaintAll(Graphics dc) {}
        protected virtual void OurLayout() { }
               
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

    }
}
