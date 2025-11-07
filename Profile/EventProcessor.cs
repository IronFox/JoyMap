namespace JoyMap.Profile
{
    public class EventProcessor
    {
        private EventInstance Source { get; }
        private Func<bool> IsTriggered { get; }
        private bool LastTriggeredState { get; set; } = false;

        private List<ActionProcessor> ActionProcessors { get; } = [];
        public EventProcessor(EventInstance source)
        {
            Source = source;
            if (source.Event.TriggerCombiner?.ToLower() == "and")
            {
                IsTriggered = () => Source.TriggerInstances
                    .All(t => t.IsTriggered);
            }
            else if (source.Event.TriggerCombiner?.ToLower() == "or")
            {
                IsTriggered = () => Source.TriggerInstances
                    .Any(t => t.IsTriggered);
            }
            else
                IsTriggered = () => false;

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
