namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct LunPlexProperties
    {
        public Guid Id;
        public ulong Size;
        public LunPlexType Type;
        public LunPlexStatus Status;
        public Microsoft.Storage.Vds.Health Health;
        public Microsoft.Storage.Vds.TransitionState TransitionState;
        public LunPlexFlags Flags;
        public uint StripeSize;
        public short RebuildPriority;
    }
}

