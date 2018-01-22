using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Cache;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.ComponentModel;

namespace CSharp.cc
{
    
    public class ReportException : BackgroundImageLoader
    {
        static public string url = "http://manok.me/exception.php";
        static public void WebReportBackground(Exception ex)
        {
            WebReport(ex);
        }

        static public void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            WebReport(e.Argument as Exception);
        }
      
        static public void WebReport(Exception ex)
        {
            HttpRequestCacheLevel cacheLevel = HttpRequestCacheLevel.BypassCache;  // CacheIfAvailable, Reload
            HttpRequestCachePolicy cachePolicy = new HttpRequestCachePolicy(cacheLevel);
            WebRequest request;
            WebResponse response;

            try
            {
                request = (HttpWebRequest)WebRequest.Create(url); //  + EncodeTo64(playerImgUrl));
                request.CachePolicy = cachePolicy;


                // http://msdn.microsoft.com/en-us/library/debx8sh9.aspx
                // Create a request using a URL that can receive a post. 
                // WebRequest request = WebRequest.Create("http://www.contoso.com/PostAccepter.aspx ");
                // Set the Method property of the request to POST.
                request.Method = "POST";
                // Create POST data and convert it to a byte array.

                JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                Dictionary<String, String> jsonError = new Dictionary<string, string>();
                jsonError.Add("Message", ex.Message);
                jsonError.Add("StackTrace", ex.StackTrace);
                if (ex.Data.Count > 0)
                {
                    foreach (System.Collections.DictionaryEntry pair in ex.Data)
                    {
                        if (pair.Key is string && pair.Value is string)
                        {
                            if (pair.Value.ToString().StartsWith("file://"))
                            {
                                // using (BinaryReader b = new BinaryReader(File.Open((pair.Value as string).Replace("file://", ""), FileMode.Open, FileAccess.Read))) {
                                jsonError.Add(pair.Key.ToString(), FileToBase64((pair.Value as string).Replace("file://", "")));
                            }
                            else
                            {

                                jsonError.Add(pair.Key.ToString(), pair.Value.ToString());
                            }
                        } 
                    }
                }
                string postData = oSerializer.Serialize(jsonError);
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/json";
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;
                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.
                dataStream.Close();
                // Get the response.
                response = request.GetResponse();
                // Display the status.
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                // dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                // StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                // string responseFromServer = reader.ReadToEnd();
                // Display the content.
                // Console.WriteLine(responseFromServer);
                // Clean up the streams.
                // reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception  e)
            {
                System.Console.WriteLine(e.Message + ex.StackTrace);
            }
        }

        // http://msdn.microsoft.com/en-us/library/dhx0d524.aspx
        static public string FileToBase64(String inputFileName)
        {
            System.IO.FileStream inFile;
            byte[] binaryData;

            try
            {
                inFile = new System.IO.FileStream(inputFileName,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read);
                binaryData = new Byte[inFile.Length];
                long bytesRead = inFile.Read(binaryData, 0,
                                     (int)inFile.Length);
                inFile.Close();
            }
            catch (System.Exception exp)
            {
                // Error creating stream or reading from it.
                System.Console.WriteLine("{0}", exp.Message);
                return "Exception: " + exp.Message;
            }

            // Convert the binary input into Base64 UUEncoded output.
            string base64String;
            try
            {
                base64String =
                  System.Convert.ToBase64String(binaryData,
                                         0,
                                         binaryData.Length);
            }
            catch (System.ArgumentNullException)
            {
                System.Console.WriteLine("Binary data array is null.");
                return "Exception: Binary data array is null.";
            }
#if false
            // Write the UUEncoded version to the output file.
            System.IO.StreamWriter outFile;
            try
            {
                outFile = new System.IO.StreamWriter(outputFileName,
                                     false,
                                     System.Text.Encoding.ASCII);
                outFile.Write(base64String);
                outFile.Close();
            }
            catch (System.Exception exp)
            {
                // Error creating stream or writing to it.
                System.Console.WriteLine("{0}", exp.Message);
            }
#endif

            return base64String;
        }

    }


}

/*
Assuming data is your Base64CharArray

Byte[] bitmapData = new Byte[data.Length];

bitmapData = Convert.FromBase64CharArray(data, 0, data.length);

System.IO.MemoryStream streamBitmap = new 
     System.IO.MemoryStream(bitmapData);

Bitmap bitImage = new 
     Bitmap((Bitmap)Image.FromStream(streamBitmap));
 * 
 * 
 * 
 * public string ImageToBase64String(Image imageData ImageFormat format)
{
string base64;
MemoryStream memory = new MemoryStream();
imageData.Save(memory, format);
base64 = System.Convert.ToBase64String(memory.ToArray());
memory.Close();
memory = null;

return base64;
}

*/