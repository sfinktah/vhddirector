namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("be666735-7800-4a77-9d9c-40f85b87e292")]
    public interface IVdsSubSystem2
    {
        void GetProperties2(out SubSystemProperties2 properties);
        void GetDrive2([In] short busNumber, [In] short slotNumber, [In] uint enclosureNumber, out IVdsDrive drive);
        void CreateLun2([In] LunType type, [In] ulong sizeInBytes, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=3)] Guid[] driveIdArray, [In] int numberOfDrives, [In, MarshalAs(UnmanagedType.LPWStr)] string unmaskingList, [In] ref Hints2 hints2, out IVdsAsync async);
        void QueryMaxLunCreateSize2([In] LunType type, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=3)] Guid[] driveIdArray, [In] int numberOfDrives, [In] ref Hints2 hints2, out ulong maxLunSize);
    }
}

