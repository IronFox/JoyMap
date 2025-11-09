using JoyMap.Profile;

namespace JoyMap.Undo.Action
{
    internal class SetProcessNameRegexAction : CommonFieldChangeAction, IUndoableAction
    {

        public SetProcessNameRegexAction(
            MainForm mainForm,
            WorkProfile targetProfile,
            TextBox textBox)
            : base(mainForm,
                  targetProfile,
                  textBox,
                  targetProfile.ProcessNameRegex)
        { }

        public string Name => "Set Process Regex";

        protected override void Set(string windowRegex)
        {
            TargetProfile.ProcessNameRegex = windowRegex;
            Form.WithNoEvent(() => TextBox.Text = windowRegex);
            Registry.Persist(TargetProfile, Form);
        }
    }
}
