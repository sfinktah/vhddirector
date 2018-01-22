namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class Drive : Wrapper, IDisposable
    {
        private IVdsDrive drive;
        private DriveProperties driveProp;
        private IVdsMaintenance maintenance;
        private bool refresh;

        public Drive()
        {
            this.refresh = true;
            this.drive = null;
        }

        public Drive(object driveUnknown, IVdsService vdsService) : base(driveUnknown, vdsService)
        {
            this.refresh = true;
            this.InitializeComInterfaces();
        }

        public override void InitializeComInterfaces()
        {
            base.InitializeComInterfaces();
            if (this.drive == null)
            {
                try
                {
                    this.drive = (IVdsDrive) base.ComUnknown;
                }
                catch (InvalidCastException exception)
                {
                    throw new VdsException("QueryInterface for IVdsDrive failed.", exception);
                }
            }
            if (this.maintenance == null)
            {
                try
                {
                    this.maintenance = (IVdsMaintenance) base.ComUnknown;
                }
                catch (InvalidCastException)
                {
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
                    this.drive.GetProperties(out this.driveProp);
                    this.refresh = false;
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsDrive::GetProperties failed.", exception);
            }
        }

        public void PulseMaintenance(MaintenanceOperation operation, uint count)
        {
            this.InitializeComInterfaces();
            if (this.maintenance == null)
            {
                throw new NotSupportedException("The drive does not support maintenance operations.");
            }
            try
            {
                this.maintenance.PulseMaintenance(operation, count);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsMaintenance::PulseMaintenance failed.", exception);
            }
        }

        public void Refresh()
        {
            this.refresh = true;
        }

        public void StartMaintenance(MaintenanceOperation operation)
        {
            this.InitializeComInterfaces();
            if (this.maintenance == null)
            {
                throw new NotSupportedException("The drive does not support maintenance operations.");
            }
            try
            {
                this.maintenance.StartMaintenance(operation);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsMaintenance::StartMaintenance failed.", exception);
            }
        }

        public void StopMaintenance(MaintenanceOperation operation)
        {
            this.InitializeComInterfaces();
            if (this.maintenance == null)
            {
                throw new NotSupportedException("The drive does not support maintenance operations.");
            }
            try
            {
                this.maintenance.StopMaintenance(operation);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsMaintenance::StopMaintenance failed.", exception);
            }
        }

        public List<DriveExtent> Extents
        {
            get
            {
                List<DriveExtent> list;
                this.InitializeComInterfaces();
                try
                {
                    IntPtr ptr2;
                    int num;
                    this.drive.QueryExtents(out ptr2, out num);
                    IntPtr ptr = ptr2;
                    list = new List<DriveExtent>(num);
                    for (int i = 0; i < num; i++)
                    {
                        DriveExtent item = (DriveExtent) Marshal.PtrToStructure(ptr, typeof(DriveExtent));
                        list.Add(item);
                        Marshal.DestroyStructure(ptr, typeof(DriveExtent));
                        ptr = Utilities.IntPtrAddOffset(ptr, (uint) Marshal.SizeOf(typeof(DriveExtent)));
                    }
                    Marshal.FreeCoTaskMem(ptr2);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsDrive::QueryExtents failed.", exception);
                }
                return list;
            }
        }

        public DriveFlags Flags
        {
            get
            {
                this.InitializeProperties();
                return this.driveProp.Flags;
            }
        }

        public string FriendlyName
        {
            get
            {
                this.InitializeProperties();
                return this.driveProp.FriendlyName;
            }
        }

        public Microsoft.Storage.Vds.Health Health
        {
            get
            {
                this.InitializeProperties();
                return this.driveProp.Health;
            }
        }

        public bool HotSpare
        {
            get
            {
                this.InitializeProperties();
                return (DriveFlags.Hotspare == (this.driveProp.Flags & DriveFlags.Hotspare));
            }
            set
            {
                this.InitializeComInterfaces();
                try
                {
                    if (value)
                    {
                        this.drive.SetFlags(DriveFlags.Hotspare);
                    }
                    else
                    {
                        this.drive.ClearFlags(DriveFlags.Hotspare);
                    }
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsDrive::Set/ClearFlags failed.", exception);
                }
                this.refresh = true;
            }
        }

        public Guid Id
        {
            get
            {
                this.InitializeProperties();
                return this.driveProp.Id;
            }
        }

        public string Identification
        {
            get
            {
                this.InitializeProperties();
                return this.driveProp.Identification;
            }
        }

        public short InternalBusNumber
        {
            get
            {
                this.InitializeProperties();
                return this.driveProp.InternalBusNumber;
            }
        }

        public ulong Size
        {
            get
            {
                this.InitializeProperties();
                return this.driveProp.Size;
            }
        }

        public short SlotNumber
        {
            get
            {
                this.InitializeProperties();
                return this.driveProp.SlotNumber;
            }
        }

        public DriveStatus Status
        {
            get
            {
                this.InitializeProperties();
                return this.driveProp.Status;
            }
            set
            {
                this.InitializeComInterfaces();
                try
                {
                    this.drive.SetStatus(value);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsDrive::SetStatus failed.", exception);
                }
                this.refresh = true;
            }
        }

        public Microsoft.Storage.Vds.SubSystem SubSystem
        {
            get
            {
                Microsoft.Storage.Vds.SubSystem system;
                this.InitializeComInterfaces();
                try
                {
                    IVdsSubSystem system2;
                    this.drive.GetSubSystem(out system2);
                    system = new Microsoft.Storage.Vds.SubSystem(system2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsDrive::GetSubSystem failed.", exception);
                }
                return system;
            }
        }
    }
}

