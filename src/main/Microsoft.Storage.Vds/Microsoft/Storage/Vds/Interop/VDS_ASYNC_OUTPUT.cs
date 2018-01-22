namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct VDS_ASYNC_OUTPUT
    {
        public VDS_ASYNC_OUTPUT_TYPE Type;
        public VDS_ASYNC_OUTPUT_INFO Info;
    }
}

