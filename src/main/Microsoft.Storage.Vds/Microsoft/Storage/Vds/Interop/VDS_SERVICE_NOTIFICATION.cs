namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct VDS_SERVICE_NOTIFICATION
    {
        public VDS_NOTIFICATION_EVENT Event;
        public ServiceRecoverAction Action;
    }
}

