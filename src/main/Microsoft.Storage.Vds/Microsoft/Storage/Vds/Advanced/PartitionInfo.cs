namespace Microsoft.Storage.Vds.Advanced
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct PartitionInfo
    {
        [FieldOffset(0)]
        public GptPartitionInfo Gpt;
        [FieldOffset(0)]
        public MbrPartitionInfo Mbr;
    }
}

