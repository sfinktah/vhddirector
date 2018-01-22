namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct ServerEndpoint
    {
        [FieldOffset(0)]
        public Guid HbaPortId;
        [FieldOffset(0)]
        public Guid InitiatorAdapterId;
    }
}

