namespace Microsoft.Storage.Vds
{
    using System;

    public class DiskEventArgs : EventArgs
    {
        private Guid id;

        public DiskEventArgs(Guid diskId)
        {
            this.id = diskId;
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

