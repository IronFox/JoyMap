using JoyMap.Extensions;
using JoyMap.Profile;

namespace JoyMap.Undo.Action.Binding
{
    internal class ToggleSuspendXBoxBindingInstancesAction : CommonAction, IUndoableAction
    {
        public ToggleSuspendXBoxBindingInstancesAction(MainForm mainForm, WorkProfile targetProfile, IReadOnlyList<int> bindingIndexes)
            : base(mainForm, targetProfile)

        {
            SingleName = bindingIndexes
                .Select(i => mainForm.BindingListView.Items[i].Name)
                .SafeSingleOrDefault();
            BindingIndexes = bindingIndexes;
        }
        public string? SingleName;
        public string Name => SingleName is not null
            ? $"Toggle Suspend of '{SingleName}'" :
            "Toggle Suspend";

        public IReadOnlyList<int> BindingIndexes { get; }

        public void Execute()
        {
            Form.WithNoEvent(() =>
            {
                foreach (var idx in BindingIndexes)
                {
                    var bound = Form.BindingListView.Items[idx]?.Tag as XBoxAxisBindingInstance;
                    if (bound is not null)
                        bound.IsSuspended = !bound.IsSuspended;
                }
            });
        }

        public void Undo()
        {
            Execute();
        }
    }
}
