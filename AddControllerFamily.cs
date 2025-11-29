using JoyMap.ControllerTracking;

namespace JoyMap
{
    public partial class AddControllerFamily : Form
    {
        private DevicePoller dp;
        public AddControllerFamily(JsonControllerFamily? load)
        {
            dp = new DevicePoller();


            InitializeComponent();
            listDevices.Items.Clear();
            if (load is not null)
            {
                textFamilyName.Text = load.FamilyName;
                foreach (var member in load.Members)
                {
                    listDevices.Items.Add(new Product(member.Guid, member.Name));
                    listDevices.SetItemChecked(listDevices.Items.Count - 1, true);
                }
            }
        }

        private void timerRedetectDevices_Tick(object sender, EventArgs e)
        {
            try
            {
                foreach (var dev in dp)
                {
                    bool found = false;
                    foreach (Product item in listDevices.Items)
                    {
                        if (item.Guid == dev.Guid)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                        listDevices.Items.Add(new Product(dev.Guid, dev.Name));
                }
            }
            catch { }
        }

        public string FamilyName => textFamilyName.Text;

        public Product[] SelectedMembers
        {
            get
            {
                List<Product> members = new List<Product>();
                foreach (Product item in listDevices.CheckedItems)
                {
                    members.Add(item);
                }
                return members.ToArray();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            timerRedetectDevices.Stop();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }
    }
}
