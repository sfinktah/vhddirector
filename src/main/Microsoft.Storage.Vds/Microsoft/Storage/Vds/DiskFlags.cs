namespace Microsoft.Storage.Vds
{
    using System;

    [Flags]
    public enum DiskFlags
    {
        AudioCD = 1,
        BootDisk = 0x80,
        Clustered = 0x20,
        CrashDumpDisk = 0x400,
        HibernationFileDisk = 0x200,
        HotSpare = 2,
        Masked = 8,
        None = 0,
        PageFileDisk = 0x100,
        ReadOnly = 0x40,
        ReserveCapable = 4,
        StyleConvertible = 0x10
    }
}

