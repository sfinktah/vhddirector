namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("4daa0135-e1d1-40f1-aaa5-3cc1e53221c3"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsVolumePlex
    {
        void GetProperties(out VolumePlexProperties pPlexProperties);
        void GetVolume(out IVdsVolume volume);
        void QueryExtents(out IntPtr diskExtents, out int numberOfExtents);
        void Repair([In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=1)] InputDisk[] inputDiskArray, [In] int numberOfDisks, out IVdsAsync async);
    }
}

