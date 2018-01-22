using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VhdDirectorApp
{
    public class LogicalDiskModel
    {
        public string DeviceID { get; set; }

        public string Name { get; set; }

        public ulong FreeSpace { get; set; }

        public string FileSystem { get; set; }

        public string VolumeName { get; set; }

        public ushort Access { get; set; }

        public override string ToString()
        {
            return FileSystem + " (" + Name + ")" + " '" + VolumeName + "'";
        }
    }
}
