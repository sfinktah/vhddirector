namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("88306bb2-e71f-478c-86a2-79da200a0f11"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsVolume
    {
        void GetProperties(out VolumeProperties volProp);
        void GetPack(out IVdsPack pack);
        void QueryPlexes(out IEnumVdsObject plexes);
        void Extend([In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=1)] InputDisk[] inputDiskArray, [In] uint numberOfDisks, out IVdsAsync async);
        void Shrink([In] ulong numberOfBytesToRemove, out IVdsAsync async);
        void AddPlex([In] Guid volumeId, out IVdsAsync async);
        void BreakPlex([In] Guid plexId, out IVdsAsync async);
        void RemovePlex([In] Guid plexId, out IVdsAsync async);
        void Delete([In] uint bForce);
        void SetFlags([In] VolumeFlags flags, [In] uint revertOnClose);
        void ClearFlags([In] VolumeFlags flags);
    }
}

