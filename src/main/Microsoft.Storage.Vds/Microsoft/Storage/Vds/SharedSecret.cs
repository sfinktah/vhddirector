namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SharedSecret
    {
        public SecureStringAnsiHandle Secret;
        public uint SecretSize;
        public SharedSecret(SecureStringAnsiHandle sharedSecret)
        {
            this.Secret = sharedSecret;
            this.SecretSize = (uint) sharedSecret.Length;
        }
    }
}

