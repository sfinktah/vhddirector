namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Interop;
    using Microsoft.Storage.Vds.Properties;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;

    public class Service : Wrapper, IDisposable
    {
        private uint cookie;
        private bool disposed;
        private QueryProviderFlags queryProviderFlags;
        private bool refresh;
        private IVdsService service;
        private IVdsServiceHba serviceHba;
        private IVdsServiceIscsi serviceIscsi;
        private ServiceProperties serviceProp;
        private IVdsServiceUninstallDisk uninstallDisk;
        private AdviseSink vdsNotificationSink;

        public event EventHandler<ControllerEventArgs> ControllerArrived;

        public event EventHandler<ControllerEventArgs> ControllerDeparted;

        public event EventHandler<DiskEventArgs> DiskArrived;

        public event EventHandler<DiskEventArgs> DiskDeparted;

        public event EventHandler<DiskEventArgs> DiskModified;

        public event EventHandler<DriveEventArgs> DriveArrived;

        public event EventHandler<DriveEventArgs> DriveDeparted;

        public event EventHandler<DriveLetterEventArgs> DriveLetterAssigned;

        public event EventHandler<DriveLetterEventArgs> DriveLetterFreed;

        public event EventHandler<DriveEventArgs> DriveModified;

        public event EventHandler<FileSystemEventArgs> FileSystemFormatting;

        public event EventHandler<FileSystemEventArgs> FileSystemModified;

        public event EventHandler<LunEventArgs> LunArrived;

        public event EventHandler<LunEventArgs> LunDeparted;

        public event EventHandler<LunEventArgs> LunModified;

        public event EventHandler<MountPointEventArgs> MountPointModified;

        public event EventHandler<PackEventArgs> PackArrived;

        public event EventHandler<PackEventArgs> PackDeparted;

        public event EventHandler<PackEventArgs> PackModified;

        public event EventHandler<PartitionEventArgs> PartitionArrived;

        public event EventHandler<PartitionEventArgs> PartitionDeparted;

        public event EventHandler<PartitionEventArgs> PartitionModified;

        public event EventHandler<PortalEventArgs> PortalArrived;

        public event EventHandler<PortalEventArgs> PortalDeparted;

        public event EventHandler<PortalGroupEventArgs> PortalGroupArrived;

        public event EventHandler<PortalGroupEventArgs> PortalGroupDeparted;

        public event EventHandler<PortalGroupEventArgs> PortalGroupModified;

        public event EventHandler<PortalEventArgs> PortalModified;

        public event EventHandler<PortEventArgs> PortArrived;

        public event EventHandler<PortEventArgs> PortDeparted;

        public event EventHandler<ServiceEventArgs> ServiceOutOfSync;

        public event EventHandler<SubSystemEventArgs> SubSystemArrived;

        public event EventHandler<SubSystemEventArgs> SubSystemDeparted;

        public event EventHandler<TargetEventArgs> TargetArrived;

        public event EventHandler<TargetEventArgs> TargetDeparted;

        public event EventHandler<TargetEventArgs> TargetModified;

        public event EventHandler<VolumeEventArgs> VolumeArrived;

        public event EventHandler<VolumeEventArgs> VolumeDeparted;

        public event EventHandler<VolumeEventArgs> VolumeModified;

        public event EventHandler<VolumeEventArgs> VolumeRebuilding;

        public Service(IVdsService vdsService) : base(vdsService)
        {
            this.refresh = true;
            this.queryProviderFlags = QueryProviderFlags.HardwareProviders | QueryProviderFlags.SoftwareProviders;
            this.service = vdsService;
            base.VdsService = this.service;
            try
            {
                this.serviceHba = (IVdsServiceHba) vdsService;
            }
            catch (InvalidCastException)
            {
            }
            try
            {
                this.vdsNotificationSink = new AdviseSink(this);
                this.service.Advise(this.vdsNotificationSink, out this.cookie);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsService::Advise failed.", exception);
            }
            catch (InvalidCastException exception2)
            {
                throw new VdsException("QueryInterface for IVdsService failed.", exception2);
            }
        }

        public void CleanupObsoleteMountPoints()
        {
            try
            {
                this.service.CleanupObsoleteMountPoints();
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsService::CleanupObsoleteMountPoints failed.", exception);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.vdsNotificationSink.Dispose();
                }
                try
                {
                    if (this.cookie != 0)
                    {
                        IVdsService service = InteropHelpers.QueryInterface<IVdsService>(base.ComUnknown);
                        if (service != null)
                        {
                            service.Unadvise(this.cookie);
                        }
                        this.cookie = 0;
                    }
                }
                catch (COMException)
                {
                }
                catch (InvalidComObjectException)
                {
                }
                this.disposed = true;
                base.Dispose(disposing);
            }
        }

        ~Service()
        {
            try
            {
                this.Dispose(false);
            }
            catch (VdsException)
            {
            }
        }

        public Guid GetDiskIdFromLunInfo(LunInformation lunInfo)
        {
            Guid guid;
            this.InitializeComInterfaces();
            if (lunInfo == null)
            {
                throw new ArgumentNullException("lunInfo");
            }
            if (this.uninstallDisk == null)
            {
                throw new NotSupportedException("Uninstall disk methods are not supported in this version of the service.");
            }
            try
            {
                this.uninstallDisk.GetDiskIdFromLunInfo(lunInfo.Info, out guid);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsUninstallDisk::GetDiskIdFromLunInfo failed.", exception);
            }
            return guid;
        }

        public T GetObject<T>(Guid id) where T: Wrapper, new()
        {
            T local2;
            this.InitializeComInterfaces();
            try
            {
                object obj2;
                if (typeof(T) == typeof(Provider))
                {
                    this.service.GetObject(id, ObjectType.Provider, out obj2);
                }
                else if (typeof(T) == typeof(Microsoft.Storage.Vds.HardwareProvider))
                {
                    this.service.GetObject(id, ObjectType.Provider, out obj2);
                    if (!(obj2 is IVdsHwProvider))
                    {
                        throw new ArgumentException(Resources.InvalidHardwareProviderIdArgumentExceptionMessage, "id");
                    }
                }
                else if (typeof(T) == typeof(Microsoft.Storage.Vds.SoftwareProvider))
                {
                    this.service.GetObject(id, ObjectType.Provider, out obj2);
                    if (!(obj2 is IVdsSwProvider))
                    {
                        throw new ArgumentException(Resources.InvalidSoftwareProviderIdArgumentExceptionMessage, "id");
                    }
                }
                else if (typeof(T) == typeof(Pack))
                {
                    this.service.GetObject(id, ObjectType.Pack, out obj2);
                }
                else if (typeof(T) == typeof(Volume))
                {
                    this.service.GetObject(id, ObjectType.Volume, out obj2);
                }
                else if (typeof(T) == typeof(Disk))
                {
                    this.service.GetObject(id, ObjectType.Disk, out obj2);
                }
                else if (typeof(T) == typeof(SubSystem))
                {
                    this.service.GetObject(id, ObjectType.SubSystem, out obj2);
                }
                else if (typeof(T) == typeof(Drive))
                {
                    this.service.GetObject(id, ObjectType.Drive, out obj2);
                }
                else if (typeof(T) == typeof(Lun))
                {
                    this.service.GetObject(id, ObjectType.Lun, out obj2);
                }
                else if (typeof(T) == typeof(LunPlex))
                {
                    this.service.GetObject(id, ObjectType.LunPlex, out obj2);
                }
                else if (typeof(T) == typeof(Controller))
                {
                    this.service.GetObject(id, ObjectType.Controller, out obj2);
                }
                else if (typeof(T) == typeof(ControllerPort))
                {
                    this.service.GetObject(id, ObjectType.Port, out obj2);
                }
                else
                {
                    if (typeof(T) != typeof(HbaPort))
                    {
                        throw new InvalidOperationException(Resources.UnknownObjectTypeInvalidOperationExceptionMessage);
                    }
                    this.service.GetObject(id, ObjectType.HbaPort, out obj2);
                }
                T local = Activator.CreateInstance<T>();
                local.ComUnknown = obj2;
                local.VdsService = base.VdsService;
                local.InitializeComInterfaces();
                local2 = local;
            }
            catch (COMException exception)
            {
                throw new VdsException("The method call to IVdsService::GetObject failed.", exception);
            }
            return local2;
        }

        public override void InitializeComInterfaces()
        {
            base.InitializeComInterfaces();
            if (this.service == null)
            {
                this.service = InteropHelpers.QueryInterface<IVdsService>(base.ComUnknown);
                if (this.service == null)
                {
                    throw new VdsException("QueryInterface for IVdsSerivce failed.");
                }
            }
            if (this.serviceHba == null)
            {
                this.serviceHba = InteropHelpers.QueryInterface<IVdsServiceHba>(base.ComUnknown);
            }
            if (this.serviceIscsi == null)
            {
                this.serviceIscsi = InteropHelpers.QueryInterface<IVdsServiceIscsi>(base.ComUnknown);
            }
            if (this.uninstallDisk == null)
            {
                this.uninstallDisk = InteropHelpers.QueryInterface<IVdsServiceUninstallDisk>(base.ComUnknown);
            }
        }

        public override void InitializeProperties()
        {
            this.InitializeComInterfaces();
            if (this.refresh)
            {
                try
                {
                    this.service.GetProperties(out this.serviceProp);
                    this.refresh = false;
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsService::GetProperties failed.", exception);
                }
            }
        }

        public void OnControllerArrived(VDS_CONTROLLER_NOTIFICATION controller)
        {
            if (this.ControllerArrived != null)
            {
                ControllerEventArgs e = new ControllerEventArgs(controller.ControllerId);
                this.ControllerArrived(this, e);
            }
        }

        public void OnControllerDeparted(VDS_CONTROLLER_NOTIFICATION controller)
        {
            if (this.ControllerDeparted != null)
            {
                ControllerEventArgs e = new ControllerEventArgs(controller.ControllerId);
                this.ControllerDeparted(this, e);
            }
        }

        public void OnDiskArrived(VDS_DISK_NOTIFICATION disk)
        {
            if (this.DiskArrived != null)
            {
                DiskEventArgs e = new DiskEventArgs(disk.DiskId);
                this.DiskArrived(this, e);
            }
        }

        public void OnDiskDeparted(VDS_DISK_NOTIFICATION disk)
        {
            if (this.DiskDeparted != null)
            {
                DiskEventArgs e = new DiskEventArgs(disk.DiskId);
                this.DiskDeparted(this, e);
            }
        }

        public void OnDiskModified(VDS_DISK_NOTIFICATION disk)
        {
            if (this.DiskModified != null)
            {
                DiskEventArgs e = new DiskEventArgs(disk.DiskId);
                this.DiskModified(this, e);
            }
        }

        public void OnDriveArrived(VDS_DRIVE_NOTIFICATION drive)
        {
            if (this.DriveArrived != null)
            {
                DriveEventArgs e = new DriveEventArgs(drive.DriveId);
                this.DriveArrived(this, e);
            }
        }

        public void OnDriveDeparted(VDS_DRIVE_NOTIFICATION drive)
        {
            if (this.DriveDeparted != null)
            {
                DriveEventArgs e = new DriveEventArgs(drive.DriveId);
                this.DriveDeparted(this, e);
            }
        }

        public void OnDriveLetterAssigned(VDS_DRIVE_LETTER_NOTIFICATION driveLetter)
        {
            if (this.DriveLetterAssigned != null)
            {
                DriveLetterEventArgs e = new DriveLetterEventArgs(driveLetter.Letter, driveLetter.VolumeId);
                this.DriveLetterAssigned(this, e);
            }
        }

        public void OnDriveLetterFreed(VDS_DRIVE_LETTER_NOTIFICATION driveLetter)
        {
            if (this.DriveLetterFreed != null)
            {
                DriveLetterEventArgs e = new DriveLetterEventArgs(driveLetter.Letter, driveLetter.VolumeId);
                this.DriveLetterFreed(this, e);
            }
        }

        public void OnDriveModified(VDS_DRIVE_NOTIFICATION drive)
        {
            if (this.DriveModified != null)
            {
                DriveEventArgs e = new DriveEventArgs(drive.DriveId);
                this.DriveModified(this, e);
            }
        }

        public void OnFileSystemFormatting(VDS_FILE_SYSTEM_NOTIFICATION fileSystem)
        {
            if (this.FileSystemFormatting != null)
            {
                FileSystemEventArgs e = new FileSystemEventArgs(fileSystem.VolumeId, fileSystem.PercentCompleted);
                this.FileSystemFormatting(this, e);
            }
        }

        public void OnFileSystemModified(VDS_FILE_SYSTEM_NOTIFICATION fileSystem)
        {
            if (this.FileSystemModified != null)
            {
                FileSystemEventArgs e = new FileSystemEventArgs(fileSystem.VolumeId, fileSystem.PercentCompleted);
                this.FileSystemModified(this, e);
            }
        }

        public void OnLunArrived(VDS_LUN_NOTIFICATION lun)
        {
            if (this.LunArrived != null)
            {
                LunEventArgs e = new LunEventArgs(lun.LunId);
                this.LunArrived(this, e);
            }
        }

        public void OnLunDeparted(VDS_LUN_NOTIFICATION lun)
        {
            if (this.LunDeparted != null)
            {
                LunEventArgs e = new LunEventArgs(lun.LunId);
                this.LunDeparted(this, e);
            }
        }

        public void OnLunModified(VDS_LUN_NOTIFICATION lun)
        {
            if (this.LunModified != null)
            {
                LunEventArgs e = new LunEventArgs(lun.LunId);
                this.LunModified(this, e);
            }
        }

        public void OnMountPointModified(VDS_MOUNT_POINT_NOTIFICATION mountPoint)
        {
            if (this.MountPointModified != null)
            {
                MountPointEventArgs e = new MountPointEventArgs(mountPoint.VolumeId);
                this.MountPointModified(this, e);
            }
        }

        public void OnPackArrived(VDS_PACK_NOTIFICATION pack)
        {
            if (this.PackArrived != null)
            {
                PackEventArgs e = new PackEventArgs(pack.PackId);
                this.PackModified(this, e);
            }
        }

        public void OnPackDeparted(VDS_PACK_NOTIFICATION pack)
        {
            if (this.PackDeparted != null)
            {
                PackEventArgs e = new PackEventArgs(pack.PackId);
                this.PackDeparted(this, e);
            }
        }

        public void OnPackModified(VDS_PACK_NOTIFICATION pack)
        {
            if (this.PackModified != null)
            {
                PackEventArgs e = new PackEventArgs(pack.PackId);
                this.PackModified(this, e);
            }
        }

        public void OnPartitionArrived(VDS_PARTITION_NOTIFICATION partition)
        {
            if (this.PartitionArrived != null)
            {
                PartitionEventArgs e = new PartitionEventArgs(partition.DiskId, partition.Offset);
                this.PartitionArrived(this, e);
            }
        }

        public void OnPartitionDeparted(VDS_PARTITION_NOTIFICATION partition)
        {
            if (this.PartitionDeparted != null)
            {
                PartitionEventArgs e = new PartitionEventArgs(partition.DiskId, partition.Offset);
                this.PartitionDeparted(this, e);
            }
        }

        public void OnPartitionModified(VDS_PARTITION_NOTIFICATION partition)
        {
            if (this.PartitionModified != null)
            {
                PartitionEventArgs e = new PartitionEventArgs(partition.DiskId, partition.Offset);
                this.PartitionModified(this, e);
            }
        }

        public void OnPortalArrived(VDS_PORTAL_NOTIFICATION portal)
        {
            if (this.PortalArrived != null)
            {
                PortalEventArgs e = new PortalEventArgs(portal.PortalId);
                this.PortalArrived(this, e);
            }
        }

        public void OnPortalDeparted(VDS_PORTAL_NOTIFICATION portal)
        {
            if (this.PortalDeparted != null)
            {
                PortalEventArgs e = new PortalEventArgs(portal.PortalId);
                this.PortalDeparted(this, e);
            }
        }

        public void OnPortalGroupArrived(VDS_PORTAL_GROUP_NOTIFICATION portalGroup)
        {
            if (this.PortalGroupArrived != null)
            {
                PortalGroupEventArgs e = new PortalGroupEventArgs(portalGroup.PortalGroupId);
                this.PortalGroupArrived(this, e);
            }
        }

        public void OnPortalGroupDeparted(VDS_PORTAL_GROUP_NOTIFICATION portalGroup)
        {
            if (this.PortalGroupDeparted != null)
            {
                PortalGroupEventArgs e = new PortalGroupEventArgs(portalGroup.PortalGroupId);
                this.PortalGroupDeparted(this, e);
            }
        }

        public void OnPortalGroupModified(VDS_PORTAL_GROUP_NOTIFICATION portalGroup)
        {
            if (this.PortalGroupModified != null)
            {
                PortalGroupEventArgs e = new PortalGroupEventArgs(portalGroup.PortalGroupId);
                this.PortalGroupModified(this, e);
            }
        }

        public void OnPortalModified(VDS_PORTAL_NOTIFICATION portal)
        {
            if (this.PortalModified != null)
            {
                PortalEventArgs e = new PortalEventArgs(portal.PortalId);
                this.PortalModified(this, e);
            }
        }

        public void OnPortArrived(VDS_PORT_NOTIFICATION port)
        {
            if (this.PortArrived != null)
            {
                PortEventArgs e = new PortEventArgs(port.PortId);
                this.PortArrived(this, e);
            }
        }

        public void OnPortDeparted(VDS_PORT_NOTIFICATION port)
        {
            if (this.PortDeparted != null)
            {
                PortEventArgs e = new PortEventArgs(port.PortId);
                this.PortDeparted(this, e);
            }
        }

        public void OnServiceOutOfSync(VDS_SERVICE_NOTIFICATION service)
        {
            if (this.ServiceOutOfSync != null)
            {
                ServiceEventArgs e = new ServiceEventArgs(service.Action);
                this.ServiceOutOfSync(this, e);
            }
        }

        public void OnSubSystemArrived(VDS_SUB_SYSTEM_NOTIFICATION subSystem)
        {
            if (this.SubSystemArrived != null)
            {
                SubSystemEventArgs e = new SubSystemEventArgs(subSystem.SubSystemId);
                this.SubSystemArrived(this, e);
            }
        }

        public void OnSubSystemDeparted(VDS_SUB_SYSTEM_NOTIFICATION subSystem)
        {
            if (this.SubSystemDeparted != null)
            {
                SubSystemEventArgs e = new SubSystemEventArgs(subSystem.SubSystemId);
                this.SubSystemDeparted(this, e);
            }
        }

        public void OnTargetArrived(VDS_TARGET_NOTIFICATION target)
        {
            if (this.TargetArrived != null)
            {
                TargetEventArgs e = new TargetEventArgs(target.TargetId);
                this.TargetArrived(this, e);
            }
        }

        public void OnTargetDeparted(VDS_TARGET_NOTIFICATION target)
        {
            if (this.TargetDeparted != null)
            {
                TargetEventArgs e = new TargetEventArgs(target.TargetId);
                this.TargetDeparted(this, e);
            }
        }

        public void OnTargetModified(VDS_TARGET_NOTIFICATION target)
        {
            if (this.TargetModified != null)
            {
                TargetEventArgs e = new TargetEventArgs(target.TargetId);
                this.TargetModified(this, e);
            }
        }

        public void OnVolumeArrived(VDS_VOLUME_NOTIFICATION volume)
        {
            if (this.VolumeArrived != null)
            {
                VolumeEventArgs e = new VolumeEventArgs(volume.VolumeId, volume.PlexId, volume.PercentCompleted);
                this.VolumeArrived(this, e);
            }
        }

        public void OnVolumeDeparted(VDS_VOLUME_NOTIFICATION volume)
        {
            if (this.VolumeDeparted != null)
            {
                VolumeEventArgs e = new VolumeEventArgs(volume.VolumeId, volume.PlexId, volume.PercentCompleted);
                this.VolumeDeparted(this, e);
            }
        }

        public void OnVolumeModified(VDS_VOLUME_NOTIFICATION volume)
        {
            if (this.VolumeModified != null)
            {
                VolumeEventArgs e = new VolumeEventArgs(volume.VolumeId, volume.PlexId, volume.PercentCompleted);
                this.VolumeModified(this, e);
            }
        }

        public void OnVolumeRebuilding(VDS_VOLUME_NOTIFICATION volume)
        {
            if (this.VolumeRebuilding != null)
            {
                VolumeEventArgs e = new VolumeEventArgs(volume.VolumeId, volume.PlexId, volume.PercentCompleted);
                this.VolumeRebuilding(this, e);
            }
        }

        public DriveLetterProperties[] QueryDriveLetters(char firstLetter, uint count)
        {
            DriveLetterProperties[] propertiesArray;
            try
            {
                propertiesArray = new DriveLetterProperties[count];
                this.service.QueryDriveLetters(firstLetter, count, propertiesArray);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsService::QueryDriveLetters failed.", exception);
            }
            return propertiesArray;
        }

        public void Reboot()
        {
            try
            {
                this.service.Reboot();
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsService::Reboot failed.", exception);
            }
        }

        public void Reenumerate()
        {
            try
            {
                this.service.Reenumerate();
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsService::Reenumerate failed.", exception);
            }
        }

        public void Refresh()
        {
            this.refresh = true;
            try
            {
                this.service.Refresh();
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsService::Refresh failed.", exception);
            }
        }

        public void RememberTargetSharedSecret(Guid targetId, SecureString targetSharedSecret)
        {
            this.InitializeComInterfaces();
            if (this.serviceIscsi == null)
            {
                throw new NotSupportedException("iSCSI not supported in this version of the service.");
            }
            SecureStringAnsiHandle sharedSecret = new SecureStringAnsiHandle(targetSharedSecret);
            try
            {
                SharedSecret secret = new SharedSecret(sharedSecret);
                this.serviceIscsi.RememberTargetSharedSecret(targetId, ref secret);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsServiceIscsi::RememberTargetSharedSecret failed.", exception);
            }
            finally
            {
                if (sharedSecret != null)
                {
                    sharedSecret.Dispose();
                }
            }
        }

        public void SetAllIpsecSecurity(Guid targetPortalId, IpsecFlags securityFlags, SecureString ipsecKey)
        {
            this.InitializeComInterfaces();
            if (this.serviceIscsi == null)
            {
                throw new NotSupportedException("iSCSI not supported in this version of the service.");
            }
            SecureStringAnsiHandle handle = new SecureStringAnsiHandle(ipsecKey);
            try
            {
                IpsecKey key = new IpsecKey(handle);
                this.serviceIscsi.SetAllIpsecSecurity(targetPortalId, securityFlags, ref key);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsServiceIscsi::SetAllIpsecSecurity failed.", exception);
            }
            finally
            {
                if (handle != null)
                {
                    handle.Dispose();
                }
            }
        }

        private void SetAllIpsecTunnelAddresses(IPAddress tunnelAddress, IPAddress destinationAddress)
        {
            this.InitializeComInterfaces();
            if (this.serviceIscsi == null)
            {
                throw new NotSupportedException("iSCSI not supported in this version of the service.");
            }
            try
            {
                this.serviceIscsi.SetAllIpsecTunnelAddresses(ref tunnelAddress, ref destinationAddress);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsServiceIscsi::SetAllIpsecTunnelAddress failed.", exception);
            }
        }

        public void SetInitiatorSharedSecret(SecureString initiatorSharedSecret, Guid targetId)
        {
            this.InitializeComInterfaces();
            if (this.serviceIscsi == null)
            {
                throw new NotSupportedException("iSCSI not supported in this version of the service.");
            }
            SecureStringAnsiHandle sharedSecret = new SecureStringAnsiHandle(initiatorSharedSecret);
            try
            {
                SharedSecret secret = new SharedSecret(sharedSecret);
                this.serviceIscsi.SetInitiatorSharedSecret(ref secret, targetId);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsServiceIscsi::SetInitiatorSharedSecret failed.", exception);
            }
            finally
            {
                if (sharedSecret != null)
                {
                    sharedSecret.Dispose();
                }
            }
        }

        public void SetIpsecGroupPresharedKey(SecureString ipsecKey)
        {
            this.InitializeComInterfaces();
            if (this.serviceIscsi == null)
            {
                throw new NotSupportedException("iSCSI not supported in this version of the service.");
            }
            SecureStringAnsiHandle handle = new SecureStringAnsiHandle(ipsecKey);
            try
            {
                IpsecKey key = new IpsecKey(handle);
                this.serviceIscsi.SetIpsecGroupPresharedKey(ref key);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call the IVdsServiceIscsi::SetIpsecGroupPresharedKey failed.", exception);
            }
            finally
            {
                if (handle != null)
                {
                    handle.Dispose();
                }
            }
        }

        public bool UninstallDisk(Guid diskId, bool force)
        {
            uint num = 0;
            int[] results = null;
            uint reboot = 0;
            Guid[] diskIds = new Guid[] { diskId };
            int num3 = this.UninstallDisks(diskIds, force, out reboot, out results);
            if (((num3 >= 0) && (results != null)) && (results[0] < 0))
            {
                num3 = results[0];
            }
            if (num3 >= 0)
            {
                num = reboot;
            }
            return (num == 0);
        }

        private int UninstallDisks(Guid[] diskIds, bool force, out uint reboot, out int[] results)
        {
            results = null;
            reboot = 0;
            if (diskIds == null)
            {
                throw new ArgumentNullException("diskIds");
            }
            if (diskIds.Length == 0)
            {
                throw new ArgumentOutOfRangeException("diskIds");
            }
            this.InitializeComInterfaces();
            if (this.uninstallDisk == null)
            {
                throw new NotSupportedException("Uninstall disk methods are not supported in this version of the service.");
            }
            int num = -1;
            try
            {
                int[] hresults = new int[diskIds.Length];
                num = this.uninstallDisk.UninstallDisks(diskIds, (uint) diskIds.Length, force ? 1U : 0U, out reboot, hresults);
                results = hresults;
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsUnintallDisk::UninstallDisks failed.", exception);
            }
            return num;
        }

        public void WaitForServiceReady()
        {
            if (this.service.WaitForServiceReady() != 0)
            {
                throw new VdsException("VDS failed to initialize.");
            }
        }

        public bool AutoMount
        {
            get
            {
                this.InitializeProperties();
                return (ServiceFlags.AutoMountOff != (this.serviceProp.Flags & ServiceFlags.AutoMountOff));
            }
            set
            {
                try
                {
                    if (value)
                    {
                        this.service.ClearFlags(ServiceFlags.AutoMountOff);
                    }
                    else
                    {
                        this.service.SetFlags(ServiceFlags.AutoMountOff);
                    }
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsService::Set/ClearFlags failed.", exception);
                }
                this.refresh = true;
            }
        }

        public ServiceFlags Flags
        {
            get
            {
                this.InitializeProperties();
                return this.serviceProp.Flags;
            }
        }

        public bool HardwareProvider
        {
            get
            {
                return (QueryProviderFlags.HardwareProviders == (this.queryProviderFlags & QueryProviderFlags.HardwareProviders));
            }
            set
            {
                if (value)
                {
                    this.queryProviderFlags |= QueryProviderFlags.HardwareProviders;
                }
                else
                {
                    this.queryProviderFlags &= ~QueryProviderFlags.HardwareProviders;
                }
            }
        }

        public Collection<HbaPort> HbaPorts
        {
            get
            {
                Collection<HbaPort> collection;
                this.InitializeComInterfaces();
                if (this.serviceHba == null)
                {
                    throw new NotSupportedException("The service does not support HBA port operations.");
                }
                try
                {
                    IEnumVdsObject obj2;
                    this.serviceHba.QueryHbaPorts(out obj2);
                    collection = new Collection<HbaPort>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsServiceHba::QueryHbaPorts failed.", exception);
                }
                return collection;
            }
        }

        public Collection<InitiatorAdapter> InitiatorAdapters
        {
            get
            {
                Collection<InitiatorAdapter> collection;
                this.InitializeComInterfaces();
                if (this.serviceIscsi == null)
                {
                    throw new NotSupportedException("The service does not support iSCSI operations.");
                }
                try
                {
                    IEnumVdsObject obj2;
                    this.serviceIscsi.QueryInitiatorAdapters(out obj2);
                    collection = new Collection<InitiatorAdapter>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsServiceIscsi::QueryInitiatorAdapters failed.", exception);
                }
                return collection;
            }
        }

        public string InitiatorName
        {
            get
            {
                string str;
                this.InitializeComInterfaces();
                if (this.serviceIscsi == null)
                {
                    throw new NotSupportedException("The service does not support iSCSI operations.");
                }
                try
                {
                    this.serviceIscsi.GetInitiatorName(out str);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsServiceIscsi::GetInitiatorName failed.", exception);
                }
                return str;
            }
        }

        public bool IsServiceReady
        {
            get
            {
                return (this.service.IsServiceReady() == 0);
            }
        }

        public Collection<Disk> MaskedDisks
        {
            get
            {
                Collection<Disk> collection;
                try
                {
                    IEnumVdsObject obj2;
                    this.service.QueryMaskedDisks(out obj2);
                    collection = new Collection<Disk>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsService::QueryMaskedDisks failed.", exception);
                }
                return collection;
            }
        }

        public Collection<Provider> Providers
        {
            get
            {
                Collection<Provider> collection;
                try
                {
                    IEnumVdsObject obj2;
                    this.service.QueryProviders(this.queryProviderFlags, out obj2);
                    collection = new Collection<Provider>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsService::QueryProviders failed.", exception);
                }
                return collection;
            }
        }

        public bool SoftwareProvider
        {
            get
            {
                return (QueryProviderFlags.SoftwareProviders == (this.queryProviderFlags & QueryProviderFlags.SoftwareProviders));
            }
            set
            {
                if (value)
                {
                    this.queryProviderFlags |= QueryProviderFlags.SoftwareProviders;
                }
                else
                {
                    this.queryProviderFlags &= ~QueryProviderFlags.SoftwareProviders;
                }
            }
        }

        public Collection<Disk> UnallocatedDisks
        {
            get
            {
                Collection<Disk> collection;
                try
                {
                    IEnumVdsObject obj2;
                    this.service.QueryUnallocatedDisks(out obj2);
                    collection = new Collection<Disk>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsService::QueryUnallocatedDisks failed.", exception);
                }
                return collection;
            }
        }

        public string Version
        {
            get
            {
                this.InitializeProperties();
                return this.serviceProp.Version;
            }
        }
    }
}

