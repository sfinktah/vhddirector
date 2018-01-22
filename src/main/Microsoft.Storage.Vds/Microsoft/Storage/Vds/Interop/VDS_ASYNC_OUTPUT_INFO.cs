namespace Microsoft.Storage.Vds.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct VDS_ASYNC_OUTPUT_INFO
    {
        [FieldOffset(0)]
        public bvp Bvp;
        [FieldOffset(0)]
        public cl Cl;
        [FieldOffset(0)]
        public cp Cp;
        [FieldOffset(0)]
        public cpg Cpg;
        [FieldOffset(0)]
        public ct Ct;
        [FieldOffset(0)]
        public cv Cv;
    }
}

