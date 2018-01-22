namespace Microsoft.Storage.Vds.Advanced
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit, Size=0x48)]
    public struct GptPartitionInfoName
    {
        [FieldOffset(0)]
        private char chars;
    }
}

