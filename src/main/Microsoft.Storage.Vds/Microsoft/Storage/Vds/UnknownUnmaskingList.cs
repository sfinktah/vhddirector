namespace Microsoft.Storage.Vds
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UnknownUnmaskingList : Microsoft.Storage.Vds.UnmaskingList
    {
        private List<string> unmaskingList = new List<string>();

        public UnknownUnmaskingList(string[] unmaskingListStringArray)
        {
            foreach (string str in unmaskingListStringArray)
            {
                this.unmaskingList.Add(str);
            }
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

