namespace Microsoft.Storage.Vds.Advanced
{
    using Microsoft.Storage.Vds.Interop;
    using System;

    public class GptPartition : Partition
    {
        private string partitionName;

        public GptPartition(PartitionProperties partitionProperties, string partitionName, IVdsAdvancedDisk advancedDisk) : base(partitionProperties, advancedDisk)
        {
            this.partitionName = partitionName;
        }

        public ulong Attributes
        {
            get
            {
                return this.partProp.Info.Gpt.Attributes;
            }
        }

        public Guid GptPartitionType
        {
            get
            {
                return this.partProp.Info.Gpt.PartitionType;
            }
        }

        public Guid Id
        {
            get
            {
                return this.partProp.Info.Gpt.PartitionId;
            }
        }

        public string Name
        {
            get
            {
                return this.partitionName;
            }
        }
    }
}

