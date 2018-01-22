using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.ComponentModel;
using System.Net;

namespace VhdDirectorApp
{
    public delegate void NoUpdateEventHandler(object sender, EventArgsString e);
    public delegate void UpdateEventHandler(object sender, EventArgsString e);

    public class EventArgsString : EventArgs
    {
        public string Target;
        public EventArgsString(string s)
        {
            Target = s;
        }
    }


    public class CheckForUpdates : CSharp.cc.Threading.Backgrounder
    {

        public event NoUpdateEventHandler updateNotRequired;
        public event UpdateEventHandler updateRequired;

        private string currentMd5;
        private string ourFileName;
        public  string versionUrl;
        private string latestVersion = string.Empty;
        private FileDownloader.CompleteHandler completed;
        private string checkUpdateUrl;
        private string GetOurMd5()
        {
            ourFileName = Ourself.FileName();
            MD5 md5p = MD5CryptoServiceProvider.Create();
            byte[] md5 = md5p.ComputeHash(ReadBinaryFile(ourFileName));
            return ToHexa(md5);
            // Console.WriteLine("MD5 Hash for {0} is {1}", sFileName, );
        }

        private string GetUpdateUrl() {
            string url = this.versionUrl;
            string currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            url += "?v=" + currentVersion + "&h=" + currentMd5 + "&p=" + System.Web.HttpUtility.UrlEncode(ourFileName);
            TimeSpan t = (DateTime.UtcNow - new DateTime(1970, 1, 1));
            int timestamp = (int)t.TotalSeconds;
            url += "&t=" + timestamp.ToString();
            return url;
        }

        public CheckForUpdates(String versionUrl, UpdateEventHandler updateRequiredHandler, NoUpdateEventHandler updateNotRequiredHandler) : this(versionUrl, updateRequiredHandler)
        {
            this.updateNotRequired = updateNotRequiredHandler;
            
        }
        public CheckForUpdates(String versionUrl, UpdateEventHandler updateRequiredHandler) : this(versionUrl)
        {
            this.updateRequired = updateRequiredHandler;
        }

        public CheckForUpdates(String versionUrl) : this()
        {
            this.versionUrl = versionUrl;
        }

        public CheckForUpdates()
        {
        }

        public void Start()
        {
            this.currentMd5 = GetOurMd5();
            this.checkUpdateUrl = GetUpdateUrl();

            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;

            DoWork += CheckForUpdates_DoWork;
            RunWorkerCompleted +=
                 new System.ComponentModel.RunWorkerCompletedEventHandler(this.VersionCheckComplete);
            RunWorkerAsync();
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
                CheckForUpdates cu = sender as CheckForUpdates;
                if (cu.latestVersion.StartsWith("http"))
                {
                    OnUpdateRequired(new EventArgsString(cu.latestVersion));
                }
                else
                {
                    OnUpdateNotRequired(new EventArgsString(cu.latestVersion));
                }
            }
        }

        public void CheckForUpdates_DoWork(object sender, DoWorkEventArgs e)
        {
            WebRequest wreq;
            WebResponse wresp = null;


            try
            {

                wreq = WebRequest.Create(this.checkUpdateUrl);
                wreq.Timeout = 15000;

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
                System.Windows.Forms.MessageBox.Show("Error checking " + this.checkUpdateUrl + " " + ex.Message, "Download Error");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error checking " + this.checkUpdateUrl + " " + ex.Message, "Download Error");

            }

        }

        protected virtual void OnUpdateRequired(EventArgsString e)
        {
            if (updateRequired != null)
                updateRequired(this, e);
        }

        protected virtual void OnUpdateNotRequired(EventArgsString e)
        {
            if (updateNotRequired != null)
                updateNotRequired(this, e);
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

        public string UpdateUrl { get; set; }
    }
}
