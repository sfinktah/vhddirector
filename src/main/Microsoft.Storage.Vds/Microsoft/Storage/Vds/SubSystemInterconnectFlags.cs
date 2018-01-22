namespace Microsoft.Storage.Vds
{
    using System;

    [Flags]
    public enum SubSystemInterconnectFlags : uint
    {
        FibreChannel = 2,
        IScsi = 4,
        None = 0,
        PciRaid = 1,
        Sas = 8
    }
}

