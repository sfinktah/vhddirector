using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHD_Director
{
    class Format
    {
        public static string FormatBytes(long bytes)
        {
            const int scale = 1000;
            string[] orders = new string[] { "PB", "TB", "GB", "MB", "kB", "Bytes" };
            long max = (long)Math.Pow(scale, orders.Length - 1);

            foreach (string order in orders)
            {
                if (bytes > max)
                    return string.Format("{0:##.#} {1}", decimal.Divide(bytes, max), order);

                max /= scale;
            }
            return "0 Bytes";
        }
    }
}
