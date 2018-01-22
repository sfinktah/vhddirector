namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("0027346f-40d0-4b45-8cec-5906dc0380c8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsSubSystemIscsi
    {
        void QueryTargets(out IEnumVdsObject targets);
        void QueryPortals(out IEnumVdsObject portals);
        void CreateTarget([In, MarshalAs(UnmanagedType.LPWStr)] string iscsiName, [In, MarshalAs(UnmanagedType.LPWStr)] string friendlyName, out IVdsAsync async);
        void SetIpsecGroupPresharedKey([In] ref IpsecKey ipsecKey);
    }
}

