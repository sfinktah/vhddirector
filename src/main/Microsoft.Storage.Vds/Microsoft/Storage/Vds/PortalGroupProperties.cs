namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PortalGroupProperties
    {
        public Guid Id;
        public ushort Tag;
    }
}

