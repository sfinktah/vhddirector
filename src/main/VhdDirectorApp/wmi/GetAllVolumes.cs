using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Management;
using System.IO;

namespace VhdDirectorApp.wmi
{
    /*
     * 
     * The "\\.\" prefix will access the Win32 device namespace instead of the Win32 file namespace. This is how access to physical disks and volumes is accomplished directly, without going through the file system, if the API supports this type of access. You can access many devices other than disks this way (using the CreateFile and DefineDosDevice functions, for example).

Another example of using the Win32 device namespace is using the CreateFile function with "\\.\PhysicalDiskX" (where X is a valid integer value) or "\\.\CdRomX". This allows you to access those devices directly, bypassing the file system. This works because these device names are created by the system as these devices are enumerated, and some drivers will also create other aliases in the system. For example, the device driver that implements the name "C:\" has its own namespace that also happens to be the file system.

APIs that go through the CreateFile function generally work with the "\\.\" prefix because CreateFile is the function used to open both files and devices, depending on the parameters you use.

If you're working with Windows API functions, you should use the "\\.\" prefix to access devices only and not files.

Most APIs won't support "\\.\"; only those that are designed to work with the device namespace will recognize it. Always check the reference topic for each API to be sure.

     */
    public class GetAllVolumes
    {


        public static string MOUNTPOINT_DIRS_KEY = "Confused";
        public static Dictionary<string, NameValueCollection> GetAllVolumeDeviceIDs()
        {
            Dictionary<string, NameValueCollection> ret = new Dictionary<string, NameValueCollection>();

            // retrieve information from Win32_Volume
            try
            {
                using (ManagementClass volClass = new ManagementClass("Win32_Volume"))
                {
                    using (ManagementObjectCollection mocVols = volClass.GetInstances())
                    {
                        // iterate over every volume
                        foreach (ManagementObject moVol in mocVols)
                        {
                            // get the volume's device ID (will be key into our dictionary)                            
                            string devId = moVol.GetPropertyValue("DeviceID").ToString();

                            ret.Add(devId, new NameValueCollection());

                            Console.WriteLine("Vol: {0}", devId);

                            // for each non-null property on the Volume, add it to our NameValueCollection
                            foreach (PropertyData p in moVol.Properties)
                            {
                                if (p.Value == null)
                                    continue;
                                ret[devId].Add(p.Name, p.Value.ToString());
                                Console.WriteLine("\t{0}: {1}", p.Name, p.Value);
                            }

                            // find the mountpoints of this volume
                            using (ManagementObjectCollection mocMPs = moVol.GetRelationships("Win32_MountPoint"))
                            {
                                foreach (ManagementObject moMP in mocMPs)
                                {
                                    // only care about adding directory
                                    // Directory prop will be something like "Win32_Directory.Name=\"C:\\\\\""
                                    string dir = moMP["Directory"].ToString();

                                    // find opening/closing quotes in order to get the substring we want
                                    int first = dir.IndexOf('"') + 1;
                                    int last = dir.LastIndexOf('"');
                                    string dirSubstr = dir.Substring(first, last - first);

                                    // use GetFullPath to normalize/unescape any extra backslashes
                                    string fullpath = Path.GetFullPath(dirSubstr);

                                    ret[devId].Add(MOUNTPOINT_DIRS_KEY, fullpath);

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem retrieving Volume information from WMI. {0} - \n{1}", ex.Message, ex.StackTrace);
                return ret;
            }

            return ret;

        }




    }
}
