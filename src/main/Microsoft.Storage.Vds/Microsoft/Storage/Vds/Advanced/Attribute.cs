namespace Microsoft.Storage.Vds.Advanced
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct Attribute
    {
        [FieldOffset(0)]
        public ulong Attributes;
        [FieldOffset(0)]
        public uint BootIndicator;
    }
}

