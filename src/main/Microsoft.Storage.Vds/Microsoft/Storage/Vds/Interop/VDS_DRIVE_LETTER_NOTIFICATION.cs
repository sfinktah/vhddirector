namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct VDS_DRIVE_LETTER_NOTIFICATION
    {
        public VDS_NOTIFICATION_EVENT Event;
        public char Letter;
        public Guid VolumeId;
    }
}

