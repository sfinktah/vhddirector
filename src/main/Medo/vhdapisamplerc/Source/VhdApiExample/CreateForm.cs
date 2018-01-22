using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace VhdApiExample {
    public partial class CreateForm : Form {
        public Medo.IO.VirtualDisk Disk;

        public CreateForm() {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
        }

        private void buttonFileBrowse_Click(object sender, EventArgs e) {
            if (saveDialog.ShowDialog(this) == DialogResult.OK) {
                textFile.Text = saveDialog.FileName;
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e) {
            textFile.Enabled = false;
            buttonFileBrowse.Enabled = false;
            size.Enabled = false;
            buttonCreate.Enabled = false;
            progress.Visible = true;
            bw.RunWorkerAsync(textFile.Text);
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e) {
            var fileName = (string)e.Argument;
            if (File.Exists(fileName)) { File.Delete(fileName); }
            this.Disk = new Medo.IO.VirtualDisk(fileName);
            this.Disk.CreateAsync((long)size.Value * 1024 * 1024);

            var progress = this.Disk.GetCreateProgress();
            while (!progress.IsDone) {
                bw.ReportProgress((int)progress.ProgressPercentage);
                System.Threading.Thread.Sleep(250);
                progress = this.Disk.GetCreateProgress();
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            progress.Value = e.ProgressPercentage;
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            this.DialogResult = DialogResult.OK;
        }

    }
}
