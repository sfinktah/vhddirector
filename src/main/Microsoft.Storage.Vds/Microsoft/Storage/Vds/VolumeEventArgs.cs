namespace Microsoft.Storage.Vds
{
    using System;

    public class VolumeEventArgs : EventArgs
    {
        private Guid id;
        private uint percentCompleted;
        private Guid plexId;

        public VolumeEventArgs(Guid volumeId, Guid plexId, uint percentCompleted)
        {
            this.id = volumeId;
            this.plexId = plexId;
            this.percentCompleted = percentCompleted;
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

        public uint PercentCompleted
        {
            get
            {
                return this.percentCompleted;
            }
            set
            {
                this.percentCompleted = value;
            }
        }

        public Guid PlexId
        {
            get
            {
                return this.plexId;
            }
            set
            {
                this.plexId = value;
            }
        }
    }
}

