using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHD_Director
{
    public class DiskModel
    {
        public Footer footer;
        public DynamicHeader dynamicHeader;
        public BlockAllocationTable blockAllocationTable;
        public MasterBootRecord masterBootRecord;

        public DiskModel() { }

        public DiskModel(Footer footer, DynamicHeader dynamicHeader, BlockAllocationTable blockAllocationTable, MasterBootRecord masterBootRecord)
        {
            // TODO: Complete member initialization
            this.footer = footer;
            this.dynamicHeader = dynamicHeader;
            this.blockAllocationTable = blockAllocationTable;
            this.masterBootRecord = masterBootRecord;
        }
    }
}
