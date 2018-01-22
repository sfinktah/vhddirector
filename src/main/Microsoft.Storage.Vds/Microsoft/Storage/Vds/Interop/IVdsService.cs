namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("0818a8ef-9ba9-40d8-a6f9-e22833cc771e"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsService
    {
        [PreserveSig]
        int IsServiceReady();
        [PreserveSig]
        int WaitForServiceReady();
        void GetProperties(out ServiceProperties serviceProp);
        void QueryProviders([In] QueryProviderFlags mask, out IEnumVdsObject providers);
        void QueryMaskedDisks(out IEnumVdsObject disks);
        void QueryUnallocatedDisks(out IEnumVdsObject disks);
        void GetObject([In] Guid objectId, [In] ObjectType type, [MarshalAs(UnmanagedType.IUnknown)] out object vdsObject);
        void QueryDriveLetters([In] char firstLetter, [In] uint count, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=1)] DriveLetterProperties[] driveLetterPropArray);
        void QueryFileSystemTypes(out IntPtr fileSystemTypeProps, out uint numberOfFileSystems);
        void Reenumerate();
        void Refresh();
        void CleanupObsoleteMountPoints();
        void Advise([In] IVdsAdviseSink sink, out uint cookie);
        void Unadvise([In] uint cookie);
        void Reboot();
        void SetFlags([In] ServiceFlags flags);
        void ClearFlags([In] ServiceFlags flags);
    }
}

