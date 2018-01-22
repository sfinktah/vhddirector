using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;

namespace VHD_Director
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                Console.WriteLine("AssemblyResolve: {0}", args.Name);
                String resourceName = "VHD_Director.dll." + new AssemblyName(args.Name).Name + ".dll";
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            };


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new DiskUsageForm());
            }
            catch (System.MissingMethodException mex)
            {
                new Error(mex);
                // MessageBox.Show("Missing method: " + mex.Message);
            }
            catch (Exception ex)
            {
                new Error(ex);
                // CSharp.cc.ReportException.WebReportBackground(ex);
            }
        }


        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;

            DialogResult result = DialogResult.Abort;
            if (!Error.showing)
            {
                Error err = new Error("Unhandled Exception", ex);
            }
            else
            {
                result = MessageBox.Show("Please don't contact the developers "
                        + "with the following information:\n\n" + ex.Message
                        + ex.StackTrace, "Unhandled Exception",
                        MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
                if (result == DialogResult.Abort)
                {
                    Application.Exit();
                }
            }
        }


        public static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs ex)
        {
            DialogResult result = DialogResult.Abort;
            if (!Error.showing)
            {
                Error err = new Error("Application ThreadException", ex.Exception);
            }
            else
            {

                result = MessageBox.Show("Please don't contact the developers "
                        + "with the following information:\n\n" + ex.Exception.Message
                        + ex.Exception.StackTrace, "Thread Exception",
                        MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
                if (result == DialogResult.Abort)
                {
                    Application.Exit();
                }
            }
        }

    }
}

