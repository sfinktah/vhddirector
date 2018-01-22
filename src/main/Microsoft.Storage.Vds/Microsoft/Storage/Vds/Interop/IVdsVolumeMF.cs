namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("ee2d5ded-6236-4169-931d-b9778ce03dc6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsVolumeMF
    {
        void GetFileSystemProperties(out FileSystemProperties fileSystemProp);
        void Format([In] FileSystemType type, [In, MarshalAs(UnmanagedType.LPWStr)] string label, [In] uint dwUnitAllocationSize, [In] uint force, [In] uint quickFormat, [In] uint enableCompression, out IVdsAsync async);
        void AddAccessPath([In, MarshalAs(UnmanagedType.LPWStr)] string path);
        void QueryAccessPaths(out IntPtr accessPaths, out int numberOfAccessPaths);
        void QueryReparsePoints(out IntPtr reparsePointProps, out int numberOfReparsePointProps);
        void DeleteAccessPath([In, MarshalAs(UnmanagedType.LPWStr)] string path, [In] uint force);
        void Mount();
        void Dismount([In] uint force, [In] uint permanent);
        void SetFileSystemFlags([In] FileSystemFlags flags);
        void ClearFileSystemFlags([In] FileSystemFlags flags);
    }
}

