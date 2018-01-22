namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct VdsStorageIdentifier
    {
        public StorageIdentifierCodeSet CodeSet;
        public StorageIdentifierType Type;
        public uint IdentifierLength;
        public IntPtr Identifier;
    }
}

