namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class InitiatorAdapter : Wrapper, IDisposable
    {
        private IVdsIscsiInitiatorAdapter initiatorAdapter;
        private InitiatorAdapterProperties initiatorAdapterProp;
        private bool refresh;

        public InitiatorAdapter()
        {
            this.refresh = true;
        }

        public InitiatorAdapter(object comUnknown, IVdsService vdsService) : base(comUnknown, vdsService)
        {
            this.refresh = true;
            this.InitializeComInterfaces();
        }

        public Async BeginLoginToTarget(IscsiLoginType loginType, Guid targetId, Guid targetPortalId, Guid initiatorPortalId, IscsiLoginFlags loginFlags, bool headerDigest, bool dataDigest, IscsiAuthorizationType authorizationType, AsyncCallback callback, object state)
        {
            IVdsAsync async;
            this.InitializeComInterfaces();
            try
            {
                this.initiatorAdapter.LoginToTarget(loginType, targetId, targetPortalId, initiatorPortalId, loginFlags, headerDigest ? 1U : 0U, dataDigest ? 1U : 0U, authorizationType, out async);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsIscsiInitiatorAdapter::LoginToTarget failed.", exception);
            }
            Async async2 = new Async(async, callback, state);
            new Thread(new ThreadStart(async2.Wait)).Start();
            return async2;
        }

        public Async BeginLogoutFromTarget(Guid targetId, AsyncCallback callback, object state)
        {
            IVdsAsync async;
            this.InitializeComInterfaces();
            try
            {
                this.initiatorAdapter.LogoutFromTarget(targetId, out async);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsIscsiInitiatorAdapter::LogoutFromTarget failed.", exception);
            }
            Async async2 = new Async(async, callback, state);
            new Thread(new ThreadStart(async2.Wait)).Start();
            return async2;
        }

        public void EndLoginToTarget(IAsyncResult asyncResult)
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
                    throw new VdsException("Login to target failed with the following error code: " + num);
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsAsync::Wait failed.", exception);
            }
        }

        public void EndLogoutFromTarget(IAsyncResult asyncResult)
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
                    throw new VdsException("Logout from target failed with the following error code: " + num);
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
            if (this.initiatorAdapter == null)
            {
                this.initiatorAdapter = InteropHelpers.QueryInterface<IVdsIscsiInitiatorAdapter>(base.ComUnknown);
                if (this.initiatorAdapter == null)
                {
                    throw new VdsException("QueryInterface for IVdsIscsiInitiatorAdapter failed.");
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
                    this.initiatorAdapter.GetProperties(out this.initiatorAdapterProp);
                    this.refresh = false;
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsIscsiInitiatorAdpater::GetProperties failed.", exception);
            }
        }

        public void Refresh()
        {
            this.refresh = true;
        }

        public Guid Id
        {
            get
            {
                this.InitializeProperties();
                return this.initiatorAdapterProp.Id;
            }
        }

        public Collection<InitiatorPortal> InitiatorPortals
        {
            get
            {
                Collection<InitiatorPortal> collection;
                this.InitializeComInterfaces();
                try
                {
                    IEnumVdsObject obj2;
                    this.initiatorAdapter.QueryInitiatorPortals(out obj2);
                    collection = new Collection<InitiatorPortal>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsIscsiInitiatorAdapter::QueryInitiatorPortals failed.", exception);
                }
                return collection;
            }
        }

        public string Name
        {
            get
            {
                this.InitializeProperties();
                return this.initiatorAdapterProp.Name;
            }
        }
    }
}

