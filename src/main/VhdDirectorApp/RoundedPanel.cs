using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using csharp_debug;

namespace VhdDirectorApp
{
    public partial class RoundedPanel : TransparentControl
    {
    
        public RoundedPanel()
        {
            InitializeComponent();

            // Debugging: http://www.csharp-station.com/Articles/DebuggingTechniques.aspx
            // this.Margin = new Padding(10);

            Debug.DebugWinControl(this);


        }
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

        protected virtual void RedrawBackgroundIfRequired()
        {
            if (!IsRedrawBackgroundRequired())
            {
                return;
            }


            if (_background != null)
            {
                _background.Dispose();
            }

            _background = new Bitmap(Size.Width, ClientSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (Graphics dc = Graphics.FromImage(_background))
            {
                // Paint Background
                using (Brush brushBackColor = new SolidBrush(BackColor))
                {
                    dc.FillRectangle(brushBackColor, ClientRectangle);  // NB: Using ClientRectangle, not ClientSize. Should we change the ClientSize checks and events to match?
                }

                // Paint Control
                Rectangle windowStroke = ClientRectangle;
                windowStroke.Inflate(0 - _strokeMarginX, 0 - _strokeMarginY);

                if (CornerRadius == 0)
                {
                    CornerRadius = 10;
                }
                DrawRoundedRectangle(dc, windowStroke, CornerRadius, _strokePen, RectangleFillColor);
            }
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
                value = null;
                try
                {
                    value = pi.GetValue(this, null);
                }
                catch
                {
                    value = null;
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


        public virtual void DrawRoundedRectangle(Graphics gfx, Rectangle Bounds, int CornerRadius, Pen DrawPen, Color FillColor)
        {
            int strokeOffset = Convert.ToInt32(Math.Ceiling(DrawPen.Width));
            Bounds = Rectangle.Inflate(Bounds, -strokeOffset, -strokeOffset);

            gfx.SmoothingMode = SmoothingMode.AntiAlias;
            DrawPen.Width = StrokeWidth;
            DrawPen.EndCap = DrawPen.StartCap = LineCap.Round;

            using (GraphicsPath gfxPath = new GraphicsPath())
            {
                gfxPath.AddArc(Bounds.X, Bounds.Y, CornerRadius, CornerRadius, 180, 90);
                gfxPath.AddArc(Bounds.X + Bounds.Width - CornerRadius, Bounds.Y, CornerRadius, CornerRadius, 270, 90);
                gfxPath.AddArc(Bounds.X + Bounds.Width - CornerRadius, Bounds.Y + Bounds.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
                gfxPath.AddArc(Bounds.X, Bounds.Y + Bounds.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
                gfxPath.CloseAllFigures();

                gfx.FillPath(new SolidBrush(FillColor), gfxPath);
                gfx.DrawPath(DrawPen, gfxPath);

                DrawRoundedRectangleCustomPost(gfx, Bounds, CornerRadius, DrawPen, FillColor);
            }
        }

        
        protected virtual void DrawRoundedRectangleCustomPost(Graphics gfx, Rectangle Bounds, int CornerRadius, Pen DrawPen, Color FillColor)
        {
        }


        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // base.OnPaintBackground(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            RedrawBackgroundIfRequired();
            e.Graphics.DrawImageUnscaled(_background, 0, 0);
            base.OnPaint(e);
        }

        private void RoundedPanel_Load(object sender, EventArgs e)
        {

        }

        // Pen Pen65 = new Pen(Color.FromArgb(70, Color.Black));
        // Pen Pen50 = new Pen(Color.FromArgb(100, Color.Black));

        private Pen _strokePen = new Pen(Color.FromArgb(70, Color.Black));
        private Color _strokeColor = Color.FromArgb(70, Color.Black);
        private int _strokeMarginX;
        private int _strokeMarginY;

        // http://msdn.microsoft.com/en-us/library/a19191fh.aspx
        // Attributes and Design-Time Support
         // Attribute applied to a property.


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
    }

  
}