using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace VHD_Director
{
    public class TransparentPanel : Panel
    {
        public TransparentPanel()
        {
            Transparent = true;
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

        Rectangle lastBackgroundRect = new Rectangle(0, 0, 0, 0);
        protected override void OnPaintBackground(PaintEventArgs e)
        {

            //Rectangle rect = new Rectangle(Left, Top, Width, Height);
            //if (rect.Equals(lastBackgroundRect))
            //{
            //    return;
            //}

            //lastBackgroundRect = rect;
            //csharp_debug.Debug.DebugWinControl(this);
            //// base.OnPaintBackground(e);

            //PaintParentBackground(e);
            //return;

            //// base.OnPaintBackground(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // base.OnPaint(e);
            e.Graphics.DrawImage(global::VHD_Director.Properties.Resources.burn_yellow, 0, 0);
        }

        public System.Drawing.Bitmap Image { get; set; }
        public System.Drawing.Bitmap InitialImage { get; set; }

        public bool Transparent { get; set; }
    }
}
