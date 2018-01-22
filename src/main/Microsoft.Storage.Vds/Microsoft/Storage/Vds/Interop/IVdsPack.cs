namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("3b69d7f5-9d94-4648-91ca-79939ba263bf"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsPack
    {
        void GetProperties(out PackProperties packProp);
        void GetProvider(out IVdsProvider provider);
        void QueryVolumes(out IEnumVdsObject volumes);
        void QueryDisks(out IEnumVdsObject disks);
        void CreateVolume([In] VolumeType type, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=2)] InputDisk[] inputDiskArray, [In] int numberOfDisks, [In] uint stripeSize, out IVdsAsync async);
        void AddDisk([In] Guid diskId, [In] PartitionStyle partitionStyle, [In, MarshalAs(UnmanagedType.U4)] uint asHotSpare);
        void slot7();
        void ReplaceDisk([In] Guid oldDiskId, [In] Guid newDiskId, out IVdsAsync async);
        void RemoveMissingDisk([In] Guid diskId);
        void Recover(out IVdsAsync async);
    }
}

