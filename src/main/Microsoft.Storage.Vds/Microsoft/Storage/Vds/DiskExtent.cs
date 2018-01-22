namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DiskExtent
    {
        public Guid DiskId;
        public DiskExtentType Type;
        public ulong Offset;
        public ulong Size;
        public Guid VolumeId;
        public Guid PlexId;
        public uint MemberIndex;
    }
}

