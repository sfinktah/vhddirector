namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    public class IscsiPath : Path
    {
        public IscsiPath(PathInfo pathInfo) : base(pathInfo)
        {
        }

        public Guid InitiatorAdapterId
        {
            get
            {
                return this.pathInfo.ServerEndpoint.InitiatorAdapterId;
            }
        }

        public IPAddress InitiatorPortalIPAddress
        {
            get
            {
                return (IPAddress) Marshal.PtrToStructure(this.pathInfo.AdditionalInfo, typeof(IPAddress));
            }
        }

        public Guid TargetPortId
        {
            get
            {
                return this.pathInfo.SubSystemEndpoint.TargetPortalId;
            }
        }
    }
}

