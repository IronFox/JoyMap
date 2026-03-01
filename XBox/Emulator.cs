

namespace JoyMap.XBox
{
    internal static class Emulator
    {
        private static VirtualController? Controller { get; set; }
        private static bool TriedCreate { get; set; }
        private static object Lock { get; } = new();
        public static void Destroy()
        {
            try
            {
                Controller?.Dispose();
                Controller = null;
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
            if (Controller is null && !TriedCreate)
            {
                lock (Lock)
                {
                    if (Controller is null && !TriedCreate)
                    {
                        TriedCreate = true;
                        try
                        {
                            Controller = new VirtualController();
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
            Controller?.UpdateFromInputState(feed);
        }

        internal static void UpdateButtonState(XBoxButton value, bool nowDown)
        {
            SignalStart();
            Controller?.ChangeButtonState(value, nowDown);
        }
    }
}
