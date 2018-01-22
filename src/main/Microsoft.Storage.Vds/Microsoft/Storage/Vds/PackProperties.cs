namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PackProperties
    {
        public Guid Id;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Name;
        public PackStatus Status;
        public PackFlags Flags;
    }
}

