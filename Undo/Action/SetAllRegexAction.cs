using JoyMap.Profile;

namespace JoyMap.Undo.Action
{
    internal class SetAllRegexAction : CommonChangeAction<(string ProcessName, string WindowName)>, IUndoableAction
    {

        public SetAllRegexAction(
            MainForm mainForm,
            WorkProfile activeProfile,
            string processName,
            string windowName,
            TextBox textProcessNameRegex,
            TextBox textWindowNameRegex
            )
            : base(mainForm, activeProfile, (activeProfile.ProcessNameRegex, activeProfile.WindowNameRegex), (processName, windowName))
        {
            TextProcessNameRegex = textProcessNameRegex;
            TextWindowNameRegex = textWindowNameRegex;
        }

        public string Name => "Set All Regex";

        public TextBox TextProcessNameRegex { get; }
        public TextBox TextWindowNameRegex { get; }

        protected override void Set((string ProcessName, string WindowName) to)
        {
            TargetProfile.ProcessNameRegex = to.ProcessName;
            TargetProfile.WindowNameRegex = to.WindowName;
            Form.WithNoEvent(() =>
            {
                TextProcessNameRegex.Text = TargetProfile.ProcessNameRegex;
                TextWindowNameRegex.Text = TargetProfile.WindowNameRegex;
            });
            Registry.Persist(TargetProfile, Form);
        }
    }
}
