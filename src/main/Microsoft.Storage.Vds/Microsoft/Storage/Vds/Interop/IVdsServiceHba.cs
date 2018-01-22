namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("0ac13689-3134-47c6-a17c-4669216801be")]
    public interface IVdsServiceHba
    {
        void QueryHbaPorts(out IEnumVdsObject ports);
    }
}

