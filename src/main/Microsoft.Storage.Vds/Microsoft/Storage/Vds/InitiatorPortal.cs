namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public class InitiatorPortal : Wrapper, IDisposable
    {
        private IVdsIscsiInitiatorPortal initiatorPortal;
        private InitiatorPortalProperties initiatorPortalProp;
        private bool refresh;

        public InitiatorPortal()
        {
            this.refresh = true;
        }

        public InitiatorPortal(object comUnknown, IVdsService vdsService) : base(comUnknown, vdsService)
        {
            this.refresh = true;
            this.InitializeComInterfaces();
        }

        public IpsecFlags GetIpsecSecurity(Guid targetPortalId)
        {
            IpsecFlags flags;
            this.InitializeComInterfaces();
            try
            {
                this.initiatorPortal.GetIpsecSecurity(targetPortalId, out flags);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsIscsiInitiatorPortal::GetIpsecSecurity failed.", exception);
            }
            return flags;
        }

        public override void InitializeComInterfaces()
        {
            base.InitializeComInterfaces();
            if (this.initiatorPortal == null)
            {
                this.initiatorPortal = InteropHelpers.QueryInterface<IVdsIscsiInitiatorPortal>(base.ComUnknown);
                if (this.initiatorPortal == null)
                {
                    throw new VdsException("QueryInterface for IVdsIscsiInitiatorPortal failed.");
                }
            }
        }

        public override void InitializeProperties()
        {
            this.InitializeComInterfaces();
            try
            {
                if (this.refresh)
                {
                    this.initiatorPortal.GetProperties(out this.initiatorPortalProp);
                    this.refresh = false;
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsIscsiInitiatorPortal::GetProperties failed.", exception);
            }
        }

        public void Refresh()
        {
            this.refresh = true;
        }

        public void SetIpsecSecurity(Guid targetPortalId, IpsecFlags securityFlags, SecureString ipsecKey)
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
                this.initiatorPortal.SetIpsecSecurity(targetPortalId, securityFlags, ref key);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsIscsiInitiatorPortal::SetIpsecSecurity failed.", exception);
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
                this.initiatorPortal.SetIpsecTunnelAddress(ref tunnelAddress, ref destinationAddress);
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
                return this.initiatorPortalProp.Address;
            }
        }

        public Guid Id
        {
            get
            {
                this.InitializeProperties();
                return this.initiatorPortalProp.Id;
            }
        }

        public Microsoft.Storage.Vds.InitiatorAdapter InitiatorAdapter
        {
            get
            {
                Microsoft.Storage.Vds.InitiatorAdapter adapter;
                this.InitializeComInterfaces();
                try
                {
                    IVdsIscsiInitiatorAdapter adapter2;
                    this.initiatorPortal.GetInitiatorAdapter(out adapter2);
                    adapter = new Microsoft.Storage.Vds.InitiatorAdapter(adapter2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsIscsiInitiatorPortal::GetInitiatorAdapter failed.", exception);
                }
                return adapter;
            }
        }

        public uint PortIndex
        {
            get
            {
                this.InitializeProperties();
                return this.initiatorPortalProp.PortIndex;
            }
        }
    }
}

