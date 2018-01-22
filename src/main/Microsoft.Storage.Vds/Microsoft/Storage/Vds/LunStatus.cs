namespace Microsoft.Storage.Vds
{
    using System;

    public enum LunStatus
    {
        Failed = 5,
        NotReady = 2,
        Offline = 4,
        Online = 1,
        PartiallyManaged = 9,
        Unknown = 0
    }
}

