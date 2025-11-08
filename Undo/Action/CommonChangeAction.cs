using JoyMap.Profile;

namespace JoyMap.Undo.Action
{
    internal abstract class CommonChangeAction<T> : CommonAction
    {
        public CommonChangeAction(MainForm mainForm, WorkProfile targetProfile, T oldValue, T newValue) : base(mainForm, targetProfile)
        {
            Old = oldValue;
            New = newValue;
        }

        protected T New { get; set; }
        protected T Old { get; }

        protected abstract void Set(T to);

        public void Execute()
        {
            if (!IsValid)
                return;
            Set(New);
        }

        public void Undo()
        {
            if (!IsValid)
                return;
            Set(Old);
        }

        internal void UpdateNewValue(T newValue)
        {
            if (!IsValid)
                return;
            New = newValue;
            Set(New);
        }
    }
}