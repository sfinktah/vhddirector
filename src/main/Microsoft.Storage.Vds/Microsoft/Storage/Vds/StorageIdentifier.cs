namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    public class StorageIdentifier : IDisposable
    {
        private bool disposed;
        private VdsStorageIdentifier vdsIdentifier;

        public StorageIdentifier(VdsStorageIdentifier vdsIdentifier)
        {
            this.vdsIdentifier = vdsIdentifier;
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
                Marshal.FreeCoTaskMem(this.vdsIdentifier.Identifier);
                this.vdsIdentifier.Identifier = IntPtr.Zero;
                this.disposed = true;
            }
        }

        ~StorageIdentifier()
        {
            this.Dispose(false);
        }

        public StorageIdentifierCodeSet CodeSet
        {
            get
            {
                return this.vdsIdentifier.CodeSet;
            }
        }

        public byte[] Identifier
        {
            get
            {
                byte[] destination = new byte[this.vdsIdentifier.IdentifierLength];
                Marshal.Copy(this.vdsIdentifier.Identifier, destination, 0, destination.Length);
                return destination;
            }
        }

        public StorageIdentifierType Type
        {
            get
            {
                return this.vdsIdentifier.Type;
            }
        }
    }
}

