namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("cb53d96e-dffb-474a-a078-790d1e2bc082"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsController
    {
        void GetProperties(out ControllerProperties controllerProp);
        void GetSubSystem(out IVdsSubSystem subSystem);
        void Slot3();
        void FlushCache();
        void InvalidateCache();
        void Reset();
        void Slot7();
        void SetStatus([In] ControllerStatus status);
    }
}

