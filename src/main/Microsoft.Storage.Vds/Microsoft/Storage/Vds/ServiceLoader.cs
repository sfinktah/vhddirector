namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Globalization;
    using System.Reflection;

    public class ServiceLoader : Wrapper, IDisposable
    {
        private const string GetInstanceMethodName = "GetInstance";
        private const string mockDllAssemblyName = "Microsoft.Storage.Vds.TestModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
        private IVdsServiceLoader serviceLoader;
        private const string TmVdsLoaderClassName = "Microsoft.Storage.Vds.TestModel.TmVdsLoader";

        public ServiceLoader()
        {
            if (Globals.IsMockVdsAvailable)
            {
                this.serviceLoader = this.GetMockVdsServiceLoader();
            }
            if (this.serviceLoader == null)
            {
                this.serviceLoader = (IVdsServiceLoader) new VdsServiceLoaderClass();
                Globals.IsMockVdsAvailable = false;
            }
            base.ComUnknown = this.serviceLoader;
        }

        private IVdsServiceLoader GetMockVdsServiceLoader()
        {
            IVdsServiceLoader mockObject = null;
            Assembly assembly = null;
            try
            {
                assembly = Assembly.Load("Microsoft.Storage.Vds.TestModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
            }
            catch (Exception exception)
            {
                if (VdsException.IsFatalException(exception))
                {
                    throw;
                }
            }
            Type type = null;
            if (assembly != null)
            {
                type = assembly.GetType("Microsoft.Storage.Vds.TestModel.TmVdsLoader", false);
            }
            if (type != null)
            {
                try
                {
                    IMockObject obj3 = type.InvokeMember("GetInstance", BindingFlags.InvokeMethod, null, null, new object[0], CultureInfo.InvariantCulture) as IMockObject;
                    if (obj3 != null)
                    {
                        mockObject = obj3.GetMockObject() as IVdsServiceLoader;
                    }
                }
                catch (Exception exception2)
                {
                    if (VdsException.IsFatalException(exception2))
                    {
                        throw;
                    }
                }
            }
            return mockObject;
        }

        public override void InitializeComInterfaces()
        {
            base.InitializeComInterfaces();
            try
            {
                if (this.serviceLoader == null)
                {
                    this.serviceLoader = InteropHelpers.QueryInterface<IVdsServiceLoader>(base.ComUnknown);
                }
            }
            catch (Exception exception)
            {
                if (VdsException.IsFatalException(exception))
                {
                    throw;
                }
                throw new VdsException("QueryInterface for IVdsServiceLoader failed.", exception);
            }
        }

        public Service LoadService(string machineName)
        {
            IVdsService service;
            this.serviceLoader.LoadService(machineName, out service);
            return new Service(service);
        }
    }
}

