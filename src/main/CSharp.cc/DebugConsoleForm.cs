using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSharp.cc
{
    public partial class DebugConsoleForm : Form
    {
        public DebugConsoleForm()
        {
            InitializeComponent();
        }

        public void AppendText(string text)
        {
            this.console.AppendText(text);
        }
    }
}
