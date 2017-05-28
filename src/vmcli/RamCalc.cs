using System;

namespace vmcli
{
    internal class RamCalc
    {
        const int bytes = 1;
        const int kbytes = 1024;
        const int mbytes = 1024 * 1024;

        internal static uint Calc(string value)
        {
            uint ramSize = 1;
            string str = value.ToUpper();

            if (str.Contains("M"))
            {
                str = str.Replace('M', ' ');
                ramSize = mbytes;
            }
            else if (str.Contains("K"))
            {
                str = str.Replace('K', ' ');
                ramSize = mbytes;
            }
            else if (str.Contains("B"))
            {
                str = str.Replace('B', ' ');
                ramSize = bytes;
            }
            else
                ramSize = bytes;
            uint size;

            if (uint.TryParse(str, out size))
            {
                ramSize *= size;
            }
            else
                ramSize *= 512;
            return ramSize;
        }
    }
}