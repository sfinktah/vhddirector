namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class VolumePlex : Wrapper, IDisposable
    {
        private bool refresh;
        private VolumePlexProperties volPlexProp;
        private IVdsVolumePlex volumePlex;

        public VolumePlex()
        {
            this.refresh = true;
            this.volumePlex = null;
        }

        public VolumePlex(object comUnknown, IVdsService vdsService) : base(comUnknown, vdsService)
        {
            this.refresh = true;
            this.InitializeComInterfaces();
        }

        public void EndRepair(IAsyncResult asyncResult)
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
                    throw new VdsException("Format failed with the following error code: " + num);
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsAsync::Wait(...) failed.", exception);
            }
        }

        public override void InitializeComInterfaces()
        {
            base.InitializeComInterfaces();
            if (this.volumePlex == null)
            {
                this.volumePlex = InteropHelpers.QueryInterface<IVdsVolumePlex>(base.ComUnknown);
                if (this.volumePlex == null)
                {
                    throw new VdsException("QueryInterface for IVdsVolumePlex failed.");
                }
            }
        }

        public override void InitializeProperties()
        {
            this.InitializeComInterfaces();
            if (this.refresh)
            {
                try
                {
                    this.volumePlex.GetProperties(out this.volPlexProp);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsVolumePlex::GetProperties(...) failed.", exception);
                }
                this.refresh = false;
            }
        }

        public void Refresh()
        {
            this.refresh = true;
        }

        public Async Repair(InputDisk[] inputDisks, AsyncCallback callback, object state)
        {
            IVdsAsync async;
            this.InitializeComInterfaces();
            try
            {
                this.volumePlex.Repair((inputDisks.Length > 0) ? inputDisks : null, inputDisks.Length, out async);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsVolumePlex.Repair(...) failed.", exception);
            }
            Async async2 = new Async(async, callback, state);
            new Thread(new ThreadStart(async2.Wait)).Start();
            return async2;
        }

        public List<DiskExtent> Extents
        {
            get
            {
                List<DiskExtent> list;
                this.InitializeComInterfaces();
                try
                {
                    IntPtr ptr2;
                    int num;
                    this.volumePlex.QueryExtents(out ptr2, out num);
                    IntPtr ptr = ptr2;
                    list = new List<DiskExtent>(num);
                    for (int i = 0; i < num; i++)
                    {
                        DiskExtent item = (DiskExtent) Marshal.PtrToStructure(ptr, typeof(DiskExtent));
                        list.Add(item);
                        Marshal.DestroyStructure(ptr, typeof(DiskExtent));
                        ptr = Utilities.IntPtrAddOffset(ptr, (uint) Marshal.SizeOf(typeof(DiskExtent)));
                    }
                    Marshal.FreeCoTaskMem(ptr2);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsDrive::QueryExtents(...) failed.", exception);
                }
                return list;
            }
        }

        public Microsoft.Storage.Vds.Health Health
        {
            get
            {
                this.InitializeProperties();
                return this.volPlexProp.Health;
            }
        }

        public Guid Id
        {
            get
            {
                this.InitializeProperties();
                return this.volPlexProp.Id;
            }
        }

        public uint NumberOfMembers
        {
            get
            {
                this.InitializeProperties();
                return this.volPlexProp.NumberOfMembers;
            }
        }

        public ulong Size
        {
            get
            {
                this.InitializeProperties();
                return this.volPlexProp.Size;
            }
        }

        public VolumePlexStatus Status
        {
            get
            {
                this.InitializeProperties();
                return this.volPlexProp.Status;
            }
        }

        public uint StripeSize
        {
            get
            {
                this.InitializeProperties();
                return this.volPlexProp.StripeSize;
            }
        }

        public Microsoft.Storage.Vds.TransitionState TransitionState
        {
            get
            {
                this.InitializeProperties();
                return this.volPlexProp.TransitionState;
            }
        }

        public VolumePlexType Type
        {
            get
            {
                this.InitializeProperties();
                return this.volPlexProp.Type;
            }
        }

        public Microsoft.Storage.Vds.Volume Volume
        {
            get
            {
                Microsoft.Storage.Vds.Volume volume;
                this.InitializeComInterfaces();
                try
                {
                    IVdsVolume volume2;
                    this.volumePlex.GetVolume(out volume2);
                    volume = new Microsoft.Storage.Vds.Volume(volume2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsVolumePlex::GetVolume(...) failed.", exception);
                }
                return volume;
            }
        }
    }
}

