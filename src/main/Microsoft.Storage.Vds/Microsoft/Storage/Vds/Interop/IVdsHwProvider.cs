namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("d99bdaae-b13a-4178-9fdb-e27f16b4603e")]
    public interface IVdsHwProvider
    {
        void QuerySubSystems(out IEnumVdsObject subSystems);
        void Reenumerate();
        void Refresh();
    }
}

