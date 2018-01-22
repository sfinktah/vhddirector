using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.cc
{
    public class Debug {
    // [System.Diagnostics.Conditional("DEBUG")]
        public static void DebugWindowSize(System.Drawing.Size size)
        {
#if DEBUG_TRANSPARENT_PANEL
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
    }
}
