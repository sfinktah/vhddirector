namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds.Advanced;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("9882f547-cfc3-420b-9750-00dfbec50662")]
    public interface IVdsCreatePartitionEx
    {
        void CreatePartitionEx([In] ulong offset, [In] ulong size, [In] uint align, [In] CreatePartitionParameters parameters, out IVdsAsync async);
    }
}

