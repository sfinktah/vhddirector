namespace Microsoft.Storage.Vds
{
    using System;

    [Flags]
    public enum VolumeFlags
    {
        Active = 4,
        BootVolume = 2,
        CanExtend = 0x20,
        CanShrink = 0x40,
        CrashDump = 0x200,
        Fat32NotSupported = 0x8000,
        FatNotSupported = 0x10000,
        Formatting = 0x1000,
        Hibernation = 0x100,
        Hidden = 0x10,
        Installable = 0x400,
        LbnRemapEnabled = 0x800,
        NoDefaultDriveLetter = 0x20000,
        None = 0,
        NotFormattable = 0x2000,
        NtfsNotSupported = 0x4000,
        PageFile = 0x80,
        PermanentDismountSupported = 0x80000,
        PermanentlyDismounted = 0x40000,
        ReadOnly = 8,
        SystemVolume = 1
    }
}

