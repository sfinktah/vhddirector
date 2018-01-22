namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct VolumePlexProperties
    {
        public Guid Id;
        public VolumePlexType Type;
        public VolumePlexStatus Status;
        public Microsoft.Storage.Vds.Health Health;
        public Microsoft.Storage.Vds.TransitionState TransitionState;
        public ulong Size;
        public uint StripeSize;
        public uint NumberOfMembers;
    }
}

