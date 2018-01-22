namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SubSystemProperties
    {
        public Guid Id;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string FriendlyName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Identification;
        public SubSystemFlags Flags;
        public uint StripeSizeFlags;
        public SubSystemStatus Status;
        public Microsoft.Storage.Vds.Health Health;
        public short NumberOfInternalBuses;
        public short MaxNumberOfSlotsEachBus;
        public short MaxNumberOfControllers;
        public short RebuildPriority;
    }
}

