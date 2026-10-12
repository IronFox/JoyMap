

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
            lock (Lock)
                Controller.UpdateFromInputState(feed);
        }

        /// <summary>
        /// Forces the given axis to 1 while <paramref name="forced"/> is true.
        /// When released, the axis returns to its natural (mapped) value.
        /// Callers aggregate overlapping sources; this receives only the combined state.
        /// </summary>
        internal static void SetAxisOverride(XBoxAxis axis, bool forced)
        {
            SignalStart();
            if (Controller is null)
                return;
            lock (Lock)
                Controller.SetAxisOverride(axis, forced);
        }

        internal static void UpdateButtonState(XBoxButton value, bool nowDown)
        {
            SignalStart();
            Controller?.ChangeButtonState(value, nowDown);
        }
    }
}
