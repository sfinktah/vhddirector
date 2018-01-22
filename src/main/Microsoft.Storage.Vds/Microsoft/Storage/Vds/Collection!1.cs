namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Advanced;
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Collections;

    public class Collection<T> : IEnumerable where T: Wrapper, new()
    {
        private IEnumVdsObject vdsEnumerator;
        private IVdsService vdsService;

        public Collection(IEnumVdsObject vdsEnumerator, IVdsService vdsService)
        {
            this.vdsEnumerator = vdsEnumerator;
            this.vdsService = vdsService;
        }

        public Enumerator<T> GetEnumerator()
        {
            return new Enumerator<T>(this.vdsEnumerator, this.vdsService);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public class Enumerator<S> : IEnumerator where S: Wrapper, new()
        {
            private object currentObject;
            private IEnumVdsObject vdsEnumerator;
            private IVdsService vdsService;

            public Enumerator(IEnumVdsObject vdsEnumerator, IVdsService vdsService)
            {
                this.vdsEnumerator = vdsEnumerator;
                this.vdsService = vdsService;
            }

            public bool MoveNext()
            {
                uint num;
                this.vdsEnumerator.Next(1, out this.currentObject, out num);
                return (0 != num);
            }

            public void Reset()
            {
                this.vdsEnumerator.Reset();
                this.currentObject = null;
            }

            public object Current
            {
                get
                {
                    if (typeof(S) == typeof(Provider))
                    {
                        if (this.currentObject is IVdsHwProvider)
                        {
                            return new HardwareProvider(this.currentObject, this.vdsService);
                        }
                        if (!(this.currentObject is IVdsSwProvider))
                        {
                            throw new VdsException("Unknown provider type returned from IVdsProvider::GetProperties");
                        }
                        return new SoftwareProvider(this.currentObject, this.vdsService);
                    }
                    if (typeof(S) == typeof(Disk))
                    {
                        return new AdvancedDisk(this.currentObject, this.vdsService);
                    }
                    S local = Activator.CreateInstance<S>();
                    local.ComUnknown = this.currentObject;
                    local.VdsService = this.vdsService;
                    local.InitializeComInterfaces();
                    return local;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }
        }
    }
}

