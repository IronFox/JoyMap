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
                XBoxButton.ShoulderLeft => Xbox360Button.LeftShoulder,
                XBoxButton.ShoulderRight => Xbox360Button.RightShoulder,
                XBoxButton.ThumbLeft => Xbox360Button.LeftThumb,
                XBoxButton.ThumbRight => Xbox360Button.RightThumb,
                _ => throw new ArgumentOutOfRangeException(nameof(button), $"Unhandled button: {button}"),
            };
            Controller.SetButtonState(xboxButton, pressed);
            SubmitSafe();
        }

        public void UpdateFromInputState(
            IReadOnlyDictionary<XBoxAxis, Func<float?>> analogFeed
            )
        {
            // Map axes (normalize from [0,1] to short range)
            ApplyAxis(XBoxAxis.MoveX, Xbox360Axis.LeftThumbX, analogFeed);
            ApplyAxis(XBoxAxis.MoveY, Xbox360Axis.LeftThumbY, analogFeed);
            ApplyAxis(XBoxAxis.LookX, Xbox360Axis.RightThumbX, analogFeed);
            ApplyAxis(XBoxAxis.LookY, Xbox360Axis.RightThumbY, analogFeed);

            // Triggers (0-255)
            ApplySlider(XBoxAxis.TriggerLeft, Xbox360Slider.LeftTrigger, analogFeed);

            SubmitSafe();
        }

        private void ApplySlider(XBoxAxis inTriggerAxis, Xbox360Slider outSlider, IReadOnlyDictionary<XBoxAxis, Func<float?>> feed)
        {
            if (feed.TryGetValue(inTriggerAxis, out var get))
            {
                var v = get();
                if (v.HasValue)
                    Controller.SetSliderValue(outSlider, (byte)Math.Clamp(v.Value * 255, 0, 255));
            }
        }

        private void ApplyAxis(XBoxAxis inAxis, Xbox360Axis outAxis, IReadOnlyDictionary<XBoxAxis, Func<float?>> feed)
        {
            if (feed.TryGetValue(inAxis, out var get))
            {
                var v = get();
                if (v.HasValue)
                    Controller.SetAxisValue(outAxis, NormalizeAxis(v.Value));
            }
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
