namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PortalProperties
    {
        public Guid Id;
        public IPAddress Address;
        public PortalStatus Status;
    }
}

