namespace Microsoft.Storage.Vds
{
    using System;

    [Flags]
    public enum HbaPortSpeedFlags
    {
        FourGBit = 8,
        NotNegoititaed = 0x8000,
        OneGBit = 1,
        TenGBit = 4,
        TwoGBit = 2,
        Unknown = 0
    }
}

