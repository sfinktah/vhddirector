namespace Microsoft.Storage.Vds
{
    using System;

    [Flags]
    public enum LoadBalanceSupportFlags
    {
        DynamicLeastQueueDepth = 8,
        Failover = 1,
        LeastBlocks = 0x20,
        None = 0,
        RoundRobin = 2,
        RoundRobinWithSubset = 4,
        VendorSpecific = 0x40,
        WeightedPaths = 0x10
    }
}

