namespace Microsoft.Storage.Vds
{
    using System;

    public class DriveEventArgs : EventArgs
    {
        private Guid id;

        public DriveEventArgs(Guid driveId)
        {
            this.id = driveId;
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

