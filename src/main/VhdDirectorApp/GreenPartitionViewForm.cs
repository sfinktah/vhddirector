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
    public partial class GreenPartitionViewForm : Form
    {
        public GreenPartitionViewForm()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBoxH.Text = (trackBarH.Value / 10D).ToString();
            textBoxS.Text = (trackBarS.Value / 10D).ToString();
            textBoxL.Text = (trackBarL.Value / 10D).ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    greenPartitionView1.Hue = (float)Convert.ToDouble(textBoxH.Text);
                }
                catch { }

                try
                {
                    greenPartitionView1.Saturation = (float)Convert.ToDouble(textBoxS.Text);
                }
                catch { }

                try
                {
                    greenPartitionView1.Luminance = (float)Convert.ToDouble(textBoxL.Text);
                }
                catch { }

                greenPartitionView1.InvalidateBuffer(true);
                greenPartitionView1.Invalidate(true);
            }
            catch { }
        }

        private void GreenPartitionViewForm_Load(object sender, EventArgs e)
        {
            // Preferences prefs = new Preferences();

            App.Prefs.AddEditForm("Theme.GreenPartition", this.GetType());

            textBoxH.Text = App.Prefs.GetPreference("Theme.GreenPartition.Hue", "1.0");
            textBoxS.Text = App.Prefs.GetPreference("Theme.GreenPartition.Saturation", "1.0");
            textBoxL.Text = App.Prefs.GetPreference("Theme.GreenPartition.Luminance", "1.0");

            greenPartitionView1.Hue = (float)Convert.ToDouble(textBoxH.Text);
            greenPartitionView1.Saturation = (float)Convert.ToDouble(textBoxS.Text);
            greenPartitionView1.Luminance = (float)Convert.ToDouble(textBoxL.Text);

            trackBarH.Value = (int)(float.Parse(App.Prefs.GetPreference("Theme.GreenPartition.Hue", "1.0")) * 10);
            trackBarS.Value = (int)(float.Parse(App.Prefs.GetPreference("Theme.GreenPartition.Saturation", "1.0")) * 10);
            trackBarL.Value = (int)(float.Parse(App.Prefs.GetPreference("Theme.GreenPartition.Luminance", "1.1")) * 10);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            App.Prefs.SetPreference("Theme.GreenPartition.Hue", greenPartitionView1.Hue.ToString());
            App.Prefs.SetPreference("Theme.GreenPartition.Saturation", greenPartitionView1.Saturation.ToString());
            App.Prefs.SetPreference("Theme.GreenPartition.Luminance", greenPartitionView1.Luminance.ToString());
            Close();
        }
    }
}
