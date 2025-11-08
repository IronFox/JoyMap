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


        public static bool IsChildOf(this Form? child, Form parent)
        {
            if (child is null)
                return false;
            var p = child.Parent;
            while (p is not null)
            {
                if (p == parent)
                    return true;
                p = p.Parent;
            }
            return false;
        }


        public static int[] ToArray(this ListView.SelectedIndexCollection items)
        {
            var arr = new int[items.Count];
            for (int i = 0; i < items.Count; i++)
            {
                arr[i] = items[i];
            }
            return arr;
        }

        public static IEnumerable<int> ToEnumerable(this ListView.SelectedIndexCollection items)
        {
            foreach (int item in items)
            {
                yield return item;
            }
        }

        public static object?[] ToArray(this ComboBox.ObjectCollection items)
        {
            var arr = new object?[items.Count];
            for (int i = 0; i < items.Count; i++)
            {
                arr[i] = items[i];
            }
            return arr;
        }

        public static IEnumerable<object> ToEnumerable(this ComboBox.ObjectCollection items)
        {
            foreach (var item in items)
            {
                yield return item;
            }
        }

        public static IEnumerable<ListViewItem> ToEnumerable(this ListView.SelectedListViewItemCollection items)
        {
            foreach (ListViewItem item in items)
            {
                yield return item;
            }
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
