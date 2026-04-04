using JoyMap.Profile;

namespace JoyMap.Undo.Action
{
    internal class DeleteProfileAction : IUndoableAction
    {
        private readonly MainForm _form;
        private readonly WorkProfile _profile;

        public DeleteProfileAction(MainForm form, WorkProfile profile)
        {
            _form = form;
            _profile = profile;
        }

        public string Name => $"Delete Profile '{_profile.Name}'";

        public void Execute()
        {
            _form.Flush();
            Registry.DeleteProfile(_profile.Id);
            _form.RefreshProfileList();
        }

        public void Undo()
        {
            Registry.Persist(_profile);
            _form.RefreshProfileList(_profile.Id);
        }
    }
}
