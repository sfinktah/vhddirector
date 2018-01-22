using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VhdDirectorApp
{
    // If you need to drag and drop a transparent borderless windows: http://www.c-sharpcorner.com/UploadFile/scottlysle/XparentFormsCS10282007212944PM/XparentFormsCS.aspx
    // Opacity = 0.5
    // BackColor = Color.Lime;
    // TransparencyKey = Color.Lime;

    // Moving an alpha window under a another window for true alpha background: http://www.codeproject.com/KB/graphics/alphaBG.aspx

    // Hardcore Alpha Form with 24 bit transparency: http://www.codeproject.com/KB/GDI/pxalphablend.aspx

    public partial class ZoomOverlayForm : Form
    {
        public ZoomOverlayForm()
        {
            InitializeComponent();
        }

        public int offsetX { set { zoomOverlay1.offsetX = value; } get { return zoomOverlay1.offsetX; } }
        public int offsetY { get { return 0; } }
        public Bitmap Image { set { zoomOverlay1.Image = value; } }

        protected Bitmap _fullbmp;


        public int maximumX { set { zoomOverlay1.maximumX = value; } get { return zoomOverlay1.maximumX; } }
    }
}
