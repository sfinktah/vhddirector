namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct VDS_PORT_NOTIFICATION
    {
        public VDS_NOTIFICATION_EVENT Event;
        public Guid PortId;
    }
}

