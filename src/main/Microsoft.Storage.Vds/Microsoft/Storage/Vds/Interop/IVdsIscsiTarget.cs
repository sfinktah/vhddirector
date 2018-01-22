namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("aa8f5055-83e5-4bcc-aa73-19851a36a849")]
    public interface IVdsIscsiTarget
    {
        void GetProperties(out TargetProperties targetProp);
        void GetSubSystem(out IVdsSubSystem subSystem);
        void QueryPortalGroups(out IEnumVdsObject portalGroups);
        void QueryAssociatedLuns(out IEnumVdsObject luns);
        void CreatePortalGroup(out IVdsAsync async);
        void Delete(out IVdsAsync async);
        void SetFriendlyName([In, MarshalAs(UnmanagedType.LPWStr)] string friendlyName);
        void SetSharedSecret([In] ref SharedSecret targetSharedSecret, [In, MarshalAs(UnmanagedType.LPWStr)] string initiatorName);
        void RememberInitiatorSharedSecret([In, MarshalAs(UnmanagedType.LPWStr)] string initiatorName, [In] ref SharedSecret initiatorSharedSecret);
        void GetConnectedInitiators(out IntPtr initiators, out int numberOfInitiators);
    }
}

