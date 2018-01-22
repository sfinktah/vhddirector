namespace Microsoft.Storage.Vds
{
    using System;

    [Flags]
    public enum IpsecFlags : ulong
    {
        AggresiveMode = 8L,
        Ike = 2L,
        MainMode = 4L,
        PfsEnable = 0x10L,
        TransportModePreferred = 0x20L,
        TunnelModePreferred = 0x40L,
        Valid = 1L
    }
}

