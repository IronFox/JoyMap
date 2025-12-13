using JoyMap.XBox;

namespace JoyMap.Profile.Processing
{
    public class XBoxAxisProcessor : IProcessor
    {
        public XBoxAxisProcessor(IReadOnlyList<XBoxMappingInstance> xBoxMappingInstance)
        {
            Feed = xBoxMappingInstance
                .ToDictionary(
                    mi => mi.Mapping.OutAxis,
                    mi => mi.GetValue
                    );
            Emulator.SignalStart();
        }

        private IReadOnlyDictionary<XBoxAxis, Func<float?>> Feed { get; }

        public void Dispose()
        {
            Emulator.SignalEnd();
        }

        public void Update()
        {
            Emulator.UpdateAxisState(Feed);
        }
    }
}
