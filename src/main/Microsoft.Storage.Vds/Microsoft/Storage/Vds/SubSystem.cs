namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Threading;

    public class SubSystem : Wrapper, IDisposable
    {
        private IVdsSubSystemImportTarget importTarget;
        private IVdsMaintenance maintenance;
        private bool refresh;
        private IVdsSubSystem subSystem;
        private IVdsSubSystem2 subSystem2;
        private IVdsSubSystemInterconnect subSystemInterconnect;
        private IVdsSubSystemIscsi subSystemIscsi;
        private IVdsSubSystemNaming subSystemNaming;
        private SubSystemProperties2 subSystemProp;

        public SubSystem()
        {
            this.refresh = true;
            this.subSystem = null;
            this.maintenance = null;
            this.subSystemIscsi = null;
        }

        public SubSystem(object subSystemUnknown, IVdsService vdsService) : base(subSystemUnknown, vdsService)
        {
            this.refresh = true;
            this.InitializeComInterfaces();
        }

        public Async BeginCreateLun(LunType type, ulong sizeInBytes, Guid[] driveIdArray, UnmaskingList unmaskingList, LunHints hints, AsyncCallback callback, object state)
        {
            IVdsAsync async;
            if (unmaskingList == null)
            {
                throw new ArgumentNullException("unmaskingList");
            }
            if (hints == null)
            {
                throw new ArgumentNullException("hints");
            }
            Hints hints2 = hints.Hints;
            this.InitializeComInterfaces();
            try
            {
                this.subSystem.CreateLun(type, sizeInBytes, (driveIdArray.Length != 0) ? driveIdArray : null, driveIdArray.Length, unmaskingList.ToString(), ref hints2, out async);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsSubSystem::CreateLun failed.", exception);
            }
            Async async2 = new Async(async, callback, state);
            new Thread(new ThreadStart(async2.Wait)).Start();
            return async2;
        }

        public Async BeginCreateTarget(string iscsiName, string friendlyName, AsyncCallback callback, object state)
        {
            IVdsAsync async;
            this.InitializeComInterfaces();
            if (this.subSystemIscsi == null)
            {
                throw new NotSupportedException("IScsi methods are not supported on this subsystem");
            }
            try
            {
                this.subSystemIscsi.CreateTarget(iscsiName, friendlyName, out async);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsSubSystemIscsi::CreateTarget failed.", exception);
            }
            Async async2 = new Async(async, callback, state);
            new Thread(new ThreadStart(async2.Wait)).Start();
            return async2;
        }

        public Lun EndCreateLun(IAsyncResult asyncResult)
        {
            IVdsLun lun;
            this.InitializeComInterfaces();
            try
            {
                uint num;
                VDS_ASYNC_OUTPUT vds_async_output;
                Async async = (Async) asyncResult;
                if (async == null)
                {
                    throw new ArgumentNullException("asyncResult");
                }
                InteropHelpers.QueryInterfaceThrow<IVdsAsync>(async.ComUnknown).Wait(out num, out vds_async_output);
                if (num != 0)
                {
                    throw new VdsException("Create lun failed with the following error code: " + num);
                }
                try
                {
                    lun = InteropHelpers.QueryInterfaceThrow<IVdsLun>(Marshal.GetUniqueObjectForIUnknown(vds_async_output.Info.Cl.LunUnknown));
                }
                finally
                {
                    Marshal.Release(vds_async_output.Info.Cl.LunUnknown);
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsAsync::Wait failed.", exception);
            }
            catch (InvalidCastException exception2)
            {
                throw new VdsException("QueryInterface for IVdsLun failed.", exception2);
            }
            return new Lun(lun, base.VdsService);
        }

        public Target EndCreateTarget(IAsyncResult asyncResult)
        {
            IVdsIscsiTarget objectForIUnknown;
            this.InitializeComInterfaces();
            try
            {
                uint num;
                VDS_ASYNC_OUTPUT vds_async_output;
                Async async = (Async) asyncResult;
                if (async == null)
                {
                    throw new ArgumentNullException("asyncResult");
                }
                InteropHelpers.QueryInterfaceThrow<IVdsAsync>(async.ComUnknown).Wait(out num, out vds_async_output);
                if (num != 0)
                {
                    throw new VdsException("Create target failed with the following error code: " + num);
                }
                objectForIUnknown = (IVdsIscsiTarget) Marshal.GetObjectForIUnknown(vds_async_output.Info.Ct.TargetUnknown);
                try
                {
                    objectForIUnknown = (IVdsIscsiTarget) Marshal.GetObjectForIUnknown(vds_async_output.Info.Ct.TargetUnknown);
                }
                finally
                {
                    Marshal.Release(vds_async_output.Info.Ct.TargetUnknown);
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsAsync::Wait failed.", exception);
            }
            catch (InvalidCastException exception2)
            {
                throw new VdsException("QueryInterface for IVdsIscsiTarget failed.", exception2);
            }
            return new Target(objectForIUnknown, base.VdsService);
        }

        public Drive GetDrive(ushort busNumber, ushort slotNumber)
        {
            IVdsDrive drive;
            this.InitializeComInterfaces();
            try
            {
                this.subSystem.GetDrive(busNumber, slotNumber, out drive);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsSubSystem::GetDrives failed.", exception);
            }
            return new Drive(drive, base.VdsService);
        }

        public override void InitializeComInterfaces()
        {
            base.InitializeComInterfaces();
            if (this.subSystem == null)
            {
                this.subSystem = InteropHelpers.QueryInterface<IVdsSubSystem>(base.ComUnknown);
                if (this.subSystem == null)
                {
                    throw new VdsException("QueryInterface for IVdsSubSystem failed.");
                }
            }
            if (this.subSystem2 == null)
            {
                this.subSystem2 = InteropHelpers.QueryInterface<IVdsSubSystem2>(base.ComUnknown);
            }
            if (this.maintenance == null)
            {
                this.maintenance = InteropHelpers.QueryInterface<IVdsMaintenance>(base.ComUnknown);
            }
            if (this.subSystemIscsi == null)
            {
                this.subSystemIscsi = InteropHelpers.QueryInterface<IVdsSubSystemIscsi>(base.ComUnknown);
            }
            if (this.importTarget == null)
            {
                this.importTarget = InteropHelpers.QueryInterface<IVdsSubSystemImportTarget>(base.ComUnknown);
            }
            if (this.subSystemNaming == null)
            {
                this.subSystemNaming = InteropHelpers.QueryInterface<IVdsSubSystemNaming>(base.ComUnknown);
            }
            if (this.subSystemInterconnect == null)
            {
                this.subSystemInterconnect = InteropHelpers.QueryInterface<IVdsSubSystemInterconnect>(base.ComUnknown);
            }
        }

        public override void InitializeProperties()
        {
            this.InitializeComInterfaces();
            try
            {
                if (this.refresh)
                {
                    if (this.subSystem2 != null)
                    {
                        this.subSystem2.GetProperties2(out this.subSystemProp);
                    }
                    else
                    {
                        SubSystemProperties properties;
                        this.subSystem.GetProperties(out properties);
                        this.subSystemProp = new SubSystemProperties2(properties);
                    }
                    this.refresh = false;
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsSubSystem::GetProperties failed.", exception);
            }
        }

        public void PulseMaintenance(MaintenanceOperation operation, uint count)
        {
            this.InitializeComInterfaces();
            if (this.maintenance == null)
            {
                throw new NotSupportedException("The subsystem does not support maintenance operations.");
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

        public ulong QueryMaxLunCreateSize(LunType type, Guid[] driveIdArray, LunHints hints)
        {
            ulong num;
            if (hints == null)
            {
                throw new ArgumentNullException("hints");
            }
            Hints hints2 = hints.Hints;
            this.InitializeComInterfaces();
            try
            {
                this.subSystem.QueryMaxLunCreateSize(type, (driveIdArray.Length != 0) ? driveIdArray : null, driveIdArray.Length, ref hints2, out num);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsSubSystem::QueryMaxLunCreateSize failed.", exception);
            }
            return num;
        }

        public void Reenumerate()
        {
            this.InitializeComInterfaces();
            try
            {
                this.subSystem.Reenumerate();
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsSubSystem::Reenumerate failed.", exception);
            }
        }

        public void Refresh()
        {
            this.refresh = true;
        }

        public void ReplaceDrive(Guid driveToBeReplaced, Guid replacementDrive)
        {
            this.InitializeComInterfaces();
            try
            {
                this.subSystem.ReplaceDrive(driveToBeReplaced, replacementDrive);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsSubSystem::ReplaceDrive failed.", exception);
            }
        }

        public void SetIpsecGroupPresharedKey(SecureString ipsecKey)
        {
            if (ipsecKey == null)
            {
                throw new ArgumentNullException("ipsecKey", "ipsecKey cannot be null");
            }
            this.InitializeComInterfaces();
            SecureStringAnsiHandle handle = new SecureStringAnsiHandle(ipsecKey);
            try
            {
                IpsecKey key = new IpsecKey(handle);
                this.subSystemIscsi.SetIpsecGroupPresharedKey(ref key);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to SetIpsecGroupPresharedKey failed.", exception);
            }
            finally
            {
                if (handle != null)
                {
                    handle.Dispose();
                }
            }
        }

        public void StartMaintenance(MaintenanceOperation operation)
        {
            this.InitializeComInterfaces();
            if (this.maintenance == null)
            {
                throw new NotSupportedException("The subsystem does not support maintenance operations.");
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
                throw new NotSupportedException("The subsystem does not support maintenance operations.");
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

        public Collection<Controller> Controllers
        {
            get
            {
                Collection<Controller> collection;
                this.InitializeComInterfaces();
                try
                {
                    IEnumVdsObject obj2;
                    this.subSystem.QueryControllers(out obj2);
                    collection = new Collection<Controller>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsSubSystem::QueryControllers failed.", exception);
                }
                return collection;
            }
        }

        public Collection<Drive> Drives
        {
            get
            {
                Collection<Drive> collection;
                this.InitializeComInterfaces();
                try
                {
                    IEnumVdsObject obj2;
                    this.subSystem.QueryDrives(out obj2);
                    collection = new Collection<Drive>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsSubSystem::QueryDrives failed.", exception);
                }
                return collection;
            }
        }

        public SubSystemFlags Flags
        {
            get
            {
                this.InitializeProperties();
                return this.subSystemProp.Flags;
            }
        }

        public string FriendlyName
        {
            get
            {
                this.InitializeProperties();
                return this.subSystemProp.FriendlyName;
            }
            set
            {
                this.InitializeComInterfaces();
                if (this.subSystemNaming == null)
                {
                    throw new NotSupportedException("Sub system naming is not supported on this sub system");
                }
                try
                {
                    this.subSystemNaming.SetFriendlyName(value);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsSubSystemNaming::SetFriendlyName failed.", exception);
                }
            }
        }

        public Microsoft.Storage.Vds.Health Health
        {
            get
            {
                this.InitializeProperties();
                return this.subSystemProp.Health;
            }
        }

        public Guid Id
        {
            get
            {
                this.InitializeProperties();
                return this.subSystemProp.Id;
            }
        }

        public string Identification
        {
            get
            {
                this.InitializeProperties();
                return this.subSystemProp.Identification;
            }
        }

        public string ImportTargetName
        {
            get
            {
                string str;
                this.InitializeComInterfaces();
                if (this.importTarget == null)
                {
                    throw new NotSupportedException("iSCSI is not supported on this subsystem");
                }
                try
                {
                    this.importTarget.GetImportTarget(out str);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsIscsiImportTarget::GetImportTarget failed.", exception);
                }
                return str;
            }
            set
            {
                this.InitializeComInterfaces();
                if (this.importTarget == null)
                {
                    throw new NotSupportedException("iSCSI is not supported on this subsystem");
                }
                try
                {
                    this.importTarget.SetImportTarget(value);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsIscsiImportTarget::SetImportTarget failed.", exception);
                }
            }
        }

        public SubSystemInterconnectFlags InterconnectFlags
        {
            get
            {
                SubSystemInterconnectFlags none = SubSystemInterconnectFlags.None;
                this.InitializeComInterfaces();
                if (this.subSystemInterconnect != null)
                {
                    try
                    {
                        this.subSystemInterconnect.GetSupportedInterconnects(out none);
                    }
                    catch (COMException exception)
                    {
                        throw new VdsException("The call to IVdsSubSystemInterconnect::GetSupportedInterconnects failed", exception);
                    }
                }
                return none;
            }
        }

        public Collection<Lun> Luns
        {
            get
            {
                Collection<Lun> collection;
                this.InitializeComInterfaces();
                try
                {
                    IEnumVdsObject obj2;
                    this.subSystem.QueryLuns(out obj2);
                    collection = new Collection<Lun>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsSubSystem::QueryLuns failed.", exception);
                }
                return collection;
            }
        }

        public short MaxNumberOfControllers
        {
            get
            {
                this.InitializeProperties();
                return this.subSystemProp.MaxNumberOfControllers;
            }
        }

        public short MaxNumberOfSlotsEachBus
        {
            get
            {
                this.InitializeProperties();
                return this.subSystemProp.MaxNumberOfSlotsEachBus;
            }
        }

        public uint NumberOfEnclosures
        {
            get
            {
                this.InitializeProperties();
                return this.subSystemProp.NumberOfEnclosures;
            }
        }

        public short NumberOfInternalBuses
        {
            get
            {
                this.InitializeProperties();
                return this.subSystemProp.NumberOfInternalBuses;
            }
        }

        public Collection<Portal> Portals
        {
            get
            {
                Collection<Portal> collection;
                this.InitializeComInterfaces();
                if (this.subSystemIscsi == null)
                {
                    throw new NotSupportedException("IScsi methods are not supported on this subsystem");
                }
                try
                {
                    IEnumVdsObject obj2;
                    this.subSystemIscsi.QueryPortals(out obj2);
                    collection = new Collection<Portal>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsSubSystemIscsi::QueryPortals failed.", exception);
                }
                return collection;
            }
        }

        public HardwareProvider Provider
        {
            get
            {
                HardwareProvider provider;
                this.InitializeComInterfaces();
                try
                {
                    IVdsProvider provider2;
                    this.subSystem.GetProvider(out provider2);
                    provider = new HardwareProvider(provider2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsSubSystem::GetProvider failed.", exception);
                }
                return provider;
            }
        }

        public short RebuildPriority
        {
            get
            {
                this.InitializeProperties();
                return this.subSystemProp.RebuildPriority;
            }
        }

        public SubSystemStatus Status
        {
            get
            {
                this.InitializeProperties();
                return this.subSystemProp.Status;
            }
            set
            {
                this.InitializeComInterfaces();
                try
                {
                    this.subSystem.SetStatus(value);
                }
                catch (COMException exception)
                {
                    throw new VdsException("Call to IVdsSubSystem::SetStatus failed.", exception);
                }
            }
        }

        public uint StripeSizeFlags
        {
            get
            {
                this.InitializeProperties();
                return this.subSystemProp.StripeSizeFlags;
            }
        }

        public SubSystemSupportedRaidTypes SupportedRaidTypes
        {
            get
            {
                this.InitializeProperties();
                return this.subSystemProp.SupportedRaidTypes;
            }
        }

        public Collection<Target> Targets
        {
            get
            {
                Collection<Target> collection;
                this.InitializeComInterfaces();
                if (this.subSystemIscsi == null)
                {
                    throw new NotSupportedException("IScsi methods are not supported on this subsystem");
                }
                try
                {
                    IEnumVdsObject obj2;
                    this.subSystemIscsi.QueryTargets(out obj2);
                    collection = new Collection<Target>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsSubSystemIscsi::QueryTargets failed.", exception);
                }
                return collection;
            }
        }
    }
}

