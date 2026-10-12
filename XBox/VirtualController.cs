using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using System.Diagnostics;

namespace JoyMap.XBox
{
    public class VirtualController : IDisposable
    {
        private ViGEmClient Client { get; }
        private IXbox360Controller Controller { get; }

        public VirtualController()
        {
            Client = new ViGEmClient();
            Controller = Client.CreateXbox360Controller();
            Controller.Connect();
        }

        public void ChangeButtonState(XBoxButton button, bool pressed)
        {
            // Map XBoxButton to Xbox360Button
            Xbox360Button xboxButton = button switch
            {
                XBoxButton.A => Xbox360Button.A,
                XBoxButton.B => Xbox360Button.B,
                XBoxButton.X => Xbox360Button.X,
                XBoxButton.Y => Xbox360Button.Y,
                XBoxButton.Back => Xbox360Button.Back,
                XBoxButton.Start => Xbox360Button.Start,
                XBoxButton.Guide => Xbox360Button.Guide,
                XBoxButton.ShoulderLeft => Xbox360Button.LeftShoulder,
                XBoxButton.ShoulderRight => Xbox360Button.RightShoulder,
                XBoxButton.ThumbLeft => Xbox360Button.LeftThumb,
                XBoxButton.ThumbRight => Xbox360Button.RightThumb,
                XBoxButton.DPadUp => Xbox360Button.Up,
                XBoxButton.DPadDown => Xbox360Button.Down,
                XBoxButton.DPadLeft => Xbox360Button.Left,
                XBoxButton.DPadRight => Xbox360Button.Right,
                _ => throw new ArgumentOutOfRangeException(nameof(button), $"Unhandled button: {button}"),
            };
            Controller.SetButtonState(xboxButton, pressed);
            SubmitSafe();
        }

        private static IReadOnlyDictionary<XBoxAxis, Func<float?>> EmptyFeed { get; } = new Dictionary<XBoxAxis, Func<float?>>();

        /// <summary>
        /// The most recent analog feed, re-applied when an axis override changes outside of the regular update loop.
        /// </summary>
        private IReadOnlyDictionary<XBoxAxis, Func<float?>> LastFeed { get; set; } = EmptyFeed;

        /// <summary>
        /// Axes currently forced to 1 by event actions. A forced axis ignores its natural feed value.
        /// </summary>
        private bool[] AxisOverrides { get; } = new bool[Enum.GetValues<XBoxAxis>().Length];

        public void SetAxisOverride(XBoxAxis axis, bool forced)
        {
            if (AxisOverrides[(int)axis] == forced)
                return;
            AxisOverrides[(int)axis] = forced;
            UpdateFromInputState(LastFeed);
        }

        public void UpdateFromInputState(
            IReadOnlyDictionary<XBoxAxis, Func<float?>> analogFeed
            )
        {
            LastFeed = analogFeed;

            // Map axes (normalize from [0,1] to short range)
            ApplyAxis(XBoxAxis.MoveX, Xbox360Axis.LeftThumbX, analogFeed);
            ApplyAxis(XBoxAxis.MoveY, Xbox360Axis.LeftThumbY, analogFeed);
            ApplyAxis(XBoxAxis.LookX, Xbox360Axis.RightThumbX, analogFeed);
            ApplyAxis(XBoxAxis.LookY, Xbox360Axis.RightThumbY, analogFeed);

            // Triggers (0-255)
            ApplySlider(XBoxAxis.TriggerLeft, Xbox360Slider.LeftTrigger, analogFeed);
            ApplySlider(XBoxAxis.TriggerRight, Xbox360Slider.RightTrigger, analogFeed);

            SubmitSafe();
        }

        public void ResetState()
        {
            LastFeed = EmptyFeed;
            Controller.SetAxisValue(Xbox360Axis.LeftThumbX, 0);
            Controller.SetAxisValue(Xbox360Axis.LeftThumbY, 0);
            Controller.SetAxisValue(Xbox360Axis.RightThumbX, 0);
            Controller.SetAxisValue(Xbox360Axis.RightThumbY, 0);
            Controller.SetSliderValue(Xbox360Slider.LeftTrigger, 0);
            Controller.SetSliderValue(Xbox360Slider.RightTrigger, 0);
            foreach (Xbox360Button btn in Enum.GetValues(typeof(Xbox360Button)).Cast<Xbox360Button>())
                Controller.SetButtonState(btn, false);
            SubmitSafe();
        }

        private float? GetValue(XBoxAxis axis, IReadOnlyDictionary<XBoxAxis, Func<float?>> feed)
        {
            if (AxisOverrides[(int)axis])
                return 1f;
            return feed.TryGetValue(axis, out var get) ? get() : 0f;
        }

        private void ApplySlider(XBoxAxis inTriggerAxis, Xbox360Slider outSlider, IReadOnlyDictionary<XBoxAxis, Func<float?>> feed)
        {
            var value = GetValue(inTriggerAxis, feed);
            Controller.SetSliderValue(
                outSlider,
                value.HasValue ? (byte)Math.Clamp(value.Value * 255, 0, 255) : (byte)0);
        }

        private void ApplyAxis(XBoxAxis inAxis, Xbox360Axis outAxis, IReadOnlyDictionary<XBoxAxis, Func<float?>> feed)
        {
            var value = GetValue(inAxis, feed);
            Controller.SetAxisValue(outAxis, value.HasValue ? NormalizeAxis(value.Value) : (short)0);
        }

        private const int SlowSubmitThresholdMs = 50;

        private void SubmitSafe()
        {
            var start = Stopwatch.GetTimestamp();
            try
            {
                Controller.SubmitReport();
                var elapsed = Stopwatch.GetElapsedTime(start);
                if (elapsed.TotalMilliseconds >= SlowSubmitThresholdMs)
                    MainForm.Log($"XBox SubmitReport slow: {elapsed.TotalMilliseconds:F0}ms");
            }
            catch (Exception ex)
            {
                var elapsed = Stopwatch.GetElapsedTime(start);
                MainForm.Log($"XBox SubmitReport failed after {elapsed.TotalMilliseconds:F0}ms", ex);
            }
        }

        private short NormalizeAxis(float value)
        {
            return (short)((value) * 32767);
        }

        private void SetButton(Xbox360Button button, bool pressed)
        {
            Controller.SetButtonState(button, pressed);
        }

        public void Dispose()
        {
            Controller?.Disconnect();
            Client?.Dispose();
        }
    }
}
