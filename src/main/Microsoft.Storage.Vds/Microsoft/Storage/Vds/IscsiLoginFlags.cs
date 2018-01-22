namespace Microsoft.Storage.Vds
{
    using System;

    [Flags]
    public enum IscsiLoginFlags
    {
        MultipathEnabled = 2,
        RequireIpsec = 1
    }
}

