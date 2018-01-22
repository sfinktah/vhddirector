namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct FullPathId
    {
        public ulong SourceId;
        public ulong PathId;
    }
}

