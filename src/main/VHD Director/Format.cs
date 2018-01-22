using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHD_Director
{
    class FormatMethods
    {
        // The Win32_Volume class is not available under Windows XP and earlier
        public static bool FormatDrive(string driveLetter, string fileSystem = "NTFS", bool quickFormat = true, int clusterSize = 8192, string label = "", bool enableCompression = false)
        {
            if (driveLetter.Length != 2 || driveLetter[1] != ':' || !char.IsLetter(driveLetter[0]))
                return false;

            //query and format given drive         
            System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher
             (@"select * from Win32_Volume WHERE DriveLetter = '" + driveLetter + "'");
            foreach (System.Management.ManagementObject vi in searcher.Get())
            {
                vi.InvokeMethod("Format", new object[] { fileSystem, quickFormat, clusterSize, label, enableCompression });
            }

            return true;
        }

        // SHFormatDrive - SHFormatDrive function - Opens the Shell's Format dialog.
        //    http://msdn.microsoft.com/en-us/library/windows/desktop/bb762169%28v=vs.85%29.aspx
        //    http://pinvoke.net/default.aspx/shell32.SHFormatDrive
        // Note  This function is available through Windows XP Service Pack 2 (SP2) and Windows Server 2003. It might be altered or unavailable in subsequent versions of Windows.

        // The format is controlled by the dialog interface. That is, the user must click the OK button to actually begin the format—the format cannot be started programmatically.

        //DWORD SHFormatDrive(
        //  __in  HWND hwnd,
        //  UINT drive,
        //  UINT fmtID,
        //  UINT options
        //);




        // CFormatDriveDialog - A wrapper class for the undocumented SHFormatDrive API function
        //    http://www.codeproject.com/KB/dialog/cformatdrivedialog.aspx
    }
}
