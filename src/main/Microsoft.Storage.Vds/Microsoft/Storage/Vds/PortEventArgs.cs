namespace Microsoft.Storage.Vds
{
    using System;

    public class PortEventArgs : EventArgs
    {
        private Guid id;

        public PortEventArgs(Guid portId)
        {
            this.id = portId;
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

