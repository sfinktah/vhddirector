namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("ad837c28-52c1-421d-bf04-fae7da665396"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsIscsiPortalLocal
    {
        void SetIpsecSecurityLocal([In] IpsecFlags securityFlags, [In] ref IpsecKey ipsecKey);
    }
}

