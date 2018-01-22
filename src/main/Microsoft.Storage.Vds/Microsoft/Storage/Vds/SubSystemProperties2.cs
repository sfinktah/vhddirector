namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SubSystemProperties2
    {
        public Guid Id;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string FriendlyName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Identification;
        public SubSystemFlags Flags;
        public uint StripeSizeFlags;
        [MarshalAs(UnmanagedType.U4)]
        public SubSystemSupportedRaidTypes SupportedRaidTypes;
        public SubSystemStatus Status;
        public Microsoft.Storage.Vds.Health Health;
        public short NumberOfInternalBuses;
        public short MaxNumberOfSlotsEachBus;
        public short MaxNumberOfControllers;
        public short RebuildPriority;
        public uint NumberOfEnclosures;
        public SubSystemProperties2(SubSystemProperties props)
        {
            this.Id = props.Id;
            this.FriendlyName = props.FriendlyName;
            this.Identification = props.Identification;
            this.Flags = props.Flags;
            this.StripeSizeFlags = props.StripeSizeFlags;
            this.SupportedRaidTypes = SubSystemSupportedRaidTypes.None;
            this.Status = props.Status;
            this.Health = props.Health;
            this.NumberOfInternalBuses = props.NumberOfInternalBuses;
            this.MaxNumberOfSlotsEachBus = props.MaxNumberOfSlotsEachBus;
            this.MaxNumberOfControllers = props.MaxNumberOfControllers;
            this.RebuildPriority = props.RebuildPriority;
            this.NumberOfEnclosures = 0;
        }
    }
}

