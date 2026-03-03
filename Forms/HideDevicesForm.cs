using JoyMap.ControllerTracking;
using JoyMap.Profile;

namespace JoyMap
{
    public partial class HideDevicesForm : Form
    {
        private const string DownloadUrl = "https://github.com/nefarius/HidHide/releases";

        private DevicePoller DevicePoller { get; }
        private IReadOnlySet<string> BlockedIds { get; set; }

        public HideDevicesForm()
        {
            InitializeComponent();
            if (Application.OpenForms.Count > 0 && Application.OpenForms[0]?.Icon is Icon icon)
                Icon = icon;
            DevicePoller = new DevicePoller();

            if (!HidHideService.IsAvailable)
            {
                labelStatus.Text = "⚠ HidHide driver is not installed. Download from:";
                labelStatus.ForeColor = Color.OrangeRed;
                linkLabelDownload.Text = DownloadUrl;
                linkLabelDownload.Visible = true;
                listDevices.Enabled = false;
                btnApply.Enabled = false;
            }
            else
            {
                labelStatus.Text = "✓ HidHide is installed.";
                labelStatus.ForeColor = Color.DarkGreen;
            }

            BlockedIds = HidHideService.GetBlockedInstanceIds();

            foreach (var product in Registry.LoadHiddenProducts())
                AddOrUpdateProduct(product);
        }

        private void AddOrUpdateProduct(Product product)
        {
            for (int i = 0; i < listDevices.Items.Count; i++)
            {
                if (listDevices.Items[i] is Product existing && existing.Guid == product.Guid)
                    return;
            }
            var isHidden = HidHideService.IsProductHidden(product.Guid, BlockedIds);
            listDevices.Items.Add(product, isHidden);
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            foreach (var dev in DevicePoller)
                AddOrUpdateProduct(dev);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Apply();
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Apply()
        {
            var toHide = new List<Product>();
            var toReveal = new List<Product>();

            for (int i = 0; i < listDevices.Items.Count; i++)
            {
                if (listDevices.Items[i] is not Product product)
                    continue;
                var wasHidden = HidHideService.IsProductHidden(product.Guid, BlockedIds);
                var shouldHide = listDevices.GetItemChecked(i);

                if (shouldHide && !wasHidden)
                    toHide.Add(product);
                else if (!shouldHide && wasHidden)
                    toReveal.Add(product);
            }

            try
            {
                HidHideService.Apply(toHide, toReveal, BlockedIds);
                BlockedIds = HidHideService.GetBlockedInstanceIds();

                var hiddenProducts = new List<Product>();
                for (int i = 0; i < listDevices.Items.Count; i++)
                    if (listDevices.GetItemChecked(i) && listDevices.Items[i] is Product p)
                        hiddenProducts.Add(p);

                Registry.SaveHiddenProducts(hiddenProducts);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to update HidHide settings: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabelDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = DownloadUrl,
                UseShellExecute = true
            });
        }

        private void copyUrlMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(DownloadUrl);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            timerRefresh.Stop();
        }
    }
}
