namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Runtime.InteropServices;

    public class HardwareProvider : Provider, IDisposable
    {
        private IVdsHwProvider hwProvider;
        private IVdsHwProviderType hwProviderType;
        private IVdsHwProviderType2 hwProviderType2;

        public HardwareProvider()
        {
        }

        public HardwareProvider(object providerUnk, IVdsService vdsService) : base(providerUnk, vdsService)
        {
            this.InitializeComInterfaces();
        }

        public override void InitializeComInterfaces()
        {
            base.InitializeComInterfaces();
            if (this.hwProvider == null)
            {
                this.hwProvider = InteropHelpers.QueryInterface<IVdsHwProvider>(base.ComUnknown);
                if (this.hwProvider == null)
                {
                    throw new VdsException("QueryInterface for IVdsHwProvider failed.");
                }
            }
            if (this.hwProviderType2 == null)
            {
                this.hwProviderType2 = InteropHelpers.QueryInterface<IVdsHwProviderType2>(base.ComUnknown);
            }
            if ((this.hwProviderType2 == null) && (this.hwProviderType == null))
            {
                this.hwProviderType = InteropHelpers.QueryInterface<IVdsHwProviderType>(base.ComUnknown);
            }
        }

        public void Reenumerate()
        {
            this.InitializeComInterfaces();
            try
            {
                this.hwProvider.Reenumerate();
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsHwProvider::Reenumerate failed.", exception);
            }
        }

        public void Refresh()
        {
            base.Refresh();
            this.InitializeComInterfaces();
            try
            {
                this.hwProvider.Refresh();
            }
            catch (COMException exception)
            {
                throw new VdsException("Call to IVdsHwProvider::Refresh failed.", exception);
            }
        }

        public HardwareProviderType HardwareType
        {
            get
            {
                HardwareProviderType type;
                this.InitializeComInterfaces();
                if ((this.hwProviderType == null) && (this.hwProviderType2 == null))
                {
                    return HardwareProviderType.Unknown;
                }
                try
                {
                    if (this.hwProviderType2 != null)
                    {
                        this.hwProviderType2.GetProviderType2(out type);
                        return type;
                    }
                    this.hwProviderType.GetProviderType(out type);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsHwProviderType::GetProviderType failed.", exception);
                }
                return type;
            }
        }

        public Collection<SubSystem> SubSystems
        {
            get
            {
                Collection<SubSystem> collection;
                this.InitializeComInterfaces();
                try
                {
                    IEnumVdsObject obj2;
                    this.hwProvider.QuerySubSystems(out obj2);
                    collection = new Collection<SubSystem>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsHwProvider::QuerySubSystems failed.", exception);
                }
                return collection;
            }
        }
    }
}

