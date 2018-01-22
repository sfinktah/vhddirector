namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
    public struct IPAddress
    {
        public IPAddressType Type;
        public uint Ipv4Address;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x10)]
        public byte[] Ipv6Address;
        public uint Ipv6FlowInfo;
        public uint Ipv6ScopeId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst=0x101)]
        public string TextAddress;
        public uint Port;
    }
}

