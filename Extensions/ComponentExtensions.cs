using System.Globalization;

namespace JoyMap.Extensions
{
    public static class ComponentExtensions
    {
        public static int? GetInt(this TextBox textBox)
        {
            var s = textBox.Text;
            if (int.TryParse(s, out var val))
            {
                return val;
            }
            return null;
        }
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

        public static void Configure<T>(this ComboBox comboBox, IEnumerable<T> options)
            where T : notnull
        {
            comboBox.Items.Clear();
            foreach (var opt in options)
                comboBox.Items.Add(opt);
            comboBox.FlatStyle = FlatStyle.Flat;
        }

        /// <summary>
        /// Enables owner-draw on a ToolTip so it renders with a dark background and light text.
        /// </summary>
        public static void MakeDark(this ToolTip toolTip)
        {
            toolTip.OwnerDraw = true;
            toolTip.Draw += static (sender, e) =>
            {
                var backColor = Color.FromArgb(30, 30, 30);
                var foreColor = Color.FromArgb(220, 220, 220);
                var borderColor = Color.FromArgb(90, 90, 90);

                using var backBrush = new SolidBrush(backColor);
                using var foreBrush = new SolidBrush(foreColor);
                using var borderPen = new Pen(borderColor);

                e.Graphics.FillRectangle(backBrush, e.Bounds);
                e.Graphics.DrawRectangle(borderPen, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1));

                var textBounds = new RectangleF(
                    e.Bounds.X + 4,
                    e.Bounds.Y + 3,
                    e.Bounds.Width - 8,
                    e.Bounds.Height - 6);

                e.Graphics.DrawString(e.ToolTipText, e.Font ?? SystemFonts.DefaultFont, foreBrush, textBounds);
            };
            toolTip.Popup += static (sender, e) =>
            {
                // Measure the text so the balloon fits correctly with the custom padding
                if (sender is not ToolTip tt) return;
                var text = e.AssociatedControl is not null
                    ? (tt.GetToolTip(e.AssociatedControl))
                    : null;
                if (string.IsNullOrEmpty(text)) return;

                using var g = e.AssociatedControl!.CreateGraphics();
                var font = e.AssociatedControl.Font ?? SystemFonts.DefaultFont;
                var measured = g.MeasureString(text, font, 400);
                e.ToolTipSize = new Size((int)Math.Ceiling(measured.Width) + 12, (int)Math.Ceiling(measured.Height) + 8);
            };
        }

    }
}
