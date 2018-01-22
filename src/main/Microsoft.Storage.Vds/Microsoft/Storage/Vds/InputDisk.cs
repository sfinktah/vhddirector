namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct InputDisk
    {
        public Guid DiskId;
        public ulong Size;
        public Guid PlexId;
        public uint MemberIndex;
    }
}

