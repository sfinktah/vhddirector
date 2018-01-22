namespace Microsoft.Storage.Vds
{
    using System;

    public enum DiskExtentType
    {
        Data = 2,
        Esp = 4,
        Free = 1,
        Ldm = 6,
        Msr = 5,
        Oem = 3,
        Unknown = 0,
        Unusable = 0x7fff
    }
}

