using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using System.IO;


namespace VhdDirectorApp
{
    public class DownloadRotateWait
    {
        public delegate void CompleteHandler(object source, object eventArgument);
        public event CompleteHandler completed;

        public DownloadRotateWait(string url, string destinationFile, string description)
        {
            this.DownloadUrl = url;
            this.Description = description;
            this.destinationFile = destinationFile;
            this.DownloadName = Path.GetFileNameWithoutExtension(this.destinationFile);
            this.tempFile = System.IO.Path.GetTempPath() + this.DownloadName + ".downloading";
 
            if (File.Exists(this.tempFile))
            {
                File.Delete(this.tempFile);
            }

            // To receive notification when the file is available, add an event handler to the DownloadFileCompleted event. 
            rw = new RotateWait();
            rw.ShowProgress = true;
            
            rw.Progress.Value = 0;
            rw.Progress.Maximum = 100;
            rw.Progress.Step = 1;

            // Create a new WebClient instance.
            WebClient myWebClient = new WebClient();
            myWebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(myWebClient_DownloadProgressChanged);
            myWebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(myWebClient_DownloadFileCompleted);

            // Concatenate the domain with the Web resource filename.
            rw.WaitText = String.Format("Downloading \"{0}\"", this.Description);
            // Download the Web resource and save it into the current filesystem folder.

            rw.Hold = true;
            rw.Show();

            myWebClient.DownloadFileAsync(new Uri(this.DownloadUrl), tempFile);
            // myWebClient_DownloadFileCompleted(null, null);
        }

        void myWebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            rw.Progress.Value = 100;
            rw.WaitText = "Copying...";
            if (File.Exists(destinationFile))
            {
                if (File.Exists(destinationFile + ".old"))
                {
                    File.Delete(destinationFile + ".old");
                }
                File.Move(destinationFile, destinationFile + ".old");
            }

            // rw.Hold = false;
            File.Move(this.tempFile, this.destinationFile);
            rw.Hold = false;
           // rw.Close();
        }

        void myWebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            rw.Value = e.ProgressPercentage;
        }

        private string destinationFile;
        private string downloadName;
        private string tempFile;
        private RotateWait rw;

        public string DownloadName
        {
            get
            {
                return downloadName;
            }
            set
            {
                downloadName = value; 
                if (rw != null) {
                    rw.WaitText = String.Format("Downloading \"{0}\"", this.Description);
                }
            }
        }

        public string DownloadUrl { get; set; }
        public string DownloadPage { get; set; }
        public string DownloadTo { get; set; }

        public string Description { get; set; }
    }
}

