using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Net.Cache;
using System.Net;
using System.IO;

using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
// 1. Make sure the backgroundcolor set to 'white' for better performance, not set to 'transparent'.
// 2. The backgroundimagelayout set to be either 'center' or 'streach'.
// 3. Save the image as a resource file and assign it to the form at runtime.
// 4. Use the DoubleBuffer, set to 'true'.
namespace CSharp.cc
{
    public class BackgroundImageLoader
    {
        static public Queue<QueueItem<String>> m_Queue = new Queue<QueueItem<String>>();

        /// <summary>
        /// The method create a Base64 encoded string from a normal string.
        /// </summary>
        /// <param name="toEncode">The String containing the characters to encode.</param>
        /// <returns>The Base64 encoded string.</returns>
        static public string EncodeTo64(string toEncode)
        {

            byte[] toEncodeAsBytes

                  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);

            string returnValue

                  = System.Convert.ToBase64String(toEncodeAsBytes);

            return returnValue.Replace("/", "_");

        }

        /// <summary>
        /// The method to Decode your Base64 strings.
        /// </summary>
        /// <param name="encodedData">The String containing the characters to decode.</param>
        /// <returns>A String containing the results of decoding the specified sequence of bytes.</returns>
        public static string DecodeFrom64(string encodedData)
        {
            try
            {
                byte[] encodedDataAsBytes
                    = System.Convert.FromBase64String(encodedData.Replace("_", "/"));

                string returnValue =
                   System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);

                return returnValue;
            }
            catch (Exception)
            {
                return encodedData;
            }
        }

        static public Bitmap BitmapFromUrl(String playerImgUrl)
        {
            Bitmap bmp = null;
            HttpRequestCacheLevel cacheLevel = HttpRequestCacheLevel.CacheIfAvailable;  // CacheIfAvailable, Reload
            HttpRequestCachePolicy cachePolicy = new HttpRequestCachePolicy(cacheLevel);
            HttpWebRequest request;
            HttpWebResponse response;
            Stream receiveStream = null;

            try
            {
                request = (HttpWebRequest)WebRequest.Create("http://chips3.nt4.com/image_proxy.php/" + EncodeTo64(playerImgUrl));
                request.CachePolicy = cachePolicy;
                request.ServicePoint.ConnectionLimit = 1;
                request.KeepAlive = true;
                request.Proxy = new WebProxy();
                response = (HttpWebResponse)request.GetResponse();
                receiveStream = response.GetResponseStream();
                if (receiveStream != null)
                {
                    try
                    {
                        bmp = new Bitmap(receiveStream);
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine("Caught exception processing downloaded image: {0}", ex.Message);
                        bmp = null;
                        throw new Exception(ex.Message);
                    }
                }

                Console.WriteLine("Before reading stream: used " + (response.IsFromCache ? "CACHE" : "SERVER")
                        + " version for servicing request "
                        + playerImgUrl + " (cache level = " + cacheLevel + ")" + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught exception downloading {0}: {1}", playerImgUrl, ex.Message);
            }

            if (receiveStream != null) receiveStream.Close();

            return bmp;
        }


        public void ExampleUsage(String playerImgUrl)
        {


            QueuedBackgroundWorker.QueueWorkItem(m_Queue, playerImgUrl,
                (args) => { return BitmapFromUrl(playerImgUrl); },
                (args) => { if (args.Result != null && args.Result is Bitmap) ; }
            );
        }
    }
}