namespace Microsoft.Storage.Vds
{
    using System;

    public enum TransitionState
    {
        Unknown,
        Stable,
        Extending,
        Shrinking,
        Reconfiging,
        Restriping
    }
}

