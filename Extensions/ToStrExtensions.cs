using System.Globalization;

namespace JoyMap.Extensions
{
    public static class ToStrExtensions
    {
        public static string ToStr(this float v)
        {
            return v.ToString(CultureInfo.InvariantCulture);
        }
        public static string ToStr(this double v)
        {
            return v.ToString(CultureInfo.InvariantCulture);
        }
        public static string ToStr(this float? v)
        {
            return v?.ToString(CultureInfo.InvariantCulture) ?? "<null>";
        }
        public static string ToStr(this double? v)
        {
            return v?.ToString(CultureInfo.InvariantCulture) ?? "<null>";
        }
    }
}
