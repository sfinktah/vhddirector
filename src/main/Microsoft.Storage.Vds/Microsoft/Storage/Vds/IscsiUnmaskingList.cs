namespace Microsoft.Storage.Vds
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class IscsiUnmaskingList : Microsoft.Storage.Vds.UnmaskingList
    {
        private List<string> unmaskingList;

        public IscsiUnmaskingList()
        {
            base.type = UnmaskingListType.IScsi;
            this.unmaskingList = new List<string>();
        }

        public IscsiUnmaskingList(bool unmaskAll) : base(unmaskAll)
        {
            base.type = UnmaskingListType.IScsi;
            this.unmaskingList = new List<string>();
        }

        public IscsiUnmaskingList(List<string> unmaskingList)
        {
            this.unmaskingList = unmaskingList;
            base.type = UnmaskingListType.IScsi;
        }

        public IscsiUnmaskingList(string[] unmaskingListStringArray)
        {
            this.unmaskingList = new List<string>();
            foreach (string str in unmaskingListStringArray)
            {
                this.unmaskingList.Add(str);
            }
            base.type = UnmaskingListType.IScsi;
        }

        public override string ToString()
        {
            if (this.unmaskingList.Count == 0)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            foreach (string str in this.unmaskingList)
            {
                builder.AppendFormat("{0};", str);
            }
            return builder.ToString(0, builder.Length - 1);
        }

        public override bool UnmaskAll
        {
            get
            {
                return base.unmaskAll;
            }
            set
            {
                base.unmaskAll = value;
                this.unmaskingList.Clear();
            }
        }

        public List<string> UnmaskingList
        {
            get
            {
                return this.unmaskingList;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                this.unmaskingList = value;
                base.unmaskAll = false;
            }
        }
    }
}

