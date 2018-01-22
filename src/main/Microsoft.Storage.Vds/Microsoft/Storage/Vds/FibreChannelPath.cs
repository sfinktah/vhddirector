namespace Microsoft.Storage.Vds
{
    using System;

    public class FibreChannelPath : Path
    {
        public FibreChannelPath(PathInfo pathInfo) : base(pathInfo)
        {
        }

        public Guid ControllerPortId
        {
            get
            {
                return this.pathInfo.SubSystemEndpoint.ControllerPortId;
            }
        }

        public Guid HbaPortId
        {
            get
            {
                return this.pathInfo.ServerEndpoint.HbaPortId;
            }
        }
    }
}

