using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace VHD_Director
{
    public class ShadedClusterBitmaps : ClusterBitmaps
    {
        public IEnumerable<Color> GetGradients(Color start, Color end, int steps)
        {
            
            double A, R, G, B;

            A = (end.A - start.A) / (double)(steps + 1);
            R = (end.R - start.R) / (double)(steps - 1);
            G = (end.G - start.G) / (double)(steps - 1);
            B = (end.B - start.B) / (double)(steps - 1);
           
            for (int i = 0; i < steps; i++)
            {
                yield return Color.FromArgb(start.A + (int)(A * i),
                                            start.R + (int)(R * i),
                                            start.G + (int)(G * i),
                                            start.B + (int)(B * i));
            }
        }

        public BitArray Clusters { get; set; }
        public Color GradientStart { get;set; }
        public Color GradientEnd  { get;set; }
        public int GradientSteps { get { return Steps; } set { Steps = value; }}
        protected int Steps = 16;

        public ShadedClusterBitmaps()
            : base()
        {
            this.SetStyle(
          ControlStyles.ResizeRedraw |
          ControlStyles.UserPaint |
          ControlStyles.AllPaintingInWmPaint |
          ControlStyles.DoubleBuffer,
          true);

        }

        public ShadedClusterBitmaps(Color start, Color end, int steps) : this()
        {
            GradientStart = start;
            GradientEnd = end;
            Steps = steps;
        }

        protected int spriteColorByCluster(int nSprite, int nLevels)
        {
            int set = 0;
            int unset = 0;
            int i = nSprite * nClustersPerSprite;
            int z = i + nClustersPerSprite;
            for ( ; i<z; i++ ) {
                if (Clusters.Get(i)) {
                    set ++;
                } else {
                    unset ++;
                }
            }

            if (set == 0)
            {
                return nLevels;
            }
            double percent = (double)set / (set + unset);
            int level = (int)(percent * nLevels);
            if (level >= nLevels)
            {
                level = nLevels - 1;
            }
            return level;
        }

        protected int nClusters;
        protected int nClustersPerSprite;
        protected int nMaxSprites;
        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap[] squares = new Bitmap[Steps + 1];
            if (Clusters == null)
            {
                nMaxSprites = 0;
            }
            else
            {
                nClusters = Clusters.Length;
                nClustersPerSprite = (int)Math.Ceiling((double)((double)nClusters / numberOfSprites));
                nMaxSprites = nClusters / nClustersPerSprite;
                // InvalidateEx();
                // CopyParentBackground(ClientRectangle, g);
                // _backbuff.DisposeGraphics();

               
                // colorArray = colorList.ToArray();
            }
            int i = 0;
            foreach (Color c in GetGradients(GradientStart, GradientEnd, Steps))
            {
                squares[i] = new Bitmap(Sprite.Width, Sprite.Height);
                Sprite.FillColor = c;
                Sprite.DrawBitmap(squares[i], 0, 0);
                i++;
            }
            squares[i] = new Bitmap(Sprite.Width, Sprite.Height);
            Sprite.FillColor = Color.WhiteSmoke;
            Sprite.DrawBitmap(squares[i], 0, 0);
            i++;

            Graphics g = e.Graphics;

            int n = 0;
            int spriteX;
            int spriteY;
            int colorIndex;

            for (int y = 0; y < lines; y++)
            {
                for (int x = 0; x < spritesPerLine; x++)
                {
                    spriteX = x * Sprite.Width;
                    spriteY = y * Sprite.Height;
                    if (e.ClipRectangle.IntersectsWith(new Rectangle(spriteX, spriteY, Sprite.Width, Sprite.Height)))
                    {
                        if (nMaxSprites == 0)
                        {
                            colorIndex = (n % squares.Length);
                        }
                        else
                        {
                            colorIndex = spriteColorByCluster(n, Steps);
                        }
                        if (squares[colorIndex] != null)
                        {
                            g.DrawImageUnscaled(squares[colorIndex], spriteX, spriteY);
                        }
                    }
                    n++;
                    if (nMaxSprites > 0 && n >= nMaxSprites)
                    {
                        break;
                    }
                }
            }

            foreach (Bitmap bmp in squares)
            {
                if (bmp != null)
                {
                    bmp.Dispose();
                }
            }
        }
    }
}
