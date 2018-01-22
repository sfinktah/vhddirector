namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Runtime.InteropServices;

    public class ControllerPort : Wrapper, IDisposable
    {
        private IVdsControllerPort port;
        private PortProperties portProp;
        private bool refresh;

        public ControllerPort()
        {
            this.refresh = true;
            this.port = null;
        }

        public ControllerPort(object comUnknown, IVdsService vdsService) : base(comUnknown, vdsService)
        {
            this.refresh = true;
            this.InitializeComInterfaces();
        }

        public override void InitializeComInterfaces()
        {
            base.InitializeComInterfaces();
            if (this.port == null)
            {
                try
                {
                    this.port = (IVdsControllerPort) base.ComUnknown;
                }
                catch (InvalidCastException exception)
                {
                    throw new VdsException("QueryInterface for IVdsControllerPort failed.", exception);
                }
            }
        }

        public override void InitializeProperties()
        {
            this.InitializeComInterfaces();
            try
            {
                if (this.refresh)
                {
                    this.port.GetProperties(out this.portProp);
                    this.refresh = false;
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsControllerPort::GetProperties failed.", exception);
            }
        }

        public void Refresh()
        {
            this.refresh = true;
        }

        public void Reset()
        {
            this.InitializeComInterfaces();
            try
            {
                this.port.Reset();
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsControllerPort::Reset failed.", exception);
            }
        }

        public Collection<Lun> AssociatedLuns
        {
            get
            {
                Collection<Lun> collection;
                try
                {
                    IEnumVdsObject obj2;
                    this.port.QueryAssociatedLuns(out obj2);
                    collection = new Collection<Lun>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsControllerPort::QueryAssociatedLuns failed.", exception);
                }
                return collection;
            }
        }

        public Microsoft.Storage.Vds.Controller Controller
        {
            get
            {
                Microsoft.Storage.Vds.Controller controller;
                this.InitializeComInterfaces();
                try
                {
                    IVdsController controller2;
                    this.port.GetController(out controller2);
                    controller = new Microsoft.Storage.Vds.Controller(controller2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsControllerPort::GetController failed.", exception);
                }
                return controller;
            }
        }

        public string FriendlyName
        {
            get
            {
                this.InitializeProperties();
                return this.portProp.FriendlyName;
            }
        }

        public Guid Id
        {
            get
            {
                this.InitializeProperties();
                return this.portProp.Id;
            }
        }

        public string Identification
        {
            get
            {
                this.InitializeProperties();
                return this.portProp.Identification;
            }
        }

        public PortStatus Status
        {
            get
            {
                this.InitializeProperties();
                return this.portProp.Status;
            }
            set
            {
                this.InitializeComInterfaces();
                try
                {
                    this.port.SetStatus(value);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsControllerPort::SetStatus failed.", exception);
                }
            }
        }
    }
}

