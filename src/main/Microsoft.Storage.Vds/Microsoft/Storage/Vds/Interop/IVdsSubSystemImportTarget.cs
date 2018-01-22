namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("83bfb87f-43fb-4903-baa6-127f01029eec")]
    public interface IVdsSubSystemImportTarget
    {
        void GetImportTarget([MarshalAs(UnmanagedType.LPWStr)] out string targetName);
        void SetImportTarget([In, MarshalAs(UnmanagedType.LPWStr)] string targetName);
    }
}

