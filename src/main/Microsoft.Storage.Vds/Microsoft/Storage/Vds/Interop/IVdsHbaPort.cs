namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("2abd757f-2851-4997-9a13-47d2a885d6ca"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsHbaPort
    {
        void GetProperties(out HbaPortProperties hbaPortProp);
        void SetAllPathStatuses([In] PathStatus status);
    }
}

