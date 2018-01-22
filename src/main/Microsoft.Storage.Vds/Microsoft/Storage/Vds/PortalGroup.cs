namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class PortalGroup : Wrapper, IDisposable
    {
        private IVdsIscsiPortalGroup portalGroup;
        private PortalGroupProperties portalGroupProp;
        private bool refresh;

        public PortalGroup()
        {
            this.refresh = true;
            this.portalGroup = null;
        }

        public PortalGroup(object comUnknown, IVdsService vdsService) : base(comUnknown, vdsService)
        {
            this.refresh = true;
            this.InitializeComInterfaces();
        }

        public Async BeginAddPortal(Guid portalId, AsyncCallback callback, object state)
        {
            IVdsAsync async;
            this.InitializeComInterfaces();
            try
            {
                this.portalGroup.AddPortal(portalId, out async);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsIscsiPortalGroup::AddPortal failed.", exception);
            }
            Async async2 = new Async(async, callback, state);
            new Thread(new ThreadStart(async2.Wait)).Start();
            return async2;
        }

        public Async BeginDelete(AsyncCallback callback, object state)
        {
            IVdsAsync async;
            this.InitializeComInterfaces();
            try
            {
                this.portalGroup.Delete(out async);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsIscsiPortalGroup::Delete failed.", exception);
            }
            Async async2 = new Async(async, callback, state);
            new Thread(new ThreadStart(async2.Wait)).Start();
            return async2;
        }

        public Async BeginRemovePortal(Guid portalId, AsyncCallback callback, object state)
        {
            IVdsAsync async;
            this.InitializeComInterfaces();
            try
            {
                this.portalGroup.RemovePortal(portalId, out async);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsIscsiPortalGroup::RemovePortal failed.", exception);
            }
            Async async2 = new Async(async, callback, state);
            new Thread(new ThreadStart(async2.Wait)).Start();
            return async2;
        }

        public void EndAddPortal(IAsyncResult asyncResult)
        {
            try
            {
                uint num;
                VDS_ASYNC_OUTPUT vds_async_output;
                Async async = (Async) asyncResult;
                if (async == null)
                {
                    throw new ArgumentNullException("asyncResult");
                }
                InteropHelpers.QueryInterfaceThrow<IVdsAsync>(async.ComUnknown).Wait(out num, out vds_async_output);
                if (num != 0)
                {
                    throw new VdsException("Add portal failed with the following error code: " + num);
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsAsync::Wait failed.", exception);
            }
        }

        public void EndDelete(IAsyncResult asyncResult)
        {
            try
            {
                uint num;
                VDS_ASYNC_OUTPUT vds_async_output;
                Async async = (Async) asyncResult;
                if (async == null)
                {
                    throw new ArgumentNullException("asyncResult");
                }
                InteropHelpers.QueryInterfaceThrow<IVdsAsync>(async.ComUnknown).Wait(out num, out vds_async_output);
                if (num != 0)
                {
                    throw new VdsException("Delete portal group failed with the following error code: " + num);
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsAsync::Wait failed.", exception);
            }
        }

        public void EndRemovePortal(IAsyncResult asyncResult)
        {
            try
            {
                uint num;
                VDS_ASYNC_OUTPUT vds_async_output;
                Async async = (Async) asyncResult;
                if (async == null)
                {
                    throw new ArgumentNullException("asyncResult");
                }
                InteropHelpers.QueryInterfaceThrow<IVdsAsync>(async.ComUnknown).Wait(out num, out vds_async_output);
                if (num != 0)
                {
                    throw new VdsException("Remove portal failed with the following error code: " + num);
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsAsync::Wait failed.", exception);
            }
        }

        public override void InitializeComInterfaces()
        {
            base.InitializeComInterfaces();
            if (this.portalGroup == null)
            {
                try
                {
                    this.portalGroup = (IVdsIscsiPortalGroup) base.ComUnknown;
                }
                catch (InvalidCastException exception)
                {
                    throw new VdsException("QueryInterface for IVdsIscsiPortalGroup failed.", exception);
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
                    this.portalGroup.GetProperties(out this.portalGroupProp);
                    this.refresh = false;
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsIscsiPortalGroup::GetProperties failed.", exception);
            }
        }

        public void Refresh()
        {
            this.refresh = true;
        }

        public Collection<Portal> AssociatedPortals
        {
            get
            {
                Collection<Portal> collection;
                this.InitializeComInterfaces();
                try
                {
                    IEnumVdsObject obj2;
                    this.portalGroup.QueryAssociatedPortals(out obj2);
                    collection = new Collection<Portal>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsIscsiPortalGroup::QueryAssociatedPortals failed.", exception);
                }
                return collection;
            }
        }

        public Guid Id
        {
            get
            {
                this.InitializeProperties();
                return this.portalGroupProp.Id;
            }
        }

        public ushort Tag
        {
            get
            {
                this.InitializeProperties();
                return this.portalGroupProp.Tag;
            }
        }

        public Microsoft.Storage.Vds.Target Target
        {
            get
            {
                Microsoft.Storage.Vds.Target target2;
                this.InitializeComInterfaces();
                try
                {
                    IVdsIscsiTarget target;
                    this.portalGroup.GetTarget(out target);
                    target2 = new Microsoft.Storage.Vds.Target(target, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsIscsiPortalGroup::GetTarget failed.", exception);
                }
                return target2;
            }
        }
    }
}

