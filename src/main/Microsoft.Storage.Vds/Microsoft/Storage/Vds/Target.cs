namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Threading;

    public class Target : Wrapper, IDisposable
    {
        private bool refresh;
        private IVdsIscsiTarget target;
        private TargetProperties targetProp;

        public Target()
        {
            this.refresh = true;
        }

        public Target(object comUnknown, IVdsService vdsService) : base(comUnknown, vdsService)
        {
            this.refresh = true;
            this.InitializeComInterfaces();
        }

        public Async BeginCreatePortalGroup(AsyncCallback callback, object state)
        {
            IVdsAsync async;
            this.InitializeComInterfaces();
            try
            {
                this.target.CreatePortalGroup(out async);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsIscsiTarget::CreatePortalGroup failed.", exception);
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
                this.target.Delete(out async);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsIscsiTarget::Delete failed.", exception);
            }
            Async async2 = new Async(async, callback, state);
            new Thread(new ThreadStart(async2.Wait)).Start();
            return async2;
        }

        public PortalGroup EndCreatePortalGroup(IAsyncResult asyncResult)
        {
            IVdsIscsiPortalGroup objectForIUnknown;
            this.InitializeComInterfaces();
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
                    throw new VdsException("Create portal group failed with the following error code: " + num);
                }
                try
                {
                    objectForIUnknown = (IVdsIscsiPortalGroup) Marshal.GetObjectForIUnknown(vds_async_output.Info.Cpg.PortalGroupUnknown);
                }
                finally
                {
                    Marshal.Release(vds_async_output.Info.Cpg.PortalGroupUnknown);
                }
                objectForIUnknown = (IVdsIscsiPortalGroup) Marshal.GetObjectForIUnknown(vds_async_output.Info.Cpg.PortalGroupUnknown);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsAsync::Wait failed.", exception);
            }
            catch (InvalidCastException exception2)
            {
                throw new VdsException("QueryInterface for IVdsIscsiPortalGroup failed.", exception2);
            }
            return new PortalGroup(objectForIUnknown, base.VdsService);
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
                    throw new VdsException("Delete target failed with the following error code: " + num);
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
            if (this.target == null)
            {
                try
                {
                    this.target = (IVdsIscsiTarget) base.ComUnknown;
                }
                catch (InvalidCastException exception)
                {
                    throw new VdsException("QueryInterface for IVdsIscsiTarget failed.", exception);
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
                    this.target.GetProperties(out this.targetProp);
                    this.refresh = false;
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsIscsiTarget::GetProperties failed.", exception);
            }
        }

        public void Refresh()
        {
            this.refresh = true;
        }

        public void RememberInitiatorSharedSecret(string initiatorName, SecureString initiatorSharedSecret)
        {
            if (initiatorSharedSecret == null)
            {
                throw new ArgumentNullException("initiatorSharedSecret", "Cannot pass in null for shared secret");
            }
            this.InitializeComInterfaces();
            SecureStringAnsiHandle sharedSecret = new SecureStringAnsiHandle(initiatorSharedSecret);
            try
            {
                SharedSecret secret = new SharedSecret(sharedSecret);
                this.target.RememberInitiatorSharedSecret(initiatorName, ref secret);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsIscsiTarget::RememberInitiatorSharedSecret failed.", exception);
            }
            finally
            {
                if (sharedSecret != null)
                {
                    sharedSecret.Dispose();
                }
            }
        }

        public void SetSharedSecret(SecureString targetSharedSecret, string initiatorName)
        {
            if (targetSharedSecret == null)
            {
                throw new ArgumentNullException("targetSharedSecret", "Cannot pass in null for shared secret");
            }
            this.InitializeComInterfaces();
            SecureStringAnsiHandle sharedSecret = new SecureStringAnsiHandle(targetSharedSecret);
            try
            {
                SharedSecret secret = new SharedSecret(sharedSecret);
                this.target.SetSharedSecret(ref secret, initiatorName);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsIscsiTarget::SetSharedSecret failed.", exception);
            }
            finally
            {
                if (sharedSecret != null)
                {
                    sharedSecret.Dispose();
                }
            }
        }

        public Collection<Lun> AssociatedLuns
        {
            get
            {
                Collection<Lun> collection;
                this.InitializeComInterfaces();
                try
                {
                    IEnumVdsObject obj2;
                    this.target.QueryAssociatedLuns(out obj2);
                    collection = new Collection<Lun>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsIscsiTarget::QueryAssociatedLuns failed.", exception);
                }
                return collection;
            }
        }

        public bool ChapEnabled
        {
            get
            {
                this.InitializeProperties();
                return (this.targetProp.ChapEnabled != 0);
            }
        }

        public List<string> ConnectedInitiators
        {
            get
            {
                IntPtr ptr2;
                int num;
                int num2 = Marshal.SizeOf(typeof(IntPtr));
                this.InitializeComInterfaces();
                try
                {
                    this.target.GetConnectedInitiators(out ptr2, out num);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsIscsiTarget::GetConnectedIniators failed", exception);
                }
                List<string> list = new List<string>(num);
                IntPtr ptr = ptr2;
                for (int i = 0; i < num; i++)
                {
                    IntPtr ptr3 = Marshal.ReadIntPtr(ptr);
                    list.Add(Marshal.PtrToStringAuto(ptr3));
                    Marshal.FreeCoTaskMem(ptr3);
                    ptr = Utilities.IntPtrAddOffset(ptr, (uint) num2);
                }
                Marshal.FreeCoTaskMem(ptr2);
                return list;
            }
        }

        public string FriendlyName
        {
            get
            {
                this.InitializeProperties();
                return this.targetProp.FriendlyName;
            }
            set
            {
                this.InitializeComInterfaces();
                try
                {
                    this.target.SetFriendlyName(value);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsIscsiTarget::SetFriendlyName failed.", exception);
                }
            }
        }

        public Guid Id
        {
            get
            {
                this.InitializeProperties();
                return this.targetProp.Id;
            }
        }

        public string IscsiName
        {
            get
            {
                this.InitializeProperties();
                return this.targetProp.IscsiName;
            }
        }

        public Collection<PortalGroup> PortalGroups
        {
            get
            {
                Collection<PortalGroup> collection;
                this.InitializeComInterfaces();
                try
                {
                    IEnumVdsObject obj2;
                    this.target.QueryPortalGroups(out obj2);
                    collection = new Collection<PortalGroup>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsIscsiTarget::QueryPortalGroups failed.", exception);
                }
                return collection;
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
                    this.target.GetSubSystem(out system);
                    system2 = new Microsoft.Storage.Vds.SubSystem(system, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsIscsiTarget::GetSubsystem failed.", exception);
                }
                return system2;
            }
        }
    }
}

