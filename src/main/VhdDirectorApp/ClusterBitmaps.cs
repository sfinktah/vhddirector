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
    public partial class ClusterBitmaps : UserControl
    {
        public int SpritesPerLine { get { return spritesPerLine; } }
        public int Lines { get { return lines; } }
        public int SpriteCount { get { return numberOfSprites; } }

        protected int spritesPerLine;
        protected int lines;
        protected int numberOfSprites;
        public Color ClusterColor { get { return Sprite.FillColor; } set { Sprite.FillColor = value; } }
        public BitmapRaw Sprite = new BitmapRaw();
        public ClusterBitmaps()
        {
            InitializeComponent();
            // Sprite.FillColor = ClusterColor;
            // Sprite.ApplyFillColor();

            spritesPerLine = Width / Sprite.Width;
            lines = Height / Sprite.Height;
            numberOfSprites = lines * spritesPerLine;
        }

        protected void CopyParentBackground(Rectangle rect, Graphics graphics)
        {
            if (Parent == null) return;

            Rectangle dst = new Rectangle(rect.Location, rect.Size);
            Point p = PointToScreen(rect.Location);
            Rectangle src = new Rectangle(Parent.PointToClient(p), rect.Size);

            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            DrawToBitmap(bmp, rect);
            graphics.DrawImageUnscaled(bmp, 0, 0);
            bmp.Dispose();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // InvalidateEx();
            // CopyParentBackground(ClientRectangle, g);
            // _backbuff.DisposeGraphics();

            Bitmap bmp = new Bitmap(Sprite.Width, Sprite.Height);
            Sprite.DrawBitmap(bmp, 0, 0);
            int spriteX;
            int spriteY;
        
            Graphics g = e.Graphics;

            for (int y=0; y<lines; y++) {
                for (int x=0; x<spritesPerLine; x++) {
                    spriteX = x * Sprite.Width;
                    spriteY = y * Sprite.Height;
                    if (e.ClipRectangle.IntersectsWith(new Rectangle(spriteX, spriteY, Sprite.Width, Sprite.Height))) {
                        g.DrawImageUnscaled(bmp, spriteX, spriteY);
                    }
                }
            }

// g.Dispose();        
        }
        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);

             spritesPerLine = Width / Sprite.Width;
             lines = Height / Sprite.Height;
             numberOfSprites = lines * spritesPerLine;
        }
    }
}
