using SharpDX.DirectInput;

namespace JoyMap.ControllerTracking
{
    public class InstanceStatus(Guid id, string controllerName, ControllerStatus product)
                : ControllerStatus(id)
    {
        public ControllerStatus Product { get; } = product;
        public string ControllerName { get; } = controllerName;


        public ControllerId ControllerId => new(
            ProductGuid: Product.Id,
            InstanceGuid: Id
            );



        protected override void SignalSignificantChanges(IReadOnlyCollection<InputAxisChange> changes)
        {
            EventRecorder.SignalSignificantChanges(this, changes);
        }

        public new void Update(InputState state)
        {
            base.Update(state);
            Product.Update(state);
        }

        /// <summary>
        /// Fetches the latest status value for the specified input.
        /// </summary>
        public new float? Get(InputAxis which)
        {
            return Product.Get(which) ?? base.Get(which);
        }

        internal override void SignalBegin(Guid instanceGuid)
        {
            base.SignalBegin(instanceGuid);
            Product.SignalBegin(instanceGuid);
        }

        internal override void SignalEnd(Guid instanceGuid)
        {
            base.SignalEnd(instanceGuid);
            Product.SignalEnd(instanceGuid);
        }

        private float XOf(int[] povs, int index)
        {
            if (povs.Length > index)
            {
                var pov = povs[index];
                if (pov >= 0 && pov <= 36000)
                {
                    var deg = pov / 100f;
                    var rad = deg * (MathF.PI / 180f);
                    var rs = MathF.Sin(rad);
                    return rs;
                }
            }
            return 0;
        }

        private float YOf(int[] povs, int index)
        {
            if (povs.Length > index)
            {
                var pov = povs[index];
                if (pov >= 0 && pov <= 36000)
                {
                    var deg = pov / 100f;
                    var rad = deg * (MathF.PI / 180f);
                    var rs = MathF.Cos(rad);
                    return rs;
                }
            }
            return 0;
        }

        private static ulong PackButtons(bool[]? buttons)
        {
            if (buttons is null || buttons.Length == 0)
                return 0UL;
            ulong bits = 0UL;
            int count = Math.Min(buttons.Length, 64);
            for (int i = 0; i < count; i++)
            {
                if (buttons[i])
                    bits |= (1UL << i);
            }
            return bits;
        }

        internal void UpdateInstance(JoystickState state, int axisResolution)
        {
            var buttons = state.Buttons;
            int buttonCount = buttons?.Length ?? 0;
            ulong buttonsBits = PackButtons(buttons);

            var currentState = new InputState(
                X: state.X / (float)axisResolution,
                Y: state.Y / (float)axisResolution,
                Z: state.Z / (float)axisResolution,
                Slider1: state.Sliders.Length > 0 ? state.Sliders[0] / (float)axisResolution : 0f,
                RotationX: state.RotationX / (float)axisResolution,
                RotationY: state.RotationY / (float)axisResolution,
                RotationZ: state.RotationZ / (float)axisResolution,
                PovX: XOf(state.PointOfViewControllers, 0),
                PovY: YOf(state.PointOfViewControllers, 0),
                ButtonCount: buttonCount,
                ButtonsBits: buttonsBits
            );
            Update(currentState);
        }
    }
}
