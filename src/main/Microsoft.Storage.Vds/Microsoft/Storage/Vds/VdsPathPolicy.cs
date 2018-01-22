namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct VdsPathPolicy
    {
        public FullPathId PathId;
        public uint PrimaryPath;
        public uint Weight;
    }
}

