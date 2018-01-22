namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("d5d23b6d-5a55-4492-9889-397a3c2d2dbc"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsAsync
    {
        void Cancel();
        void Wait([MarshalAs(UnmanagedType.Error)] out uint hrResult, out VDS_ASYNC_OUTPUT asyncOut);
        void QueryStatus([MarshalAs(UnmanagedType.Error)] out uint hrResult, out uint percentCompleted);
    }
}

