namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Advanced;
    using Microsoft.Storage.Vds.Interop;
    using Microsoft.Storage.Vds.Properties;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class Lun : Wrapper, IDisposable
    {
        private IVdsLun lun;
        private IVdsLunControllerPorts lunControllerPorts;
        private IVdsLunIscsi lunIscsi;
        private IVdsLunMpio lunMpio;
        private IVdsLunNaming lunNaming;
        private IVdsLunNumber lunNumber;
        private LunProperties lunProp;
        private IVdsMaintenance maintenance;
        private bool refresh;

        public Lun()
        {
            this.refresh = true;
            this.lun = null;
        }

        public Lun(object lunUnknown, IVdsService vdsService) : base(lunUnknown, vdsService)
        {
            this.refresh = true;
            this.InitializeComInterfaces();
        }

        public void AssociateControllerPorts(Guid[] activeControllerPortIdArray, Guid[] inactiveControllerPortIdArray)
        {
            this.InitializeComInterfaces();
            if (this.lunControllerPorts == null)
            {
                throw new NotSupportedException("The LUN does not support AssociateControllerPorts method.");
            }
            try
            {
                this.lunControllerPorts.AssociateControllerPorts(activeControllerPortIdArray, activeControllerPortIdArray.Length, inactiveControllerPortIdArray, inactiveControllerPortIdArray.Length);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsLunControllerPorts::AssociateControllerPorts failed.", exception);
            }
        }

        public void AssociateTargets(Guid[] targetIdArray)
        {
            this.InitializeComInterfaces();
            if (this.lunIscsi == null)
            {
                throw new NotSupportedException("The LUN does not support the AssociateTargets method.");
            }
            try
            {
                this.lunIscsi.AssociateTargets(targetIdArray, targetIdArray.Length);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsLunIscsi::AssociateTargets failed.", exception);
            }
        }

        public Async BeginAddPlex(Guid lunId, AsyncCallback callback, object state)
        {
            IVdsAsync async;
            this.InitializeComInterfaces();
            try
            {
                this.lun.AddPlex(lunId, out async);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsLun::AddPlex failed.", exception);
            }
            Async async2 = new Async(async, callback, state);
            new Thread(new ThreadStart(async2.Wait)).Start();
            return async2;
        }

        public Async BeginExtend(ulong numberOfBytesToAdd, Guid[] driveIdArray, AsyncCallback callback, object state)
        {
            IVdsAsync async;
            this.InitializeComInterfaces();
            try
            {
                this.lun.Extend(numberOfBytesToAdd, (driveIdArray.Length != 0) ? driveIdArray : null, driveIdArray.Length, out async);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsLun::Extend failed.", exception);
            }
            Async async2 = new Async(async, callback, state);
            new Thread(new ThreadStart(async2.Wait)).Start();
            return async2;
        }

        public Async BeginRecover(AsyncCallback callback, object state)
        {
            IVdsAsync async;
            this.InitializeComInterfaces();
            try
            {
                this.lun.Recover(out async);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsLun::Recover failed.", exception);
            }
            Async async2 = new Async(async, callback, state);
            new Thread(new ThreadStart(async2.Wait)).Start();
            return async2;
        }

        public Async BeginRemovePlex(Guid plexId, AsyncCallback callback, object state)
        {
            IVdsAsync async;
            this.InitializeComInterfaces();
            try
            {
                this.lun.RemovePlex(plexId, out async);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsLun::RemovePlex failed.", exception);
            }
            Async async2 = new Async(async, callback, state);
            new Thread(new ThreadStart(async2.Wait)).Start();
            return async2;
        }

        public Async BeginShrink(ulong numberOfBytesToRemove, AsyncCallback callback, object state)
        {
            IVdsAsync async;
            this.InitializeComInterfaces();
            try
            {
                this.lun.Shrink(numberOfBytesToRemove, out async);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsLun::Shrink failed.", exception);
            }
            Async async2 = new Async(async, callback, state);
            new Thread(new ThreadStart(async2.Wait)).Start();
            return async2;
        }

        public void Delete()
        {
            this.InitializeComInterfaces();
            try
            {
                this.lun.Delete();
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsLun::Delete failed.", exception);
            }
        }

        public void EndAddPlex(IAsyncResult asyncResult)
        {
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
                    throw new VdsException("Add plex failed with the following error code: " + num);
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsAsync::Wait failed.", exception);
            }
        }

        public void EndExtend(IAsyncResult asyncResult)
        {
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
                    throw new VdsException("Extend lun failed with the following error code: " + num);
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsAsync::Wait failed.", exception);
            }
        }

        public void EndRecover(IAsyncResult asyncResult)
        {
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
                    throw new VdsException("Recover lun failed with the following error code: " + num);
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsAsync::Wait failed.", exception);
            }
        }

        public void EndRemovePlex(IAsyncResult asyncResult)
        {
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
                    throw new VdsException("Remove plex failed with the following error code: " + num);
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsAsync::Wait failed.", exception);
            }
        }

        public void EndShrink(IAsyncResult asyncResult)
        {
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
                    throw new VdsException("Shrink lun failed with the following error code: " + num);
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsAsync::Wait failed.", exception);
            }
        }

        public LunInformation GetIdentificationData()
        {
            LunInformation information2;
            this.InitializeComInterfaces();
            try
            {
                VdsLunInformation information;
                this.lun.GetIdentificationData(out information);
                information2 = new LunInformation(information);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsLun::GetIdentificationData failed.", exception);
            }
            return information2;
        }

        public UnmaskingList GetUnmaskingList(HardwareProviderType providerType)
        {
            string[] strArray;
            this.InitializeProperties();
            string unmaskingList = this.lunProp.UnmaskingList;
            if (unmaskingList == null)
            {
                unmaskingList = string.Empty;
            }
            if (unmaskingList.Length != 0)
            {
                strArray = unmaskingList.Split(new char[] { ';' });
            }
            else
            {
                strArray = new string[0];
            }
            switch (providerType)
            {
                case HardwareProviderType.Unknown:
                    try
                    {
                        return new FibreChannelUnmaskingList(strArray);
                    }
                    catch (ArgumentException)
                    {
                        return new UnknownUnmaskingList(strArray);
                    }
                    goto Label_008D;

                case HardwareProviderType.FibreChannel:
                case HardwareProviderType.Sas:
                    try
                    {
                        return new FibreChannelUnmaskingList(strArray);
                    }
                    catch (ArgumentException)
                    {
                        return new UnknownUnmaskingList(strArray);
                    }
                    break;

                case HardwareProviderType.IScsi:
                    break;

                default:
                    goto Label_008D;
            }
            return new IscsiUnmaskingList(strArray);
        Label_008D:
            return UnmaskingList.None;
        }

        public override void InitializeComInterfaces()
        {
            base.InitializeComInterfaces();
            if (this.lun == null)
            {
                this.lun = InteropHelpers.QueryInterface<IVdsLun>(base.ComUnknown);
                if (this.lun == null)
                {
                    throw new VdsException("QueryInterface for IVdsLun failed.");
                }
            }
            if (this.lunNaming == null)
            {
                this.lunNaming = InteropHelpers.QueryInterface<IVdsLunNaming>(base.ComUnknown);
            }
            if (this.lunMpio == null)
            {
                this.lunMpio = InteropHelpers.QueryInterface<IVdsLunMpio>(base.ComUnknown);
            }
            if (this.lunControllerPorts == null)
            {
                this.lunControllerPorts = InteropHelpers.QueryInterface<IVdsLunControllerPorts>(base.ComUnknown);
            }
            if (this.lunIscsi == null)
            {
                this.lunIscsi = InteropHelpers.QueryInterface<IVdsLunIscsi>(base.ComUnknown);
            }
            if (this.maintenance == null)
            {
                this.maintenance = InteropHelpers.QueryInterface<IVdsMaintenance>(base.ComUnknown);
            }
            if (this.lunNumber == null)
            {
                this.lunNumber = InteropHelpers.QueryInterface<IVdsLunNumber>(base.ComUnknown);
            }
        }

        public override void InitializeProperties()
        {
            this.InitializeComInterfaces();
            try
            {
                if (this.refresh)
                {
                    this.lun.GetProperties(out this.lunProp);
                    this.refresh = false;
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsLun::GetProperties failed.", exception);
            }
        }

        public void PulseMaintenance(MaintenanceOperation operation, uint count)
        {
            this.InitializeComInterfaces();
            if (this.maintenance == null)
            {
                throw new NotSupportedException("The lun does not support maintenance operations.");
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

        public ulong QueryMaxLunExtendSize(Guid[] driveIdArray)
        {
            ulong num;
            this.InitializeComInterfaces();
            try
            {
                this.lun.QueryMaxLunExtendSize((driveIdArray.Length != 0) ? driveIdArray : null, driveIdArray.Length, out num);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsLun::QueryMaxLunExtendSize failed.", exception);
            }
            return num;
        }

        public void Refresh()
        {
            this.refresh = true;
        }

        public void SetUnmaskingList(UnmaskingList list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            this.InitializeComInterfaces();
            try
            {
                this.lun.SetMask(list.ToString());
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsLun::SetMask failed.", exception);
            }
            this.refresh = true;
        }

        public void StartMaintenance(MaintenanceOperation operation)
        {
            this.InitializeComInterfaces();
            if (this.maintenance == null)
            {
                throw new NotSupportedException("The lun does not support maintenance operations.");
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
                throw new NotSupportedException("The lun does not support maintenance operations.");
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

        public Collection<ControllerPort> ActiveControllerPorts
        {
            get
            {
                Collection<ControllerPort> collection;
                this.InitializeComInterfaces();
                if (this.lunControllerPorts == null)
                {
                    throw new NotSupportedException("The LUN does not support querying active ports.");
                }
                try
                {
                    IEnumVdsObject obj2;
                    this.lunControllerPorts.QueryActiveControllerPorts(out obj2);
                    collection = new Collection<ControllerPort>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsLunControllerPorts::QueryActivePorts failed.", exception);
                }
                return collection;
            }
        }

        public Collection<Target> AssociatedTargets
        {
            get
            {
                Collection<Target> collection;
                this.InitializeComInterfaces();
                if (this.lunIscsi == null)
                {
                    throw new NotSupportedException("The LUN does not support querying associated targets.");
                }
                try
                {
                    IEnumVdsObject obj2;
                    this.lunIscsi.QueryAssociatedTargets(out obj2);
                    collection = new Collection<Target>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsLunIscsi::QueryAssociatedTargets failed.", exception);
                }
                return collection;
            }
        }

        public Microsoft.Storage.Vds.Disk Disk
        {
            get
            {
                IVdsDisk disk;
                DiskProperties properties;
                object obj2;
                this.InitializeComInterfaces();
                try
                {
                    disk = InteropHelpers.QueryInterfaceThrow<IVdsDisk>(base.ComUnknown);
                }
                catch (InvalidCastException exception)
                {
                    throw new VdsException("The LUN does not have an associated disk object.", exception);
                }
                catch (COMException exception2)
                {
                    throw new VdsException("QueryInterface for IVdsDisk interface failed.", exception2);
                }
                try
                {
                    disk.GetProperties(out properties);
                }
                catch (COMException exception3)
                {
                    throw new VdsException("IVdsDisk::GetProperties failed.", exception3);
                }
                try
                {
                    base.VdsService.GetObject(properties.Id, ObjectType.Disk, out obj2);
                }
                catch (COMException exception4)
                {
                    throw new VdsException("GetObject for disk failed.", exception4);
                }
                return new AdvancedDisk(obj2, base.VdsService);
            }
        }

        public LunFlags Flags
        {
            get
            {
                this.InitializeProperties();
                return this.lunProp.Flags;
            }
        }

        public string FriendlyName
        {
            get
            {
                this.InitializeProperties();
                return this.lunProp.FriendlyName;
            }
            set
            {
                this.InitializeComInterfaces();
                if (this.lunNaming == null)
                {
                    throw new NotSupportedException("The LUN does not support setting it's friendly name.");
                }
                try
                {
                    this.lunNaming.SetFriendlyName(value);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsLunNaming::SetFriendlyName failed.", exception);
                }
            }
        }

        public Microsoft.Storage.Vds.Health Health
        {
            get
            {
                this.InitializeProperties();
                return this.lunProp.Health;
            }
        }

        public LunHints Hints
        {
            get
            {
                Microsoft.Storage.Vds.Hints hints;
                this.InitializeComInterfaces();
                try
                {
                    this.lun.QueryHints(out hints);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsLun::QueryHints failed.", exception);
                }
                return new LunHints(hints);
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                Microsoft.Storage.Vds.Hints hints = value.Hints;
                this.InitializeComInterfaces();
                try
                {
                    this.lun.ApplyHints(ref hints);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsLun::ApplyHints failed.", exception);
                }
                this.refresh = true;
            }
        }

        public Guid Id
        {
            get
            {
                this.InitializeProperties();
                return this.lunProp.Id;
            }
        }

        public string Identification
        {
            get
            {
                this.InitializeProperties();
                return this.lunProp.Identification;
            }
        }

        public bool IsLocalDisk
        {
            get
            {
                this.InitializeComInterfaces();
                try
                {
                    InteropHelpers.QueryInterfaceThrow<IVdsDisk>(base.ComUnknown);
                }
                catch (InvalidCastException)
                {
                    return false;
                }
                catch (COMException exception)
                {
                    throw new VdsException("QueryInterface for IVdsDisk interface failed.", exception);
                }
                return true;
            }
        }

        public Microsoft.Storage.Vds.LoadBalancePolicy LoadBalancePolicy
        {
            get
            {
                LoadBalancePolicyType type;
                IntPtr ptr;
                int num;
                this.InitializeComInterfaces();
                if (this.lunMpio == null)
                {
                    throw new NotSupportedException("The LUN does not support MPIO");
                }
                try
                {
                    this.lunMpio.GetLoadBalancePolicy(out type, out ptr, out num);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsLunMpio::GetLoadBalancePolicy failed.", exception);
                }
                VdsPathPolicy[] pathPolicies = new VdsPathPolicy[num];
                for (uint i = 0; i < num; i++)
                {
                    pathPolicies[i] = (VdsPathPolicy) Marshal.PtrToStructure(ptr, typeof(VdsPathPolicy));
                    Marshal.DestroyStructure(ptr, typeof(VdsPathPolicy));
                    ptr = Utilities.IntPtrAddOffset(ptr, (uint) Marshal.SizeOf(typeof(VdsPathPolicy)));
                }
                return new Microsoft.Storage.Vds.LoadBalancePolicy(type, pathPolicies);
            }
            set
            {
                this.InitializeComInterfaces();
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                if (this.lunMpio == null)
                {
                    throw new NotSupportedException("The LUN does not support MPIO");
                }
                try
                {
                    this.lunMpio.SetLoadBalancePolicy(value.Type, value.VdsPathPolicies, value.VdsPathPolicies.Length);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsLunMpio::SetLoadBalancePolicy failed.", exception);
                }
            }
        }

        public uint? LunNumber
        {
            get
            {
                this.InitializeComInterfaces();
                if (this.lunNumber == null)
                {
                    return null;
                }
                uint lunNumber = 0;
                try
                {
                    this.lunNumber.GetLunNumber(out lunNumber);
                }
                catch (COMException exception)
                {
                    throw new VdsException(Resources.UnableToGetLunNumberExceptionMessage, "IVdsLunNumber", "GetLunNumber", (uint) exception.ErrorCode);
                }
                return new uint?(lunNumber);
            }
        }

        public List<Path> Paths
        {
            get
            {
                IntPtr ptr;
                int num;
                this.InitializeComInterfaces();
                if (this.lunMpio == null)
                {
                    throw new NotSupportedException("The LUN does not support MPIO");
                }
                try
                {
                    this.lunMpio.GetPathInfo(out ptr, out num);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsLunMpio::GetPathInfo failed.", exception);
                }
                List<Path> list = new List<Path>(num);
                for (int i = 0; i < num; i++)
                {
                    PathInfo pathInfo = (PathInfo) Marshal.PtrToStructure(ptr, typeof(PathInfo));
                    switch (pathInfo.Type)
                    {
                        case HardwareProviderType.FibreChannel:
                        case HardwareProviderType.Sas:
                            list.Add(new FibreChannelPath(pathInfo));
                            break;

                        case HardwareProviderType.IScsi:
                            list.Add(new IscsiPath(pathInfo));
                            break;

                        default:
                            throw new NotSupportedException("The LUN has an unknown path type");
                    }
                    Marshal.DestroyStructure(ptr, typeof(PathInfo));
                    ptr = Utilities.IntPtrAddOffset(ptr, (uint) Marshal.SizeOf(typeof(PathInfo)));
                }
                return list;
            }
        }

        public Collection<LunPlex> Plexes
        {
            get
            {
                Collection<LunPlex> collection;
                this.InitializeComInterfaces();
                try
                {
                    IEnumVdsObject obj2;
                    this.lun.QueryPlexes(out obj2);
                    collection = new Collection<LunPlex>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsLun::QueryPlexes failed.", exception);
                }
                return collection;
            }
        }

        public short RebuildPriority
        {
            get
            {
                this.InitializeProperties();
                return this.lunProp.RebuildPriority;
            }
        }

        public ulong Size
        {
            get
            {
                this.InitializeProperties();
                return this.lunProp.Size;
            }
        }

        public LunStatus Status
        {
            get
            {
                this.InitializeProperties();
                return this.lunProp.Status;
            }
            set
            {
                this.InitializeComInterfaces();
                try
                {
                    this.lun.SetStatus(value);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsLun::SetStatus failed.", exception);
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
                    this.lun.GetSubSystem(out system2);
                    system = new Microsoft.Storage.Vds.SubSystem(system2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsLun::GetSubSystem failed.", exception);
                }
                return system;
            }
        }

        public Microsoft.Storage.Vds.TransitionState TransitionState
        {
            get
            {
                this.InitializeProperties();
                return this.lunProp.TransitionState;
            }
        }

        public LunType Type
        {
            get
            {
                this.InitializeProperties();
                return this.lunProp.Type;
            }
        }
    }
}

