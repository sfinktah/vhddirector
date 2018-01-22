namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    [ComVisible(false), SuppressUnmanagedCodeSecurity]
    public static class InteropHelpers
    {
        public static bool IsInterface<T>(object obj)
        {
            if ((obj == null) || !Marshal.IsComObject(obj))
            {
                return (obj is T);
            }
            IntPtr zero = IntPtr.Zero;
            Guid gUID = typeof(T).GUID;
            if (Marshal.QueryInterface(Marshal.GetIUnknownForObject(obj), ref gUID, out zero) < 0)
            {
                return false;
            }
            return (zero != IntPtr.Zero);
        }

        public static T QueryInterface<T>(object obj)
        {
            if ((obj == null) || !Marshal.IsComObject(obj))
            {
                return (T) obj;
            }
            IntPtr zero = IntPtr.Zero;
            Guid gUID = typeof(T).GUID;
            if ((Marshal.QueryInterface(Marshal.GetIUnknownForObject(obj), ref gUID, out zero) >= 0) && (zero != IntPtr.Zero))
            {
                return (T) Marshal.GetUniqueObjectForIUnknown(zero);
            }
            return default(T);
        }

        public static T QueryInterfaceThrow<T>(object obj)
        {
            if ((obj == null) || !Marshal.IsComObject(obj))
            {
                return (T) obj;
            }
            IntPtr zero = IntPtr.Zero;
            Guid gUID = typeof(T).GUID;
            if (Marshal.QueryInterface(Marshal.GetIUnknownForObject(obj), ref gUID, out zero) < 0)
            {
                throw new InvalidCastException(gUID.ToString());
            }
            if (zero == IntPtr.Zero)
            {
                throw new InvalidCastException(gUID.ToString());
            }
            return (T) Marshal.GetUniqueObjectForIUnknown(zero);
        }
    }
}

