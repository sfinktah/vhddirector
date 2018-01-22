namespace Microsoft.Storage.Vds
{
    using System;

    [Flags]
    public enum SubSystemSupportedRaidTypes
    {
        None = 0,
        SupportsRaid01Luns = 0x20,
        SupportsRaid03Luns = 0x40,
        SupportsRaid05Luns = 0x80,
        SupportsRaid10Luns = 0x100,
        SupportsRaid15Luns = 0x200,
        SupportsRaid2Luns = 1,
        SupportsRaid30Luns = 0x400,
        SupportsRaid3Luns = 2,
        SupportsRaid4Luns = 4,
        SupportsRaid50Luns = 0x800,
        SupportsRaid51Luns = 0x1000,
        SupportsRaid53Luns = 0x2000,
        SupportsRaid5Luns = 8,
        SupportsRaid60Luns = 0x4000,
        SupportsRaid61Luns = 0x8000,
        SupportsRaid6Luns = 0x10
    }
}

