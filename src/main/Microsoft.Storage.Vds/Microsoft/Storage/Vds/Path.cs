namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    public abstract class Path : IDisposable
    {
        private bool disposed;
        public PathInfo pathInfo;

        public Path(PathInfo pathInfo)
        {
            this.pathInfo = pathInfo;
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
                Marshal.FreeCoTaskMem(this.pathInfo.AdditionalInfo);
                this.pathInfo.AdditionalInfo = IntPtr.Zero;
                this.disposed = true;
            }
        }

        ~Path()
        {
            this.Dispose(false);
        }

        public FullPathId Id
        {
            get
            {
                return this.pathInfo.PathId;
            }
        }

        public PathStatus Status
        {
            get
            {
                return this.pathInfo.Status;
            }
        }
    }
}

