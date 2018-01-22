namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DriveExtent
    {
        public Guid DriveId;
        public Guid LunId;
        public ulong Size;
        public uint Used;
    }
}

