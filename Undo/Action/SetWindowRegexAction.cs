using JoyMap.Profile;
using System.Text.RegularExpressions;

namespace JoyMap.Undo.Action
{
    internal class SetWindowRegexAction : CommonAction, IUndoableAction
    {
        private string NewWindowRegex { get; set; }
        private string OldWindowRegex { get; }

        public SetWindowRegexAction(MainForm mainForm, WorkProfile activeProfile, string newWindowRegex)
            : base(mainForm, activeProfile)
        {
            NewWindowRegex = newWindowRegex;
            OldWindowRegex = activeProfile.WindowRegex;
        }

        public string Name => "Set Window Regex";

        private void Set(string windowRegex)
        {
            if (Regex.Escape(TargetProfile.Name) == TargetProfile.WindowRegex)
            {
                TargetProfile.Name = windowRegex;
                Form.WithNoEvent(() => Form.TextProfileName.Text = TargetProfile.Name);
            }
            TargetProfile.WindowRegex = windowRegex;
            Form.WithNoEvent(() => Form.TextWindowRegex.Text = TargetProfile.WindowRegex);
            Registry.Persist(TargetProfile);
        }

        public void Execute()
        {
            if (!IsValid)
                return;
            Set(NewWindowRegex);
        }

        public void Undo()
        {
            if (!IsValid)
                return;
            Set(OldWindowRegex);
        }

        internal void UpdateNewValue(string text)
        {
            if (!IsValid)
                return;
            NewWindowRegex = text;
            Set(NewWindowRegex);
        }
    }
}
