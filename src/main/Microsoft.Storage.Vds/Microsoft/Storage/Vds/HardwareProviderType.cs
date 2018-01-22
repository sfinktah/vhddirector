namespace Microsoft.Storage.Vds
{
    using System;

    public enum HardwareProviderType
    {
        Unknown,
        PciRaid,
        FibreChannel,
        IScsi,
        Sas,
        Hybrid
    }
}

