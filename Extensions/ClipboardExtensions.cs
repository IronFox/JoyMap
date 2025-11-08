using JoyMap.Util;

namespace JoyMap.Extensions
{
    public static class ClipboardExtensions
    {
        public static bool CopyToClipboard(this string text)
        {
            return ClipboardUtil.CopyText(text);
        }

        public static bool CopyToClipboard(this IReadOnlyList<Profile.Event> events)
        {
            return ClipboardUtil.Copy(events);
        }
    }
}
