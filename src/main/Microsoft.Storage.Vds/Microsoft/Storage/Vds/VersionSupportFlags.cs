namespace Microsoft.Storage.Vds
{
    using System;

    [Flags]
    public enum VersionSupportFlags : uint
    {
        None = 0,
        Version1_0 = 1,
        Version1_1 = 2
    }
}

