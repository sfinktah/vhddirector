namespace Microsoft.Storage.Vds
{
    using System;

    public class PortalEventArgs : EventArgs
    {
        private Guid id;

        public PortalEventArgs(Guid portalId)
        {
            this.id = portalId;
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

