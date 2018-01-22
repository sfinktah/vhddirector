namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct VDS_NOTIFICATION
    {
        public VDS_NOTIFICATION_TARGET_TYPE ObjectType;
        public VDS_NOTIFICATION_INFO NotificationInfo;
    }
}

