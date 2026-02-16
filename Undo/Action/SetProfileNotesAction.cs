using JoyMap.Profile;

namespace JoyMap.Undo.Action
{
    internal class SetProfileNotesAction : CommonFieldChangeAction, IUndoableAction
    {
        public SetProfileNotesAction(MainForm mainForm, WorkProfile activeProfile, TextBox textProfileNotes)
            : base(mainForm, activeProfile, textProfileNotes, activeProfile.Notes ?? "")
        {
        }
        public string Name => "Set Profile Notes";
        protected override void Set(string text)
        {
            TargetProfile.Notes = text;
            Form.WithNoEvent(() => TextBox.Text = text);
            Registry.Persist(TargetProfile);
        }
    }
}
