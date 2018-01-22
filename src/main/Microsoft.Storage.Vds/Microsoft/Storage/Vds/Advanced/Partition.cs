namespace Microsoft.Storage.Vds.Advanced
{
    using Microsoft.Storage.Vds;
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Runtime.InteropServices;

    public class Partition
    {
        public IVdsAdvancedDisk advancedDisk;
        public PartitionProperties partProp;

        public Partition(PartitionProperties partitionProperties, IVdsAdvancedDisk advancedDisk)
        {
            this.partProp = partitionProperties;
            this.advancedDisk = advancedDisk;
        }

        public void Delete(bool force, bool forceProtected)
        {
            try
            {
                this.advancedDisk.DeletePartition(this.partProp.Offset, force ? 1U : 0U, forceProtected ? 1U : 0U);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsAdvancedDisk::DeletePartition failed.", exception);
            }
        }

        public PartitionFlags Flags
        {
            get
            {
                return this.partProp.Flags;
            }
        }

        public ulong Offset
        {
            get
            {
                return this.partProp.Offset;
            }
        }

        public uint PartitionNumber
        {
            get
            {
                return this.partProp.PartitionNumber;
            }
        }

        public ulong Size
        {
            get
            {
                return this.partProp.Size;
            }
        }

        public PartitionStyle Style
        {
            get
            {
                return this.partProp.Style;
            }
        }
    }
}

