using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VhdDirectorApp
{
    public partial class ZoomOverlay : TransparentControl2
    {
        public ZoomOverlay()
        {
            InitializeComponent();
        }

        private void ZoomOverlay_Load(object sender, EventArgs e)
        {
            // Not used
        }

        protected override void BackgroundImagePaint(ref Bitmap _bmp)
        {
            if (_fullbmp == null)
            {
                return;
            }
            int w = _bmp.Width;
            int h = _bmp.Height;
            double pixelRatio = 1D;
            if (maximumX > 0)
            {
                pixelRatio = _fullbmp.Width / (double)maximumX;
            }
            int adjustedWidth = (_fullbmp.Width / maximumX) * 20;
            int adjustedStart = adjustedWidth - 10;
            if (adjustedStart < 0)
            {
                adjustedStart = 0;
            }


            // using (Bitmap section = (Bitmap)cropImage(_fullbmp, new Rectangle(offsetX, offsetY, offsetX + Width, offsetY + Height)))
                Point TopLeft = new Point(0, 0);
                Rectangle TargetArea = new Rectangle(0, 0, _bmp.Width, _bmp.Height);
                Rectangle SourceArea = new Rectangle((int)((offsetX - 10) * pixelRatio), offsetY, 
                    // w > _fullbmp.Width ? _fullbmp.Width : w, h > _fullbmp.Height ? _fullbmp.Height : h);
                    adjustedWidth,
                    h > _fullbmp.Height ? _fullbmp.Height : h);
                using (Graphics dc = Graphics.FromImage(_bmp))
                {
                    // DrawImage(Image, Rectangle, Rectangle, GraphicsUnit)

                    // for (int y = 0; y < _bmp.Height; y++)
                    dc.DrawImage(_fullbmp, TargetArea, SourceArea, GraphicsUnit.Pixel);
                }
        }
        public int offsetX { get; set; }
        public int offsetY { get { return 0; } }
        public int maximumX { get; set; }
        public int maximumY { get; set; }
        public Bitmap Image { set { _fullbmp = value; } }

        protected Bitmap _fullbmp;
    }
}
