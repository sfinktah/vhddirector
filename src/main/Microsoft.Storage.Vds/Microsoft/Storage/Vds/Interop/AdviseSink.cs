namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;

    public class AdviseSink : IVdsAdviseSink, IDisposable
    {
        private bool disposed;
        private Service service;

        public AdviseSink(Service service)
        {
            this.service = service;
            this.disposed = false;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.service = null;
                }
                this.disposed = true;
            }
        }

        ~AdviseSink()
        {
            this.Dispose(false);
        }

        public void OnNotify(uint numberOfNotifications, VDS_NOTIFICATION[] notificationArray)
        {
            uint index = 0;
            for (index = 0; index < numberOfNotifications; index++)
            {
                switch (notificationArray[index].ObjectType)
                {
                    case VDS_NOTIFICATION_TARGET_TYPE.VDS_NTT_PACK:
                        switch (notificationArray[index].NotificationInfo.Pack.Event)
                        {
                            case VDS_NOTIFICATION_EVENT.VDS_NF_PACK_ARRIVE:
                                goto Label_00C3;

                            case VDS_NOTIFICATION_EVENT.VDS_NF_PACK_DEPART:
                                goto Label_00E5;

                            case VDS_NOTIFICATION_EVENT.VDS_NF_PACK_MODIFY:
                                goto Label_0107;
                        }
                        break;

                    case VDS_NOTIFICATION_TARGET_TYPE.VDS_NTT_VOLUME:
                        switch (notificationArray[index].NotificationInfo.Volume.Event)
                        {
                            case VDS_NOTIFICATION_EVENT.VDS_NF_VOLUME_ARRIVE:
                                goto Label_0162;

                            case VDS_NOTIFICATION_EVENT.VDS_NF_VOLUME_DEPART:
                                goto Label_0184;

                            case VDS_NOTIFICATION_EVENT.VDS_NF_VOLUME_MODIFY:
                                goto Label_01A6;

                            case VDS_NOTIFICATION_EVENT.VDS_NF_VOLUME_REBUILDING_PROGRESS:
                                goto Label_01C8;
                        }
                        break;

                    case VDS_NOTIFICATION_TARGET_TYPE.VDS_NTT_DISK:
                        switch (notificationArray[index].NotificationInfo.Disk.Event)
                        {
                            case VDS_NOTIFICATION_EVENT.VDS_NF_DISK_ARRIVE:
                                goto Label_021F;

                            case VDS_NOTIFICATION_EVENT.VDS_NF_DISK_DEPART:
                                goto Label_0241;

                            case VDS_NOTIFICATION_EVENT.VDS_NF_DISK_MODIFY:
                                goto Label_0263;
                        }
                        break;

                    case VDS_NOTIFICATION_TARGET_TYPE.VDS_NTT_SUB_SYSTEM:
                        switch (notificationArray[index].NotificationInfo.SubSystem.Event)
                        {
                            case VDS_NOTIFICATION_EVENT.VDS_NF_SUB_SYSTEM_ARRIVE:
                                goto Label_048E;

                            case VDS_NOTIFICATION_EVENT.VDS_NF_SUB_SYSTEM_DEPART:
                                goto Label_04B0;
                        }
                        break;

                    case VDS_NOTIFICATION_TARGET_TYPE.VDS_NTT_CONTROLLER:
                        switch (notificationArray[index].NotificationInfo.Controller.Event)
                        {
                            case VDS_NOTIFICATION_EVENT.VDS_NF_CONTROLLER_ARRIVE:
                                goto Label_0504;

                            case VDS_NOTIFICATION_EVENT.VDS_NF_CONTROLLER_DEPART:
                                goto Label_0526;
                        }
                        break;

                    case VDS_NOTIFICATION_TARGET_TYPE.VDS_NTT_DRIVE:
                        switch (notificationArray[index].NotificationInfo.Drive.Event)
                        {
                            case VDS_NOTIFICATION_EVENT.VDS_NF_DRIVE_ARRIVE:
                                goto Label_057E;

                            case VDS_NOTIFICATION_EVENT.VDS_NF_DRIVE_DEPART:
                                goto Label_05A0;

                            case VDS_NOTIFICATION_EVENT.VDS_NF_DRIVE_MODIFY:
                                goto Label_05C2;
                        }
                        break;

                    case VDS_NOTIFICATION_TARGET_TYPE.VDS_NTT_LUN:
                        switch (notificationArray[index].NotificationInfo.Lun.Event)
                        {
                            case VDS_NOTIFICATION_EVENT.VDS_NF_LUN_ARRIVE:
                                goto Label_061A;

                            case VDS_NOTIFICATION_EVENT.VDS_NF_LUN_DEPART:
                                goto Label_063C;

                            case VDS_NOTIFICATION_EVENT.VDS_NF_LUN_MODIFY:
                                goto Label_065E;
                        }
                        break;

                    case VDS_NOTIFICATION_TARGET_TYPE.VDS_NTT_PORT:
                        switch (notificationArray[index].NotificationInfo.Port.Event)
                        {
                            case VDS_NOTIFICATION_EVENT.VDS_NF_PORT_ARRIVE:
                                goto Label_06B2;

                            case VDS_NOTIFICATION_EVENT.VDS_NF_PORT_DEPART:
                                goto Label_06D4;
                        }
                        break;

                    case VDS_NOTIFICATION_TARGET_TYPE.VDS_NTT_PORTAL:
                        switch (notificationArray[index].NotificationInfo.Portal.Event)
                        {
                            case VDS_NOTIFICATION_EVENT.VDS_NF_PORTAL_ARRIVE:
                                goto Label_072C;

                            case VDS_NOTIFICATION_EVENT.VDS_NF_PORTAL_DEPART:
                                goto Label_074E;

                            case VDS_NOTIFICATION_EVENT.VDS_NF_PORTAL_MODIFY:
                                goto Label_0770;
                        }
                        break;

                    case VDS_NOTIFICATION_TARGET_TYPE.VDS_NTT_TARGET:
                        switch (notificationArray[index].NotificationInfo.Target.Event)
                        {
                            case VDS_NOTIFICATION_EVENT.VDS_NF_TARGET_ARRIVE:
                                goto Label_07C8;

                            case VDS_NOTIFICATION_EVENT.VDS_NF_TARGET_DEPART:
                                goto Label_07EA;

                            case VDS_NOTIFICATION_EVENT.VDS_NF_TARGET_MODIFY:
                                goto Label_080C;
                        }
                        break;

                    case VDS_NOTIFICATION_TARGET_TYPE.VDS_NTT_PORTAL_GROUP:
                        switch (notificationArray[index].NotificationInfo.PortalGroup.Event)
                        {
                            case VDS_NOTIFICATION_EVENT.VDS_NF_PORTAL_GROUP_ARRIVE:
                                goto Label_0867;

                            case VDS_NOTIFICATION_EVENT.VDS_NF_PORTAL_GROUP_DEPART:
                                goto Label_0886;

                            case VDS_NOTIFICATION_EVENT.VDS_NF_PORTAL_GROUP_MODIFY:
                                goto Label_08A5;
                        }
                        break;

                    case VDS_NOTIFICATION_TARGET_TYPE.VDS_NTT_PARTITION:
                        switch (notificationArray[index].NotificationInfo.Partition.Event)
                        {
                            case VDS_NOTIFICATION_EVENT.VDS_NF_PARTITION_ARRIVE:
                                goto Label_02BB;

                            case VDS_NOTIFICATION_EVENT.VDS_NF_PARTITION_DEPART:
                                goto Label_02DD;

                            case VDS_NOTIFICATION_EVENT.VDS_NF_PARTITION_MODIFY:
                                goto Label_02FF;
                        }
                        break;

                    case VDS_NOTIFICATION_TARGET_TYPE.VDS_NTT_DRIVE_LETTER:
                        switch (notificationArray[index].NotificationInfo.Letter.Event)
                        {
                            case VDS_NOTIFICATION_EVENT.VDS_NF_DRIVE_LETTER_FREE:
                                goto Label_0356;

                            case VDS_NOTIFICATION_EVENT.VDS_NF_DRIVE_LETTER_ASSIGN:
                                goto Label_0378;
                        }
                        break;

                    case VDS_NOTIFICATION_TARGET_TYPE.VDS_NTT_FILE_SYSTEM:
                        switch (notificationArray[index].NotificationInfo.FileSystem.Event)
                        {
                            case VDS_NOTIFICATION_EVENT.VDS_NF_FILE_SYSTEM_MODIFY:
                                goto Label_03CF;

                            case VDS_NOTIFICATION_EVENT.VDS_NF_FILE_SYSTEM_FORMAT_PROGRESS:
                                goto Label_03F1;
                        }
                        break;

                    case VDS_NOTIFICATION_TARGET_TYPE.VDS_NTT_MOUNT_POINT:
                        if (notificationArray[index].NotificationInfo.MountPoint.Event == VDS_NOTIFICATION_EVENT.VDS_NF_MOUNT_POINTS_CHANGE)
                        {
                            this.service.OnMountPointModified(notificationArray[index].NotificationInfo.MountPoint);
                        }
                        break;

                    case VDS_NOTIFICATION_TARGET_TYPE.VDS_NTT_SERVICE:
                        if (notificationArray[index].NotificationInfo.Service.Event == VDS_NOTIFICATION_EVENT.VDS_NF_SERVICE_OUT_OF_SYNC)
                        {
                            this.service.OnServiceOutOfSync(notificationArray[index].NotificationInfo.Service);
                        }
                        break;
                }
                continue;
            Label_00C3:
                this.service.OnPackArrived(notificationArray[index].NotificationInfo.Pack);
                continue;
            Label_00E5:
                this.service.OnPackDeparted(notificationArray[index].NotificationInfo.Pack);
                continue;
            Label_0107:
                this.service.OnPackModified(notificationArray[index].NotificationInfo.Pack);
                continue;
            Label_0162:
                this.service.OnVolumeArrived(notificationArray[index].NotificationInfo.Volume);
                continue;
            Label_0184:
                this.service.OnVolumeDeparted(notificationArray[index].NotificationInfo.Volume);
                continue;
            Label_01A6:
                this.service.OnVolumeModified(notificationArray[index].NotificationInfo.Volume);
                continue;
            Label_01C8:
                this.service.OnVolumeRebuilding(notificationArray[index].NotificationInfo.Volume);
                continue;
            Label_021F:
                this.service.OnDiskArrived(notificationArray[index].NotificationInfo.Disk);
                continue;
            Label_0241:
                this.service.OnDiskDeparted(notificationArray[index].NotificationInfo.Disk);
                continue;
            Label_0263:
                this.service.OnDiskModified(notificationArray[index].NotificationInfo.Disk);
                continue;
            Label_02BB:
                this.service.OnPartitionArrived(notificationArray[index].NotificationInfo.Partition);
                continue;
            Label_02DD:
                this.service.OnPartitionDeparted(notificationArray[index].NotificationInfo.Partition);
                continue;
            Label_02FF:
                this.service.OnPartitionModified(notificationArray[index].NotificationInfo.Partition);
                continue;
            Label_0356:
                this.service.OnDriveLetterFreed(notificationArray[index].NotificationInfo.Letter);
                continue;
            Label_0378:
                this.service.OnDriveLetterAssigned(notificationArray[index].NotificationInfo.Letter);
                continue;
            Label_03CF:
                this.service.OnFileSystemModified(notificationArray[index].NotificationInfo.FileSystem);
                continue;
            Label_03F1:
                this.service.OnFileSystemFormatting(notificationArray[index].NotificationInfo.FileSystem);
                continue;
            Label_048E:
                this.service.OnSubSystemArrived(notificationArray[index].NotificationInfo.SubSystem);
                continue;
            Label_04B0:
                this.service.OnSubSystemDeparted(notificationArray[index].NotificationInfo.SubSystem);
                continue;
            Label_0504:
                this.service.OnControllerArrived(notificationArray[index].NotificationInfo.Controller);
                continue;
            Label_0526:
                this.service.OnControllerDeparted(notificationArray[index].NotificationInfo.Controller);
                continue;
            Label_057E:
                this.service.OnDriveArrived(notificationArray[index].NotificationInfo.Drive);
                continue;
            Label_05A0:
                this.service.OnDriveDeparted(notificationArray[index].NotificationInfo.Drive);
                continue;
            Label_05C2:
                this.service.OnDriveModified(notificationArray[index].NotificationInfo.Drive);
                continue;
            Label_061A:
                this.service.OnLunArrived(notificationArray[index].NotificationInfo.Lun);
                continue;
            Label_063C:
                this.service.OnLunDeparted(notificationArray[index].NotificationInfo.Lun);
                continue;
            Label_065E:
                this.service.OnLunModified(notificationArray[index].NotificationInfo.Lun);
                continue;
            Label_06B2:
                this.service.OnPortArrived(notificationArray[index].NotificationInfo.Port);
                continue;
            Label_06D4:
                this.service.OnPortDeparted(notificationArray[index].NotificationInfo.Port);
                continue;
            Label_072C:
                this.service.OnPortalArrived(notificationArray[index].NotificationInfo.Portal);
                continue;
            Label_074E:
                this.service.OnPortalDeparted(notificationArray[index].NotificationInfo.Portal);
                continue;
            Label_0770:
                this.service.OnPortalModified(notificationArray[index].NotificationInfo.Portal);
                continue;
            Label_07C8:
                this.service.OnTargetArrived(notificationArray[index].NotificationInfo.Target);
                continue;
            Label_07EA:
                this.service.OnTargetDeparted(notificationArray[index].NotificationInfo.Target);
                continue;
            Label_080C:
                this.service.OnTargetModified(notificationArray[index].NotificationInfo.Target);
                continue;
            Label_0867:
                this.service.OnPortalGroupArrived(notificationArray[index].NotificationInfo.PortalGroup);
                continue;
            Label_0886:
                this.service.OnPortalGroupDeparted(notificationArray[index].NotificationInfo.PortalGroup);
                continue;
            Label_08A5:
                this.service.OnPortalGroupModified(notificationArray[index].NotificationInfo.PortalGroup);
            }
        }
    }
}

