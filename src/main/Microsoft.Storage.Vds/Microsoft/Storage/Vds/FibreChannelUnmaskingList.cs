namespace Microsoft.Storage.Vds
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class FibreChannelUnmaskingList : UnmaskingList
    {
        private List<WorldWideName> wwnList;

        public FibreChannelUnmaskingList()
        {
            base.type = UnmaskingListType.FibreChannel;
            this.wwnList = new List<WorldWideName>();
        }

        public FibreChannelUnmaskingList(bool unmaskAll) : base(unmaskAll)
        {
            base.type = UnmaskingListType.FibreChannel;
            this.wwnList = new List<WorldWideName>();
        }

        public FibreChannelUnmaskingList(List<WorldWideName> wwnList)
        {
            this.wwnList = wwnList;
            base.type = UnmaskingListType.FibreChannel;
        }

        public FibreChannelUnmaskingList(string[] unmaskingListStringArray)
        {
            this.wwnList = new List<WorldWideName>();
            foreach (string str in unmaskingListStringArray)
            {
                this.wwnList.Add(new WorldWideName(str));
            }
            base.type = UnmaskingListType.FibreChannel;
        }

        public override string ToString()
        {
            if (this.wwnList.Count == 0)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            foreach (WorldWideName name in this.wwnList)
            {
                builder.AppendFormat("{0};", name.ToString());
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
                this.wwnList.Clear();
            }
        }

        public List<WorldWideName> WwnList
        {
            get
            {
                return this.wwnList;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                this.wwnList = value;
                base.unmaskAll = false;
            }
        }
    }
}

