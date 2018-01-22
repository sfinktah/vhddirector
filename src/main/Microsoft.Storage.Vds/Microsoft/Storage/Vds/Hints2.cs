namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Hints2
    {
        public HintFlags HintMask;
        public ulong ExpectedMaximumSize;
        public uint OptimalReadSize;
        public uint OptimalReadAlignment;
        public uint OptimalWriteSize;
        public uint OptimalWriteAlignment;
        public uint MaximumDriveCount;
        public uint StripeSize;
        public uint Reserved1;
        public uint Reserved2;
        public uint Reserved3;
        [MarshalAs(UnmanagedType.U4)]
        public uint FastCrashRecoveryRequired;
        [MarshalAs(UnmanagedType.U4)]
        public uint MostlyReads;
        [MarshalAs(UnmanagedType.U4)]
        public uint OptimizeForSequentialReads;
        [MarshalAs(UnmanagedType.U4)]
        public uint OptimizeForSequentialWrites;
        [MarshalAs(UnmanagedType.U4)]
        public uint RemapEnabled;
        [MarshalAs(UnmanagedType.U4)]
        public uint ReadBackVerifyEnabled;
        [MarshalAs(UnmanagedType.U4)]
        public uint WriteThroughCachingEnabled;
        [MarshalAs(UnmanagedType.U4)]
        public uint HardwareChecksumEnabled;
        [MarshalAs(UnmanagedType.U4)]
        public uint IsYankable;
        [MarshalAs(UnmanagedType.U4)]
        public uint AllocateHotSpare;
        [MarshalAs(UnmanagedType.U4)]
        public uint UseMirroredCache;
        [MarshalAs(UnmanagedType.U4)]
        public uint ReadCachingEnabled;
        [MarshalAs(UnmanagedType.U4)]
        public uint WriteCachingEnabled;
        [MarshalAs(UnmanagedType.U4)]
        public uint MediaScanEnabled;
        [MarshalAs(UnmanagedType.U4)]
        public uint ConsistencyCheckEnabled;
        public StorageBusType BusType;
        [MarshalAs(UnmanagedType.U4)]
        public uint BoolReserved1;
        [MarshalAs(UnmanagedType.U4)]
        public uint BoolReserved2;
        [MarshalAs(UnmanagedType.U4)]
        public uint BoolReserved3;
        public short RebuildPriority;
    }
}

