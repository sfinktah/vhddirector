namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("118610b7-8d94-4030-b5b8-500889788e4e")]
    public interface IEnumVdsObject
    {
        void Next([In] uint numberOfObjects, [MarshalAs(UnmanagedType.IUnknown)] out object objectUnk, out uint numberFetched);
        void Skip([In] uint NumberOfObjects);
        void Reset();
        void Clone(out IEnumVdsObject Enum);
    }
}

