namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("90681B1D-6A7F-48e8-9061-31B7AA125322")]
    public interface IVdsDiskOnline
    {
        void Online();
        void Offline();
    }
}

