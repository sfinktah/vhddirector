namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public class Portal : Wrapper, IDisposable
    {
        private IVdsIscsiPortal portal;
        private IVdsIscsiPortalLocal portalLocal;
        private PortalProperties portalProp;
        private bool refresh;

        public Portal()
        {
            this.refresh = true;
        }

        public Portal(object comUnknown, IVdsService vdsService) : base(comUnknown, vdsService)
        {
            this.refresh = true;
            this.InitializeComInterfaces();
        }

        public IpsecFlags GetIpsecSecurity(IPAddress initiatorPortalAddress)
        {
            IpsecFlags flags;
            this.InitializeComInterfaces();
            try
            {
                this.portal.GetIpsecSecurity(ref initiatorPortalAddress, out flags);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsIscsiPortal::GetIpsecSecurity failed.", exception);
            }
            return flags;
        }

        public override void InitializeComInterfaces()
        {
            base.InitializeComInterfaces();
            if (this.portal == null)
            {
                this.portal = InteropHelpers.QueryInterface<IVdsIscsiPortal>(base.ComUnknown);
                if (this.portal == null)
                {
                    throw new VdsException("QueryInterface for IVdsIscsiPortal failed.");
                }
            }
            if (this.portalLocal == null)
            {
                this.portalLocal = InteropHelpers.QueryInterface<IVdsIscsiPortalLocal>(base.ComUnknown);
            }
        }

        public override void InitializeProperties()
        {
            this.InitializeComInterfaces();
            try
            {
                if (this.refresh)
                {
                    this.portal.GetProperties(out this.portalProp);
                    this.refresh = false;
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsIscsiPortal::GetProperties failed.", exception);
            }
        }

        public void Refresh()
        {
            this.refresh = true;
        }

        public void SetIpsecSecurity(IPAddress initiatorPortalAddress, IpsecFlags securityFlags, SecureString ipsecKey)
        {
            if (ipsecKey == null)
            {
                throw new ArgumentNullException("ipsecKey", "ipsecKey cannot be set to null");
            }
            this.InitializeComInterfaces();
            SecureStringAnsiHandle handle = new SecureStringAnsiHandle(ipsecKey);
            try
            {
                IpsecKey key = new IpsecKey(handle);
                this.portal.SetIpsecSecurity(ref initiatorPortalAddress, securityFlags, ref key);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsIscsiPortal::SetIpsecSecurity failed.", exception);
            }
            finally
            {
                if (handle != null)
                {
                    handle.Dispose();
                }
            }
        }

        public void SetIpsecSecurityLocal(IpsecFlags securityFlags, SecureString ipsecKey)
        {
            if (ipsecKey == null)
            {
                throw new ArgumentNullException("ipsecKey", "ipsecKey cannot be set to null");
            }
            this.InitializeComInterfaces();
            if (this.portalLocal == null)
            {
                throw new NotSupportedException("The portal is not a local portal");
            }
            SecureStringAnsiHandle handle = new SecureStringAnsiHandle(ipsecKey);
            try
            {
                IpsecKey key = new IpsecKey(handle);
                this.portalLocal.SetIpsecSecurityLocal(securityFlags, ref key);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsIscsiPortalLocal::SetIpsecSecurityLocal failed.", exception);
            }
            finally
            {
                if (handle != null)
                {
                    handle.Dispose();
                }
            }
        }

        public void SetIpsecTunnelAddress(IPAddress tunnelAddress, IPAddress destinationAddress)
        {
            this.InitializeComInterfaces();
            try
            {
                this.portal.SetIpsecTunnelAddress(ref tunnelAddress, ref destinationAddress);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsIscsiPortal::SetIpsecTunnelAddress failed.", exception);
            }
        }

        public IPAddress Address
        {
            get
            {
                this.InitializeProperties();
                return this.portalProp.Address;
            }
        }

        public Collection<PortalGroup> AssociatedPortalGroups
        {
            get
            {
                Collection<PortalGroup> collection;
                this.InitializeComInterfaces();
                try
                {
                    IEnumVdsObject obj2;
                    this.portal.QueryAssociatedPortalGroups(out obj2);
                    collection = new Collection<PortalGroup>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsIscsiPortal::QueryAssociatedPortalGroups failed.", exception);
                }
                return collection;
            }
        }

        public Guid Id
        {
            get
            {
                this.InitializeProperties();
                return this.portalProp.Id;
            }
        }

        public PortalStatus Status
        {
            get
            {
                this.InitializeProperties();
                return this.portalProp.Status;
            }
            set
            {
                this.InitializeComInterfaces();
                try
                {
                    this.portal.SetStatus(value);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsIscsiPortal::SetStatus failed.", exception);
                }
            }
        }

        public Microsoft.Storage.Vds.SubSystem SubSystem
        {
            get
            {
                Microsoft.Storage.Vds.SubSystem system2;
                this.InitializeComInterfaces();
                try
                {
                    IVdsSubSystem system;
                    this.portal.GetSubSystem(out system);
                    system2 = new Microsoft.Storage.Vds.SubSystem(system, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsIscsiPortal::GetSubsystem failed.", exception);
                }
                return system2;
            }
        }
    }
}

