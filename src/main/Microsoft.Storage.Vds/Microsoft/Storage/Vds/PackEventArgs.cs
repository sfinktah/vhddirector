namespace Microsoft.Storage.Vds
{
    using System;

    public class PackEventArgs : EventArgs
    {
        private Guid id;

        public PackEventArgs(Guid packId)
        {
            this.id = packId;
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

