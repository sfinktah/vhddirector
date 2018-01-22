namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct cp
    {
        public ulong Offset;
        public Guid VolumeId;
    }
}

