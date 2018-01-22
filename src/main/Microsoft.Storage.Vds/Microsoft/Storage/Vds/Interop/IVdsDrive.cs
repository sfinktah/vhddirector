namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("ff24efa4-aade-4b6b-898b-eaa6a20887c7"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsDrive
    {
        void GetProperties(out DriveProperties driveProp);
        void GetSubSystem(out IVdsSubSystem subSystem);
        void QueryExtents(out IntPtr driveExtents, out int numberOfExtents);
        void SetFlags([In] DriveFlags flags);
        void ClearFlags([In] DriveFlags flags);
        void SetStatus([In] DriveStatus status);
    }
}

