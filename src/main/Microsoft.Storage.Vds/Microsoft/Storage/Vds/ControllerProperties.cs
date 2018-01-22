namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ControllerProperties
    {
        public Guid Id;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string FriendlyName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Identification;
        public ControllerStatus status;
        public Microsoft.Storage.Vds.Health Health;
        public ushort NumberOfPorts;
    }
}

