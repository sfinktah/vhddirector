namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct VDS_FILE_SYSTEM_NOTIFICATION
    {
        public VDS_NOTIFICATION_EVENT Event;
        public Guid VolumeId;
        public uint PercentCompleted;
    }
}

