namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("907504cb-6b4e-4d88-a34d-17ba661fbb06")]
    public interface IVdsLunNaming
    {
        void SetFriendlyName([In, MarshalAs(UnmanagedType.LPWStr)] string friendlyName);
    }
}

