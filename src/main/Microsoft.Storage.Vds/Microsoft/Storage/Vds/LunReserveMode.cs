namespace Microsoft.Storage.Vds
{
    using System;

    public enum LunReserveMode
    {
        None,
        ExclusiveReadWrite,
        ExclusiveReadOnly,
        SharedReadOnly,
        SharedReadWrite
    }
}

