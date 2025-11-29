using JoyMap.ControllerTracking;
using JoyMap.Profile;

namespace JoyMap
{
    public partial class ControllerFamiliesForm : Form
    {
        public ControllerFamiliesForm(InputMonitor monitor)
        {
            Monitor = monitor;
            InitializeComponent();

            foreach (var family in Monitor.ExportAllFamilies())
            {
                var item = listFamilies.Items.Add(family.FamilyName);
                item.SubItems.Add("" + family.Members.Count);
                item.Tag = family;
            }
        }

        public InputMonitor Monitor { get; }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new AddControllerFamily(null);
            var rs = form.ShowDialog();
            if (rs == DialogResult.OK)
            {
                var item = listFamilies.Items.Add(form.FamilyName);
                var family = Monitor.CreateFamily(form.FamilyName, form.SelectedMembers); ;
                item.Tag = family;
                item.SubItems.Add("" + family.Members.Count);
                Registry.SaveAllFamilies(Monitor);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listFamilies.SelectedItems.Count == 1 && listFamilies.SelectedItems[0].Tag is JsonControllerFamily family)
            {
                var item = listFamilies.SelectedItems[0];
                using var form = new AddControllerFamily(family);
                var rs = form.ShowDialog();
                if (rs == DialogResult.OK)
                {
                    var newFamily = Monitor.UpdateFamily(family, form.FamilyName, form.SelectedMembers); ;
                    item.Tag = newFamily;
                    item.Text = form.FamilyName;
                    item.SubItems[0].Text = newFamily.FamilyName;
                    item.SubItems[1].Text = "" + newFamily.Members.Count;

                    Registry.SaveAllFamilies(Monitor);
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listFamilies.SelectedItems.Count == 1 && listFamilies.SelectedItems[0].Tag is JsonControllerFamily family)
            {
                if (MessageBox.Show(this, $"Are you sure you want to delete family '{family.FamilyName}'? This operation cannot be undone!", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    var item = listFamilies.SelectedItems[0];
                    Monitor.DeleteFamily(family);
                    listFamilies.Items.Remove(item);
                    Registry.SaveAllFamilies(Monitor);
                }
            }

        }
    }
}
