using JoyMap.Profile;

namespace JoyMap.Undo.Action
{
    internal class SetWindowNameRegexAction : CommonFieldChangeAction, IUndoableAction
    {

        public SetWindowNameRegexAction(
            MainForm mainForm,
            WorkProfile targetProfile,
            TextBox textBox)
            : base(mainForm,
                  targetProfile,
                  textBox,
                  targetProfile.WindowNameRegex)
        { }

        public string Name => "Set Window Regex";

        protected override void Set(string windowRegex)
        {
            TargetProfile.WindowNameRegex = windowRegex;
            Form.WithNoEvent(() => TextBox.Text = windowRegex);
            Registry.Persist(TargetProfile, Form);
        }
    }
}
