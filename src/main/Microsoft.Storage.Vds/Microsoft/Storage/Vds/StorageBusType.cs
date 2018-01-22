namespace Microsoft.Storage.Vds
{
    using System;

    public enum StorageBusType
    {
        Ata = 3,
        Atapi = 2,
        Fibre = 6,
        Iscsi = 9,
        MaxReserved = 0x7f,
        Raid = 8,
        Sas = 10,
        Sata = 11,
        Scsi = 1,
        Ssa = 5,
        Type1394 = 4,
        Unknown = 0,
        Usb = 7
    }
}

