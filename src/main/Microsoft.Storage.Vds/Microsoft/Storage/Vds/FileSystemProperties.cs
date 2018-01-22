namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct FileSystemProperties
    {
        public FileSystemType Type;
        public Guid VolumeId;
        public FileSystemFlags Flags;
        public ulong TotalAllocationUnits;
        public ulong AvailableAllocationUnits;
        public uint AllocationUnitSize;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Label;
    }
}

