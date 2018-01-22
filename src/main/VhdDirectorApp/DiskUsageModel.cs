using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VhdDirectorApp
{
    public class DiskUsageModel
    {
        protected List<PartitionUsageModel> partitionModels = new List<PartitionUsageModel>();
        // You can convert it back to an array if you would like to
        // PartitionUsageModel[] pms = partitionModels.ToArray();

        public int PartitionCount
        {
            get { return partitionModels.Count; }
        }
        public PartitionUsageModel GetPartitionUsageModel(int partition)
        {
            return partitionModels[partition];
        }
        public DiskUsageModel() { }
        public DiskUsageModel(BiosPartitionRecord[] partitions)
        {
            for (int i = 0; i < partitions.Length; i++)
            {
                BiosPartitionRecord bpr = partitions[i];
                if (bpr.IsValid)
                {
                    PartitionUsageModel pm = new PartitionUsageModel();
                    pm.PartitionType = bpr.PartitionType;
                    pm.FriendlyPartitionType = bpr.FriendlyPartitionType;
                    pm.Status = bpr.Status;
                    pm.LBAStart = bpr.LBAStart;
                    pm.LBALength = bpr.LBALength;

                    partitionModels.Add(pm);
                }
            }
        }
    }
}
