namespace Microsoft.Storage.Vds
{
    using System;

    public class PartitionEventArgs : EventArgs
    {
        private Guid diskId;
        private ulong offset;

        public PartitionEventArgs(Guid diskId, ulong offset)
        {
            this.diskId = diskId;
            this.offset = offset;
        }

        public Guid DiskId
        {
            get
            {
                return this.diskId;
            }
            set
            {
                this.diskId = value;
            }
        }

        public ulong Offset
        {
            get
            {
                return this.offset;
            }
            set
            {
                this.offset = value;
            }
        }
    }
}

