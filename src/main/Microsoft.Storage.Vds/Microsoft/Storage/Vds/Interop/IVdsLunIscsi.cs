namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("0d7c1e64-b59b-45ae-b86a-2c2cc6a42067"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVdsLunIscsi
    {
        void AssociateTargets([In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=1)] Guid[] targetIdArray, [In] int numberOfTargets);
        void QueryAssociatedTargets(out IEnumVdsObject targets);
    }
}

