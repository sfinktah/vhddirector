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
using CodeKicker.BBCode;


namespace VHD_Director
{
    public partial class DownloadUnpackForm : Form
    {

        static public void Example() {
            DownloadUnpackForm f = new DownloadUnpackForm();   
            f.DownloadName = "Contig";                                                      // This will be used in form names and dialog boxes as "the name" of the *thing* we are downloading
            f.DownloadUrl = "http://download.sysinternals.com/Files/Contig.zip";            // This will require ZIP procressing
            f.DownloadUrl = "http://live.sysinternals.com/Contig.exe";                      // (and easy .exe install)
            f.DownloadPage = "http://technet.microsoft.com/en-us/sysinternals/bb897428";    // This will be shown if automatic download fails
            f.DownloadDescriptionBBCode = @"Test[test=this]a test[/test] of BBCode to HTML converter [i](for [b]VHD Director[/b]'s plugin configuration).[/i]
[font=arial,helvetica,sans-serif]Arial, [font=""courier new"",courier,monospace]Courier, [font=lucida sans unicode,lucida grande,sans-serif]Lucidia, [font=tahoma,geneva,sans-serif]Tahoma. [size=6]24pt. [/size][/font][/font][/font][/font]Normal.  [color=#ff0000]Red Text. [/color] Stupid Smiley[list=1]
[*] :)
[*] :cold:
[*] :loleverybody:
[/list]
[u]Indentation[/u]
[indent=1]Once[/indent]
[indent=1]Once[/indent]
[indent=2]Twice[/indent][list]
[*]Thrice
[*]with
[*]points
[/list]
[CODE]
	    static public void Example() {
		    DownloadUnpackForm f = new DownloadUnpackForm();  
		    f.DownloadName = 'Contig';													  // This will be used in form names and dialog boxes as 'the name' of the *thing* we are downloading
		    f.DownloadUrl = 'http://download.sysinternals.com/Files/Contig.zip';		    // This will require ZIP procressing
		    f.DownloadUrl = 'http://live.sysinternals.com/Contig.exe';					  // (and easy .exe install)
		    f.DownloadPage = 'http://technet.microsoft.com/en-us/sysinternals/bb897428';    // This will be shown if automatic download fails
		    f.DownloadDescriptionBBCode = '(this post)';
		    f.Show();
	    }
[/CODE]

[quote]
[b]Who can use it?[/b]
As this parser is written in C#, it can be used in any .NET-Language like C# or VB.NET. The reason why we wrote it is that all existing BBCode-parsers for .NET seem to be very buggy and immature. Now we don't have trouble anymore.
[/quote] [url=""http://bbcode.codeplex.com/""]CodeKicker [/url]

[spoiler]I don't imagine this spoiler will work so well.
[img]http://nt4.com/ss/bbcodetest.png[/img]
[/spoiler]Font's and colors... [font font=courier new color=#ff0000]Red Courier[/font]";

            f.DownloadDescriptionBBCode = @"[i]Contig[/i] is a single-file defragmenter that attempts to make files contiguous on disk. Its perfect for quickly optimizing files that are continuously becoming fragmented, or that you want to ensure are in as few fragments as possible.

[i]Contig[/i] can be used to defrag an existing file, or to create a new file of a specified size and name, optimizing its placement on disk. Contig uses standard Windows defragmentation APIs so it won't cause disk corruption, even if you terminate it while its running.";
            // %VARS% for possible use in "DownloadTo" property
            // http://technet.microsoft.com/en-us/library/cc749104%28WS.10%29.aspx
            // PROGRAMFILES 
            // PROGRAMFILES(X86)
            // SYSTEM 	Refers to %WINDIR%\system32.
            // SYSTEM32
            // SYSTEMROOT
            // WINDIR
            // Variables that are recognized only in the user context
            // APPDATA  The file system directory that serves as a common repository for application-specific data.
            // CSIDL_DESKTOPDIRECTORY   A typical path is C:\Documents and Settings\username\Desktop.
            // CSIDL_LOCAL_APPDATA      A typical path is C:\Documents and Settings\username\Local Settings\Application Data.
            // CSIDL_MYDOCUMENTS 
            // CSIDL_PROGRAMS           A typical path is C:\Documents and Settings\username\Start Menu\Programs.
            // CSIDL_SENDTO             A typical path is C:\Documents and Settings\username\SendTo.
            // CSIDL_STARTUP            A typical path is C:\Documents and Settings\username\Start Menu\Programs\Startup.
            // USERPROFILE              (same as HOMEPATH)
            // HOMEPATH                 (same as CSIDL_PROFILE)
            // CSIDL_PROFILE            A typical path is C:\Documents and Settings\username. Applications should not create files or folders at this level; they should put their data under the locations referred to by CSIDL_APPDATA or CSIDL_LOCAL_APPDATA.
            // TEMP or TMP              a typical path is %USERPROFILE%\AppData\Local\Temp. 
             
            f.DownloadTo = "plugins/contig/contig.exe";

            f.PostDownloadExecute = new[] {
                new ExecuteTask("cmd.exe", "echo", "plugins/contig/contig.exe"),
            };
            f.FinalCheckFileExists = new[] {
                "plugins/contig/contig.exe"
            };

            f.Show();
        }

        public DownloadUnpackForm()
        {
            InitializeComponent();
            /*
            downloadDescription.Rtf = @"{\rtf1\ansi\ansicpg65001\cocoartf1038\cocoasubrtf360
{\fonttbl\f0\fnil\fcharset0 SegoeUI;}
{\colortbl;\red255\green255\blue255;}
\paperw11900\paperh16840\margl1440\margr1440\vieww18820\viewh9040\viewkind0
\deftab720
\pard\pardeftab720\sa320\ql\qnatural

\f0\fs36 \cf0 Microsoft Virtual Server 2005 R2 SP1 - Enterprise Edition
\fs24 \
\pard\pardeftab720\sa300\ql\qnatural
\cf0 Download the service pack 1 for Microsoft Virtual Server 2005 R2 Enterprise Edition. Virtual Server 2005 R2 SP1 is a cost-effective and well supported server virtualization technology for the Windows Server System\'99 platform. As a key part of any server consolidation strategy, Virtual Server increases hardware utilization and enables organizations to rapidly configure and deploy new servers.}";
        
             */
        }

        private void DownloadFileForm_Load(object sender, EventArgs e)
        {

        }

        public string url = "http://download.microsoft.com/download/d/7/2/d7235926-a10d-482c-a2ff-6e0d3130f869/64-BIT/setup.exe";
        private string downloadName;
        private string downloadDescriptionBBCode;
        
        private void button2_Click(object sender, EventArgs e)
        {
#if false
            if (!Is64bitOS)
            {
                url = url.Replace("64-BIT", "32-BIT");
            }
            // FileDownloader fd = new FileDownloader(url, fileCompleteHandler);
#endif
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
            textBox1.AppendText(String.Format("Downlo%ading \"{0}\" from \"{1}\" .......\n\n", fileName, url));
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

        protected void ProcessBBCode(String bbcode) {

            var parser = new BBCodeParser(new[]
                {
                    new BBTag("b", "<b>", "</b>"), 
                    new BBTag("i", "<span style=\"font-style:italic;\">", "</span>"), 
                    new BBTag("u", "<span style=\"text-decoration:underline;\">", "</span>"), 
                    new BBTag("code", "<pre class=\"prettyprint\">", "</pre>"), 
                    new BBTag("img", "<img src=\"${content}\" />", "", false, true), 
                    new BBTag("quote", "<blockquote>", "</blockquote>"), 
                    new BBTag("list", "<ul>", "</ul>"), 
                    new BBTag("*", "<li>", "</li>", true, false),
                    new BBTag("url", "<a href=\"${href}\">", "</a>", new BBAttribute("href", ""), new BBAttribute("href", "href")), 
                    new BBTag("test", "<test tagattribute=\"${tagattr}\">", "</test>", new BBAttribute("tagattr", ""), new BBAttribute("tagattr", "tagattr")), 
                    new BBTag("font", "<span style=\"font-family: ${tagattr}\">", "</span>", true, true, new BBAttribute("tagattr", ""), new BBAttribute("tagattr", "tagattr")), 
                    new BBTag("color", "<span style=\"color: ${tagattr}\">", "</span>", true, true, new BBAttribute("tagattr", ""), new BBAttribute("tagattr", "tagattr")), 
                    new BBTag("size", "<span style=\"size: ${tagattr}em\">", "</span>", true, true, new BBAttribute("tagattr", ""), new BBAttribute("tagattr", "tagattr")), 
                    new BBTag("indent", "<span style=\"padding-left: ${tagattr}em\">", "</span>", true, true, new BBAttribute("tagattr", ""), new BBAttribute("tagattr", "tagattr")), 


                });

            String html = parser.ToHtml(bbcode
                .Replace("[list=1", "[list"))
                .Replace("\n", "<br>");

            webBrowser1.DocumentText = html;

            // new BBTag("font", "FONT:${}", "ENDFONT", true, false),  // Parser Test
            //new BBTag("test", "<span style=\"${test}\">", "</span>", true, true,
            //    new BBAttribute("test", "test", 
            //        // I'm just copying these functions from an example, but they seem to work.
            //        attributeRenderingContext => string.IsNullOrEmpty(attributeRenderingContext.AttributeValue) 
            //            ? "" 
            //            : "test:" 
            //            + attributeRenderingContext.AttributeValue + ";")),


            //new BBTag("font", "<span style=\"${font}\">", "</span>", true, true,
            //    new BBAttribute("font", "font", 
            //        // I'm just copying these functions from an example, but they seem to work.
            //        attributeRenderingContext => string.IsNullOrEmpty(attributeRenderingContext.AttributeValue) 
            //            ? "" 
            //            : "font-family:" 
            //            + attributeRenderingContext.AttributeValue + ";")),

            //new BBTag("color", "<span style=\"${color}\">", "</span>", true, true,
            //    new BBAttribute("color", "color", 
            //        attributeRenderingContext => string.IsNullOrEmpty(attributeRenderingContext.AttributeValue) 
            //            ? "" 
            //            : "color:" 
            //            + attributeRenderingContext.AttributeValue + ";")),

            //new BBTag("size", "<!-- {$size} -->", "<!-- /size -->", true, true,  // TODO - Requires contentTransform (size=6 is 24pt)
            //    new BBAttribute("size", "size", 
            //        attributeRenderingContext => string.IsNullOrEmpty(attributeRenderingContext.AttributeValue) 
            //            ? "" 
            //            : "font-family:" 
            //            + attributeRenderingContext.AttributeValue + ";")),


            //new BBTag("indent", "<div style=\"padding-left: 40px\">", "</div>", true, true,
            //    new BBAttribute("indent", "indent", 
            //        attributeRenderingContext => String.Empty))

        
        }

        public string DownloadName
        {
            get
            {
                return downloadName;
            }
            set
            {
                downloadName = value; this.Name = "Download " + value;
            }
        }

        public string DownloadUrl { get; set; }

        public string DownloadPage { get; set; }

        public string DownloadDescriptionBBCode { get { return downloadDescriptionBBCode; }  set { downloadDescriptionBBCode = value; ProcessBBCode(value); } }

        public string DownloadTo { get; set; }

        public ExecuteTask[] PostDownloadExecute { get; set; }

        public string[] FinalCheckFileExists { get; set; }
    }
}
