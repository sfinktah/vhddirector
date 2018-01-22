namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Runtime.InteropServices;

    public class SoftwareProvider : Provider, IDisposable
    {
        private IVdsSwProvider swProvider;

        public SoftwareProvider()
        {
        }

        public SoftwareProvider(object providerUnk, IVdsService vdsService) : base(providerUnk, vdsService)
        {
            this.InitializeComInterfaces();
        }

        public Pack CreatePack()
        {
            IVdsPack pack;
            this.InitializeComInterfaces();
            try
            {
                this.swProvider.CreatePack(out pack);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsSwProvider::CreatePack failed.", exception);
            }
            return new Pack(pack, base.VdsService);
        }

        public override void InitializeComInterfaces()
        {
            base.InitializeComInterfaces();
            if (this.swProvider == null)
            {
                this.swProvider = InteropHelpers.QueryInterface<IVdsSwProvider>(base.ComUnknown);
                if (this.swProvider == null)
                {
                    throw new VdsException("QueryInterface for IVdsSwProvider failed.");
                }
            }
        }

        public Collection<Pack> Packs
        {
            get
            {
                Collection<Pack> collection;
                this.InitializeComInterfaces();
                try
                {
                    IEnumVdsObject obj2;
                    this.swProvider.QueryPacks(out obj2);
                    collection = new Collection<Pack>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsSwProvider::QueryPacks failed.", exception);
                }
                return collection;
            }
        }
    }
}

