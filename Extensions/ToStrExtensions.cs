using System.Globalization;

namespace JoyMap.Extensions
{
    public static class ToStrExtensions
    {
        public static string ToStr(this float f)
        {
            return f.ToString(CultureInfo.InvariantCulture);
        }
        public static string ToStr(this double f)
        {
            return f.ToString(CultureInfo.InvariantCulture);
        }
    }
}
