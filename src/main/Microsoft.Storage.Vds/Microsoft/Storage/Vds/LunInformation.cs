namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class LunInformation : IDisposable
    {
        private StorageDeviceIdDescriptor deviceIdDescriptor;
        private bool disposed;
        private VdsLunInformation info;
        private List<Interconnect> interconnects;

        public LunInformation(VdsLunInformation vdsLunInfo)
        {
            this.info = vdsLunInfo;
            this.deviceIdDescriptor = null;
            this.disposed = false;
            this.interconnects = null;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.deviceIdDescriptor.Dispose();
                    if (this.interconnects != null)
                    {
                        foreach (Interconnect interconnect in this.interconnects)
                        {
                            interconnect.Dispose();
                        }
                    }
                }
                Marshal.FreeCoTaskMem(this.info.Interconnects);
                this.disposed = true;
            }
        }

        ~LunInformation()
        {
            this.Dispose(false);
        }

        public StorageBusType BusType
        {
            get
            {
                return this.info.BusType;
            }
        }

        public bool CommandQueueing
        {
            get
            {
                return (this.info.CommandQueueing == 1);
            }
        }

        public StorageDeviceIdDescriptor DeviceIdDescriptor
        {
            get
            {
                if (this.deviceIdDescriptor == null)
                {
                    this.deviceIdDescriptor = new StorageDeviceIdDescriptor(this.info.DeviceIdDescriptor);
                }
                return this.deviceIdDescriptor;
            }
        }

        public byte DeviceType
        {
            get
            {
                return this.info.DeviceType;
            }
        }

        public byte DeviceTypeModifier
        {
            get
            {
                return this.info.DeviceTypeModifier;
            }
        }

        public Guid DiskSignature
        {
            get
            {
                return this.info.DiskSignature;
            }
        }

        public VdsLunInformation Info
        {
            get
            {
                return this.info;
            }
        }

        public List<Interconnect> Interconnects
        {
            get
            {
                if (this.interconnects == null)
                {
                    IntPtr interconnects = this.info.Interconnects;
                    this.interconnects = new List<Interconnect>((int) this.info.NumberOfInterconnects);
                    for (int i = 0; i < this.info.NumberOfInterconnects; i++)
                    {
                        VdsInterconnect interconnect = (VdsInterconnect) Marshal.PtrToStructure(interconnects, typeof(VdsInterconnect));
                        this.interconnects.Add(new Interconnect(interconnect));
                        interconnects = Utilities.IntPtrAddOffset(interconnects, (uint) Marshal.SizeOf(typeof(VdsInterconnect)));
                    }
                }
                return this.interconnects;
            }
        }

        public string ProductId
        {
            get
            {
                return this.info.ProductId;
            }
        }

        public string ProductRevision
        {
            get
            {
                return this.info.ProductRevision;
            }
        }

        public string SerialNumber
        {
            get
            {
                return this.info.SerialNumber;
            }
        }

        public string VendorId
        {
            get
            {
                return this.info.VendorId;
            }
        }

        public uint Version
        {
            get
            {
                return this.info.Version;
            }
        }
    }
}

