namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("38a0a9ab-7cc8-4693-ac07-1f28bd03c3da"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsIscsiInitiatorPortal
    {
        void GetProperties(out InitiatorPortalProperties initiatorPortalProp);
        void GetInitiatorAdapter(out IVdsIscsiInitiatorAdapter initiatorAdapter);
        void SetIpsecTunnelAddress([In] ref IPAddress tunnelAddress, [In] ref IPAddress destinationAddress);
        void GetIpsecSecurity([In] Guid targetPortalId, out IpsecFlags securityFlags);
        void SetIpsecSecurity([In] Guid targetPortalId, [In] IpsecFlags securityFlags, [In] ref IpsecKey ipsecKey);
    }
}

