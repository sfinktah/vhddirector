using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.ComponentModel;
using System.Security.Cryptography;
namespace VHD_Director
{
    class CheckUpdates : CSharp.cc.Threading.Backgrounder
    {
        public String url = String.Empty;
        public String currentVersion;
        public String latestVersion = String.Empty;
        // public delegate void CompleteHandler(object source, object eventArgument);
        public event FileDownloader.CompleteHandler completed;



        /// <summary>
        /// Write the binary file.
        /// The file is the concatenation of prefix, goodFile and evilFile
        /// </summary>
        /// <param name="sOutFileName">File Name</param>
        /// <param name="prefix">prefix vector</param>
        /// <param name="goodFile">binary data of good file</param>
        /// <param name="evilFile">binary data of evil file</param>
        private static void WriteBinary(string sOutFileName, byte[] prefix, byte[] goodFile, byte[] evilFile)
        {
            using (FileStream fs = File.OpenWrite(sOutFileName))
            {
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    writer.Write(prefix);
                    writer.Write(goodFile.Length);
                    writer.Write(evilFile.Length);
                    fs.Write(goodFile, 0, goodFile.Length);
                    fs.Write(evilFile, 0, evilFile.Length);
                    fs.Close();
                }
            }
        }



        /// <summary>
        /// Dump binary data in hexadecimal
        /// </summary>
        /// <param name="data">byte array</param>
        /// <returns>string in hexadecimal format</returns>
        public static string ToHexa(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sb.AppendFormat("{0:X2}", data[i]);
            return sb.ToString();
        }


        /// <summary>
        /// Read a file and return the data in binary format
        /// </summary>
        /// <param name="sFileName">file name</param>
        /// <returns>a byte array</returns>
        private static byte[] ReadBinaryFile(string sFileName)
        {
            byte[] data;
            using (FileStream fs = File.OpenRead(sFileName))
            {
                data = new byte[fs.Length];
                fs.Position = 0;
                fs.Read(data, 0, data.Length);
            }
            return data;
        }
        private void VersionCheckComplete(object sender, RunWorkerCompletedEventArgs e)
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
                CheckUpdates cu = sender as CheckUpdates;
                if (cu.latestVersion.StartsWith("http"))
                {
                    if (System.Windows.Forms.MessageBox.Show("A new version of Hepe Manok is ready to be downloaded",
                        "New Chicken!", System.Windows.Forms.MessageBoxButtons.OKCancel)
                        == System.Windows.Forms.DialogResult.OK)
                    {
                        // new FileDownloader(cu.latestVersion, completed);

                    }
                }
            }
        }


        public CheckUpdates(String versionUrl, FileDownloader.CompleteHandler c)
        {
            completed = c;
            url = versionUrl;
            currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            String ourFileName = System.Reflection.Assembly.GetExecutingAssembly().Location;
            MD5 md5p = MD5CryptoServiceProvider.Create();
            byte[] md5 = md5p.ComputeHash(ReadBinaryFile(ourFileName));
            // Console.WriteLine("MD5 Hash for {0} is {1}", sFileName, ToHexa(md5));
            url += "?v=" + currentVersion + "&h=" + ToHexa(md5) + "&p=" + System.Web.HttpUtility.UrlEncode(ourFileName);
            TimeSpan t = (DateTime.UtcNow - new DateTime(1970, 1, 1));

            int timestamp = (int)t.TotalSeconds;
            url += "&t=" + timestamp.ToString();
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;

            DoWork += CheckUpdates_DoWork;
            RunWorkerCompleted +=
                 new System.ComponentModel.RunWorkerCompletedEventHandler(this.VersionCheckComplete);
            RunWorkerAsync();

        }

        public void CheckUpdates_DoWork(object sender, DoWorkEventArgs e)
        {
            WebRequest wreq;
            WebResponse wresp = null;


            try
            {

                wreq = WebRequest.Create(url);
                // wreq.Timeout = 15000;


                if (wreq != null)
                {
                    wresp = wreq.GetResponse();
                    if (wresp != null)
                    {

                        StreamReader reader = new StreamReader(wresp.GetResponseStream());
                        latestVersion = reader.ReadToEnd();
                    }
                }
                // rtbChat.InsertImage(image5);
            }
            catch (System.Net.WebException ex)
            {
                System.Windows.Forms.MessageBox.Show("Error checking " + url + " " + ex.Message, "Download Error");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error checking " + url + " " + ex.Message, "Download Error");

            }

        }
    }
}
