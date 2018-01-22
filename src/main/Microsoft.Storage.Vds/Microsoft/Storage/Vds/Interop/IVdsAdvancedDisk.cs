namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds.Advanced;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("6e6f6b40-977c-4069-bddd-ac710059f8c0"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsAdvancedDisk
    {
        void GetPartitionProperties([In] ulong offset, out PartitionProperties partitionProp);
        void QueryPartitions(out IntPtr partitionPropArray, out int numberOfPartitions);
        void Slot3();
        void DeletePartition([In] ulong offset, [In] uint force, [In] uint forceProtected);
        void ChangeAttributes([In] ulong offset, [In] ChangeAttributesParameters parameters);
        void AssignDriveLetter([In] ulong offset, [In] char letter);
        void DeleteDriveLetter([In] ulong offset, [In] char letter);
        void GetDriveLetter([In] ulong offset, out char letter);
        void Slot9();
        void Clean([In] uint force, [In] uint forceOEM, [In] uint fullClean, out IVdsAsync async);
    }
}

