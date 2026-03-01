namespace JoyMap.Profile.Processing
{
    public class GlobalStatusProcessor : IProcessor
    {
        private IReadOnlyList<GlobalStatusInstance> Statuses { get; }

        public GlobalStatusProcessor(IReadOnlyList<GlobalStatusInstance> statuses)
        {
            Statuses = statuses;
        }

        public void Update()
        {
            foreach (var s in Statuses)
                s.Update();
        }

        public void Dispose() { }
    }
}
