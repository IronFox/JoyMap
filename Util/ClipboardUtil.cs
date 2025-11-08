using JoyMap.Profile;

namespace JoyMap.Util
{
    public static class ClipboardUtil
    {
        public static bool HasEventsCopied => GetCopiedEvents() is not null;

        public static IReadOnlyList<Event>? GetCopiedEvents()
        {
            try
            {
                var text = System.Windows.Forms.Clipboard.GetText();
                var events = JsonUtil.Deserialize<IReadOnlyList<Event>>(text);
                if (events.Count == 0)
                    return null;
                return events;
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                return null;
            }
            catch (System.Text.Json.JsonException)
            {
                return null;
            }
        }

        internal static bool Copy(IReadOnlyList<Event> events)
        {
            var json = JsonUtil.Serialize(events);
            return CopyText(json);
        }

        internal static bool CopyText(string text)
        {
            try
            {
                System.Windows.Forms.Clipboard.SetText(text);
                return true;
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                return false;
            }
        }
    }
}
