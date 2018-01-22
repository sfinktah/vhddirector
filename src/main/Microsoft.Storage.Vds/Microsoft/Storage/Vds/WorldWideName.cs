namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct WorldWideName
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
        private byte[] wwn;
        public byte[] Wwn
        {
            get
            {
                return this.wwn;
            }
            set
            {
                this.wwn = value;
            }
        }
        public WorldWideName(string wwn)
        {
            if (wwn == null)
            {
                throw new ArgumentNullException("wwn");
            }
            if (wwn.Length == 0x17)
            {
                wwn = wwn.Replace(":", "");
            }
            if (!IsStringWwn(wwn))
            {
                throw new ArgumentException("The WWN string specified is not a valid WWN string", "wwn");
            }
            this.wwn = new byte[wwn.Length / 2];
            for (int i = 0; i < wwn.Length; i += 2)
            {
                string str = wwn.Substring(i, 2);
                this.wwn[i / 2] = Convert.ToByte(str, 0x10);
            }
        }

        public static bool IsStringWwn(string wwnString)
        {
            if (wwnString == null)
            {
                return false;
            }
            if ((wwnString.Length != 0x10) && (wwnString.Length != 0x17))
            {
                return false;
            }
            for (int i = 0; i < wwnString.Length; i++)
            {
                if (wwnString[i] == ':')
                {
                    if (((i + 1) % 3) != 0)
                    {
                        return false;
                    }
                }
                else if (!Uri.IsHexDigit(wwnString[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            return this.ToString(false);
        }

        public string ToString(bool formatted)
        {
            if (formatted)
            {
                return (this.Wwn[0].ToString("X2") + ":" + this.Wwn[1].ToString("X2") + ":" + this.Wwn[2].ToString("X2") + ":" + this.Wwn[3].ToString("X2") + ":" + this.Wwn[4].ToString("X2") + ":" + this.Wwn[5].ToString("X2") + ":" + this.Wwn[6].ToString("X2") + ":" + this.Wwn[7].ToString("X2"));
            }
            return (this.Wwn[0].ToString("X2") + this.Wwn[1].ToString("X2") + this.Wwn[2].ToString("X2") + this.Wwn[3].ToString("X2") + this.Wwn[4].ToString("X2") + this.Wwn[5].ToString("X2") + this.Wwn[6].ToString("X2") + this.Wwn[7].ToString("X2"));
        }
    }
}

