namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PathInfo
    {
        public FullPathId PathId;
        public HardwareProviderType Type;
        public PathStatus Status;
        public Microsoft.Storage.Vds.SubSystemEndpoint SubSystemEndpoint;
        public Microsoft.Storage.Vds.ServerEndpoint ServerEndpoint;
        public IntPtr AdditionalInfo;
    }
}

