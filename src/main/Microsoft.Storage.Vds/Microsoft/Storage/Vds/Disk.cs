namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class Disk : Wrapper, IDisposable
    {
        private IVdsDisk disk;
        private DiskProperties diskProp;
        private bool refresh;

        public Disk()
        {
            this.refresh = true;
            this.disk = null;
        }

        public Disk(object comUnknown, IVdsService vdsService) : base(comUnknown, vdsService)
        {
            this.refresh = true;
            this.InitializeComInterfaces();
        }

        public void ClearFlags([In] DiskFlags diskFlag)
        {
            this.InitializeComInterfaces();
            this.disk.ClearFlags(diskFlag);
            this.Refresh();
        }

        public override void InitializeComInterfaces()
        {
            base.InitializeComInterfaces();
            if (this.disk == null)
            {
                this.disk = InteropHelpers.QueryInterface<IVdsDisk>(base.ComUnknown);
                if (this.disk == null)
                {
                    throw new VdsException("QueryInterface for IVdsDisk failed.");
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
                    this.disk.GetProperties(out this.diskProp);
                    this.refresh = false;
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsDisk::GetProperties failed.", exception);
            }
        }

        public void Refresh()
        {
            this.refresh = true;
        }

        public void SetFlags([In] DiskFlags diskFlag)
        {
            this.InitializeComInterfaces();
            this.disk.SetFlags(diskFlag);
            this.Refresh();
        }

        public string AdaptorName
        {
            get
            {
                this.InitializeProperties();
                return this.diskProp.AdaptorName;
            }
        }

        public StorageBusType BusType
        {
            get
            {
                this.InitializeProperties();
                return this.diskProp.BusType;
            }
        }

        public uint BytesPerSector
        {
            get
            {
                this.InitializeProperties();
                return this.diskProp.BytesPerSector;
            }
        }

        public string DevicePath
        {
            get
            {
                this.InitializeProperties();
                return this.diskProp.DevicePath;
            }
        }

        public uint DeviceType
        {
            get
            {
                this.InitializeProperties();
                return this.diskProp.DeviceType;
            }
        }

        public string DiskAddress
        {
            get
            {
                this.InitializeProperties();
                return this.diskProp.DiskAddress;
            }
        }

        public Guid DiskGuid
        {
            get
            {
                this.InitializeProperties();
                if (this.diskProp.PartitionStyle != Microsoft.Storage.Vds.PartitionStyle.Gpt)
                {
                    return Guid.Empty;
                }
                return this.diskProp.Signature.GptDiskGuid;
            }
        }

        public List<DiskExtent> Extents
        {
            get
            {
                List<DiskExtent> list;
                IntPtr zero = IntPtr.Zero;
                IntPtr diskExtents = IntPtr.Zero;
                this.InitializeComInterfaces();
                try
                {
                    int num;
                    this.disk.QueryExtents(out diskExtents, out num);
                    zero = diskExtents;
                    list = new List<DiskExtent>(num);
                    for (int i = 0; i < num; i++)
                    {
                        DiskExtent item = (DiskExtent) Marshal.PtrToStructure(zero, typeof(DiskExtent));
                        list.Add(item);
                        zero = Utilities.IntPtrAddOffset(zero, (uint) Marshal.SizeOf(typeof(DiskExtent)));
                    }
                    Marshal.FreeCoTaskMem(diskExtents);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsDisk::QueryExtents failed.", exception);
                }
                return list;
            }
        }

        public DiskFlags Flags
        {
            get
            {
                this.InitializeProperties();
                return this.diskProp.Flags;
            }
        }

        public string FriendlyName
        {
            get
            {
                this.InitializeProperties();
                return this.diskProp.FriendlyName;
            }
        }

        public Microsoft.Storage.Vds.Health Health
        {
            get
            {
                this.InitializeProperties();
                return this.diskProp.Health;
            }
        }

        public Guid Id
        {
            get
            {
                this.InitializeProperties();
                return this.diskProp.Id;
            }
        }

        public bool IsLun
        {
            get
            {
                this.InitializeComInterfaces();
                try
                {
                    IVdsLun comUnknown = (IVdsLun) base.ComUnknown;
                }
                catch (InvalidCastException)
                {
                    return false;
                }
                catch (COMException exception)
                {
                    throw new VdsException("QueryInterface for IVdsLun failed.", exception);
                }
                return true;
            }
        }

        public Microsoft.Storage.Vds.Lun Lun
        {
            get
            {
                IVdsLun comUnknown;
                LunProperties properties;
                object obj2;
                this.InitializeComInterfaces();
                try
                {
                    comUnknown = (IVdsLun) base.ComUnknown;
                }
                catch (InvalidCastException exception)
                {
                    throw new VdsException("The disk does not have an associated LUN.", exception);
                }
                catch (COMException exception2)
                {
                    throw new VdsException("QueryInterface for IVdsLun failed.", exception2);
                }
                try
                {
                    comUnknown.GetProperties(out properties);
                }
                catch (COMException exception3)
                {
                    throw new VdsException("IVdsLun::GetProperties failed.", exception3);
                }
                try
                {
                    base.VdsService.GetObject(properties.Id, ObjectType.Lun, out obj2);
                }
                catch (COMException exception4)
                {
                    throw new VdsException("GetObject for LUN failed.", exception4);
                }
                return new Microsoft.Storage.Vds.Lun(obj2, base.VdsService);
            }
        }

        public uint MediaType
        {
            get
            {
                this.InitializeProperties();
                return this.diskProp.MediaType;
            }
        }

        public string Name
        {
            get
            {
                this.InitializeProperties();
                return this.diskProp.Name;
            }
        }

        public Microsoft.Storage.Vds.PartitionStyle PartitionStyle
        {
            get
            {
                this.InitializeProperties();
                return this.diskProp.PartitionStyle;
            }
        }

        public LunReserveMode ReserveMode
        {
            get
            {
                this.InitializeProperties();
                return this.diskProp.ReserveMode;
            }
        }

        public uint SectorsPerTrack
        {
            get
            {
                this.InitializeProperties();
                return this.diskProp.SectorsPerTrack;
            }
        }

        public uint Signature
        {
            get
            {
                this.InitializeProperties();
                if (this.diskProp.PartitionStyle != Microsoft.Storage.Vds.PartitionStyle.Mbr)
                {
                    return 0;
                }
                return this.diskProp.Signature.MbrSignature;
            }
        }

        public ulong Size
        {
            get
            {
                this.InitializeProperties();
                return this.diskProp.Size;
            }
        }

        public DiskStatus Status
        {
            get
            {
                this.InitializeProperties();
                return this.diskProp.Status;
            }
        }

        public uint TracksPerCylinder
        {
            get
            {
                this.InitializeProperties();
                return this.diskProp.TracksPerCylinder;
            }
        }
    }
}

