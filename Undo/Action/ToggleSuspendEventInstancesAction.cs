using JoyMap.Extensions;
using JoyMap.Profile;

namespace JoyMap.Undo.Action
{
    internal class ToggleSuspendEventInstancesAction : CommonAction, IUndoableAction
    {
        public ToggleSuspendEventInstancesAction(MainForm mainForm, WorkProfile targetProfile, IReadOnlyList<int> eventIndexes)
            : base(mainForm, targetProfile)

        {
            SingleEventName = eventIndexes
                .Select(i => targetProfile.Events[i].Event.Name)
                .SafeSingleOrDefault();
            EventIndexes = eventIndexes;
        }
        public string? SingleEventName;
        public string Name => SingleEventName is not null
            ? $"Toggle Suspend of '{SingleEventName}'" :
            "Toggle Suspend";

        public IReadOnlyList<int> EventIndexes { get; }

        public void Execute()
        {
            Form.WithNoEvent(() =>
            {
                foreach (var idx in EventIndexes)
                {
                    TargetProfile.Events[idx].IsSuspended = !TargetProfile.Events[idx].IsSuspended;
                }
                Registry.Persist(TargetProfile);
            });
        }

        public void Undo()
        {
            Execute();
        }
    }
}
