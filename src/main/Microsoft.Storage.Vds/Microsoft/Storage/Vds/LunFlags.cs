namespace Microsoft.Storage.Vds
{
    using System;

    [Flags]
    public enum LunFlags
    {
        ConsistencyCheckEnabled = 0x80,
        HardwareChecksumEnabled = 8,
        LbnRemapEnabled = 1,
        MediaScanEnabled = 0x40,
        None = 0,
        ReadBackVerifyEnabled = 2,
        ReadCacheEnabled = 0x10,
        Snapshot = 0x100,
        WriteCacheEnabled = 0x20,
        WriteThroughCachingEnabled = 4
    }
}

