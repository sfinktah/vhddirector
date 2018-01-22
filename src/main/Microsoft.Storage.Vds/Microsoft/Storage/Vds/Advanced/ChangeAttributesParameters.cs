namespace Microsoft.Storage.Vds.Advanced
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ChangeAttributesParameters
    {
        public PartitionStyle Style;
        public Microsoft.Storage.Vds.Advanced.Attribute Attribute;
    }
}

