using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;


namespace VhdDirectorApp
{
    public partial class RtfForm : Form
    {
        public RtfForm()
        {
            InitializeComponent();
            // Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName
            richTextBox1.Rtf = VhdDirectorApp.Properties.Resources.CompactVirtualDisk;
        }

        private void RtfForm_Load(object sender, EventArgs e)
        {
            shadowPanel1.Add(richTextBox1);
        }

        private Timer rtbSize = null;
        private void richTextBox1_ClientSizeChanged(object sender, EventArgs e)
        {
            if (rtbSize == null && Parent != null)
            {
                rtbSize = new Timer();
                rtbSize.Tick += new EventHandler(rtbCheckSize);
                // Set the Interval to 5 seconds.
                rtbSize.Enabled = true;
                rtbSize.Interval = 100;
            }

            if (rtbSize != null)
            {
                rtbSize.Start();
            }
        }

        private void rtbCheckSize(object source, EventArgs e)
        {
            if (richTextBox1.Size.Width - richTextBox1.ClientSize.Width > 10 && richTextBox1.Size.Height < richTextBox1.Parent.ClientSize.Height - 10)
            {
                richTextBox1.Size = new Size(richTextBox1.Width, richTextBox1.Size.Height + 10);
            }
            else
            {
                rtbSize.Stop();
            }
        }
        public delegate void ButtonClicked(object source, object eventArgument);
        public event ButtonClicked buttonClicked;
        private string ButtonInitialText;
        private void button2_Click(object sender, EventArgs e)
        {
            if (ButtonInitialText == null || ButtonInitialText == button2.Text)
            {
                ButtonInitialText = button2.Text;
                buttonClicked(sender, button2.Text);
                button2.Text = "Cancel";
                progressBar1.Visible = true;
            }

            else if (button2.Text.Equals("Cancel"))
            {
                buttonClicked(sender, button2.Text);
                button2.Text = "Cancelling...";
            }

            else if (button2.Text.Equals("OK"))
            {
                this.Close();
            }

            //CSharp.cc.QueuedBackgroundWorker.QueueWorkItem(CSharp.cc.QueuedBackgroundWorker.m_Queue, null,
            //    (args) => { return (int)this._disk.Compact(); },
            //    (args) => { result = args.Result; wait.Stop(args.Result == 0); }
            //);

            // this._disk.Compact();
        }

        public void Stop(string message)
        {
            progressBar1.Visible = false;
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            label1.Visible = false;
            errorBox1.Text = message;
            errorBox1.Visible = true;
            button2.Text = "OK";
        }



        public Medo.IO.VirtualDisk _disk { get; set; }
    }
}
