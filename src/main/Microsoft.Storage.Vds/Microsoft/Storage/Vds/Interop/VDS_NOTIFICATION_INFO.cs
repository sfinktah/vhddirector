namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct VDS_NOTIFICATION_INFO
    {
        [FieldOffset(0)]
        public VDS_CONTROLLER_NOTIFICATION Controller;
        [FieldOffset(0)]
        public VDS_DISK_NOTIFICATION Disk;
        [FieldOffset(0)]
        public VDS_DRIVE_NOTIFICATION Drive;
        [FieldOffset(0)]
        public VDS_FILE_SYSTEM_NOTIFICATION FileSystem;
        [FieldOffset(0)]
        public VDS_DRIVE_LETTER_NOTIFICATION Letter;
        [FieldOffset(0)]
        public VDS_LUN_NOTIFICATION Lun;
        [FieldOffset(0)]
        public VDS_MOUNT_POINT_NOTIFICATION MountPoint;
        [FieldOffset(0)]
        public VDS_PACK_NOTIFICATION Pack;
        [FieldOffset(0)]
        public VDS_PARTITION_NOTIFICATION Partition;
        [FieldOffset(0)]
        public VDS_PORT_NOTIFICATION Port;
        [FieldOffset(0)]
        public VDS_PORTAL_NOTIFICATION Portal;
        [FieldOffset(0)]
        public VDS_PORTAL_GROUP_NOTIFICATION PortalGroup;
        [FieldOffset(0)]
        public VDS_SERVICE_NOTIFICATION Service;
        [FieldOffset(0)]
        public VDS_SUB_SYSTEM_NOTIFICATION SubSystem;
        [FieldOffset(0)]
        public VDS_TARGET_NOTIFICATION Target;
        [FieldOffset(0)]
        public VDS_VOLUME_NOTIFICATION Volume;
    }
}

