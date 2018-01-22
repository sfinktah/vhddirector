namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("10c5e575-7984-4e81-a56b-431f5f92ae42"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsProvider
    {
        void GetProperties(out ProviderProperties providerProp);
    }
}

