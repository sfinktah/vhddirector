namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct VdsLunInformation
    {
        public uint Version;
        public byte DeviceType;
        public byte DeviceTypeModifier;
        public uint CommandQueueing;
        public StorageBusType BusType;
        [MarshalAs(UnmanagedType.LPStr)]
        public string VendorId;
        [MarshalAs(UnmanagedType.LPStr)]
        public string ProductId;
        [MarshalAs(UnmanagedType.LPStr)]
        public string ProductRevision;
        [MarshalAs(UnmanagedType.LPStr)]
        public string SerialNumber;
        public Guid DiskSignature;
        public VdsStorageDeviceIdDescriptor DeviceIdDescriptor;
        public uint NumberOfInterconnects;
        public IntPtr Interconnects;
    }
}

