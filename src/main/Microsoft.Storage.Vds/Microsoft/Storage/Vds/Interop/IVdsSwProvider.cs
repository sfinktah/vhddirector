namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("9aa58360-ce33-4f92-b658-ed24b14425b8")]
    public interface IVdsSwProvider
    {
        void QueryPacks(out IEnumVdsObject packs);
        void CreatePack(out IVdsPack pack);
    }
}

