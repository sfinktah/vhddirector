using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSharp.cc.Windows.Forms
{
    public partial class WaitDialog : Form
    {
        public WaitDialog()
        {
            InitializeComponent();
        }

        public WaitDialog(String text) : this()
        {
            label1.Text = text;
        }

        public void Stop(bool ok) {
            label1.Text += ok ? " (Completed)" : " (Failed)";
            progressBar1.Hide();
            button1.Text = "OK";
            Close();
        }
    }
}
