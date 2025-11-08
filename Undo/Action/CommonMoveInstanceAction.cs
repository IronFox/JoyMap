using JoyMap.Profile;

namespace JoyMap.Undo.Action
{
    internal class CommonMoveInstanceAction : CommonAction
    {
        public CommonMoveInstanceAction(MainForm mainForm, WorkProfile targetProfile, IReadOnlyList<int> selectedRowIndexes) : base(mainForm, targetProfile)
        {
            SelectedRowIndexes = selectedRowIndexes;
            SingleEventName = SelectedRowIndexes.Count == 1
                ? TargetProfile.Events[SelectedRowIndexes[0]].Event.Name
                : null;
        }


        public string? SingleEventName { get; }

        public string Name => SingleEventName is not null ? $"Move '{SingleEventName}' Down" : "Move Events Down";

        public IReadOnlyList<int> SelectedRowIndexes { get; }

    }
}
