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
    public partial class ShadowPanel2 : TransparentControl2
    {
        Pen Pen97 = new Pen(Color.FromArgb(8 - 4, Color.Black));
        Pen Pen93 = new Pen(Color.FromArgb(14 - 4, Color.Black));
        Pen Pen89 = new Pen(Color.FromArgb(20 - 4, Color.Black));
        Pen Pen85 = new Pen(Color.FromArgb(28 - 4, Color.Black));
        Pen Pen65 = new Pen(Color.FromArgb(70, Color.Black));
        Pen Pen50 = new Pen(Color.FromArgb(100, Color.Black));
        Pen PenBG = new Pen(Color.White);
        //Panel InnerPanel;
        //    InnerPanel = new Panel();

        //            this.Padding = new Padding(15);

        //    InnerPanel.BackColor = Color.White;
        //    InnerPanel.Padding = new System.Windows.Forms.Padding(5);
        //    InnerPanel.Dock = System.Windows.Forms.DockStyle.Fill;

        //    Controls.Add(InnerPanel);
        public ShadowPanel2()
        {
            InitializeComponent();
            // InnerPanel.BackColor = RectangleFillColor;
            //  http://www.csharp-examples.net/redraw-control-on-resize/          
            //  (Check that URL for ways to invalidate and consider being more surgical)
            ResizeRedraw = true;
        }

        protected override void BackgroundImagePaint(ref Bitmap _bmp)
        {

            csharp_debug.Debug.DebugWinControl(this);

            Rectangle windowStroke = new Rectangle(0, 0, _bmp.Width, _bmp.Height);

            using (Graphics dc = Graphics.FromImage(_bmp))
            {
                // dc.FillRectangle(Brushes.Cyan, BackColor);

                windowStroke.Inflate(-8, -8);
                dc.DrawRectangle(Pen97, windowStroke);

                windowStroke.Inflate(-1, -1);
                dc.DrawRectangle(Pen93, windowStroke);

                windowStroke.Inflate(-1, -1);
                dc.DrawRectangle(Pen89, windowStroke);

                windowStroke.Inflate(-1, -1);
                dc.DrawRectangle(Pen85, windowStroke);

                windowStroke.Inflate(-1, -1);
                dc.DrawRectangle(Pen65, windowStroke);

                windowStroke.Inflate(-1, -1);

                using (Brush fillBrushColor = new SolidBrush(RectangleFillColor))
                {
                    dc.FillRectangle(fillBrushColor, windowStroke);
                }
            }
        }

        public void Add(Control c)
        {
            InnerPanel.Controls.Add(c);
        }
        public void Add(RoundedPanel adl)
        {
            // If we ever decide to take over painting of all our controls, this is where we still start the hookin'
            // adl.Width = InnerPanel.ClientRectangle.Width;
            adl.Dock = DockStyle.Top;
            InnerPanel.Controls.Add(adl);
        }

        private void ShadowPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            if (Controls.Count > 1)
            {
                Control c = e.Control;
                Controls.Remove(c);
                InnerPanel.Controls.Add(c);
            }
        }
    }
}
