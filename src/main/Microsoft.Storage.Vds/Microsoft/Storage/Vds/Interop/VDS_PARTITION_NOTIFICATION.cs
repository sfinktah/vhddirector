namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct VDS_PARTITION_NOTIFICATION
    {
        public VDS_NOTIFICATION_EVENT Event;
        public Guid DiskId;
        public ulong Offset;
    }
}

