namespace Microsoft.Storage.Vds
{
    using System;

    public class UnmaskingList
    {
        public static readonly UnmaskingList All = new UnmaskingList(true);
        public static readonly UnmaskingList None = new UnmaskingList();
        protected UnmaskingListType type;
        protected bool unmaskAll;

        protected UnmaskingList()
        {
            this.unmaskAll = false;
            this.type = UnmaskingListType.Unknown;
        }

        protected UnmaskingList(bool unmaskAll)
        {
            this.unmaskAll = unmaskAll;
            this.type = UnmaskingListType.Unknown;
        }

        public override string ToString()
        {
            if (this.unmaskAll)
            {
                return "*";
            }
            return string.Empty;
        }

        public UnmaskingListType Type
        {
            get
            {
                return this.type;
            }
        }

        public virtual bool UnmaskAll
        {
            get
            {
                return this.unmaskAll;
            }
            set
            {
                this.unmaskAll = value;
            }
        }
    }
}

