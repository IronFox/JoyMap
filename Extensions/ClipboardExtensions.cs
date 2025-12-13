using JoyMap.Util;

namespace JoyMap.Extensions
{
    public static class ClipboardExtensions
    {
        public static bool CopyToClipboard(this string text)
        {
            return ClipboardUtil.CopyText(text);
        }

        public static bool CopyToClipboard<T>(this IReadOnlyList<T> events)
            where T : IJsonCompatible
        {
            return ClipboardUtil.Copy(events);
        }

    }
}
