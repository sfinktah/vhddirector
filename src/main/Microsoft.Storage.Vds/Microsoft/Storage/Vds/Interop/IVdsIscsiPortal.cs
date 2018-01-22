namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("7fa1499d-ec85-4a8a-a47b-ff69201fcd34"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsIscsiPortal
    {
        void GetProperties(out PortalProperties portalProp);
        void GetSubSystem(out IVdsSubSystem subSystem);
        void QueryAssociatedPortalGroups(out IEnumVdsObject portalGroups);
        void SetStatus([In] PortalStatus status);
        void SetIpsecTunnelAddress([In] ref IPAddress tunnelAddress, [In] ref IPAddress destinationAddress);
        void GetIpsecSecurity([In] ref IPAddress initiatorPortalAddress, out IpsecFlags securityFlags);
        void SetIpsecSecurity([In] ref IPAddress initiatorPortalAddress, [In] IpsecFlags securityFlags, [In] ref IpsecKey ipsecKey);
    }
}

