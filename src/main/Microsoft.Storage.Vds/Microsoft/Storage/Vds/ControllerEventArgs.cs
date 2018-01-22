namespace Microsoft.Storage.Vds
{
    using System;

    public class ControllerEventArgs : EventArgs
    {
        private Guid id;

        public ControllerEventArgs(Guid controllerId)
        {
            this.id = controllerId;
        }

        public Guid Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
    }
}

