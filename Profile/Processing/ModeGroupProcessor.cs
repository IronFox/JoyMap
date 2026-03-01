namespace JoyMap.Profile.Processing
{
    public class ModeGroupProcessor : IProcessor
    {
        private IReadOnlyList<ModeGroupInstance> Groups { get; }

        public ModeGroupProcessor(IReadOnlyList<ModeGroupInstance> groups)
        {
            Groups = groups;
        }

        public void Update()
        {
            foreach (var g in Groups)
                g.Update();
        }

        public void Dispose() { }
    }
}
