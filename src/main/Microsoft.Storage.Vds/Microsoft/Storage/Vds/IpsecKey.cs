namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct IpsecKey
    {
        public SecureStringAnsiHandle Key;
        public uint KeySize;
        public IpsecKey(SecureStringAnsiHandle ipsecKey)
        {
            this.Key = ipsecKey;
            this.KeySize = (uint) ipsecKey.Length;
        }
    }
}

