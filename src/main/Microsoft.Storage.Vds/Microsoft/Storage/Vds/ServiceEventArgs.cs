namespace Microsoft.Storage.Vds
{
    using System;

    public class ServiceEventArgs : EventArgs
    {
        private ServiceRecoverAction recoverAction;

        public ServiceEventArgs(ServiceRecoverAction recoverAction)
        {
            this.recoverAction = recoverAction;
        }

        public ServiceRecoverAction RecoverAction
        {
            get
            {
                return this.recoverAction;
            }
            set
            {
                this.recoverAction = value;
            }
        }
    }
}

