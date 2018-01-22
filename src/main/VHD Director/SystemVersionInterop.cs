using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;     // DLL support



namespace VHD_Director
{
    public class SystemVersionInterop
    {
        [DllImport("SystemVersionDll.dll")]
        public static extern void DisplayHelloFromDLL();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);


        [DllImport("SystemVersionDll.dll", CharSet = CharSet.Auto)]
        static extern uint GetStringFromDLL(IntPtr hwnd, StringBuilder lpszFileName, uint cchFileNameMax);

        public static string GetText()
        {
            StringBuilder fileName = new StringBuilder(16384);
            GetStringFromDLL(IntPtr.Zero, fileName, 16384);
            Debug.WriteLine(fileName.ToString());
            return fileName.ToString();
        }
    }
}
