namespace Microsoft.Storage.Vds
{
    using System;

    public enum Health
    {
        Unknown,
        Healthy,
        Rebuilding,
        Stale,
        Failing,
        FailingRedundancy,
        FailedRedundancy,
        FailedRedundancyFailing,
        Failed,
        Replaced,
        PendingFailure,
        Degraded
    }
}

