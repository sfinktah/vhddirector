using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Be.Windows.Forms;

namespace VhdDirectorApp
{
    public partial class HextFormTest : Form
    {
        public HextFormTest()
        {
            InitializeComponent();
        }

        public void SetHexBox(DynamicByteProvider dbp){
            hexBox.ByteProvider = dbp;
        }

        public void SetHexBox(string filename)
        {
            hexBox.ByteProvider = new Be.Windows.Forms.DynamicFileByteProvider(filename, true);
        }
    }
}
