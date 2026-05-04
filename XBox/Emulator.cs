

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
            catch (Exception ex)
            {
                MainForm.Log("Failed to destroy XBox controller", ex);
            }
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

        private static bool WarnedNoController { get; set; }

        internal static void UpdateAxisState(IReadOnlyDictionary<XBoxAxis, Func<float?>> feed)
        {
            if (Controller is null)
            {
                if (!WarnedNoController)
                {
                    WarnedNoController = true;
                    MainForm.Log("UpdateAxisState: no controller available — axis updates will be dropped");
                }
                return;
            }
            WarnedNoController = false;
            Controller.UpdateFromInputState(feed);
        }

        internal static void UpdateButtonState(XBoxButton value, bool nowDown)
        {
            SignalStart();
            Controller?.ChangeButtonState(value, nowDown);
        }
    }
}
