using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csharp_debug
{
    public class Debug {
    // [System.Diagnostics.Conditional("DEBUG")]
        public static void DebugWindowSize(System.Drawing.Size size)
        {
#if DEBUG_TRANSPARENT_CONTROL
            // create the stack frame for the function that called this function
            System.Diagnostics.StackFrame sf = new System.Diagnostics.StackFrame(1, false);

            // save the method name
            string methodName = sf.GetMethod().DeclaringType.Name;
            string methodClass = sf.GetMethod().Name;

            // save the file name
            string fileName = sf.GetFileName();

            // save the line number
            int lineNumber = sf.GetFileLineNumber();

            System.Console.WriteLine("{2}::{3}: {0}x{1}", size.Width, size.Height, methodName, methodClass);
#endif
        }

        public static void DebugWinControl(System.Windows.Forms.Control c)
        {
#if DEBUG_TRANSPARENT_CONTROL
            // create the stack frame for the function that called this function
            System.Diagnostics.StackFrame sf = new System.Diagnostics.StackFrame(1, false);

            // save the method name
            string methodName = sf.GetMethod().DeclaringType.Name;
            string methodClass = sf.GetMethod().Name;
       
            string methodLocation = sf.GetFileLineNumber().ToString();

            // save the file name
            string fileName = sf.GetFileName();

            // save the line number
            int lineNumber = sf.GetFileLineNumber();

            System.Console.WriteLine("{4,23}::{3,-23} {0,3} x{1,3} {2,30}:{5,4}", c.Size.Width, c.Size.Height, methodName, methodClass, c.Name, methodLocation);
#endif
        }

        
    }

    public class SectionStopWatch : IDisposable
    {
        System.Diagnostics.Stopwatch sw;

        // Track whether Dispose has been called.
        private bool disposed = false;
        private object name;

        public SectionStopWatch(string name)
        {
            this.name = name;
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
                    System.Console.WriteLine("{1} took: {0} ms", layoutTime.Milliseconds, name);
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
        ~SectionStopWatch()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }


    }
}
