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


namespace VHD_Director
{
    public partial class DownloadFileForm : Form
    {
        public DownloadFileForm()
        {
            InitializeComponent();
            richTextBox1.Rtf = @"{\rtf1\ansi\ansicpg65001\cocoartf1038\cocoasubrtf360
{\fonttbl\f0\fnil\fcharset0 SegoeUI;}
{\colortbl;\red255\green255\blue255;}
\paperw11900\paperh16840\margl1440\margr1440\vieww18820\viewh9040\viewkind0
\deftab720
\pard\pardeftab720\sa320\ql\qnatural

\f0\fs36 \cf0 Microsoft Virtual Server 2005 R2 SP1 - Enterprise Edition
\fs24 \
\pard\pardeftab720\sa300\ql\qnatural
\cf0 Download the service pack 1 for Microsoft Virtual Server 2005 R2 Enterprise Edition. Virtual Server 2005 R2 SP1 is a cost-effective and well supported server virtualization technology for the Windows Server System\'99 platform. As a key part of any server consolidation strategy, Virtual Server increases hardware utilization and enables organizations to rapidly configure and deploy new servers.}";
        }

        private void DownloadFileForm_Load(object sender, EventArgs e)
        {

        }

        private Timer rtbSize = null;
        private void richTextBox1_ClientSizeChanged(object sender, EventArgs e)
        {
            if (rtbSize == null)
            {
                rtbSize = new Timer();
                rtbSize.Tick += new EventHandler(rtbCheckSize);
                // Set the Interval to 5 seconds.
                rtbSize.Enabled = true;
                rtbSize.Interval = 300;
            }

            rtbSize.Start();
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

        public string url = "http://download.microsoft.com/download/d/7/2/d7235926-a10d-482c-a2ff-6e0d3130f869/64-BIT/setup.exe";
        private void button2_Click(object sender, EventArgs e)
        {
            if (!Is64bitOS)
            {
                url = url.Replace("64-BIT", "32-BIT");
            }
            // FileDownloader fd = new FileDownloader(url, fileCompleteHandler);

            string fileName = @"VirtualServerSetup.exe";
            string tempPath = System.IO.Path.GetTempPath();
            // string destinationFile = tempPath + downloadUrl.Remove(downloadUrl.LastIndexOf('/'));
            string destinationFile = tempPath + fileName;

            // To receive notification when the file is available, add an event handler to the DownloadFileCompleted event. 

            progressBar1.Value = 0;
            progressBar1.Maximum = 100;
            progressBar1.Step = 1;

            // Create a new WebClient instance.
            WebClient myWebClient = new WebClient();
            myWebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(myWebClient_DownloadProgressChanged);
            myWebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(myWebClient_DownloadFileCompleted);

            // Concatenate the domain with the Web resource filename.
            textBox1.AppendText(String.Format("Downloading \"{0}\" from \"{1}\" .......\n\n", fileName, url));
            // Download the Web resource and save it into the current filesystem folder.
            
            myWebClient.DownloadFileAsync(new Uri(url), destinationFile);
            // myWebClient_DownloadFileCompleted(null, null);
        }

        public bool Is64bitOS
        {
            get { return (Environment.GetEnvironmentVariable("ProgramFiles(x86)") != null); }
        }

        void myWebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            string msi = "Virtual Server 2005 Install.msi";
             string tempPath = System.IO.Path.GetTempPath();
            // string destinationFile = tempPath + downloadUrl.Remove(downloadUrl.LastIndexOf('/'));
            string sourceFile = tempPath + "VirtualServerSetup.exe";
            string destinationFile = tempPath;

            progressBar1.Value = 100;
            textBox1.AppendText("File downloaded.\r\n");
            // For the example

            try
            {
                System.IO.File.Delete(tempPath + "Virtual Server 2005 Install.msi");
            }
            catch (Exception ex)
            {
                textBox1.AppendText("Couldn't delete " + msi + "(" + ex.Message + ")" + "-" + ex.GetType().ToString() + "\r\n");
            }

            //  C:\Users\Administrator\AppData\Local\Temp\VirtualServerSetup.exe /c /t "C:\Users\Administrator\AppData\Local\Temp\"

            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = sourceFile;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = "/c /t " + destinationFile.Replace(" ", "\\ ");
            textBox1.AppendText("Extracting with: " + startInfo.FileName + " " + startInfo.Arguments + "\r\n");

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                    textBox1.AppendText(String.Format("Extraction completed with errorlevel {0}.\r\n", exeProcess.ExitCode));
                    if (exeProcess.ExitCode != 0)
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                textBox1.AppendText(String.Format("Caught Exception trying to extract msi: {0}\n", ex.Message));
                return;
                // Log error.
            }

            //  setup.exe /c /t c:\SetupFiles
            //      msiexec /i "Virtual Server 2005 Install.msi" /qn ADDLOCAL=VHDMount

            startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = "msiexec";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = "/i \"" + tempPath + msi + "\" /qb ADDLOCAL=VHDMount";
            textBox1.AppendText("Performing partial install: " + startInfo.FileName + " " + startInfo.Arguments + "\r\n");

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                    textBox1.AppendText(String.Format("Install completed with errorlevel {0}.\r\n", exeProcess.ExitCode));

                }
            }
            catch (Exception ex)
            {
                textBox1.AppendText(String.Format("Caught Exception trying to install: {0}\n", ex.Message));
                return;
                // Log error.
            }
        }

        void myWebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void fileCompleteHandler(object source, object eventArgument)
        {
        }
    }
}
