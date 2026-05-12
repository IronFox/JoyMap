

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
                TriedCreate = false;
            }
            catch (Exception ex)
            {
                MainForm.Log("Failed to destroy XBox controller", ex);
            }
        }

        /// <summary>
        /// Creates the virtual controller eagerly at app startup so it is visible
        /// to games before they enumerate XInput devices.
        /// </summary>
        internal static void EnsureStarted()
        {
            SignalStart();
        }

        internal static void SignalEnd()
        {
            // Keep the controller alive so the game keeps seeing it,
            // but zero all axes/buttons so inputs don't stick.
            Controller?.ResetState();
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
