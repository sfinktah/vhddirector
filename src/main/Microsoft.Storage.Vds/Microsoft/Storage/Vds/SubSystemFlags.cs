namespace Microsoft.Storage.Vds
{
    using System;

    [Flags]
    public enum SubSystemFlags
    {
        ConsistencyCheckCapable = 0x1000000,
        DriveExtentCapable = 8,
        HardwareChecksumCapable = 0x10,
        LunMaskingCapable = 1,
        LunNamingCapable = 0x100,
        LunPlexingCapable = 2,
        LunRemappingCapable = 4,
        MediaScanCapable = 0x800000,
        None = 0,
        RadiusCapable = 0x20,
        ReadBackVerifyCapable = 0x40,
        ReadCachingCapable = 0x200000,
        SupportsAuthChap = 0x10000,
        SupportsAuthMutualChap = 0x20000,
        SupportsFaultTolerantLuns = 0x200,
        SupportsLunNumber = 0x80000,
        SupportsMirroredCache = 0x100000,
        SupportsMirrorLuns = 0x4000,
        SupportsNonFaultTolerantLuns = 0x400,
        SupportsParityLuns = 0x8000,
        SupportsSimpleLuns = 0x800,
        SupportsSimpleTargetConfig = 0x40000,
        SupportsSpanLuns = 0x1000,
        SupportsStripeLuns = 0x2000,
        WriteCachingCapable = 0x400000,
        WriteThroughCachingCapable = 0x80
    }
}

