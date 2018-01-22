namespace Microsoft.Storage.Vds
{
    using System;

    public class TargetEventArgs : EventArgs
    {
        private Guid id;

        public TargetEventArgs(Guid targetId)
        {
            this.id = targetId;
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

