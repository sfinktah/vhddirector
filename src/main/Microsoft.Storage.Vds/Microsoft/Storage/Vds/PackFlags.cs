namespace Microsoft.Storage.Vds
{
    using System;

    [Flags]
    public enum PackFlags
    {
        Corrupted = 8,
        Foreign = 1,
        None = 0,
        NoQuorum = 2,
        OnlineError = 0x10,
        Policy = 4
    }
}

