namespace Microsoft.Storage.Vds.Interop
{
    using System;

    public enum VDS_NOTIFICATION_TARGET_TYPE
    {
        VDS_NTT_CONTROLLER = 0x1f,
        VDS_NTT_DISK = 13,
        VDS_NTT_DRIVE = 0x20,
        VDS_NTT_DRIVE_LETTER = 0x3d,
        VDS_NTT_FILE_SYSTEM = 0x3e,
        VDS_NTT_LUN = 0x21,
        VDS_NTT_MOUNT_POINT = 0x3f,
        VDS_NTT_PACK = 10,
        VDS_NTT_PARTITION = 60,
        VDS_NTT_PORT = 0x23,
        VDS_NTT_PORTAL = 0x24,
        VDS_NTT_PORTAL_GROUP = 0x26,
        VDS_NTT_SERVICE = 200,
        VDS_NTT_SUB_SYSTEM = 30,
        VDS_NTT_TARGET = 0x25,
        VDS_NTT_UNKNOWN = 0,
        VDS_NTT_VOLUME = 11
    }
}

