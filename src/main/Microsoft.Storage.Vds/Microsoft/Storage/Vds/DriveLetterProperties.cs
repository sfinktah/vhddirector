namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DriveLetterProperties
    {
        public char Letter;
        public Guid VolumeId;
        public DriveLetterFlags Flags;
        [MarshalAs(UnmanagedType.U4)]
        public uint Used;
    }
}

