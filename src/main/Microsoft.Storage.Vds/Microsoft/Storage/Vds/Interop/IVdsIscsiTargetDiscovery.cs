namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("ff7a0ece-eaab-418b-886e-999de9d8dee3")]
    public interface IVdsIscsiTargetDiscovery
    {
        void AssignToInitiator();
        void UnassignFromInitiator();
    }
}

