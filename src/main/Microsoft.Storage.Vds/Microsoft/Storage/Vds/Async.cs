namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class Async : Wrapper, IAsyncResult
    {
        private IVdsAsync async;
        private ManualResetEvent asyncWaitHandle;
        private AsyncCallback callback;
        private bool isCancelled;
        private object state;

        public Async(object asyncUnk, AsyncCallback callback, object state) : base(asyncUnk)
        {
            this.InitializeComInterfaces();
            this.callback = callback;
            this.state = state;
            this.asyncWaitHandle = new ManualResetEvent(false);
        }

        public void Cancel()
        {
            if (!this.isCancelled)
            {
                try
                {
                    this.async.Cancel();
                    this.isCancelled = true;
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsAsync::Cancel failed.", exception);
                }
            }
        }

        public override void InitializeComInterfaces()
        {
            base.InitializeComInterfaces();
            try
            {
                if (this.async == null)
                {
                    this.async = InteropHelpers.QueryInterfaceThrow<IVdsAsync>(base.ComUnknown);
                }
            }
            catch (InvalidCastException exception)
            {
                throw new VdsException("QueryInterface for IVdsAsync failed.", exception);
            }
        }

        private void ReleaseAsyncOutput(VDS_ASYNC_OUTPUT asyncOut)
        {
            IntPtr zero = IntPtr.Zero;
            switch (asyncOut.Type)
            {
                case VDS_ASYNC_OUTPUT_TYPE.VDS_ASYNCOUT_CREATETARGET:
                    zero = asyncOut.Info.Ct.TargetUnknown;
                    break;

                case VDS_ASYNC_OUTPUT_TYPE.VDS_ASYNCOUT_CREATEPORTALGROUP:
                    zero = asyncOut.Info.Cpg.PortalGroupUnknown;
                    break;

                case VDS_ASYNC_OUTPUT_TYPE.VDS_ASYNCOUT_CREATELUN:
                    zero = asyncOut.Info.Cl.LunUnknown;
                    break;

                case VDS_ASYNC_OUTPUT_TYPE.VDS_ASYNCOUT_CREATEVOLUME:
                    zero = asyncOut.Info.Cv.VolumeUnknown;
                    break;

                case VDS_ASYNC_OUTPUT_TYPE.VDS_ASYNCOUT_BREAKVOLUMEPLEX:
                    zero = asyncOut.Info.Bvp.VolumeUnknown;
                    break;
            }
            if (zero != IntPtr.Zero)
            {
                Marshal.Release(zero);
            }
        }

        public void Wait()
        {
            this.asyncWaitHandle.Reset();
            try
            {
                uint num;
                VDS_ASYNC_OUTPUT vds_async_output;
                this.async.Wait(out num, out vds_async_output);
                this.ReleaseAsyncOutput(vds_async_output);
            }
            catch (Exception exception)
            {
                if (VdsException.IsFatalException(exception))
                {
                    throw;
                }
            }
            this.asyncWaitHandle.Set();
            if (this.callback != null)
            {
                this.callback(this);
            }
        }

        public object AsyncObject
        {
            get
            {
                return null;
            }
        }

        public object AsyncState
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }

        public WaitHandle AsyncWaitHandle
        {
            get
            {
                return this.asyncWaitHandle;
            }
        }

        public AsyncCallback Callback
        {
            get
            {
                return this.callback;
            }
            set
            {
                this.callback = value;
            }
        }

        public bool CompletedSynchronously
        {
            get
            {
                return false;
            }
        }

        public bool IsCancelled
        {
            get
            {
                return this.isCancelled;
            }
        }

        public bool IsCompleted
        {
            get
            {
                uint num;
                this.InitializeComInterfaces();
                try
                {
                    uint num2;
                    this.async.QueryStatus(out num, out num2);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsAsync::QueryStatus failed.", exception);
                }
                if (0x80042409 == num)
                {
                    return false;
                }
                return true;
            }
        }

        public uint PercentCompleted
        {
            get
            {
                uint num;
                uint num2;
                this.InitializeComInterfaces();
                try
                {
                    this.async.QueryStatus(out num, out num2);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsAsync::QueryStatus failed.", exception);
                }
                if (0x80042409 == num)
                {
                    return num2;
                }
                return 100;
            }
        }
    }
}

