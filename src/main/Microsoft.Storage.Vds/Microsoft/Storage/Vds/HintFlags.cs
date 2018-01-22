namespace Microsoft.Storage.Vds
{
    using System;

    [Flags]
    public enum HintFlags : ulong
    {
        AllocateHotSpare = 0x200L,
        BusType = 0x400L,
        ConsistencyCheckEnabled = 0x8000L,
        FastCrashRecoveryRequired = 1L,
        HardwareChecksumEnabled = 0x80L,
        IsYankable = 0x100L,
        MediaScanEnabled = 0x4000L,
        MostlyReads = 2L,
        None = 0L,
        OptimizeForSequentialReads = 4L,
        OptimizeForSequentialWrites = 8L,
        ReadBackVerifyEnabled = 0x10L,
        ReadCachingEnabled = 0x1000L,
        RemapEnabled = 0x20L,
        UseMirroredCache = 0x800L,
        WriteCachingEnabled = 0x2000L,
        WriteThroughCachingEnabled = 0x40L
    }
}

