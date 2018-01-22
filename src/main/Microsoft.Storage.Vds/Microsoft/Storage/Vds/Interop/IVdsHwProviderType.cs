namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("3e0f5166-542d-4fc6-947a-012174240b7e")]
    public interface IVdsHwProviderType
    {
        void GetProviderType(out HardwareProviderType type);
    }
}

