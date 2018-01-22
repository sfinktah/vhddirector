namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("14fbe036-3ed7-4e10-90e9-a5ff991aff01"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsServiceIscsi
    {
        void GetInitiatorName([MarshalAs(UnmanagedType.LPWStr)] out string iscsiName);
        void QueryInitiatorAdapters(out IEnumVdsObject initiatorAdapters);
        void SetIpsecGroupPresharedKey([In] ref IpsecKey ipsecKey);
        void SetAllIpsecTunnelAddresses([In] ref IPAddress tunnelAddress, [In] ref IPAddress destinationAddress);
        void SetAllIpsecSecurity([In] Guid targetPortalId, [In] IpsecFlags securityFlags, [In] ref IpsecKey ipsecKey);
        void SetInitiatorSharedSecret([In] ref SharedSecret initiatorSharedSecret, [In] Guid targetId);
        void RememberTargetSharedSecret([In] Guid targetId, [In] ref SharedSecret targetSharedSecret);
    }
}

