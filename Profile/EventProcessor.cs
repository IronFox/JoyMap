using JoyMap.Util;

namespace JoyMap.Profile
{
    public class EventProcessor
    {
        private EventInstance Source { get; }
        private Func<bool> IsTriggered { get; }
        private bool LastTriggeredState { get; set; } = false;

        private List<ActionProcessor> ActionProcessors { get; } = [];

        public static Func<bool>? BuildTriggerCombiner(string? combiner, IReadOnlyList<TriggerInstance> triggerInstances)
        {
            if (combiner?.ToLower() == "and")
            {
                return () => triggerInstances
                    .All(t => t.IsTriggered);
            }
            else if (combiner?.ToLower() == "or")
            {
                return () => triggerInstances
                    .Any(t => t.IsTriggered);
            }
            else
            {
                return ExpressionCompiler.CompileBooleanExpression(
                    combiner ?? "false",
                    triggerInstances.Select((t, i) => KeyValuePair.Create($"T{i}", () => t.IsTriggered)).ToDictionary()
                );
            }
        }
        public EventProcessor(EventInstance source)
        {
            Source = source;
            IsTriggered = BuildTriggerCombiner(source.Event.TriggerCombiner, source.TriggerInstances) ?? (() => false);

            foreach (var action in source.Event.Actions)
            {
                var processor = new ActionProcessor(action);
                ActionProcessors.Add(processor);
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
            }
        }

        public void Stop()
        {
            foreach (var processor in ActionProcessors)
            {
                processor.SetTriggerStatus(false);
            }

        }
    }
}
