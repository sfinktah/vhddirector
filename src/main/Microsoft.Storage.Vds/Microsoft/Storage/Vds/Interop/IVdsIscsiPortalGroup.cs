namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("fef5f89d-a3dd-4b36-bf28-e7dde045c593")]
    public interface IVdsIscsiPortalGroup
    {
        void GetProperties(out PortalGroupProperties portalGroupProp);
        void GetTarget(out IVdsIscsiTarget target);
        void QueryAssociatedPortals(out IEnumVdsObject portals);
        void AddPortal([In] Guid portalId, out IVdsAsync async);
        void RemovePortal([In] Guid portalId, out IVdsAsync async);
        void Delete(out IVdsAsync async);
    }
}

