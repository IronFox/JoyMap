using JoyMap.Profile;

namespace JoyMap.Undo.Action
{
    internal class SetHideControllersAction : CommonAction, IUndoableAction
    {
        private CheckBox CheckBox { get; }
        private bool OldValue { get; }
        private bool NewValue { get; }

        public SetHideControllersAction(MainForm mainForm, WorkProfile activeProfile, CheckBox checkBox, bool newValue)
            : base(mainForm, activeProfile)
        {
            CheckBox = checkBox;
            OldValue = activeProfile.HideControllers;
            NewValue = newValue;
        }

        public string Name => "Set Hide Controllers";

        public void Execute() => Set(NewValue);
        public void Undo() => Set(OldValue);

        private void Set(bool value)
        {
            TargetProfile.HideControllers = value;
            Form.WithNoEvent(() => CheckBox.Checked = value);
            Form.ApplyHidingForCurrentProfile();
            Registry.Persist(TargetProfile);
        }
    }
}
