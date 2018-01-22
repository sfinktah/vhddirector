namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("8190236f-c4d0-4e81-8011-d69512fcc984"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsHwProviderType2
    {
        void GetProviderType2(out HardwareProviderType type);
    }
}

