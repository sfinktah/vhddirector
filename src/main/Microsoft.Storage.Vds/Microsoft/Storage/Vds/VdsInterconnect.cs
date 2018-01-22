namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct VdsInterconnect
    {
        public InterconnectAddressType AddressType;
        public uint PortLength;
        public IntPtr Port;
        public uint AddressLength;
        public IntPtr Address;
    }
}

