using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace VhdDirectorApp
{
    class InterfaceTests
    {
        // http://stackoverflow.com/questions/7060649/creating-a-virtual-disk-mount-point
#if IMDISK
        public bool CreateRAMDisk()
        {
            // Create Empty RAM Disk
            char driveLetter = ImDiskAPI.FindFreeDriveLetter();

            ImDiskAPI.CreateDevice(52428800, 0, 0, 0, 0, ImDiskFlags.DeviceTypeHD | ImDiskFlags.TypeVM, null, false, driveLetter.ToString(), ref deviceID, IntPtr.Zero);

            string mountPoint = driveLetter + @":\Device\ImDisk0";
            ImDiskAPI.CreateMountPoint(mountPoint, deviceID);

            // Format the Drive for NTFS
            if (FormatDrive(driveLetter.ToString(), "NTFS", true, 4096, "", false))
            {
            }
        }
#endif
        public static bool FormatDrive(string driveLetter, string fileSystem, bool quickFormat, int clusterSize, string label, bool enableCompression)
        {
            driveLetter = driveLetter + ":";

            if (driveLetter.Length != 2 || driveLetter[1] != ':' || !char.IsLetter(driveLetter[0]))
            {
                return false;
            }

            //query and format given drive         
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"select * from Win32_Volume WHERE DriveLetter = '" + driveLetter + "'");

            foreach (ManagementObject vi in searcher.Get())
            {
                vi.InvokeMethod("Format", new object[] { fileSystem, quickFormat, clusterSize, label, enableCompression });
            }

            return true;
        }
    }
}
