

namespace JoyMap.XBox
{
    internal static class Emulator
    {
        private static VirtualController? _controller;
        private static bool _triedCreate;
        private static object _lock = new();
        public static void Destroy()
        {
            try
            {
                _controller?.Dispose();
                _controller = null;
            }
            catch { }   //dont care
        }
        internal static void SignalEnd()
        {
            //let's keep it
            //_controller?.Dispose();
            //_controller = null;
        }

        internal static void SignalStart()
        {
            if (_controller is null && !_triedCreate)
            {
                lock (_lock)
                {
                    if (_controller is null && !_triedCreate)
                    {
                        _triedCreate = true;
                        try
                        {
                            _controller = new VirtualController();
                            MainForm.Log("XBox controller created");
                        }
                        catch (Exception ex)
                        {
                            MainForm.Log("Failed to create XBox controller emulator", ex);
                        }
                    }
                }
            }
        }

        internal static void UpdateAxisState(IReadOnlyDictionary<XBoxAxis, Func<float?>> feed)
        {
            _controller?.UpdateFromInputState(feed);
        }

        internal static void UpdateButtonState(XBoxButton value, bool nowDown)
        {
            SignalStart();
            _controller?.ChangeButtonState(value, nowDown);
        }
    }
}
