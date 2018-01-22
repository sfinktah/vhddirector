using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace VHD_Director
{
    public partial class TestArcControl : UserControl
    {
        public TestArcControl()
        {
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Create a GraphicsPath object.
            GraphicsPath myPath = new GraphicsPath();

            // Set up and call AddArc, and close the figure.
            Rectangle rect = new Rectangle(20, 20, 50, 100);
            myPath.StartFigure();
            myPath.AddArc(rect, 0, 180);
            myPath.CloseFigure();

            // Draw the path to screen.
            // gfx.DrawPath(new Pen(Color.Red, 3), myPath);

            // return;
            Pen DrawPen = new Pen(Color.FromArgb(70, Color.Black));

            CornerRadius = 10;
            int strokeOffset = 1;
            Rectangle Bounds = new Rectangle(strokeOffset, strokeOffset, Width - strokeOffset * 2, Height - strokeOffset * 2);

            Graphics gfx = e.Graphics;
            gfx.SmoothingMode = SmoothingMode.AntiAlias;


            DrawPen.Width = 1;
            DrawPen.EndCap = DrawPen.StartCap = LineCap.Round;
            Color FillColor = Color.WhiteSmoke;

            int notch = CornerRadius + (CornerRadius >> 1);
            int along = 200;
            using (GraphicsPath gfxPath = new GraphicsPath())
            {
                // Top Left
                //gfxPath.AddArc(Bounds.X, Bounds.Y + notch, CornerRadius, CornerRadius, 180, 90);

                //// Notch Left
                ////gfxPath.AddArc(Bounds.X + along, Bounds.Y + notch - CornerRadius, CornerRadius, CornerRadius, 90, -90);

                ////gfxPath.AddArc(Bounds.X + along + CornerRadius, Bounds.Y - CornerRadius, CornerRadius, CornerRadius, 0, 90);

                //// Top Right
                //gfxPath.AddArc(Bounds.X + Bounds.Width - CornerRadius, Bounds.Y, CornerRadius, CornerRadius, 270, 90);
                //gfxPath.AddArc(Bounds.X + Bounds.Width - CornerRadius, Bounds.Y + notch + Bounds.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
                //gfxPath.AddArc(Bounds.X, Bounds.Y + notch + Bounds.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);

                int X = Bounds.X;
                int radius = CornerRadius;
                int Y = Bounds.Y + notch;
                int width = Bounds.Width;
                int height = Bounds.Height - notch;


                // TOP LINE LEFT
                //gfxPath.AddLine(X + radius, Y, X + along - (radius * 4), Y);



                gfxPath.AddArc(X + along - (int)(radius * 3), Y - notch - radius * 0, radius * 2, radius * 2, 180 + 34, 34);


                // TOP LINE RIGHT
                //gfxPath.AddLine(X + along - (radius * 0), Y - notch, X + width - (radius*2), Y - notch);


                // TOP RIGHT
                gfxPath.AddArc(X + width - (radius * 2), Y - notch, radius * 2, radius * 2, 270, 90);

                // RIGHT LINE
                // gfxPath.AddLine(X + width, Y + (radius * 1), X + width, Y + height - (radius * 2));

                // BOTTOM RIGHT
                gfxPath.AddArc(X + width - (radius * 2), Y + height - (radius * 2), radius * 2, radius * 2, 0, 90);

                // BOTTOM LINE
                // gfxPath.AddLine(X + width - (radius * 2), Y + height, X + radius, Y + height);

                // BOTTOM LEFT ?
                gfxPath.AddArc(X, Y + height - (radius * 2), radius * 2, radius * 2, 90, 90);

                // LEFT LINE
                // gfxPath.AddLine(X, Y + height - (radius * 2), X, Y + radius + notch);

                // TOP LEFT
                gfxPath.AddArc(X, Y, radius * 2, radius * 2, 180, 90);



                gfxPath.AddArc(X + along - (radius * 5), Y - radius * 2, radius * 2, radius * 2, 90, -45);

                gfxPath.CloseAllFigures();

                gfx.FillPath(new SolidBrush(FillColor), gfxPath);
                gfx.DrawPath(DrawPen, gfxPath);


            }
        }

        [System.ComponentModel.DefaultValue(10)]
        public int CornerRadius { get; set; }
    }
}
