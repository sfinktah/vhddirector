namespace Microsoft.Storage.Vds.Advanced
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PartitionProperties
    {
        public PartitionStyle Style;
        public PartitionFlags Flags;
        public uint PartitionNumber;
        public ulong Offset;
        public ulong Size;
        public PartitionInfo Info;
    }
}

