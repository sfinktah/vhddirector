namespace Microsoft.Storage.Vds
{
    using System;

    public class LunEventArgs : EventArgs
    {
        private Guid id;

        public LunEventArgs(Guid lunId)
        {
            this.id = lunId;
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

