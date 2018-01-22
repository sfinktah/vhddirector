namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("9e6fa560-c141-477b-83ba-0b6c38f7febf")]
    public interface IVdsSubSystemInterconnect
    {
        void GetSupportedInterconnects(out SubSystemInterconnectFlags supportedInterconnectsFlag);
    }
}

