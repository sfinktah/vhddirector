#region Copyright
//	This program is licensed under the terms of the eBay Common Development and
//  Distribution License (CDDL) Version 1.0 (the "License") and any subsequent
//  version thereof released by eBay.  The then-current version of the License
//  can be found at http://www.opensource.org/licenses/cddl1.php
#endregion

// Background Worker
// http://msdn.microsoft.com/en-us/library/system.componentmodel.backgroundworker.aspx

// Threading basics
// http://www.albahari.com/threading/

// Async Web MSDN 
// http://msdn.microsoft.com/en-us/library/system.net.webrequest.begingetresponse.aspx

// Events
// http://www.codeproject.com/KB/cs/csevents01.aspx

// Event handling and proper practice
// http://trevorunlocked.blogspot.com/2007/10/c-advanced-event-handling-memory.html

// More Threading (Good detailed)
// http://www.codeproject.com/KB/threads/ThreadingDotNet.aspx

// XML Document/Element/Node/Textnode insertion
// http://www.devx.com/tips/Tip/21168



using System;
using System.Windows.Forms;
using System.Net;			// for the HttpWebRequest
using System.Text;			// for the character encoding
using System.IO;			// for Stream(s)
using System.Runtime;
using System.Xml;			// for XmlDocument Processing
using System.Configuration; // for AppSettings (keys, tokens, etc)
using System.Reflection;	// for Embedded XML File
using System.Threading;
using System.Xml.XPath;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using CSharp.cc.Data;
using CSharp.cc.Xml;


namespace CSharp.cc.eBay
{
    /// <summary>
    /// Background Retrieval of eBay API calls
    /// </summary>
    /// <summary>
    /// Background Retrieval of eBay API calls
    /// </summary>
    public class XmlObjectRetriever
    {
        public String apiCall = String.Empty;
        public Object CompletedReturnObject = null;
        public XmlApi Api;
        public XmlData dataStoreObject = null;
        public XmlNode request;
        public String ResultFilter = String.Empty;

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        public delegate void CompleteHandler(object source, object eventArgument);
        public event CompleteHandler completed;

        private void Init()
        {
            Api = new XmlApi(apiCall);
            request = Api.innerNode;
            InitializeBackgoundWorker();
        }

        /// <summary>
        /// Stores result from eBay ApiCall in xmlDataObject 
        /// </summary>
        public XmlObjectRetriever() { Init(); }

        public XmlObjectRetriever(String apiCall)
        {
            this.apiCall = apiCall;
            Init();
        }
        /// <summary>
        /// Stores result from eBay ApiCall in xmlDataObject 
        /// </summary>
        /// <param name="apiCall">Name of Api Call</param>
        /// <param name="dataStoreObject">Object to store result</param>
        public XmlObjectRetriever(String apiCall, XmlData dataStoreObject)
        {
            this.dataStoreObject = dataStoreObject;
            this.apiCall = apiCall;
            this.Init();
        }

        /// <summary>
        /// Run the ebay request in the backgound
        /// </summary>
        public void Run()
        {
            for (; ; )
            {
                Api.Execute();
                if (Api.Error())
                {
                    String ShortMessage = Xml.XPath.SelectSingleNode(Api.ResponseXml, "/*/Errors/ShortMessage");
                    Exception ex = new Exception(ShortMessage);
                    throw ex;
                }
                dataStoreObject.Load(Api.ResponseXml);


                String TotalPages = XPath.SelectSingleNode(Api.ResponseXml, "//PaginationResult/TotalNumberOfPages");
                String CurrentPage = XPath.SelectSingleNode(Api.ResponseXml, "//PageNumber");

                int nTotalPages = Convert.ToInt16(TotalPages);
                int nCurrentPage = Convert.ToInt16(CurrentPage);

                if (false) // nCurrentPage > 0 && nCurrentPage < nTotalPages)
                {
                    // updateStatus("Moving to page (" + (++nCurrentPage).ToString() + " of " + nTotalPages + ")");
                    // progressBar1.Maximum = nTotalPages;
                    // progressBar1.Value = nCurrentPage - 1;

                    nCurrentPage++;
                    Api.innerNode["Pagination"]["PageNumber"].InnerText = nCurrentPage.ToString();
                }
                else
                {
                    // updateStatus("Retrieved all pages.");
                    break;

                }
            }

            // CompletedHandler(this, CompletedReturnObject);
            if (completed != null)
            {
                completed(this, dataStoreObject);
            }
        }

        #region backgroundworker

        // http://msdn.microsoft.com/en-us/library/system.componentmodel.backgroundworker(VS.80).aspx
        private void InitializeBackgoundWorker()
        {
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork
                += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted
                += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged
                += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }

        public void BackgroundRun()
        {
            backgroundWorker1.RunWorkerAsync(CompletedReturnObject);
        }

        /// <summary>
        /// Background Worker calls this to start the job
        /// </summary>
        /// <param name="sender">backgroundworker internal</param>
        /// <param name="e">backgroundworker internal</param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the 
            // RunWorkerCompleted eventhandler.
            Api.Execute();
            e.Result = Api.Result;
            // e.Result = Api.Result; //  "Hello"; // ComputeFibonacci((int)e.Argument, worker, e);
            // Completed(this, 1);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        ///<summary>Event called when background worker has finished</summary>
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                MessageBox.Show("Canceled");
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                completed(this, CompletedReturnObject);
                // MessageBox.Show(e.Result.ToString());
            }

        #endregion

        }


    }


    public class XmlApiBackground
    {
        public delegate void CompleteHandler(object source, object eventArgument);
        public event CompleteHandler Completed;
        public XmlApi Api;
        public Object CompletedReturnObject = null;


        private System.ComponentModel.BackgroundWorker backgroundWorker1;

        public XmlApiBackground(String CallName)
        {
            Api = new XmlApi(CallName);

            InitializeBackgoundWorker();
        }

        // Set up the BackgroundWorker object by 
        // attaching event handlers. 
        private void InitializeBackgoundWorker()
        {
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            // backgroundWorker1.ProgressChanged +=                new ProgressChangedEventHandler(            backgroundWorker1_ProgressChanged);
        }

        public void BackgroundRun()
        {
            backgroundWorker1.RunWorkerAsync(CompletedReturnObject);
        }

        public void WaitForCompletion()
        {
            while (backgroundWorker1.IsBusy)
            {
                Thread.Sleep(1000);
            }
            return;
        }

        // This event handler is where the actual,
        // potentially time-consuming work is done.
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the 
            // RunWorkerCompleted eventhandler.
            Api.Execute();
            e.Result = Api.Result;
            // e.Result = Api.Result; //  "Hello"; // ComputeFibonacci((int)e.Argument, worker, e);
            // Completed(this, 1);
        }


        // This event handler deals with the results of the
        // background operation.
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                MessageBox.Show("Canceled");
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                Completed(this, CompletedReturnObject);
                // MessageBox.Show(e.Result.ToString());
            }


        }
    }

    public class XmlApi
    {
        protected string devID;
        protected string appID;
        protected string certID;
        protected string serverUrl;
        protected string userToken;
        protected int siteID;  // eBay SiteID = 0  (US) - UK = 3, Canada = 2, Australia = 15, ....

        public Boolean fakeTurboLister = false;
        public ArrayList requestHeaders = new ArrayList();
        public XmlDocument RequestXml = new XmlDocument();
        public XmlDocument ResponseXml = new XmlDocument();
        public String ErrorCode = null;
        public String ErrorLong = null;
        public String ErrorShort = null;
        public String ErrorSeverity = null;
        protected String xmlText = String.Empty;
        protected String _Result = String.Empty;
        public string Result
        {
            get { return _Result; }
            set { _Result = value; }
        }
        protected String _callName;
        public string Call
        {
            get { return _callName; }
            set { _callName = value; }
        }
        public String RequestNodeName
        {
            get { return _callName + "Request"; }
        }
        public String PageNumber
        {
            get { return RequestXml["GetMyeBaySellingRequest"]["Pagination"]["PageNumber"].InnerText; }
            set { RequestXml["GetMyeBaySellingRequest"]["Pagination"]["PageNumber"].InnerText = value; }
        }
        public XmlNode innerNode
        {
            get { return RequestXml.SelectSingleNode("*"); }
        }
        public String innerNodeName
        {
            get { return RequestXml.SelectSingleNode("*").Name; }
        }




        // InitGlobalConfig -> Prepare [...] Execute
        public XmlApi(String callName)
        {
            Call = callName;
            InitGlobalConfig();
            Prepare();
            // User calls execute
        }

        protected void InitGlobalConfig()
        {
            #region Declare Needed Variables

            //Get the Keys from App.Config file
            devID = ConfigurationManager.AppSettings["DevID"];
            appID = ConfigurationManager.AppSettings["AppID"];
            certID = ConfigurationManager.AppSettings["CertID"];

            //Get the Server to use (Sandbox or Production)
            serverUrl = ConfigurationManager.AppSettings["ServerUrl"];

            //Get the User Token to Use
            userToken = ConfigurationManager.AppSettings["UserToken"];

            //SiteID = 0  (US) - UK = 3, Canada = 2, Australia = 15, ....
            //SiteID Indicates the eBay site to associate the call with
            siteID = 15;
            #endregion
        }

        // <summary>Load the XML base file for Api Call</summary>
        public void Prepare()
        {

            #region GetAppDir
            string executableName = Application.ExecutablePath;
            FileInfo executableFileInfo = new FileInfo(executableName);
            string executableDirectoryName = executableFileInfo.DirectoryName;
            #endregion
            string eBayCallXmlFile = _callName + ".xml";
            string myFile = Path.Combine(executableDirectoryName, eBayCallXmlFile);

            try
            {
                RequestXml.PreserveWhitespace = false;
                RequestXml.Load(myFile);
                Console.WriteLine(RequestXml.OuterXml);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            // Add our auth key
            RequestXml[_callName + "Request"]["RequesterCredentials"]["eBayAuthToken"].InnerText = userToken;

            // Additional Options are now set by caller, who then calls Execute()
        }


        public void Execute()
        {
            HttpWebRequest request;

            #region UTF8Encode
            //Get XML into a string for use in encoding
            xmlText = RequestXml.InnerXml;



            //Put the data into a UTF8 encoded  byte array
            UTF8Encoding encoding = new UTF8Encoding();
            int dataLen = encoding.GetByteCount(xmlText);
            byte[] utf8Bytes = new byte[dataLen];
            Encoding.UTF8.GetBytes(xmlText, 0, xmlText.Length, utf8Bytes, 0);
            #endregion


            #region Setup The Request (inc. HTTP Headers
            //Create a new HttpWebRequest object for the ServerUrl
            request = (HttpWebRequest)WebRequest.Create(serverUrl);

            //Set Request Method (POST) and Content Type (text/xml)
            request.Method = "POST";
            request.ContentType = "text/xml";
            request.ContentLength = utf8Bytes.Length;
            request.AutomaticDecompression = DecompressionMethods.GZip;

            if (fakeTurboLister)
            {
                foreach (String header in CSharp.cc.Xml.XPath.SelectSingleNodes(@"
                    <requestheaders>
                        <header>X-EBAY-API-COMPATIBILITY-LEVEL: 601</header>
                        <header>X-EBAY-API-DEV-NAME: pebayktwo</header>
                        <header>X-EBAY-API-APP-NAME: deucedev</header>
                        <header>X-EBAY-API-CERT-NAME: pebayktwo9812p</header>
<!-- 
                        <header>X-EBAY-API-CALL-NAME: GetItems</header>
                        <header>X-EBAY-API-SITEID: 0</header>
-->
                        <header>X-EBAY-API-DETAIL-LEVEL: ReturnAll</header>
                        <header>X-EBAY-API-FLAGS: 711023.7.3512</header>
                      </requestheaders>", "//header"))
                {
                    request.Headers.Add(header);
                }

                request.UserAgent = "Turbo Lister";
            }
            else
            {
                //Add the Keys to the HTTP Headers
                request.Headers.Add("X-EBAY-API-DEV-NAME: " + devID);
                request.Headers.Add("X-EBAY-API-APP-NAME: " + appID);
                request.Headers.Add("X-EBAY-API-CERT-NAME: " + certID);

                //Add Compatability Level to HTTP Headers
                //Regulates versioning of the XML interface for the API
                request.Headers.Add("X-EBAY-API-COMPATIBILITY-LEVEL: 551");
            }

            //Add function name, SiteID and Detail Level to HTTP Headers
            request.Headers.Add("X-EBAY-API-CALL-NAME: " + _callName);
            request.Headers.Add("X-EBAY-API-SITEID: " + siteID.ToString());

            // WebHeaderCollection headers = request.Headers;
            if (requestHeaders.Count > 0)
            {
                foreach (String header in requestHeaders)
                {

                    request.Headers.Add(header);
                }
            }

            //Time out = 300 seconds,  set to -1 for no timeout.
            //If times-out - throws a WebException with the
            //Status property set to WebExceptionStatus.Timeout.
            request.Timeout = 300000;

            #endregion

            #region Send The Request

            Stream responseStream = null;
            String Html = String.Empty;
            HttpWebResponse response = null;

            byte[] lbPostBuffer = Encoding.Default.GetBytes(xmlText);
            request.ContentLength = lbPostBuffer.Length;

            try
            {
                Stream PostStream = request.GetRequestStream();
                PostStream.Write(lbPostBuffer, 0, lbPostBuffer.Length);
                PostStream.Close();


                response = (HttpWebResponse)request.GetResponse();
                responseStream = response.GetResponseStream();

                StreamReader Reader = new StreamReader(responseStream, Encoding.Default);
                Html = Reader.ReadToEnd();

            }
            catch (WebException wEx)
            {
                //Error has occured whilst requesting
                //Display error message and exit.
                if (wEx.Status == WebExceptionStatus.Timeout)
                    Console.WriteLine("Request Timed-Out.");
                else
                    Console.WriteLine(wEx.Message);

                Html = "<" + _callName + "Request>"
                    + Xml.Formatters.ErrorAsXml("Web Request Timeout", wEx.Message, "Error")
                    + "</" + _callName + "Request>";
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    responseStream.Close();

                }
            }
            #endregion


            #region Process Response

            ResponseXml.LoadXml(Html);


            //get the root node, for ease of use
            // The root node will be called the name of this class 
            // (idealistic thinking)

            // XmlNode root = xmlDoc["GeteBaySellingResponse"]; // "GeteBayOfficialTimeResponse"];

            // This look a bit silly, but it's automatic at least.
            // XmlNode test = ResponseXml[_callName + "Response"];
            // XmlNode root = ResponseXml[ResponseXml.DocumentElement.Name.ToString()];
            //There have been Errors

            //if (root["Errors"] != null)
            //{
            //    try
            //    {
            //        ErrorCode = root["Errors"]["ErrorCode"].InnerText;
            //        ErrorShort = root["Errors"]["ShortMessage"].InnerText;
            //        ErrorLong = root["Errors"]["LongMessage"].InnerText;
            //        ErrorSeverity = root["Errors"]["SeverityCode"].InnerText;
            //    }
            //    catch
            //    {
            //        Console.WriteLine(root["Errors"].OuterXml);
            //    }
            //}

            _Result = Html;
            #endregion


        }


        public bool Error()
        {
            /*
            - <GetItemEventsResponse xmlns="urn:ebay:apis:eBLBaseComponents">
              <Ack>Failure</Ack> 
                - <Errors>
                      <ShortMessage>Requested user is suspended.</ShortMessage> 
                      <LongMessage>The account for user ID "xxxxx" specified in this request is suspended. Sorry, you can only request information for current users.</LongMessage> 
                      <ErrorCode>841</ErrorCode> 
                      <SeverityCode>Error</SeverityCode> 
                    - <ErrorParameters ParamID="0">
                          <Value>xxxxx</Value> 
                      </ErrorParameters>
                      <ErrorClassification>RequestError</ErrorClassification> 
                  </Errors>
              </GetItemEventsResponse>
             */
            XmlNode xe;
            if ((xe = ResponseXml.SelectSingleNode("/*/Errors/SeverityCode")) != null)
            {
                if (xe.InnerText == "Error")
                {
                    return true;
                }
            }

            return false;
        }
    }
}



