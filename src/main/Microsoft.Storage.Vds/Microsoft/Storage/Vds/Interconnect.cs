namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    public class Interconnect : IDisposable
    {
        private bool disposed;
        private VdsInterconnect interconnect;

        public Interconnect(VdsInterconnect interconnect)
        {
            this.interconnect = interconnect;
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
                Marshal.FreeCoTaskMem(this.interconnect.Address);
                this.interconnect.Address = IntPtr.Zero;
                Marshal.FreeCoTaskMem(this.interconnect.Port);
                this.interconnect.Port = IntPtr.Zero;
                this.disposed = true;
            }
        }

        ~Interconnect()
        {
            this.Dispose(false);
        }

        public byte[] Address
        {
            get
            {
                byte[] destination = new byte[this.interconnect.AddressLength];
                Marshal.Copy(this.interconnect.Address, destination, 0, destination.Length);
                return destination;
            }
        }

        public InterconnectAddressType AddressType
        {
            get
            {
                return this.interconnect.AddressType;
            }
        }

        public byte[] Port
        {
            get
            {
                byte[] destination = new byte[this.interconnect.PortLength];
                Marshal.Copy(this.interconnect.Port, destination, 0, destination.Length);
                return destination;
            }
        }
    }
}

