namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class LunPlex : Wrapper, IDisposable
    {
        private IVdsLunPlex plex;
        private LunPlexProperties plexProp;
        private bool refresh;

        public LunPlex()
        {
            this.refresh = true;
            this.plex = null;
        }

        public LunPlex(object comUnknown, IVdsService vdsService) : base(comUnknown, vdsService)
        {
            this.refresh = true;
            this.InitializeComInterfaces();
        }

        public override void InitializeComInterfaces()
        {
            base.InitializeComInterfaces();
            if (this.plex == null)
            {
                try
                {
                    this.plex = (IVdsLunPlex) base.ComUnknown;
                }
                catch (InvalidCastException exception)
                {
                    throw new VdsException("QueryInterface for IVdsLunPlex failed.", exception);
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
                    this.plex.GetProperties(out this.plexProp);
                    this.refresh = false;
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsLunPlex::GetProperties failed.", exception);
            }
        }

        public void Refresh()
        {
            this.refresh = true;
        }

        public List<DriveExtent> Extents
        {
            get
            {
                List<DriveExtent> list;
                this.InitializeComInterfaces();
                try
                {
                    IntPtr ptr;
                    int num;
                    this.plex.QueryExtents(out ptr, out num);
                    list = new List<DriveExtent>(num);
                    for (int i = 0; i < num; i++)
                    {
                        DriveExtent item = (DriveExtent) Marshal.PtrToStructure(ptr, typeof(DriveExtent));
                        list.Add(item);
                        Marshal.DestroyStructure(ptr, typeof(DriveExtent));
                        ptr = Utilities.IntPtrAddOffset(ptr, (uint) Marshal.SizeOf(typeof(DriveExtent)));
                    }
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsLunPlex::QueryExtents failed.", exception);
                }
                return list;
            }
        }

        public LunPlexFlags Flags
        {
            get
            {
                this.InitializeProperties();
                return this.plexProp.Flags;
            }
        }

        public Microsoft.Storage.Vds.Health Health
        {
            get
            {
                this.InitializeProperties();
                return this.plexProp.Health;
            }
        }

        public LunHints Hints
        {
            get
            {
                Microsoft.Storage.Vds.Hints hints;
                this.InitializeComInterfaces();
                try
                {
                    this.plex.QueryHints(out hints);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsLunPlex::QueryHints failed.", exception);
                }
                return new LunHints(hints);
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                Microsoft.Storage.Vds.Hints hints = value.Hints;
                this.InitializeComInterfaces();
                try
                {
                    this.plex.ApplyHints(ref hints);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsLunPlex::ApplyHints failed.", exception);
                }
                this.refresh = true;
            }
        }

        public Guid Id
        {
            get
            {
                this.InitializeProperties();
                return this.plexProp.Id;
            }
        }

        public short RebuildPriority
        {
            get
            {
                this.InitializeProperties();
                return this.plexProp.RebuildPriority;
            }
        }

        public ulong Size
        {
            get
            {
                this.InitializeProperties();
                return this.plexProp.Size;
            }
        }

        public LunPlexStatus Status
        {
            get
            {
                this.InitializeProperties();
                return this.plexProp.Status;
            }
        }

        public uint StripeSize
        {
            get
            {
                this.InitializeProperties();
                return this.plexProp.StripeSize;
            }
        }

        public Microsoft.Storage.Vds.TransitionState TransitionState
        {
            get
            {
                this.InitializeProperties();
                return this.plexProp.TransitionState;
            }
        }

        public LunPlexType Type
        {
            get
            {
                this.InitializeProperties();
                return this.plexProp.Type;
            }
        }
    }
}

