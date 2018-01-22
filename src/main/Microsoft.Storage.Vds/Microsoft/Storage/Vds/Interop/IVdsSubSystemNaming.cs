namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("0d70faa3-9cd4-4900-aa20-6981b6aafc75"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsSubSystemNaming
    {
        void SetFriendlyName([In, MarshalAs(UnmanagedType.LPWStr)] string friendlyName);
    }
}

