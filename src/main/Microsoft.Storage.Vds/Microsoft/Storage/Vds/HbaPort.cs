namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Runtime.InteropServices;

    public class HbaPort : Wrapper, IDisposable
    {
        private IVdsHbaPort port;
        private HbaPortProperties portProp;
        private bool refresh;

        public HbaPort()
        {
            this.refresh = true;
            this.port = null;
        }

        public HbaPort(object comUnknown, IVdsService vdsService) : base(comUnknown, vdsService)
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
                    this.port = (IVdsHbaPort) base.ComUnknown;
                }
                catch (InvalidCastException exception)
                {
                    throw new VdsException("QueryInterface for IVdsHbaPort failed.", exception);
                }
            }
        }

        public override void InitializeProperties()
        {
            this.InitializeComInterfaces();
            if (this.refresh)
            {
                try
                {
                    this.port.GetProperties(out this.portProp);
                    this.refresh = false;
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsHbaPort::GetProperties failed.", exception);
                }
            }
        }

        public void Refresh()
        {
            this.refresh = true;
        }

        public void SetAllPathStatuses(PathStatus status)
        {
            this.InitializeComInterfaces();
            try
            {
                this.port.SetAllPathStatuses(status);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsHbaPort::SetAllPathStatuses failed.", exception);
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

        public WorldWideName NodeWwn
        {
            get
            {
                this.InitializeProperties();
                return this.portProp.NodeWwn;
            }
        }

        public HbaPortSpeedFlags PortSpeed
        {
            get
            {
                this.InitializeProperties();
                return this.portProp.PortSpeed;
            }
        }

        public WorldWideName PortWwn
        {
            get
            {
                this.InitializeProperties();
                return this.portProp.PortWwn;
            }
        }

        public HbaPortStatus Status
        {
            get
            {
                this.InitializeProperties();
                return this.portProp.Status;
            }
        }

        public HbaPortSpeedFlags SupportedPortSpeed
        {
            get
            {
                this.InitializeProperties();
                return this.portProp.SupportedPortSpeed;
            }
        }

        public HbaPortType Type
        {
            get
            {
                this.InitializeProperties();
                return this.portProp.Type;
            }
        }
    }
}

