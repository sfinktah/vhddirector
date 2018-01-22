namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct VDS_MOUNT_POINT_NOTIFICATION
    {
        public VDS_NOTIFICATION_EVENT Event;
        public Guid VolumeId;
    }
}

