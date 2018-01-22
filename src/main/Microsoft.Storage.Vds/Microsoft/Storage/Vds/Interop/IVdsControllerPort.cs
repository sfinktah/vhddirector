namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("18691d0d-4e7f-43e8-92e4-cf44beeed11c"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsControllerPort
    {
        void GetProperties(out PortProperties portProp);
        void GetController(out IVdsController controller);
        void QueryAssociatedLuns(out IEnumVdsObject luns);
        void Reset();
        void SetStatus([In] PortStatus status);
    }
}

