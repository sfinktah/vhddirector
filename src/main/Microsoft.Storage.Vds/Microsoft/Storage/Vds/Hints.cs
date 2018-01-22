namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Hints
    {
        public HintFlags HintMask;
        public ulong ExpectedMaximumSize;
        public uint OptimalReadSize;
        public uint OptimalReadAlignment;
        public uint OptimalWriteSize;
        public uint OptimalWriteAlignment;
        public uint MaximumDriveCount;
        public uint StripeSize;
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
        public short RebuildPriority;
    }
}

