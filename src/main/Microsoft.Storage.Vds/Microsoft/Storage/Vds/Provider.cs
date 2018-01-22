namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Runtime.InteropServices;

    public class Provider : Wrapper, IDisposable
    {
        private IVdsProvider provider;
        private ProviderProperties providerProp;
        private IVdsProviderSupport providerSupport;
        private bool refresh;

        public Provider()
        {
            this.refresh = true;
            this.provider = null;
        }

        public Provider(object providerUnk, IVdsService vdsService) : base(providerUnk, vdsService)
        {
            this.refresh = true;
            this.InitializeComInterfaces();
        }

        public override void InitializeComInterfaces()
        {
            base.InitializeComInterfaces();
            if (this.provider == null)
            {
                this.provider = InteropHelpers.QueryInterface<IVdsProvider>(base.ComUnknown);
                if (this.provider == null)
                {
                    throw new VdsException("QueryInterface for IVdsProvider failed.");
                }
            }
            if (this.providerSupport == null)
            {
                this.providerSupport = InteropHelpers.QueryInterface<IVdsProviderSupport>(base.ComUnknown);
            }
        }

        public override void InitializeProperties()
        {
            try
            {
                this.InitializeComInterfaces();
                if (this.refresh)
                {
                    this.provider.GetProperties(out this.providerProp);
                    this.refresh = false;
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsProvider::GetProperties failed.", exception);
            }
        }

        public void Refresh()
        {
            this.refresh = true;
        }

        public ProviderFlags Flags
        {
            get
            {
                this.InitializeProperties();
                return this.providerProp.Flags;
            }
        }

        public Guid Id
        {
            get
            {
                this.InitializeProperties();
                return this.providerProp.Id;
            }
        }

        public string Name
        {
            get
            {
                this.InitializeProperties();
                return this.providerProp.Name;
            }
        }

        public short RebuildPriority
        {
            get
            {
                this.InitializeProperties();
                return this.providerProp.RebuildPriority;
            }
        }

        public uint StripeSizeFlags
        {
            get
            {
                this.InitializeProperties();
                return this.providerProp.StripeSizeFlags;
            }
        }

        public ProviderType Type
        {
            get
            {
                this.InitializeProperties();
                return this.providerProp.Type;
            }
        }

        public string Version
        {
            get
            {
                this.InitializeProperties();
                return this.providerProp.Version;
            }
        }

        public Guid VersionId
        {
            get
            {
                this.InitializeProperties();
                return this.providerProp.VersionId;
            }
        }

        public VersionSupportFlags VersionSupport
        {
            get
            {
                VersionSupportFlags flags;
                this.InitializeComInterfaces();
                try
                {
                    this.providerSupport.GetVersionSupport(out flags);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsProviderSupport::GetVersionSupport failed.", exception);
                }
                return flags;
            }
        }
    }
}

