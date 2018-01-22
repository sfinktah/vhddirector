namespace Microsoft.Storage.Vds
{
    using System;

    public enum HbaPortStatus
    {
        Bypassed = 4,
        Diagnostics = 5,
        Error = 7,
        LinkDown = 6,
        Loopback = 8,
        Offline = 3,
        Online = 2,
        Unknown = 1
    }
}

