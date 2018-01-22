namespace Microsoft.Storage.Vds.Advanced
{
    using Microsoft.Storage.Vds.Interop;
    using System;

    public class MbrPartition : Partition
    {
        public MbrPartition(PartitionProperties partitionProperties, IVdsAdvancedDisk advancedDisk) : base(partitionProperties, advancedDisk)
        {
        }

        public bool BootIndicator
        {
            get
            {
                return (this.partProp.Info.Mbr.BootIndicator == 1);
            }
        }

        public byte MbrPartitionType
        {
            get
            {
                return this.partProp.Info.Mbr.PartitionType;
            }
        }
    }
}

