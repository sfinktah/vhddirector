namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ServiceProperties
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Version;
        public ServiceFlags Flags;
    }
}

