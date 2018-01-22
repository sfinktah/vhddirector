namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("0ee1a790-5d2e-4abb-8c99-c481e8be2138"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsLunPlex
    {
        void GetProperties(out LunPlexProperties plexProp);
        void GetLun(out IVdsLun lun);
        void QueryExtents(out IntPtr driveExtents, out int numberOfExtents);
        void QueryHints(out Hints hints);
        void ApplyHints([In] ref Hints hints);
    }
}

