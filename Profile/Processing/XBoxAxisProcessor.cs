using JoyMap.XBox;

namespace JoyMap.Profile.Processing
{
    public class XBoxAxisProcessor : IProcessor
    {
        public XBoxAxisProcessor(IReadOnlyList<XBoxAxisBindingInstance> xBoxMappingInstance)
        {
            Feed = xBoxMappingInstance
                .ToDictionary(
                    mi => mi.Binding.OutAxis,
                    mi =>
                    {
                        var getter = mi.GetValue;
                        return (Func<float?>)(() => mi.IsSuspended ? 0f : getter());
                    });
            MainForm.Log($"XBoxAxisProcessor created with {xBoxMappingInstance.Count} bindings: {string.Join(", ", xBoxMappingInstance.Select(m => m.Binding.OutAxis.ToString()))}");
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
