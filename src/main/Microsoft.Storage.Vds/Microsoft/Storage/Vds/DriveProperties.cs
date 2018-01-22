namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DriveProperties
    {
        public Guid Id;
        public ulong Size;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string FriendlyName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Identification;
        public DriveFlags Flags;
        public DriveStatus Status;
        public Microsoft.Storage.Vds.Health Health;
        public short InternalBusNumber;
        public short SlotNumber;
    }
}

