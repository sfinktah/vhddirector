using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHD_Director
{
    public class DiskPartitionModel
    {
        public uint DiskIndex { get; set; }

        public uint PartitionIndex { get; set; }

        public LogicalDiskModel LogicalDiskModel { get; set; }

        public bool PrimaryPartition { get; set; }

        public bool Bootable { get; set; }

        public string DeviceID { get; set; }

        public string Type { get; set; }

        public ushort Access { get; set; }

        public override string ToString()
        {
            String r = String.Format("({0},{1})", DiskIndex, PartitionIndex);
            if (LogicalDiskModel != null)
            {
                r += " " + LogicalDiskModel.ToString();
            }
            else
            {
                r += " " + Type;
            }

            return r;
        }

        public bool BootPartition { get; set; }

        public ulong Size { get; set; }
    }
}
