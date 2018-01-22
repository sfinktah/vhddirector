namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("8326cd1d-cf59-4936-b786-5efc08798e25"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsAdviseSink
    {
        void OnNotify([In] uint numberOfNotifications, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=0)] VDS_NOTIFICATION[] notificationArray);
    }
}

