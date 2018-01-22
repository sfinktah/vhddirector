namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct InitiatorAdapterProperties
    {
        public Guid Id;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Name;
    }
}

