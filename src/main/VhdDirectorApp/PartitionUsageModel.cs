using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VhdDirectorApp
{
    public class PartitionUsageModel
    {
        public byte PartitionType { get; set; }

        public string FriendlyPartitionType { get; set; }

        public byte Status { get; set; }

        public uint LBAStart { get; set; }

        public uint LBALength { get; set; }

        public uint LBAEnd { get { return LBAStart + LBALength; } }

        public bool pendingFormat { get; set; }

        public bool pendingRemove { get; set; }

        public bool pendingResize { get; set; }
        public bool isEmpty { get; set; }
    }
}
