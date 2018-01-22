using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace VhdDirectorApp
{
    public class TransparentLabel : Label
    {
        protected bool haveSetBackground;
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // return;
            if (!Transparent)
            {
                base.OnPaintBackground(pevent);
                return;
            }

            // base.OnPaintBackground(pevent);
            if (!NoPaintParentBackground)
            PaintParentBackground(pevent);
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
        protected override void OnLayout(LayoutEventArgs levent)
        {
            csharp_debug.Debug.DebugWinControl(this);
            base.OnLayout(levent);
        }

        public bool Transparent { get; set; }
//        public bool TransparentPaintParentBackground { get; set; }

        public bool NoPaintParentBackground { get; set; }
    }
}
