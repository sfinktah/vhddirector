namespace Microsoft.Storage.Vds
{
    using System;

    public class SubSystemEventArgs : EventArgs
    {
        private Guid id;

        public SubSystemEventArgs(Guid subSystemId)
        {
            this.id = subSystemId;
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

