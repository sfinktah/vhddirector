namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ProviderProperties
    {
        public Guid Id;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Name;
        public Guid VersionId;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Version;
        public ProviderType Type;
        public ProviderFlags Flags;
        public uint StripeSizeFlags;
        public short RebuildPriority;
    }
}

