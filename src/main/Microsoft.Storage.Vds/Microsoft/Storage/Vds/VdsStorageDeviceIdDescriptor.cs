namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct VdsStorageDeviceIdDescriptor
    {
        public uint Version;
        public uint NumberOfIdentifiers;
        public IntPtr Identifiers;
    }
}

