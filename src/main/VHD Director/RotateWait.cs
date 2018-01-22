using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CSharp.cc.WinApi;
// using NativeWindowApplication;

namespace VHD_Director
{
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public partial class RotateWait : Form
    {
        public MyNativeWindowListener nwl;
        private Timer hideTimer;

        public RotateWait()
        {
            InitializeComponent();
            VisibleChanged += new EventHandler(RotateWait_VisibleChanged);

            hideTimer = new Timer();
            hideTimer.Interval = 3000;
            hideTimer.Tick += new EventHandler(hideTimer_Tick);
           
            nwl = new MyNativeWindowListener(this);

            // pictureBox1.Image = RotateImageByAngle(VHD_Director.Properties.Resources.ring_48, angle);

        }
        private void hideTimer_Tick(object sender, EventArgs e)
        {
            hideTimer.Stop();
            this.Close();
        }

        void RotateWait_VisibleChanged(object sender, EventArgs e)
        {
            if (t != null)
            {
                if (!Visible)
                {
                    t.Stop();
                }
                else
                {
                    t.Start();
                }
            }
        }
        public void ApplicationActivated(bool ApplicationActivated)
        {
            // The application has been activated or deactivated
            System.Diagnostics.Debug.WriteLine("Application Active = " + ApplicationActivated.ToString());
        }
        
        internal void GotText(string p)
        {
            this.WaitText = p;
            // this.waitTextUpdated = false;
            // Rotate();
        }



        /// <summary>
        /// Rotates the image by angle.
        /// </summary>
        /// <param name="oldBitmap">The old bitmap.</param>
        /// <param name="angle">The angle.</param>
        /// <returns></returns>
        static private Bitmap RotateImageByAngle(System.Drawing.Image oldBitmap, float angle)
        {
            var newBitmap = new Bitmap(oldBitmap.Width, oldBitmap.Height);
            using (var graphics = Graphics.FromImage(newBitmap))
            {
                graphics.TranslateTransform((float)oldBitmap.Width / 2, (float)oldBitmap.Height / 2);
                graphics.RotateTransform(angle);
                graphics.TranslateTransform(-(float)oldBitmap.Width / 2, -(float)oldBitmap.Height / 2);
                graphics.DrawImage(oldBitmap, new Point(0, 0));
            }
            return newBitmap;
        }

        private void RotateWait_Load(object sender, EventArgs e)
        {
            t = new Timer();
            t.Interval = 1000 / 15;
            t.Tick += new EventHandler(t_Tick);
            t.Start();


                hideTimer.Start();
            

            Rotate();
        }

       
        void t_Tick(object sender, EventArgs e)
        {
            
            Rotate();
        }

        private void Rotate() {
            if (waitTextUpdated)
            {
                this.label1.Text = waitText;
                waitTextUpdated = false;
                hideTimer.Stop();
                hideTimer.Start();
            }

            angle += 5;
            pictureBox1.Image = RotateImageByAngle(VHD_Director.Properties.Resources.ring_48, angle);
            if (angle >= 360)
            {
                angle = 0;
            }
        }

        private Timer t;
        private float angle;
        private string waitText = "Please wait...";
        private bool hold = false;
        public bool Hold
        {
            get
            {
                return hold;
            }
            set
            {
                if (value == true)
                {
                    hideTimer.Stop();
                }
                else
                {
                    hideTimer.Start();
                }
                hold = value;

            }
        }

        private bool waitTextUpdated;
        public string WaitText
        {
            get
            {
                return waitText;
            }
            set
            {
                waitText = value;
                waitTextUpdated = true;
            }
        }



    }

    static public class RotateWaitStatic
    {
        static private RotateWait form;
        static private Timer hideTimer;

        static public void ActivateConsole(string text)
        {
            if (form == null)
            {
                hideTimer = new Timer();
                hideTimer.Interval = 3000;
                hideTimer.Tick += new EventHandler(hideTimer_Tick);
                form = new RotateWait();
                form.WaitText = text;
                form.Show();
                // form.SendToBack();
            }
            else
            {
                hideTimer.Start();
            }
        }

        static void hideTimer_Tick(object sender, EventArgs e)
        {
            hideTimer.Stop();
            form.Close();
            form = null;
        }

        static private void AppendText(string text)
        {
            ActivateConsole(text);
            System.Console.WriteLine(text);
            Application.DoEvents();
        }

        public static void WriteLine(string format, params object[] args)
        {
            if (args.Length < 1)
            {
                WriteLine("{0}", format);
                return;
            }
            AppendText(String.Format(format, args));
        }

        public static void LogException(System.Exception ex, string niceText)
        {
            AppendText("Exception: " + ex.Message + " (" + niceText + ")");
        }
    }

    static public class RotateWaitThreaded
    {
        static private IntPtr secondThreadFormHandle;

        static public void CloseConsole()
        {
            if (secondThreadFormHandle != IntPtr.Zero)
            {
                CSharp.cc.WinApi.User32.PostMessage(secondThreadFormHandle, User32.WM_CLOSE, 0, 0);
            }
        }

        static private int holdCount;
        static public RotateWaitHolder Hold(string format, params object[] args)
        {
            string text = String.Format(format, args);
            ActivateConsole(text);
            return new RotateWaitHolder(text);
        }

        static public void HoldConsole()
        {
            if (holdCount == 0)
            {
                CSharp.cc.WinApi.User32.PostMessage(secondThreadFormHandle, User32.WM_NCRBUTTONDOWN, 0, 0);
            }                
            holdCount++;
        }
        static public void ReleaseConsole()
        {
            holdCount--;
            if (holdCount == 0)
            {
                CSharp.cc.WinApi.User32.PostMessage(secondThreadFormHandle, User32.WM_NCRBUTTONUP, 0, 0);
            }

            if (holdCount < 0)
            {
                holdCount = 0;
            }
        }


        static public void SendText(string text)
        {
            if (secondThreadFormHandle != IntPtr.Zero)
            {
                CSharp.cc.WinApi.User32.SetWindowText(secondThreadFormHandle, text);
            }
        }


        static void SecondFormHandleCreated(object sender, EventArgs e)
        {
            Control second = sender as Control;
            secondThreadFormHandle = second.Handle;
            second.HandleCreated -= SecondFormHandleCreated;
        }

        static void SecondFormHandleDestroyed(object sender, EventArgs e)
        {
            Control second = sender as Control;
            secondThreadFormHandle = IntPtr.Zero;
            second.HandleDestroyed -= SecondFormHandleDestroyed;
        }

        static public void ActivateConsole(string text)
        {
            if (secondThreadFormHandle == IntPtr.Zero)
            {
                RotateWait form = new RotateWait();

                form.HandleCreated += SecondFormHandleCreated;
                form.HandleDestroyed += SecondFormHandleDestroyed;
                form.RunInNewThread(true);
                System.Threading.Thread.Sleep(500);
            }

            SendText(text);
        }

        static private void AppendText(string text)
        {
            ActivateConsole(text);
            System.Console.WriteLine(text);
            Application.DoEvents();
        }

        public static void WriteLine(string format, params object[] args)
        {
            if (args.Length < 1)
            {
                WriteLine("{0}", format);
                return;
            }
            AppendText(String.Format(format, args));
        }

        public static void LogException(System.Exception ex, string niceText)
        {
            AppendText("Exception: " + ex.Message + " (" + niceText + ")");
        }
    }

    public class RotateWaitHolder : IDisposable
    {
        System.Diagnostics.Stopwatch sw;

        // Track whether Dispose has been called.
        private bool disposed = false;
        private object name;

        public RotateWaitHolder(string name = "")
        {
            this.name = name;
            RotateWaitThreaded.HoldConsole();
            sw = new System.Diagnostics.Stopwatch();
            sw.Start();
        }

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    // component.Dispose();
                    sw.Stop();
                    TimeSpan layoutTime = sw.Elapsed;
                    System.Console.WriteLine("Held for {0} ms ({1})", layoutTime.Milliseconds, name);
                }
                else
                {

                    sw.Stop();
                    TimeSpan layoutTime = sw.Elapsed;
                    System.Console.WriteLine("(Finalise) {1} took: {0} ms", layoutTime.Milliseconds, name);
                }
                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.
                // CloseHandle(handle);
                // handle = IntPtr.Zero;

                RotateWaitThreaded.ReleaseConsole();


                // Note disposing has been done.
                disposed = true;

            }
        }

        // Use interop to call the method necessary
        // to clean up the unmanaged resource.
        // [System.Runtime.InteropServices.DllImport("Kernel32")]
        // private extern static Boolean CloseHandle(IntPtr handle);

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        ~RotateWaitHolder()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }


    }
    internal static class FormExtensions
    {
        private static void ApplicationRunProc(object state)
        {
            Application.Run(state as Form);
        }

        public static void RunInNewThread(this Form form, bool isBackground)
        {
            if (form == null)
                throw new ArgumentNullException("form");
            if (form.IsHandleCreated)
                throw new InvalidOperationException("Form is already running.");
            System.Threading.Thread thread = new System.Threading.Thread(ApplicationRunProc);
            thread.SetApartmentState(System.Threading.ApartmentState.STA);
            thread.IsBackground = isBackground;
            thread.Start(form);
        }
    }

    // NativeWindow class to listen to operating system messages.
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class MyNativeWindowListener : NativeWindow
    {

        // Constant value was found in the "windows.h" header file.
        private const int WM_ACTIVATEAPP = 0x001C;

        private RotateWait parent;

        public MyNativeWindowListener(RotateWait parent)
        {

            parent.HandleCreated += new EventHandler(this.OnHandleCreated);
            parent.HandleDestroyed += new EventHandler(this.OnHandleDestroyed);
            this.parent = parent;
        }

        // Listen for the control's window creation and then hook into it.
        public void OnHandleCreated(object sender, EventArgs e)
        {
            // Window is now created, assign handle to NativeWindow.
            AssignHandle(((RotateWait)sender).Handle);
        }
        public void OnHandleDestroyed(object sender, EventArgs e)
        {
            // Window was destroyed, release hook.
            ReleaseHandle();
        }
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            // Listen for operating system messages

            switch (m.Msg)
            {

                case CSharp.cc.WinApi.User32.WM_SETTEXT:
                    System.Console.WriteLine("WM_SETTEXT recv in MyNativeWindowListener: {0}\r\n{1}", m.ToString(),
                        System.Runtime.InteropServices.Marshal.PtrToStringUni(m.LParam)
                        );
                    parent.GotText(System.Runtime.InteropServices.Marshal.PtrToStringUni(m.LParam));
                    break;
                case CSharp.cc.WinApi.User32.WM_NCRBUTTONDOWN:
                    System.Console.WriteLine("Message recv'd in MyNativeWindowListener: {0}", m.ToString());
                    parent.Hold = true;
                    break;
                case CSharp.cc.WinApi.User32.WM_NCRBUTTONUP:
                    System.Console.WriteLine("Message recv'd in MyNativeWindowListener: {0}", m.ToString());
                    parent.Hold = false;
                    break;
                    
                case WM_ACTIVATEAPP:
                    System.Console.WriteLine("WM_ATIVEAPP recv in MyNativeWindowListener");

                    // Notify the form that this message was received.
                    // Application is activated or deactivated, 
                    // based upon the WParam parameter.
                    parent.ApplicationActivated(((int)m.WParam != 0));

                    break;
            }
            base.WndProc(ref m);
        }
    }



}
