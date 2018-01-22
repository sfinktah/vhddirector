namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct InitiatorPortalProperties
    {
        public Guid Id;
        public IPAddress Address;
        public uint PortIndex;
    }
}

