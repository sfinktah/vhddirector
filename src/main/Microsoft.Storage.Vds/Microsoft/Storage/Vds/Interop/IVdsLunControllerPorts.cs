namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("451fe266-da6d-406a-bb60-82e534f85aeb"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsLunControllerPorts
    {
        void AssociateControllerPorts([In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=1)] Guid[] activeControllerPortIdArray, [In] int numberOfActiveControllerPorts, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=3)] Guid[] inactiveControllerPortIdArray, [In] int numberOfInactiveControllerPorts);
        void QueryActiveControllerPorts(out IEnumVdsObject controllerPorts);
    }
}

