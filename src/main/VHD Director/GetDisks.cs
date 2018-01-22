using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.IO;


// The non-WMI method: http://msdn.microsoft.com/en-us/library/aa365730%28v=VS.85%29.aspx (Volume Management Functions)

namespace VHD_Director
{
    public partial class GetDisks : Form
    {
        public GetDisks()
        {
            InitializeComponent();
            GetLogicalDisks();
        }


        public void GetDiskDrivePhysicalMedia()
        {
            textBox1.AppendText("GetDiskDrivePhysicalMedia\r\n");
            textBox1.AppendText("-------------------------\r\n");
            /* There are five ways I can find to retrieve the serial number (diskid32 uses all of these) :

ReadPhysicalDriveInNTWithAdminRights()
ReadIdeDriveAsScsiDriveInNT()
ReadPhysicalDriveInNTWithZeroRights()
ReadPhysicalDriveInNTUsingSmart
using WMI

Code for the first four is here : http://www.codeproject.com/KB/mcpp/DriveInfoEx.aspx
And for WMI is here : http://www.codeproject.com/KB/cs/hard_disk_serialno.aspx
*/


            // Win32_DiskDrivePhysicalMedia

            // Other query options may be 1) Win32_LogicalDisk 2) Win32_DiskDrive
            ManagementClass mc = new ManagementClass("Win32_DiskDrivePhysicalMedia");

            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                foreach (PropertyData prop in mo.Properties)
                {
                    if (prop.Value == null)
                        continue;
                    if (prop.Value.ToString().Length > 0)
                        textBox1.AppendText(String.Format("{0}: {1}\r\n", prop.Name, prop.Value));
                }
                textBox1.AppendText("\r\n");
            }


            /* 
                Antecedent: \\WIN-2008-DEV\root\cimv2:Win32_PhysicalMedia.Tag="\\\\.\\PHYSICALDRIVE1"
                Dependent: \\WIN-2008-DEV\root\cimv2:Win32_DiskDrive.DeviceID="\\\\.\\PHYSICALDRIVE1"

             */

        }


        public void GetVirtualDiskDrives()
        {
            textBox1.AppendText("GetVirtualDiskDrives\r\n");
            textBox1.AppendText("--------------------\r\n");
            ManagementClass mc = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc = mc.GetInstances();


            foreach (ManagementObject mo in moc)
            {
                if (mo.Properties["Model"].Value == "Msft Virtual Disk SCSI Disk Device")
                    textBox1.AppendText(String.Format("{0}: {1}\r\n", "Name", mo.Properties["Name"].Value.ToString()));
                textBox1.AppendText("\r\n");
            }

            // Now we just need the reverse of GetVirtualDiskPhysicalPath 
        }



        public void GetDiskDrives()
        {
            textBox1.AppendText("GetDiskDrives\r\n");
            textBox1.AppendText("-------------\r\n");
            //ManagementScope ms = new ManagementScope();
            //ObjectQuery oq = new ObjectQuery("SELECT DeviceID, VolumeName FROM Win32_LogicalDisk"); //  WHERE DriveType = 3 OR DriveType = 2");
            //ManagementObjectSearcher mos = new ManagementObjectSearcher(ms, oq);
            //ManagementObjectCollection moc = mos.Get();


            ManagementClass mc = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc = mc.GetInstances();

            // The Win32_DiskDrive WMI class represents a physical disk drive as seen by a computer running the Windows operating system. 
            // Any interface to a Windows physical disk drive is a descendent (or member) of this class. 
            //
            // DeviceID: Unique identifier of the disk drive with other devices on the system. This property is inherited from CIM_LogicalDevice.
            // Name: (String) - Subclassed to be a key. Inherits from CIM_ManagedSystemElement.
            // Partitions: # parts
            // PNPDeviceID: Windows Plug and Play device identifier of the logical device. This property is inherited from CIM_LogicalDevice.
            // Signature
            // SystemName / SystemCreationClassName

/*            
GetDiskDrives
-------------
BytesPerSector: 512
Capabilities: System.UInt16[]
CapabilityDescriptions: System.String[]
    Caption: Msft Virtual Disk SCSI Disk Device
ConfigManagerErrorCode: 0
ConfigManagerUserConfig: False
CreationClassName: Win32_DiskDrive
Description: Disk drive
    DeviceID: \\.\PHYSICALDRIVE1
FirmwareRevision: 1.0 
Index: 1
InterfaceType: SCSI
Manufacturer: (Standard disk drives)
MediaLoaded: True
MediaType: Fixed hard disk media
    Model: Msft Virtual Disk SCSI Disk Device
Name: \\.\PHYSICALDRIVE1
    Partitions: 2
    PNPDeviceID: SCSI\DISK&VEN_MSFT&PROD_VIRTUAL_DISK\2&1F4ADFFE&0&000001
    SCSIBus: 0
    SCSILogicalUnit: 1
    SCSIPort: 5
    SCSITargetId: 0
SectorsPerTrack: 63
Signature: 4289489314
Size: 1069286400
Status: OK
SystemCreationClassName: Win32_ComputerSystem
SystemName: WIN-2008-DEV
TotalCylinders: 130
TotalHeads: 255
TotalSectors: 2088450
TotalTracks: 33150
TracksPerCylinder: 255
 */

            foreach (ManagementObject mo in moc)
            {
                foreach (PropertyData prop in mo.Properties)
                {
                    if (prop.Value == null)
                        continue;
                    if (prop.Value.ToString().Length > 0)
                    textBox1.AppendText(String.Format("{0}: {1}\r\n", prop.Name, prop.Value));
                }
                textBox1.AppendText("\r\n");
            }



            // 
            //ManagementClass mc = new ManagementClass("Win32_LogicalDisk");
            //ManagementObjectCollection moc = mc.getInstances();
            //foreach (ManagementObject mo in moc)
            // {
            // Your code here
            // }

        }
        public void GetLogicalDisks()
        {
            //ManagementScope ms = new ManagementScope();
            //ObjectQuery oq = new ObjectQuery("SELECT DeviceID, VolumeName FROM Win32_LogicalDisk"); //  WHERE DriveType = 3 OR DriveType = 2");
            //ManagementObjectSearcher mos = new ManagementObjectSearcher(ms, oq);
            //ManagementObjectCollection moc = mos.Get();
            textBox1.AppendText("GetLogicalDisks\r\n");
            textBox1.AppendText("---------------\r\n");

            ManagementClass mc = new ManagementClass("Win32_LogicalDisk");
            ManagementObjectCollection moc = mc.GetInstances();


            //0	Unknown
            //1	No Root Directory
            //2	Removeable Disk
            //3	Local Disk
            //4	Network Drive
            //5	Compact Disc
            //6	RAM Disk

            foreach (ManagementObject mo in moc)
            {
                foreach (PropertyData prop in mo.Properties)
                {
                    if (prop.Value == null)
                        continue;
                    if (prop.Value.ToString().Length > 0)
                    textBox1.AppendText(String.Format("{0}: {1}\r\n", prop.Name, prop.Value));
                }
                textBox1.AppendText("\r\n");
            }



            // 
            //ManagementClass mc = new ManagementClass("Win32_LogicalDisk");
            //ManagementObjectCollection moc = mc.getInstances();
            //foreach (ManagementObject mo in moc)
            // {
            // Your code here
            // }

        }
        public void GetDiskDrivesAndRelatedPartitions()
        {

            // willy.denoyette@pandora.be

            
            textBox1.AppendText("GetDiskDrivesAndRelatedPartitions\r\n");
            textBox1.AppendText("---------------------------------\r\n");

            using (var devs = new ManagementClass(@"Win32_Diskdrive"))
            {
                ManagementObjectCollection moc = devs.GetInstances();

                // The Win32_DiskDrive WMI class represents a physical disk drive as seen by a computer running the
                // Windows operating system. Any interface to a Windows physical disk drive is a descendent 
                // (or member) of this class. 

                foreach (ManagementObject mo in moc)
                {
                    textBox1.AppendText(String.Format("DeviceId: {0}\r\n", mo["DeviceId"] + "\r\n"));

                    // The Win32_DiskPartition WMI class represents the capabilities and management capacity of a 
                    // partitioned area of a physical disk on a computer system running Windows. 
                    // Example: Disk #0, Partition #1.

                    foreach (ManagementObject b in mo.GetRelated("Win32_DiskPartition"))
                    {
                        textBox1.AppendText(String.Format("\tDiskPartition: {0}\r\n", b["Name"]));

                        // The Win32_LogicalDisk WMI class represents a data source that resolves to an actual 
                        // local storage device on a computer system running Windows.

                        foreach (ManagementBaseObject c in b.GetRelated("Win32_LogicalDisk"))
                        {
                            textBox1.AppendText(String.Format("\t\tLogicalDisk: {0}\r\n", c["Name"]));
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
        } 
        public void GetDiskDrivePhysicalMediaAndRelatedPartitions()
        {

            // Win32_GetDiskDrivePhysicalMedia
            //   Antecedent: \\WIN-2008-DEV\root\cimv2:Win32_PhysicalMedia.Tag="\\\\.\\PHYSICALDRIVE1"
            //    Dependent: \\WIN-2008-DEV\root\cimv2:Win32_DiskDrive.DeviceID="\\\\.\\PHYSICALDRIVE1"

            // The Win32_PhysicalMedia class represents any type of documentation or storage medium, such as tapes, CD ROMs, and so on. 
            // http://msdn.microsoft.com/en-us/library/aa394346%28v=VS.85%29.aspx

            // The Win32_DiskDrive WMI class represents a physical disk drive as seen by a computer running the Windows operating system. 
            // Any interface to a Windows physical disk drive is a descendent (or member) of this class. 
            //
            // DeviceID: Unique identifier of the disk drive with other devices on the system. This property is inherited from CIM_LogicalDevice.
            // Name: (String) - Subclassed to be a key. Inherits from CIM_ManagedSystemElement.
            // Partitions: # parts
            // PNPDeviceID: Windows Plug and Play device identifier of the logical device. This property is inherited from CIM_LogicalDevice.
            // Signature
            // SystemName / SystemCreationClassName


            // The Win32_DiskDrivePhysicalMedia association WMI class defines the mapping between a disk drive and the physical components that implement it.
            // The Win32_DiskDrivePhysicalMedia class has the following properties.
            //Antecedent
            //    Data type: Win32_PhysicalMedia
            //    Access type: Read-only
            //    Physical component that implements the device. This property is inherited from CIM_Realizes.
            //Dependent
            //    Data type: Win32_DiskDrive
            //    Access type: Read-only
            //    Logical device. This property is inherited from CIM_Realizes.
            //Remarks
            //The Win32_DiskDrivePhysicalMedia class is derived from CIM_Realizes.

            textBox1.AppendText("GetDiskDrivePhysicalMediaAndRelatedPartitions\r\n");
            textBox1.AppendText("---------------------------------------------\r\n");
            return;
            using (var devs = new ManagementClass("Win32_GetDiskDrivePhysicalMedia"))
            {
                ManagementObjectCollection moc = devs.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    textBox1.AppendText(String.Format("DeviceId: {0}\r\n", mo["DeviceId"] + "\r\n"));

                    // The Win32_DiskPartition WMI class represents the capabilities and management capacity of a 
                    // partitioned area of a physical disk on a computer system running Windows. 
                    // Example: Disk #0, Partition #1.

                    foreach (ManagementObject b in mo.GetRelated("Win32_DiskPartition"))
                    {
                        textBox1.AppendText(String.Format("\tDiskPartition: {0}\r\n", b["Name"]));

                        // The Win32_LogicalDisk WMI class represents a data source that resolves to an actual 
                        // local storage device on a computer system running Windows.
                        
                        foreach (ManagementBaseObject c in b.GetRelated("Win32_LogicalDisk"))
                        {
                            textBox1.AppendText(String.Format("\t\tLogicalDisk: {0}\r\n", c["Name"]));
                        }
                    }
                }
            }
        }
    }
}

/*
 * 
GetLogicalDisks
---------------
Caption: A:
CreationClassName: Win32_LogicalDisk
Description: 3 1/2 Inch Floppy Drive
DeviceID: A:
DriveType: 2
MediaType: 5
Name: A:
SystemCreationClassName: Win32_ComputerSystem
SystemName: WIN-2008-DEV

Access: 0
Caption: C:
Compressed: False
CreationClassName: Win32_LogicalDisk
Description: Local Fixed Disk
DeviceID: C:
DriveType: 3
FileSystem: NTFS
FreeSpace: 15540703232
MaximumComponentLength: 255
MediaType: 12
Name: C:
QuotasDisabled: True
QuotasIncomplete: False
QuotasRebuilding: False
Size: 64419340288
SupportsDiskQuotas: True
SupportsFileBasedCompression: True
SystemCreationClassName: Win32_ComputerSystem
SystemName: WIN-2008-DEV
VolumeDirty: False
VolumeSerialNumber: 8C0F8147

Caption: D:
CreationClassName: Win32_LogicalDisk
Description: CD-ROM Disc
DeviceID: D:
DriveType: 5
MediaType: 11
Name: D:
SystemCreationClassName: Win32_ComputerSystem
SystemName: WIN-2008-DEV

Access: 0
Caption: E:
Compressed: False
CreationClassName: Win32_LogicalDisk
Description: Local Fixed Disk
DeviceID: E:
DriveType: 3
FileSystem: NTFS
FreeSpace: 1044287488
MaximumComponentLength: 255
MediaType: 12
Name: E:
QuotasDisabled: True
QuotasIncomplete: False
QuotasRebuilding: False
Size: 1073446912
SupportsDiskQuotas: True
SupportsFileBasedCompression: True
SystemCreationClassName: Win32_ComputerSystem
SystemName: WIN-2008-DEV
VolumeDirty: False
VolumeName: cloned
VolumeSerialNumber: 49EB5AA3

Access: 0
Caption: I:
Compressed: False
CreationClassName: Win32_LogicalDisk
Description: Network Connection
DeviceID: I:
DriveType: 4
FileSystem: NTFS
FreeSpace: 7290720780288
MaximumComponentLength: 255
MediaType: 0
Name: I:
ProviderName: \\nas\mini
Size: 11828334428160
SupportsDiskQuotas: False
SupportsFileBasedCompression: False
SystemCreationClassName: Win32_ComputerSystem
SystemName: WIN-2008-DEV
VolumeName: mini
VolumeSerialNumber: 2E8B152D

Access: 0
Caption: Y:
Compressed: False
CreationClassName: Win32_LogicalDisk
Description: Network Connection
DeviceID: Y:
DriveType: 4
FileSystem: NTFS
FreeSpace: 7290720780288
MaximumComponentLength: 255
MediaType: 0
Name: Y:
ProviderName: \\nas\mini
Size: 11828334428160
SupportsDiskQuotas: False
SupportsFileBasedCompression: False
SystemCreationClassName: Win32_ComputerSystem
SystemName: WIN-2008-DEV
VolumeName: mini
VolumeSerialNumber: 2E8B152D

Access: 0
Caption: Z:
Compressed: False
CreationClassName: Win32_LogicalDisk
Description: Network Connection
DeviceID: Z:
DriveType: 4
FileSystem: NTFS
FreeSpace: 69944565760
MaximumComponentLength: 255
MediaType: 0
Name: Z:
ProviderName: \\pt3\raid1
Size: 1198900248576
SupportsDiskQuotas: False
SupportsFileBasedCompression: False
SystemCreationClassName: Win32_ComputerSystem
SystemName: WIN-2008-DEV
VolumeName: RAID1
VolumeSerialNumber: 005400C4

GetDiskDrivePhysicalMedia
-------------------------
Antecedent: \\WIN-2008-DEV\root\cimv2:Win32_PhysicalMedia.Tag="\\\\.\\PHYSICALDRIVE1"
Dependent: \\WIN-2008-DEV\root\cimv2:Win32_DiskDrive.DeviceID="\\\\.\\PHYSICALDRIVE1"

Antecedent: \\WIN-2008-DEV\root\cimv2:Win32_PhysicalMedia.Tag="\\\\.\\PHYSICALDRIVE0"
Dependent: \\WIN-2008-DEV\root\cimv2:Win32_DiskDrive.DeviceID="\\\\.\\PHYSICALDRIVE0"

GetDiskDrives
-------------
BytesPerSector: 512
Capabilities: System.UInt16[]
CapabilityDescriptions: System.String[]
Caption: Msft Virtual Disk SCSI Disk Device
ConfigManagerErrorCode: 0
ConfigManagerUserConfig: False
CreationClassName: Win32_DiskDrive
Description: Disk drive
DeviceID: \\.\PHYSICALDRIVE1
FirmwareRevision: 1.0 
Index: 1
InterfaceType: SCSI
Manufacturer: (Standard disk drives)
MediaLoaded: True
MediaType: Fixed hard disk media
Model: Msft Virtual Disk SCSI Disk Device
Name: \\.\PHYSICALDRIVE1
Partitions: 1
PNPDeviceID: SCSI\DISK&VEN_MSFT&PROD_VIRTUAL_DISK\2&1F4ADFFE&0&000001
SCSIBus: 0
SCSILogicalUnit: 1
SCSIPort: 5
SCSITargetId: 0
SectorsPerTrack: 63
Signature: 1753344085
Size: 1069286400
Status: OK
SystemCreationClassName: Win32_ComputerSystem
SystemName: WIN-2008-DEV
TotalCylinders: 130
TotalHeads: 255
TotalSectors: 2088450
TotalTracks: 33150
TracksPerCylinder: 255

BytesPerSector: 512
Capabilities: System.UInt16[]
CapabilityDescriptions: System.String[]
Caption: VMware, VMware Virtual S SCSI Disk Device
ConfigManagerErrorCode: 0
ConfigManagerUserConfig: False
CreationClassName: Win32_DiskDrive
Description: Disk drive
DeviceID: \\.\PHYSICALDRIVE0
FirmwareRevision: 1.0 
Index: 0
InterfaceType: SCSI
Manufacturer: (Standard disk drives)
MediaLoaded: True
MediaType: Fixed hard disk media
Model: VMware, VMware Virtual S SCSI Disk Device
Name: \\.\PHYSICALDRIVE0
Partitions: 1
PNPDeviceID: SCSI\DISK&VEN_VMWARE_&PROD_VMWARE_VIRTUAL_S\5&35015FB0&0&000000
SCSIBus: 0
SCSILogicalUnit: 0
SCSIPort: 2
SCSITargetId: 0
SectorsPerTrack: 63
Signature: 1044268863
Size: 64420392960
Status: OK
SystemCreationClassName: Win32_ComputerSystem
SystemName: WIN-2008-DEV
TotalCylinders: 7832
TotalHeads: 255
TotalSectors: 125821080
TotalTracks: 1997160
TracksPerCylinder: 255

GetLogicalDisks
---------------
Caption: A:
CreationClassName: Win32_LogicalDisk
Description: 3 1/2 Inch Floppy Drive
DeviceID: A:
DriveType: 2
MediaType: 5
Name: A:
SystemCreationClassName: Win32_ComputerSystem
SystemName: WIN-2008-DEV

Access: 0
Caption: C:
Compressed: False
CreationClassName: Win32_LogicalDisk
Description: Local Fixed Disk
DeviceID: C:
DriveType: 3
FileSystem: NTFS
FreeSpace: 15540703232
MaximumComponentLength: 255
MediaType: 12
Name: C:
QuotasDisabled: True
QuotasIncomplete: False
QuotasRebuilding: False
Size: 64419340288
SupportsDiskQuotas: True
SupportsFileBasedCompression: True
SystemCreationClassName: Win32_ComputerSystem
SystemName: WIN-2008-DEV
VolumeDirty: False
VolumeSerialNumber: 8C0F8147

Caption: D:
CreationClassName: Win32_LogicalDisk
Description: CD-ROM Disc
DeviceID: D:
DriveType: 5
MediaType: 11
Name: D:
SystemCreationClassName: Win32_ComputerSystem
SystemName: WIN-2008-DEV

Access: 0
Caption: E:
Compressed: False
CreationClassName: Win32_LogicalDisk
Description: Local Fixed Disk
DeviceID: E:
DriveType: 3
FileSystem: NTFS
FreeSpace: 1044287488
MaximumComponentLength: 255
MediaType: 12
Name: E:
QuotasDisabled: True
QuotasIncomplete: False
QuotasRebuilding: False
Size: 1073446912
SupportsDiskQuotas: True
SupportsFileBasedCompression: True
SystemCreationClassName: Win32_ComputerSystem
SystemName: WIN-2008-DEV
VolumeDirty: False
VolumeName: cloned
VolumeSerialNumber: 49EB5AA3

Access: 0
Caption: I:
Compressed: False
CreationClassName: Win32_LogicalDisk
Description: Network Connection
DeviceID: I:
DriveType: 4
FileSystem: NTFS
FreeSpace: 7290720780288
MaximumComponentLength: 255
MediaType: 0
Name: I:
ProviderName: \\nas\mini
Size: 11828334428160
SupportsDiskQuotas: False
SupportsFileBasedCompression: False
SystemCreationClassName: Win32_ComputerSystem
SystemName: WIN-2008-DEV
VolumeName: mini
VolumeSerialNumber: 2E8B152D

Access: 0
Caption: Y:
Compressed: False
CreationClassName: Win32_LogicalDisk
Description: Network Connection
DeviceID: Y:
DriveType: 4
FileSystem: NTFS
FreeSpace: 7290720780288
MaximumComponentLength: 255
MediaType: 0
Name: Y:
ProviderName: \\nas\mini
Size: 11828334428160
SupportsDiskQuotas: False
SupportsFileBasedCompression: False
SystemCreationClassName: Win32_ComputerSystem
SystemName: WIN-2008-DEV
VolumeName: mini
VolumeSerialNumber: 2E8B152D

Access: 0
Caption: Z:
Compressed: False
CreationClassName: Win32_LogicalDisk
Description: Network Connection
DeviceID: Z:
DriveType: 4
FileSystem: NTFS
FreeSpace: 69944565760
MaximumComponentLength: 255
MediaType: 0
Name: Z:
ProviderName: \\pt3\raid1
Size: 1198900248576
SupportsDiskQuotas: False
SupportsFileBasedCompression: False
SystemCreationClassName: Win32_ComputerSystem
SystemName: WIN-2008-DEV
VolumeName: RAID1
VolumeSerialNumber: 005400C4

GetDiskDrivesAndRelatedPartitions
---------------------------------
DeviceId: \\.\PHYSICALDRIVE1

	DiskPartition: Disk #1, Partition #0
		LogicalDisk: E:
DeviceId: \\.\PHYSICALDRIVE0

	DiskPartition: Disk #0, Partition #0
		LogicalDisk: C:
GetDiskDrivePhysicalMediaAndRelatedPartitions
---------------------------------------------
*/