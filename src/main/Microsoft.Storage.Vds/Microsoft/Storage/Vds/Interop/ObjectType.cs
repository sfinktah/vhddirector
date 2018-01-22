namespace Microsoft.Storage.Vds.Interop
{
    using System;

    public enum ObjectType
    {
        Async = 100,
        Controller = 0x1f,
        Disk = 13,
        Drive = 0x20,
        Enum = 0x65,
        HbaPort = 90,
        InitAdapter = 0x5b,
        InitPortal = 0x5c,
        Lun = 0x21,
        LunPlex = 0x22,
        Pack = 10,
        Port = 0x23,
        Portal = 0x24,
        PortalGroup = 0x26,
        Provider = 1,
        SubSystem = 30,
        Target = 0x25,
        Unknown = 0,
        Volume = 11,
        VolumePlex = 12
    }
}

