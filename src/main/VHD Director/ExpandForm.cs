using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VHD_Director
{
    public partial class ExpandForm : Form
    {
        public ExpandForm()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        bool skipKey = false;
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (isvalid())
            {
                // pictureBox1.BackgroundImage = global::VHD_Director.Properties.Resources.burn_green;

            }
            if (skipKey)
            {
                skipKey = false;
                e.Handled = true;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // Determine whether the keystroke is a number from the top of the keyboard or numberpad, period, or backspace key.

            if ((e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9) && (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9) && e.KeyCode != Keys.Back)
            {
                if (e.KeyCode == Keys.M)
                {
                    label1.Text = "MB";
                }
                else if (e.KeyCode == Keys.G)
                {
                    label1.Text = "GB";
                }
                else if (e.KeyCode == Keys.T)
                {
                    label1.Text = "TB";
                }

                skipKey = true;
            }
        }


        private bool isvalid()
        {
            try
            {
                return (comboBox1.SelectedIndex > -1 && Convert.ToInt32(textBox1.Text) > 0);
            }
            catch
            {
                return false;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!isvalid())
            {
                return;
            }

            int m = 1;
            switch (comboBox1.SelectedText)
            {
                case "MB": m = 10 ^ 6; break;
                case "GB": m = 10 ^ 9; break;
                case "TB": m = 10 ^ 12; break;
            }

            long size = Convert.ToInt32(textBox1.Text) * m;
            (sender as Button).Enabled = false;
            buttonCancel.Enabled = false;
            buttonClicked(sender, size);
            Close();
        }

        public delegate void ButtonClicked(object source, object eventArgument);
        public event ButtonClicked buttonClicked;

    }
}
