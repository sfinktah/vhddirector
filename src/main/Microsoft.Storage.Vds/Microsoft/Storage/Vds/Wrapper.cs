namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Runtime.InteropServices;

    public class Wrapper : IDisposable
    {
        private object comUnknown;
        private bool disposed;
        private IVdsService vdsService;

        protected Wrapper()
        {
        }

        protected Wrapper(object comUnknown)
        {
            this.comUnknown = comUnknown;
            this.vdsService = null;
        }

        public Wrapper(object comUnknown, IVdsService vdsService)
        {
            this.comUnknown = comUnknown;
            this.vdsService = vdsService;
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
                if (this.comUnknown != null)
                {
                    if (!Globals.IsMockObject(this.comUnknown))
                    {
                        Marshal.ReleaseComObject(this.comUnknown);
                    }
                    this.comUnknown = null;
                }
                this.disposed = true;
            }
        }

        ~Wrapper()
        {
            try
            {
                this.Dispose(false);
            }
            catch (VdsException)
            {
            }
        }

        public virtual void InitializeComInterfaces()
        {
            if (this.comUnknown == null)
            {
                throw new VdsException("Attempting to use a VDS object which was not created by a VDS call.");
            }
        }

        public virtual void InitializeProperties()
        {
            this.InitializeComInterfaces();
        }

        public object ComUnknown
        {
            get
            {
                return this.comUnknown;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                this.comUnknown = value;
            }
        }

        public IVdsService VdsService
        {
            get
            {
                return this.vdsService;
            }
            set
            {
                this.vdsService = value;
            }
        }
    }
}

