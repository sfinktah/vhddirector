using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.ComponentModel;

namespace VHD_Director
{
    class FileDownloader : CSharp.cc.Threading.Backgrounder
    {
        protected String url;
        protected String destinationFile;
        protected Boolean successful = false;
        public delegate void CompleteHandler(object source, object eventArgument);
        public delegate void ProgressHandler(object source, object eventArgument);
        public event CompleteHandler completed;
        public event ProgressHandler progress;

        public FileDownloader(String downloadUrl, CompleteHandler c, ProgressHandler p)
        {
            completed = c;
            progress = p;

            string tempPath = System.IO.Path.GetTempPath();
            destinationFile = tempPath + downloadUrl.Remove(downloadUrl.LastIndexOf('/'));
            destinationFile = tempPath + @"\ManokInstaller.exe";

            File.Delete(destinationFile);

            url = downloadUrl;
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;
            WorkerReportsProgress = true;

           

            DoWork += FileDownloader_DoWork;
    
            RunWorkerCompleted +=
                 new System.ComponentModel.RunWorkerCompletedEventHandler(this.DownloadComplete);

            ProgressChanged +=
                new ProgressChangedEventHandler(this.ProgressUpdate);

            RunWorkerAsync();
        }
        private void ProgressUpdate(object sender, ProgressChangedEventArgs e)
        {
        }

        private void DownloadComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // ("* Unable to download Image *");
            }
            else if (e.Cancelled)
            {
                // ("* Cancelled download of Image *");
            }
            else
            {
                FileDownloader fd = sender as FileDownloader;
                if (fd.successful)
                {
                    try
                    {
                        System.Diagnostics.Process.Start(destinationFile);
                    }
                    catch (IOException ioe)
                    {
                        System.Windows.Forms.MessageBox.Show(ioe.Message, "Upgrade Error");

                        fd.successful = false;
                    }
                    catch (System.Security.SecurityException sex)
                    {
                        System.Windows.Forms.MessageBox.Show(sex.Message, "Upgrade Error");
                        fd.successful = false;
                    }

                    if (fd.successful)
                    {
                        completed(this, null);
                    }

                }
            }
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        public void SaveStreamToFile(string fileFullPath, Stream stream)
        {
            if (stream.Length == 0) return;

            // Create a FileStream object to write a stream to a file
            using (FileStream fileStream = System.IO.File.Create(fileFullPath, (int)stream.Length))
            {
                // Fill the bytes[] array with the stream data
                byte[] bytesInStream = new byte[stream.Length];
                stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

                // Use FileStream object to write to the specified file
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
        }


        public void FileDownloader_DoWork(object sender, DoWorkEventArgs e)
        {
            WebRequest wreq;
            WebResponse wresp = null;

            if (CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            int count = 100;
            while (count-- > 0)
            {
                if (CanRaiseEvents)
                {
                    float percent = count * 100f;
                    int percentDone = Convert.ToInt32(percent);
                    ReportProgress(percentDone, "Astract Class");
                }
            }
            // e.Result = true;

            try
            {

                wreq = WebRequest.Create(url);
                // wreq.Timeout = 36000;


                if (wreq != null)
                {
                    wresp = wreq.GetResponse();
                    if (wresp != null)
                    {
                        using (Stream file = File.OpenWrite(destinationFile))
                        {
                            CopyStream(wresp.GetResponseStream(), file);
                        }


                        // SaveStreamToFile(destinationFile, wresp.GetResponseStream());
                        successful = true;
                    }
                }
                // rtbChat.InsertImage(image5);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error Downloading Upgrade " + url + " " + ex.Message, "Download Error");

            }
        }
    }
}
