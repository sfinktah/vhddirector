namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ReparsePointProperties
    {
        public Guid SourceVolumeId;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Path;
    }
}

