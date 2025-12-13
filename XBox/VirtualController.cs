using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;

namespace JoyMap.XBox
{
    public class VirtualController : IDisposable
    {
        private readonly ViGEmClient _client;
        private readonly IXbox360Controller _controller;

        public VirtualController()
        {
            _client = new ViGEmClient();
            _controller = _client.CreateXbox360Controller();
            _controller.Connect();
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
            _controller.SetButtonState(xboxButton, pressed);
            _controller.SubmitReport();
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

            _controller.SubmitReport();
        }

        private void ApplySlider(XBoxAxis inTriggerAxis, Xbox360Slider outSlider, IReadOnlyDictionary<XBoxAxis, Func<float?>> feed)
        {
            if (feed.TryGetValue(inTriggerAxis, out var get))
            {
                var v = get();
                if (v.HasValue)
                    _controller.SetSliderValue(outSlider, (byte)Math.Clamp(v.Value * 255, 0, 255));
            }
        }

        private void ApplyAxis(XBoxAxis inAxis, Xbox360Axis outAxis, IReadOnlyDictionary<XBoxAxis, Func<float?>> feed)
        {
            if (feed.TryGetValue(inAxis, out var get))
            {
                var v = get();
                if (v.HasValue)
                    _controller.SetAxisValue(outAxis, NormalizeAxis(v.Value));
            }
        }

        private short NormalizeAxis(float value)
        {
            return (short)((value) * 32767);
        }

        private void SetButton(Xbox360Button button, bool pressed)
        {
            _controller.SetButtonState(button, pressed);
        }

        public void Dispose()
        {
            _controller?.Disconnect();
            _client?.Dispose();
        }
    }
}
