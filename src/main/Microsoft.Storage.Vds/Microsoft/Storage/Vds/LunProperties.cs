namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct LunProperties
    {
        public Guid Id;
        public ulong Size;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string FriendlyName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Identification;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string UnmaskingList;
        public LunFlags Flags;
        public LunType Type;
        public LunStatus Status;
        public Microsoft.Storage.Vds.Health Health;
        public Microsoft.Storage.Vds.TransitionState TransitionState;
        public short RebuildPriority;
    }
}

