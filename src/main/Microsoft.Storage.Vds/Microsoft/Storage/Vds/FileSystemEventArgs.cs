namespace Microsoft.Storage.Vds
{
    using System;

    public class FileSystemEventArgs : EventArgs
    {
        private uint percentCompleted;
        private Guid volumeId;

        public FileSystemEventArgs(Guid volumeId, uint percentCompleted)
        {
            this.volumeId = volumeId;
            this.percentCompleted = percentCompleted;
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

