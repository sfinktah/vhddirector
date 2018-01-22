namespace Microsoft.Storage.Vds.Advanced
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct MbrPartitionInfo
    {
        public byte PartitionType;
        public uint BootIndicator;
    }
}

