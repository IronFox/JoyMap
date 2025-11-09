using JoyMap.Profile;

namespace JoyMap.Undo.Action
{
    internal class SetProfileNameAction : CommonFieldChangeAction, IUndoableAction
    {
        public SetProfileNameAction(MainForm mainForm, WorkProfile activeProfile, TextBox textProfileName)
            : base(mainForm, activeProfile, textProfileName, activeProfile.Name)
        {
        }
        public string Name => "Set Profile Name";
        protected override void Set(string name)
        {
            TargetProfile.Name = name;
            Form.WithNoEvent(() => TextBox.Text = name);
            Registry.Persist(TargetProfile, Form);
            Form.RefreshProfileList();
        }
    }
}
