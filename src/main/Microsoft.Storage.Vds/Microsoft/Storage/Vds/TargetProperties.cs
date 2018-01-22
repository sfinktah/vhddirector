namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct TargetProperties
    {
        public Guid Id;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string IscsiName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string FriendlyName;
        public uint ChapEnabled;
    }
}

