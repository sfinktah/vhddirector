//Downloaded from
//Visual C# Kicks - http://vcskicks.com/
using System;
using System.Collections.Generic;
// using System.ComponentModel;
using System.Data;
using System.Text;
//Needed
using System.IO;
using System.Net;

namespace CSharp.cc.Net.Web
{

        
    public class Download 
    {
        private byte[] downloadedData;
        public int progressBar = 0;
        public int Length = 0;
        public int progressBarMaximum = 0;

        //Connects to a URL and attempts to download the file
        private void downloadData(string url)
        {
            progressBar = 0;



            downloadedData = new byte[0];
            try
            {
                //Optional
                Console.WriteLine("Connecting to " + url);
                // Application.DoEvents();

                //Get a data stream from the url
                WebRequest req = WebRequest.Create(url);
                WebResponse response = req.GetResponse();
                Stream stream = response.GetResponseStream();

                //Download in chuncks
                byte[] buffer = new byte[1024];

                //Get Total Size
                int dataLength = (int)response.ContentLength;

                //With the total data we can set up our progress indicators
                progressBarMaximum = dataLength;
                // // lbProgres.Text = "0/" + dataLength.ToString();

                Console.WriteLine("Downloading...");
                // Application.DoEvents();

                //Download to memory
                //Note: adjust the streams here to download directly to the hard drive
                MemoryStream memStream = new MemoryStream();
                while (true)
                {
                    //Try to read the data
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        //Finished downloading
                        progressBar = progressBarMaximum;
                        // // lbProgres.Text = dataLength.ToString() + "/" + dataLength.ToString();

                        // Application.DoEvents();
                        break;
                    }
                    else
                    {
                        //Write the downloaded data
                        memStream.Write(buffer, 0, bytesRead);

                        //Update the progress bar
                        if (progressBar + bytesRead <= progressBarMaximum)
                        {
                            progressBar += bytesRead;
                            // lbProgres.Text = progressBar.ToString() + "/" + dataLength.ToString();
                            
                            // progressBar1.Refresh();
                            // Application.DoEvents();
                        }                        
                    }
                }

                //Convert the downloaded stream to a byte array
                downloadedData = memStream.ToArray();

                //Clean up
                stream.Close();
                memStream.Close();
            }
            catch (Exception)
            {
                //May not be connected to the internet
                //Or the URL might not exist
                Console.WriteLine("There was an error accessing the URL.");
            }

            Length = downloadedData.Length;
            // this.Text = "Download Data through HTTP";
        }

        ////Start the downloading process
        //private void btnDownload_Click(object sender, EventArgs e)
        //{
        //    downloadData(txtUrl.Text);

        //    //Get the last part of the url, ie the file name
        //    if (downloadedData != null && downloadedData.Length != 0)
        //    {
        //        string urlName = txtUrl.Text;
        //        if (urlName.EndsWith("/"))
        //            urlName = urlName.Substring(0, urlName.Length - 1); //Chop off the last '/'

        //        urlName = urlName.Substring(urlName.LastIndexOf('/') + 1);

        //        saveDiag1.FileName = urlName;
        //    }
        //}

        ////Take data from memory and save it to the drive
        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    if (downloadedData != null && downloadedData.Length != 0)
        //    {
        //        if (saveDiag1.ShowDialog() == DialogResult.OK)
        //        {
        //            this.Text = "Saving Data...";
        //            Application.DoEvents();

        //            //Write the bytes to a file
        //            FileStream newFile = new FileStream(saveDiag1.FileName, FileMode.Create);
        //            newFile.Write(downloadedData, 0, downloadedData.Length);
        //            newFile.Close();

        //            this.Text = "Download Data";
        //            MessageBox.Show("Saved Successfully");
        //        }
        //    }
        //    else
        //        MessageBox.Show("No File was Downloaded Yet!");
        //}
    }
}