namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("e0393303-90d4-4a97-ab71-e9b671ee2729")]
    public interface IVdsServiceLoader
    {
        void LoadService([In, MarshalAs(UnmanagedType.LPWStr)] string machineName, out IVdsService vdsService);
    }
}

