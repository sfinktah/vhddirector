namespace Microsoft.Storage.Vds
{
    using System;

    [Flags]
    public enum ServiceFlags
    {
        AutoMountOff = 0x20,
        ClusterServiceConfigured = 0x10,
        None = 0,
        OSUninstallValid = 0x40,
        SupportDynamic = 1,
        SupportDynamic1394 = 8,
        SupportFaultTolerant = 2,
        SupportGpt = 4
    }
}

