using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace VHD_Director
{
    public class DisksAndPartitions
    {
        static public List<DiskPartitionModel> GetPartitions(String DeviceId)
        {
            List<DiskPartitionModel> partitions = new List<DiskPartitionModel>();

            DeviceId = DeviceId.ToLower();

            // based on code from willy.denoyette@pandora.be


            Console.WriteLine("GetDiskDrivesAndRelatedPartitions\r\n");
            Console.WriteLine("---------------------------------\r\n");

            using (var devs = new ManagementClass(@"Win32_Diskdrive"))
            {
                ManagementObjectCollection moc = devs.GetInstances();

                // The Win32_DiskDrive WMI class represents a physical disk drive as seen by a computer running the
                // Windows operating system. Any interface to a Windows physical disk drive is a descendent 
                // (or member) of this class. 

                foreach (ManagementObject mo in moc)
                {
                    Console.WriteLine("DeviceId: {0}\r\n", mo["DeviceId"] + "\r\n");
                    // DeviceId: \\.\PHYSICALDRIVE1
                    if (mo["DeviceId"].ToString().ToLower().Equals(DeviceId))
                    {
                        // The Win32_DiskPartition WMI class represents the capabilities and management capacity of a 
                        // partitioned area of a physical disk on a computer system running Windows. 
                        // Example: Disk #0, Partition #1.

                        foreach (ManagementObject b in mo.GetRelated("Win32_DiskPartition"))
                        {
                            //    DiskPartition: Disk #1, Partition #0
                            //        LogicalDisk: E:
                            //    DiskPartition: Disk #1, Partition #1

                            // Access    Data type: uint16
                                            //0 Unknown
                                            //1 Readable
                                            //2 Writable
                                            //3 Read/write Supported
                                            //4 Write Once
                            // Bootable    Data type: boolean
                            // BootPartition    Data type: boolean
                            // DeviceID     Data type: string
                            // DiskIndex    Data type: uint32 (eg: 0)
                            // Index    Data type: uint32 (eg: 1) Index number of the partition.
                            // PrimaryPartition    Data type: boolean   If True, this is the primary partition.
                            // RewritePartition    Data type: boolean   Access type: Read-only
                            //      If True, the partition information has changed. When you change a partition (with IOCTL_DISK_SET_DRIVE_LAYOUT), 
                            //      the system uses this property to determine which partitions have changed and need their information rewritten. 
                            //      If TRUE, the partition must be rewritten.
                            // Size    Data type: uint64
                            // StartingOffset    Data type: uint64  Qualifiers: Units (Bytes)   Starting offset (in bytes) of the partition.
                            // Type     Data type: string   
    
                            /*  "Unused"
                                "12-bit FAT"
                                "Xenix Type 1"
                                "Xenix Type 2"
                                "16-bit FAT"
                                "Extended Partition"
                                "MS-DOS V4 Huge"
                                "Installable File System"
                                "PowerPC Reference Platform"
                                "UNIX"
                                "NTFS"
                                "Win95 w/Extended Int 13"
                                "Extended w/Extended Int 13"
                                "Logical Disk Manager"
                                "Unknown" 
                             */
                            DiskPartitionModel partition = new DiskPartitionModel();
                            partition.DiskIndex = (UInt32)b["DiskIndex"];
                            partition.PartitionIndex = (UInt32)b["Index"];
                            partition.PrimaryPartition = (bool)b["PrimaryPartition"];
                            partition.BootPartition = (bool)b["BootPartition"];
                            partition.Bootable = (bool)b["Bootable"];
                            partition.DeviceID = (string)b["DeviceID"];
                            // partition.Access = (UInt16)b["Access"];
                            partition.Type = (string)b["Type"];
                            partition.Size = (UInt64)b["Size"];


                            Console.WriteLine("\tDiskPartition: {0}\r\n", b["Name"]);

                            // The Win32_LogicalDisk WMI class represents a data source that resolves to an actual 
                            // local storage device on a computer system running Windows.

                            // http://msdn.microsoft.com/en-us/library/aa394173(v=VS.85).aspx#methods
                            // Chkdsk()
                            // FileSystem    Data type: string  eg "NTFS"
                            // FreeSpace    Data type: uint64 (bytes)
                            // Size
                            // VolumeName   Data type: string    Access type: Read/write    Volume name of the logical disk. 
                            // DeviceID     C:   D:   E:  etc

                            foreach (ManagementBaseObject c in b.GetRelated("Win32_LogicalDisk"))
                            {
                                partition.LogicalDiskModel = new LogicalDiskModel();
                                partition.LogicalDiskModel.DeviceID = (string)c["DeviceID"];
                                partition.LogicalDiskModel.Name = (string)c["Name"];
                                // partition.LogicalDiskModel.FreeSpace = (UInt64)c["FreeSpace"];
                                partition.LogicalDiskModel.FileSystem = (string)c["FileSystem"];
                                partition.LogicalDiskModel.VolumeName = (string)c["VolumeName"];
                                partition.LogicalDiskModel.Access = (UInt16)c["Access"];



                                Console.WriteLine("\t\tLogicalDisk: {0}\r\n", c["Name"]);
                            }

                            partitions.Add(partition);
                        }
                    }
                }
            }

            // Returned:

            //GetDiskDrivesAndRelatedPartitions
            //---------------------------------
            //DeviceId: \\.\PHYSICALDRIVE1

            //    DiskPartition: Disk #1, Partition #0
            //        LogicalDisk: E:
            //    DiskPartition: Disk #1, Partition #1

            return partitions;
        } 
    }
}
