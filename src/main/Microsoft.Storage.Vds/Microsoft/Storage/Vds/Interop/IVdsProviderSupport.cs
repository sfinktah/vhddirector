namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("1732be13-e8f9-4a03-bfbc-5f616aa66ce1")]
    public interface IVdsProviderSupport
    {
        void GetVersionSupport(out VersionSupportFlags versionSupport);
    }
}

