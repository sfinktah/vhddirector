namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Utilities
    {
        public Utilities()
        {
        }

        public static IntPtr IntPtrAddOffset(IntPtr ptr, uint offset)
        {
            if (Marshal.SizeOf(typeof(IntPtr)) == Marshal.SizeOf(typeof(uint)))
            {
                return new IntPtr(ptr.ToInt32() + ((int) offset));
            }
            return new IntPtr(ptr.ToInt64() + offset);
        }

        public static string[] ParseUnmaskListTargetName(string unmaskList)
        {
            return unmaskList.Split(new char[] { ';' });
        }

        public static WorldWideName[] ParseUnmaskListWwn(string unmaskList)
        {
            string[] strArray = unmaskList.Split(new char[] { ';' });
            WorldWideName[] nameArray = new WorldWideName[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                nameArray[i] = new WorldWideName(strArray[i]);
            }
            return nameArray;
        }

        public static string TargetNamesToString(string[] targetNames)
        {
            StringBuilder builder = new StringBuilder();
            if (targetNames.Length > 0)
            {
                builder.Append(targetNames[0]);
            }
            else
            {
                return "";
            }
            for (int i = 1; i < targetNames.Length; i++)
            {
                builder.AppendFormat(";{0}", targetNames[i]);
            }
            return builder.ToString();
        }

        public static string WwnArrayToString(WorldWideName[] wwns)
        {
            StringBuilder builder = new StringBuilder();
            if (wwns.Length > 0)
            {
                builder.Append(wwns[0].ToString());
            }
            else
            {
                return "";
            }
            for (int i = 1; i < wwns.Length; i++)
            {
                builder.AppendFormat(";{0}", wwns[i].ToString());
            }
            return builder.ToString();
        }
    }
}

