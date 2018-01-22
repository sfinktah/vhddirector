using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSharp.cc
{
    public class DebugConsole
    {
        protected static DebugConsoleForm form;
        private static Timer hideTimer;

        protected static void ActivateConsole()
        {
            if (form == null)
            {
                hideTimer = new Timer();
                hideTimer.Interval = 1000;
                hideTimer.Tick += new EventHandler(hideTimer_Tick);
                form = new DebugConsoleForm();
                form.FormClosed += new FormClosedEventHandler(form_FormClosed);
                form.Show();
                // form.SendToBack();
            }
            else
            {
                hideTimer.Start();
            }
        }

        static void form_FormClosed(object sender, FormClosedEventArgs e)
        {
            form = null;
        }

        static void hideTimer_Tick(object sender, EventArgs e)
        {
            hideTimer.Stop();
            form.Hide();
        }

        protected static void AppendText(string text)
        {
            form.AppendText(text);
            System.Console.WriteLine(text);
        }

        public static void WriteLine(string format, params object[] args)
        {
            ActivateConsole();
            if (args.Length < 1)
            {
                WriteLine("{0}", format);
                return;
            }
            AppendText(String.Format(format, args) + "\r\n");
        }

        public static void LogException(System.Exception ex, string niceText)
        {
            ActivateConsole();
            AppendText("Exception: " + ex.Message + " (" + niceText + ")\r\n");
        }
    }
}
