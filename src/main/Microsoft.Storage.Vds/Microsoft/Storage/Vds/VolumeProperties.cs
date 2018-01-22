namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct VolumeProperties
    {
        public Guid Id;
        public VolumeType Type;
        public VolumeStatus Status;
        public Microsoft.Storage.Vds.Health Health;
        public Microsoft.Storage.Vds.TransitionState TransitionState;
        public ulong Size;
        public VolumeFlags Flags;
        public FileSystemType RecommendedFileSystemType;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Name;
    }
}

