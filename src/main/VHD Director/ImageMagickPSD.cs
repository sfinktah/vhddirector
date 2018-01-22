using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImageMagickNET;

namespace VHD_Director
{
    public partial class ImageMagickPSD : Form
    {
        public ImageMagickPSD()
        {
            InitializeComponent();
            Test();
        }

        public void Test() {
            MagickNet.InitializeMagick();
            ImageMagickNET.Image img = new ImageMagickNET.Image("images/SampleIconsInPlace.psd"); // VHD_Director.Properties.Resources.SampleIconsInPlace
            // img.Resize(new Geometry(100, 100, 0, 0, false, false);
            // img.Write("newFile.png");
            pictureBox1.Image = img.ToBitmap();
        }
    }
}
