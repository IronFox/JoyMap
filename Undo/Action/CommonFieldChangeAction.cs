using JoyMap.Profile;

namespace JoyMap.Undo.Action
{
    internal abstract class CommonFieldChangeAction : CommonChangeAction<string>
    {
        public CommonFieldChangeAction(
            MainForm mainForm,
            WorkProfile targetProfile,
            TextBox textBox,
            string oldValue) : base(
                mainForm,
                targetProfile,
                oldValue,
                textBox.Text)
        {
            TextBox = textBox;
        }

        public TextBox TextBox { get; }
    }
}
