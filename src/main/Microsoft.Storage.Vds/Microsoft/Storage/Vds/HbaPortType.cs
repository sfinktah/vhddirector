namespace Microsoft.Storage.Vds
{
    using System;

    public enum HbaPortType
    {
        EPort = 9,
        FLPort = 7,
        FPort = 8,
        GPort = 10,
        LPort = 20,
        NLPort = 6,
        NotPresent = 3,
        NPort = 5,
        Other = 2,
        Ptp = 0x15,
        Unknown = 1
    }
}

