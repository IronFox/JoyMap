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
        /// <param name="which"></param>
        /// <returns></returns>
        public new float? Get(InputAxis which)
        {
            return Product?.Get(which) ?? base.Get(which);
        }
        internal override void SignalBegin(Guid instanceGuid)
        {
            base.SignalBegin(instanceGuid);
            Product?.SignalBegin(instanceGuid);
        }

        internal override void SignalEnd(Guid instanceGuid)
        {
            base.SignalEnd(instanceGuid);
            Product?.SignalEnd(instanceGuid);
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
        internal void UpdateInstance(JoystickState state, int axisResolution)
        {
            var currentState = new InputState(
                Slider: (float)state.Sliders.Length > 0 ? (float)state.Sliders[0] / axisResolution : 0,
                PovX: XOf(state.PointOfViewControllers, 0),
                PovY: YOf(state.PointOfViewControllers, 0),
                X: (float)state.X / axisResolution,
                Y: (float)state.Y / axisResolution,
                Z: (float)state.Z / axisResolution,
                RotationX: (float)state.RotationX / axisResolution,
                RotationY: (float)state.RotationY / axisResolution,
                RotationZ: (float)state.RotationZ / axisResolution,
                Buttons: (bool[])state.Buttons.Clone());
            Update(currentState);
        }
    }
}
