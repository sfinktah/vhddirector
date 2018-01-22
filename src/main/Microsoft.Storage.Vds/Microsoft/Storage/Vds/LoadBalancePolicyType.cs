namespace Microsoft.Storage.Vds
{
    using System;

    public enum LoadBalancePolicyType
    {
        Unknown,
        Failover,
        RoundRobin,
        RoundRobinWithSubset,
        DynamicLeastQueueDepth,
        WeightedPaths,
        LeastBlocks,
        VendorSpecific
    }
}

