namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct DiskSignature
    {
        [FieldOffset(0)]
        public Guid GptDiskGuid;
        [FieldOffset(0)]
        public uint MbrSignature;
    }
}

