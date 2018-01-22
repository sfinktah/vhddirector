namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("b07fedd4-1682-4440-9189-a39b55194dc5"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsIscsiInitiatorAdapter
    {
        void GetProperties(out InitiatorAdapterProperties initiatorAdapterProp);
        void QueryInitiatorPortals(out IEnumVdsObject initiatorPortals);
        void LoginToTarget([In] IscsiLoginType loginType, [In] Guid targetId, [In] Guid targetPortalId, [In] Guid initiatorPortalId, [In] IscsiLoginFlags loginFlags, [In] uint bHeaderDigest, [In] uint bDataDigest, [In] IscsiAuthorizationType authType, out IVdsAsync async);
        void LogoutFromTarget([In] Guid targetId, out IVdsAsync async);
    }
}

