namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct SubSystemEndpoint
    {
        [FieldOffset(0)]
        public Guid ControllerPortId;
        [FieldOffset(0)]
        public Guid TargetPortalId;
    }
}

