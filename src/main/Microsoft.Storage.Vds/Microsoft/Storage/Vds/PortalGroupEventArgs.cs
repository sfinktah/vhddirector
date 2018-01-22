namespace Microsoft.Storage.Vds
{
    using System;

    public class PortalGroupEventArgs : EventArgs
    {
        private Guid id;

        public PortalGroupEventArgs(Guid portalGroupId)
        {
            this.id = portalGroupId;
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

