namespace Microsoft.Storage.Vds
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class LoadBalancePolicy
    {
        private VdsPathPolicy[] paths;
        private LoadBalancePolicyType type;

        public LoadBalancePolicy(LoadBalancePolicyType type, VdsPathPolicy[] pathPolicies)
        {
            this.type = type;
            this.paths = pathPolicies;
        }

        public PathPolicy this[FullPathId index]
        {
            get
            {
                for (int i = 0; i < this.paths.Length; i++)
                {
                    if ((this.paths[i].PathId.PathId == index.PathId) && (this.paths[i].PathId.SourceId == index.SourceId))
                    {
                        return new PathPolicy(this.paths[i]);
                    }
                }
                throw new InvalidOperationException("The specified path id does not exist");
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                for (int i = 0; i < this.paths.Length; i++)
                {
                    if ((this.paths[i].PathId.PathId == index.PathId) && (this.paths[i].PathId.SourceId == index.SourceId))
                    {
                        this.paths[i].Weight = value.Weight;
                        this.paths[i].PrimaryPath = value.IsPrimaryPath ? 1U : 0U;
                    }
                }
                throw new InvalidOperationException("The specified path id does not exist");
            }
        }

        public List<PathPolicy> PathPolicies
        {
            get
            {
                List<PathPolicy> list = new List<PathPolicy>(this.paths.Length);
                for (int i = 0; i < this.paths.Length; i++)
                {
                    list.Add(new PathPolicy(this.paths[i]));
                }
                return list;
            }
        }

        public LoadBalancePolicyType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                if (value == LoadBalancePolicyType.Unknown)
                {
                    throw new ArgumentException("Must specify a known lbp type.");
                }
                value = this.type;
            }
        }

        public VdsPathPolicy[] VdsPathPolicies
        {
            get
            {
                return this.paths;
            }
        }
    }
}

