namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class Pack : Wrapper, IDisposable
    {
        private IVdsPack pack;
        private PackProperties packProp;
        private bool refresh;

        public Pack()
        {
            this.refresh = true;
            this.pack = null;
        }

        public Pack(object lunUnknown, IVdsService vdsService) : base(lunUnknown, vdsService)
        {
            this.refresh = true;
            this.InitializeComInterfaces();
        }

        public void AddDisk(Guid diskId, PartitionStyle partitionStyle, bool isHotSpare)
        {
            try
            {
                this.pack.AddDisk(diskId, partitionStyle, isHotSpare ? 1U : 0U);
            }
            catch (COMException exception)
            {
                throw new VdsException("The method call IVdsPack::AddDisk failed.", exception);
            }
        }

        public Async BeginCreateVolume(VolumeType type, InputDisk[] inputDiskArray, uint stripeSize, AsyncCallback callback, object state)
        {
            IVdsAsync async;
            this.InitializeComInterfaces();
            try
            {
                this.pack.CreateVolume(type, inputDiskArray, inputDiskArray.Length, stripeSize, out async);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsPack::CreateVolume failed.", exception);
            }
            Async async2 = new Async(async, callback, state);
            new Thread(new ThreadStart(async2.Wait)).Start();
            return async2;
        }

        public Async BeginRecover(AsyncCallback callback, object state)
        {
            IVdsAsync async;
            this.InitializeComInterfaces();
            try
            {
                this.pack.Recover(out async);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsPack::Recover failed.", exception);
            }
            Async async2 = new Async(async, callback, state);
            new Thread(new ThreadStart(async2.Wait)).Start();
            return async2;
        }

        public Async BeginReplaceDisk(Guid oldDiskId, Guid newDiskId, AsyncCallback callback, object state)
        {
            IVdsAsync async;
            this.InitializeComInterfaces();
            try
            {
                this.pack.ReplaceDisk(oldDiskId, newDiskId, out async);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsPack::ReplaceDisk failed.", exception);
            }
            Async async2 = new Async(async, callback, state);
            new Thread(new ThreadStart(async2.Wait)).Start();
            return async2;
        }

        public Volume EndCreateVolume(IAsyncResult asyncResult)
        {
            IVdsVolume objectForIUnknown;
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
                    throw new VdsException("Create volume failed with the following error code: " + num);
                }
                objectForIUnknown = (IVdsVolume) Marshal.GetObjectForIUnknown(vds_async_output.Info.Cv.VolumeUnknown);
                try
                {
                    objectForIUnknown = (IVdsVolume) Marshal.GetObjectForIUnknown(vds_async_output.Info.Cv.VolumeUnknown);
                }
                finally
                {
                    Marshal.Release(vds_async_output.Info.Cv.VolumeUnknown);
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsAsync::Wait failed.", exception);
            }
            catch (InvalidCastException exception2)
            {
                throw new VdsException("QueryInterface for IVdsVolume failed.", exception2);
            }
            return new Volume(objectForIUnknown, base.VdsService);
        }

        public void EndRecover(IAsyncResult asyncResult)
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
                    throw new VdsException("Recover pack failed with the following error code: " + num);
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsAsync::Wait failed.", exception);
            }
        }

        public void EndReplaceDisk(IAsyncResult asyncResult)
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
                    throw new VdsException("Replace disk failed with the following error code: " + num);
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
            try
            {
                if (this.pack == null)
                {
                    this.pack = (IVdsPack) base.ComUnknown;
                }
            }
            catch (InvalidCastException exception)
            {
                throw new VdsException("QueryInterface for IVdsPack failed.", exception);
            }
        }

        public override void InitializeProperties()
        {
            this.InitializeComInterfaces();
            try
            {
                if (this.refresh)
                {
                    this.pack.GetProperties(out this.packProp);
                    this.refresh = false;
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsPack::GetProperties failed.", exception);
            }
        }

        public void Refresh()
        {
            this.refresh = true;
        }

        public void RemoveMissingDisk(Guid diskId)
        {
            this.InitializeComInterfaces();
            try
            {
                this.pack.RemoveMissingDisk(diskId);
            }
            catch (COMException exception)
            {
                throw new VdsException("The method call IVdsPack::RemoveMissingDisk failed.", exception);
            }
        }

        public Collection<Disk> Disks
        {
            get
            {
                Collection<Disk> collection;
                try
                {
                    IEnumVdsObject obj2;
                    this.pack.QueryDisks(out obj2);
                    collection = new Collection<Disk>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsPack::QueryDisks failed.", exception);
                }
                return collection;
            }
        }

        public PackFlags Flags
        {
            get
            {
                this.InitializeProperties();
                return this.packProp.Flags;
            }
        }

        public Guid Id
        {
            get
            {
                this.InitializeProperties();
                return this.packProp.Id;
            }
        }

        public string Name
        {
            get
            {
                this.InitializeProperties();
                return this.packProp.Name;
            }
        }

        public SoftwareProvider Provider
        {
            get
            {
                SoftwareProvider provider;
                this.InitializeComInterfaces();
                try
                {
                    IVdsProvider provider2;
                    this.pack.GetProvider(out provider2);
                    provider = new SoftwareProvider(provider2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsPack::GetProvider failed.", exception);
                }
                return provider;
            }
        }

        public PackStatus Status
        {
            get
            {
                this.InitializeProperties();
                return this.packProp.Status;
            }
        }

        public Collection<Volume> Volumes
        {
            get
            {
                Collection<Volume> collection;
                try
                {
                    IEnumVdsObject obj2;
                    this.pack.QueryVolumes(out obj2);
                    collection = new Collection<Volume>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsPack::QueryVolumes failed.", exception);
                }
                return collection;
            }
        }
    }
}

