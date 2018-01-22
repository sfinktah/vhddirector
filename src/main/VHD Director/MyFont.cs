using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;

namespace VHD_Director
{
    public partial class MyFont : Form
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]

        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
           IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection MYpfc = new PrivateFontCollection();

        public MyFont()
        {
            InitializeComponent();
            try
            {
                unsafe
                {
                    fixed (byte* pFontData = Properties.Resources.ROYALP)
                    {
                        uint dummy = 0;
                        MYpfc.AddMemoryFont((IntPtr)pFontData, Properties.Resources.ROYALP.Length);
                        AddFontMemResourceEx((IntPtr)pFontData, (uint)Properties.Resources.ROYALP.Length, IntPtr.Zero, ref dummy);
                    }
                }
                lblUseRosewoodStd_Regular.Font = new Font(MYpfc.Families[0], 36); //Font size is 36

            }
            catch
            {
                MessageBox.Show("Font does not correctly appear");

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //lblUseROYALP1.Font = new Font(MYpfc.Families[0], 22);//Font size is 22
            // lblUseROYALP2.Font = new Font(MYpfc.Families[0], 16);//Font size is 16
        }
    }
}