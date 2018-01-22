namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("ca5d735f-6bae-42c0-b30e-f2666045ce71")]
    public interface IVdsControllerControllerPort
    {
        void QueryControllerPorts(out IEnumVdsObject ports);
    }
}

