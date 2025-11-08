using JoyMap.Profile;

namespace JoyMap.Undo.Action
{
    internal class CommonAction
    {
        protected MainForm Form { get; }
        protected WorkProfile TargetProfile { get; }

        public bool IsValid => Form.ActiveProfile == TargetProfile;

        public CommonAction(MainForm mainForm, WorkProfile targetProfile)
        {
            Form = mainForm;
            TargetProfile = targetProfile;
        }

    }
}