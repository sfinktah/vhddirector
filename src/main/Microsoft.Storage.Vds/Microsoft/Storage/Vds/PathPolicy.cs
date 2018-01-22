namespace Microsoft.Storage.Vds
{
    using System;

    public class PathPolicy
    {
        private VdsPathPolicy pathPolicy;

        public PathPolicy(VdsPathPolicy pathPolicy)
        {
            this.pathPolicy = pathPolicy;
        }

        public FullPathId Id
        {
            get
            {
                return this.pathPolicy.PathId;
            }
        }

        public bool IsPrimaryPath
        {
            get
            {
                return (this.pathPolicy.PrimaryPath == 1);
            }
            set
            {
                this.pathPolicy.PrimaryPath = value ? 1U : 0U;
            }
        }

        public uint Weight
        {
            get
            {
                return this.pathPolicy.Weight;
            }
            set
            {
                this.pathPolicy.Weight = value;
            }
        }
    }
}

