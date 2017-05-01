using System.Globalization;

namespace ODataExample.Helper
{
    public class Separator
    {
        public static NumberFormatInfo changeSeparator()
        {
            NumberFormatInfo info = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();
            info.NumberDecimalSeparator = ".";
            return info;
        }
    }
}