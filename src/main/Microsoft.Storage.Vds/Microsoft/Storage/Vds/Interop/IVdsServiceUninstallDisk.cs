namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("B6B22DA8-F903-4be7-B492-C09D875AC9DA")]
    public interface IVdsServiceUninstallDisk
    {
        void GetDiskIdFromLunInfo([In] VdsLunInformation lunInfo, out Guid diskId);
        [PreserveSig]
        int UninstallDisks([In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=1)] Guid[] diskIdArray, [In] uint count, [In] uint force, out uint reboot, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=1)] int[] hresults);
    }
}

