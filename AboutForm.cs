using System.Diagnostics;
using System.Reflection;

namespace JoyMap
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            var asm = Assembly.GetExecutingAssembly();
            Version? assemblyVersion = asm.GetName().Version;
            string fileVersion = FileVersionInfo.GetVersionInfo(asm.Location).FileVersion ?? "";

            // InformationalVersion (1.0.<auto>)
            string infoVersion = asm.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "";

            // WinForms convenience (same as ProductVersion in AssemblyInfo/FileVersion)
            string productVersion = Application.ProductVersion;

            lVersion.Text = $"Version {productVersion}";
        }
    }
}
