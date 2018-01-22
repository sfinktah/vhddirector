namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DiskProperties
    {
        public Guid Id;
        public DiskStatus Status;
        public LunReserveMode ReserveMode;
        public Microsoft.Storage.Vds.Health Health;
        public uint DeviceType;
        public uint MediaType;
        public ulong Size;
        public uint BytesPerSector;
        public uint SectorsPerTrack;
        public uint TracksPerCylinder;
        public DiskFlags Flags;
        public StorageBusType BusType;
        public Microsoft.Storage.Vds.PartitionStyle PartitionStyle;
        public DiskSignature Signature;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string DiskAddress;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Name;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string FriendlyName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string AdaptorName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string DevicePath;
    }
}

