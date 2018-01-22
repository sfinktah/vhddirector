namespace Microsoft.Storage.Vds
{
    using System;

    public class MountPointEventArgs : EventArgs
    {
        private Guid volumeId;

        public MountPointEventArgs(Guid volumeId)
        {
            this.volumeId = volumeId;
        }

        public Guid VolumeId
        {
            get
            {
                return this.volumeId;
            }
            set
            {
                this.volumeId = value;
            }
        }
    }
}

