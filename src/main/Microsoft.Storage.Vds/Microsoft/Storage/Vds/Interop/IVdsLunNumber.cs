namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("d3f95e46-54b3-41f9-b678-0f1871443a08"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsLunNumber
    {
        void GetLunNumber(out uint lunNumber);
    }
}

