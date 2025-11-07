using System.Globalization;

namespace JoyMap.Extensions
{
    public static class ComponentExtensions
    {
        public static float? GetFloat(this TextBox textBox, bool isPercent)
        {
            var s = textBox.Text.Replace(',', '.');

            if (float.TryParse(s, CultureInfo.InvariantCulture, out var val))
            {
                if (isPercent)
                    return val / 100;
                return val;
            }
            return null;
        }


        public static IEnumerable<ListViewItem> ToEnumerable(this ListView.ListViewItemCollection items)
        {
            foreach (ListViewItem item in items)
            {
                yield return item;
            }
        }
    }
}
