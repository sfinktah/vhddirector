namespace Microsoft.Storage.Vds
{
    using System;

    [Flags]
    public enum ProviderFlags : uint
    {
        Dynamic = 1,
        InternalHardwareProvider = 2,
        None = 0,
        OneDiskOnlyPerPack = 4,
        OnePackOnlineOnly = 8,
        SupportDynamic = 0x80000000,
        SupportDynamic1394 = 0x20000000,
        SupportFaultTolerant = 0x40000000,
        VolumeSpaceMustBeContiguous = 0x10
    }
}

