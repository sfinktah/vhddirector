namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("07e5c822-f00c-47a1-8fce-b244da56fd06")]
    public interface IVdsDisk
    {
        void GetProperties(out DiskProperties diskProperties);
        void GetPack([Out] IVdsPack pack);
        void GetIdentificationData(IntPtr lunInfo);
        void QueryExtents(out IntPtr diskExtents, out int numberOfExtents);
        void slot4();
        void SetFlags([In] DiskFlags diskFlag);
        void ClearFlags([In] DiskFlags diskFlag);
    }
}

