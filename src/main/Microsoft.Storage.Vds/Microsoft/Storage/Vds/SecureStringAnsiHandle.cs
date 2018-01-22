namespace Microsoft.Storage.Vds
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Security;

    public class SecureStringAnsiHandle : SafeHandle
    {
        private int _length;

        public SecureStringAnsiHandle(SecureString password) : base((password == null) ? IntPtr.Zero : Marshal.SecureStringToCoTaskMemAnsi(password), true)
        {
            this._length = (password == null) ? 0 : password.Length;
        }

        public static implicit operator IntPtr(SecureStringAnsiHandle marshal)
        {
            return marshal.handle;
        }

        protected override bool ReleaseHandle()
        {
            if (base.handle != IntPtr.Zero)
            {
                Marshal.ZeroFreeCoTaskMemUnicode(base.handle);
            }
            return true;
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0} handle = {1}", new object[] { base.ToString(), this.handle.ToString() });
        }

        public override bool IsInvalid
        {
            get
            {
                return (base.handle == IntPtr.Zero);
            }
        }

        public int Length
        {
            get
            {
                return this._length;
            }
        }
    }
}

