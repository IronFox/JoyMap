namespace JoyMap.Util
{
    public static class ClipboardUtil
    {
        private const string AppKey = "JoyMap";
        private record DataContainer<T>
            (string Key, string Type, IReadOnlyList<T> Data);

        public static IReadOnlyList<T>? GetCopied<T>() where T : IJsonCompatible
        {
            try
            {
                var text = System.Windows.Forms.Clipboard.GetText();
                var events = JsonUtil.Deserialize<DataContainer<T>>(text);
                if (events.Key != AppKey || events.Type != typeof(T).Name)
                    return null;
                if (events.Data.Count == 0)
                    return null;
                return events.Data;
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

        internal static bool Copy<T>(IReadOnlyList<T> items) where T : IJsonCompatible
        {
            if (items.Count == 0)
            {
                MainForm.Log("Nothing to copy");
                return false;
            }
            var json = JsonUtil.Serialize(new DataContainer<T>(AppKey, typeof(T).Name, items));
            if (CopyText(json))
            {
                MainForm.Log($"Copied {items.Count} {typeof(T).Name}(s)");
                return true;
            }
            return false;
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
