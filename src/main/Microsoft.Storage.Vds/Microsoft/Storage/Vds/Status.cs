namespace Microsoft.Storage.Vds
{
    using System;

    public enum Status
    {
        Unknown,
        Online,
        NotReady,
        NoMedia,
        Offline,
        Failed,
        Missing,
        Standby,
        Removed,
        PartiallyManaged
    }
}

