using JoyMap.Profile.Processing;
using JoyMap.Util;

namespace JoyMap.Profile
{
    public class EventProcessor : IProcessor
    {
        private EventInstance Source { get; }
        private Func<bool> IsTriggered { get; }
        private bool LastTriggeredState { get; set; } = false;

        private List<IActionProcessor> ActionProcessors { get; } = [];

        public static Func<bool>? BuildTriggerCombiner(string? combiner, IReadOnlyList<TriggerInstance> triggerInstances)
        {
            if (combiner?.ToLower() == "and")
            {
                return () => triggerInstances
                    .All(t => t.IsTriggered());
            }
            else if (combiner?.ToLower() == "or")
            {
                return () => triggerInstances
                    .Any(t => t.IsTriggered());
            }
            else
            {
                return ExpressionCompiler.CompileBooleanExpression(
                    combiner ?? "false",
                    triggerInstances.Select((t, i) => KeyValuePair.Create($"T{i}", t.IsTriggered)).ToDictionary()
                );
            }
        }
        public EventProcessor(EventInstance source)
        {
            Source = source;
            IsTriggered = BuildTriggerCombiner(source.Event.TriggerCombiner, source.TriggerInstances) ?? (() => false);

            foreach (var action in source.Event.Actions)
            {
                if (action.SimpleInputEffect is not null)
                    ActionProcessors.Add(new SimpleActionProcessor(action, action.SimpleInputEffect));
                else if (action.ChangeTriggeredInputEffect is not null)
                    ActionProcessors.Add(new TriggeredActionProcessor(action, action.ChangeTriggeredInputEffect));
            }

        }


        public void Update()
        {
            var currentState = IsTriggered();
            if (currentState != LastTriggeredState)
            {
                LastTriggeredState = currentState;
                foreach (var processor in ActionProcessors)
                {
                    processor.SetTriggerStatus(currentState);
                }
            }
            else
            {
                if (currentState)
                    foreach (var processor in ActionProcessors)
                    {
                        processor.UpdateTriggered();
                    }
                else
                    foreach (var processor in ActionProcessors)
                    {
                        processor.UpdateNonTriggered();
                    }
            }
        }

        public void Dispose()
        {
            if (LastTriggeredState)
                foreach (var processor in ActionProcessors)
                {
                    processor.SetTriggerStatus(false);
                }
            foreach (var processor in ActionProcessors)
            {
                processor.Dispose();
            }

        }
    }
}
