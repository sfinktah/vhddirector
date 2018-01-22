namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class StorageDeviceIdDescriptor : IDisposable
    {
        private VdsStorageDeviceIdDescriptor desc;
        private bool disposed;
        private List<StorageIdentifier> identifiers;

        public StorageDeviceIdDescriptor(VdsStorageDeviceIdDescriptor desc)
        {
            this.desc = desc;
            this.disposed = false;
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
                if (disposing && (this.identifiers != null))
                {
                    foreach (StorageIdentifier identifier in this.identifiers)
                    {
                        identifier.Dispose();
                    }
                }
                Marshal.FreeCoTaskMem(this.desc.Identifiers);
                this.disposed = true;
            }
        }

        ~StorageDeviceIdDescriptor()
        {
            this.Dispose(false);
        }

        public List<StorageIdentifier> Identifiers
        {
            get
            {
                if (this.identifiers == null)
                {
                    this.identifiers = new List<StorageIdentifier>((int) this.desc.NumberOfIdentifiers);
                    IntPtr identifiers = this.desc.Identifiers;
                    for (int i = 0; i < this.desc.NumberOfIdentifiers; i++)
                    {
                        VdsStorageIdentifier vdsIdentifier = (VdsStorageIdentifier) Marshal.PtrToStructure(identifiers, typeof(VdsStorageIdentifier));
                        this.identifiers.Add(new StorageIdentifier(vdsIdentifier));
                        identifiers = Utilities.IntPtrAddOffset(identifiers, (uint) Marshal.SizeOf(typeof(VdsStorageIdentifier)));
                    }
                }
                return this.identifiers;
            }
        }

        public uint Version
        {
            get
            {
                return this.desc.Version;
            }
        }
    }
}

