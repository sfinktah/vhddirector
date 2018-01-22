namespace Microsoft.Storage.Vds
{
    using System;

    public enum VolumeType
    {
        Mirror = 13,
        Parity = 14,
        Simple = 10,
        Span = 11,
        Stripe = 12,
        Unknown = 0
    }
}

