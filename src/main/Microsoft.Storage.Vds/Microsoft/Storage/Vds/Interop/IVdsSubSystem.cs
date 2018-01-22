namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("6fcee2d3-6d90-4f91-80e2-a5c7caaca9d8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsSubSystem
    {
        void GetProperties(out SubSystemProperties subSystemProp);
        void GetProvider(out IVdsProvider provider);
        void QueryControllers(out IEnumVdsObject controllers);
        void QueryLuns(out IEnumVdsObject luns);
        void QueryDrives(out IEnumVdsObject drives);
        void GetDrive([In] ushort busNumber, [In] ushort slotNumber, out IVdsDrive drive);
        void Reenumerate();
        void Slot8();
        void CreateLun([In] LunType type, [In] ulong sizeInBytes, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=3)] Guid[] driveIdArray, [In] int numberOfDrives, [In, MarshalAs(UnmanagedType.LPWStr)] string unmaskingList, [In] ref Hints hints, out IVdsAsync async);
        void ReplaceDrive([In] Guid driveToBeReplaced, [In] Guid replacementDrive);
        void SetStatus([In] SubSystemStatus status);
        void QueryMaxLunCreateSize([In] LunType type, [In] Guid[] driveIdArray, [In] int numberOfDrives, [In] ref Hints hints, out ulong maxLunSize);
    }
}

