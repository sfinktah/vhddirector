namespace Microsoft.Storage.Vds
{
    using System;

    public enum LunPlexType
    {
        Parity = 14,
        Raid03 = 0x15,
        Raid05 = 0x16,
        Raid10 = 0x17,
        Raid15 = 0x18,
        Raid2 = 15,
        Raid3 = 0x10,
        Raid30 = 0x19,
        Raid4 = 0x11,
        Raid5 = 0x12,
        Raid50 = 0x1a,
        Raid53 = 0x1c,
        Raid6 = 0x13,
        Raid60 = 0x1d,
        Simple = 10,
        Span = 11,
        Stripe = 12,
        Unknown = 0
    }
}

