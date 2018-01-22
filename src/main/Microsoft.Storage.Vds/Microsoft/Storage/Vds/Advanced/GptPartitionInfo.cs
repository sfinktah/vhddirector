namespace Microsoft.Storage.Vds.Advanced
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct GptPartitionInfo
    {
        public Guid PartitionType;
        public Guid PartitionId;
        public ulong Attributes;
        public GptPartitionInfoName Name;
    }
}

