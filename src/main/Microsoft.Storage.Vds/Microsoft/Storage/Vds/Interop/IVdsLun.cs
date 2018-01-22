namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("3540a9c7-e60f-4111-a840-8bba6c2c83d8")]
    public interface IVdsLun
    {
        void GetProperties(out LunProperties lunProp);
        void GetSubSystem(out IVdsSubSystem subSystem);
        void GetIdentificationData(out VdsLunInformation lunInfo);
        void slot4();
        void Extend([In] ulong numberOfBytesToAdd, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=2)] Guid[] driveIdArray, [In] int numberOfDrives, out IVdsAsync async);
        void Shrink([In] ulong numberOfBytesToRemove, out IVdsAsync async);
        void QueryPlexes(out IEnumVdsObject plexes);
        void AddPlex([In] Guid lunId, out IVdsAsync async);
        void RemovePlex([In] Guid plexId, out IVdsAsync async);
        void Recover(out IVdsAsync async);
        void SetMask([In, MarshalAs(UnmanagedType.LPWStr)] string unmaskingList);
        void Delete();
        void slot13();
        void QueryHints(out Hints hints);
        void ApplyHints([In] ref Hints hints);
        void SetStatus([In] LunStatus status);
        void QueryMaxLunExtendSize([In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=1)] Guid[] driveIdArray, [In] int numberOfDrives, out ulong maxBytesToBeAdded);
    }
}

