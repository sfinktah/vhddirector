namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct HbaPortProperties
    {
        public Guid Id;
        public WorldWideName NodeWwn;
        public WorldWideName PortWwn;
        public HbaPortType Type;
        public HbaPortStatus Status;
        public HbaPortSpeedFlags PortSpeed;
        public HbaPortSpeedFlags SupportedPortSpeed;
    }
}

