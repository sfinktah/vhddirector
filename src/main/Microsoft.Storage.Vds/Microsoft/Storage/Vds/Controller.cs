namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Runtime.InteropServices;

    public class Controller : Wrapper, IDisposable
    {
        private IVdsController controller;
        private IVdsControllerControllerPort controllerControllerPort;
        private ControllerProperties controllerProp;
        private IVdsMaintenance maintenance;
        private bool refresh;

        public Controller()
        {
            this.refresh = true;
            this.controller = null;
        }

        public Controller(object comUnknown, IVdsService vdsService) : base(comUnknown, vdsService)
        {
            this.refresh = true;
            this.InitializeComInterfaces();
        }

        public void FlushCache()
        {
            this.InitializeComInterfaces();
            try
            {
                this.controller.FlushCache();
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsController::FlushCache failed.", exception);
            }
        }

        public override void InitializeComInterfaces()
        {
            base.InitializeComInterfaces();
            if (this.controller == null)
            {
                try
                {
                    this.controller = (IVdsController) base.ComUnknown;
                }
                catch (InvalidCastException exception)
                {
                    throw new VdsException("QueryInterface for IVdsController failed.", exception);
                }
            }
            if (this.controllerControllerPort == null)
            {
                try
                {
                    this.controllerControllerPort = (IVdsControllerControllerPort) base.ComUnknown;
                }
                catch (InvalidCastException)
                {
                }
            }
            if (this.maintenance == null)
            {
                try
                {
                    this.maintenance = (IVdsMaintenance) base.ComUnknown;
                }
                catch (InvalidCastException)
                {
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
                    this.controller.GetProperties(out this.controllerProp);
                    this.refresh = false;
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsController::GetProperties failed.", exception);
            }
        }

        public void InvalidateCache()
        {
            this.InitializeComInterfaces();
            try
            {
                this.controller.InvalidateCache();
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsController::InvalidateCache failed.", exception);
            }
        }

        public void PulseMaintenance(MaintenanceOperation operation, uint count)
        {
            this.InitializeComInterfaces();
            if (this.maintenance == null)
            {
                throw new NotSupportedException("The controller does not support maintenance operations.");
            }
            try
            {
                this.maintenance.PulseMaintenance(operation, count);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsMaintenance::PulseMaintenance failed.", exception);
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
                this.controller.Reset();
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsController::Reset failed.", exception);
            }
        }

        public void StartMaintenance(MaintenanceOperation operation)
        {
            this.InitializeComInterfaces();
            if (this.maintenance == null)
            {
                throw new NotSupportedException("The controller does not support maintenance operations.");
            }
            try
            {
                this.maintenance.StartMaintenance(operation);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsMaintenance::StartMaintenance failed.", exception);
            }
        }

        public void StopMaintenance(MaintenanceOperation operation)
        {
            this.InitializeComInterfaces();
            if (this.maintenance == null)
            {
                throw new NotSupportedException("The controller does not support maintenance operations.");
            }
            try
            {
                this.maintenance.StopMaintenance(operation);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsMaintenance::StopMaintenance failed.", exception);
            }
        }

        public string FriendlyName
        {
            get
            {
                this.InitializeProperties();
                return this.controllerProp.FriendlyName;
            }
        }

        public Microsoft.Storage.Vds.Health Health
        {
            get
            {
                this.InitializeProperties();
                return this.controllerProp.Health;
            }
        }

        public Guid Id
        {
            get
            {
                this.InitializeProperties();
                return this.controllerProp.Id;
            }
        }

        public string Identification
        {
            get
            {
                this.InitializeProperties();
                return this.controllerProp.Identification;
            }
        }

        public ushort NumberOfPorts
        {
            get
            {
                this.InitializeProperties();
                return this.controllerProp.NumberOfPorts;
            }
        }

        public Collection<ControllerPort> Ports
        {
            get
            {
                Collection<ControllerPort> collection;
                this.InitializeComInterfaces();
                if (this.controllerControllerPort == null)
                {
                    throw new NotSupportedException("The LUN does not support query of controller ports.");
                }
                try
                {
                    IEnumVdsObject obj2;
                    this.controllerControllerPort.QueryControllerPorts(out obj2);
                    collection = new Collection<ControllerPort>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsControllerControllerPort::QueryControllerPorts failed.", exception);
                }
                return collection;
            }
        }

        public ControllerStatus Status
        {
            get
            {
                this.InitializeProperties();
                return this.controllerProp.status;
            }
            set
            {
                this.InitializeComInterfaces();
                try
                {
                    this.controller.SetStatus(value);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsController::SetStatus failed.", exception);
                }
                this.refresh = true;
            }
        }

        public Microsoft.Storage.Vds.SubSystem SubSystem
        {
            get
            {
                Microsoft.Storage.Vds.SubSystem system;
                this.InitializeComInterfaces();
                try
                {
                    IVdsSubSystem system2;
                    this.controller.GetSubSystem(out system2);
                    system = new Microsoft.Storage.Vds.SubSystem(system2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsController::GetSubSystem failed.", exception);
                }
                return system;
            }
        }
    }
}

