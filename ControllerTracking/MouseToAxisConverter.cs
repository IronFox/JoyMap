namespace JoyMap.ControllerTracking
{
    public class MouseToAxisConverter
    {
        private float _currentValue = 0f;
        private DateTime _lastUpdate = DateTime.UtcNow;

        // Configuration
        public float Sensitivity { get; set; } = 0.2f;       // Pixels → axis value
        public float DecayRate { get; set; } = 25.0f;        // How fast axis returns to center (increased from 8.0)
        public float MaxAxisValue { get; set; } = 1.0f;      // Clamp to ±1
        public float DeadZone { get; set; } = 1.0f;          // Minimum pixel delta to register (in pixels)

        public float CurrentValue => _currentValue;

        /// <summary>
        /// Velocity-based update with decay: mouse delta controls axis velocity
        /// Axis decays back to center when mouse stops moving
        /// </summary>
        public void Update(int deltaPixels)
        {
            var now = DateTime.UtcNow;
            float deltaTime = (float)(now - _lastUpdate).TotalSeconds;
            _lastUpdate = now;

            // If mouse is moving, set axis value directly
            if (Math.Abs(deltaPixels) > DeadZone)
            {
                // Velocity mode: delta directly sets axis value
                _currentValue = Math.Clamp(
                    deltaPixels * Sensitivity,
                    -MaxAxisValue,
                    MaxAxisValue
                );
            }
            else
            {
                // Mouse at rest - apply decay to return to center
                if (deltaTime > 0 && DecayRate > 0)
                {
                    float decayAmount = DecayRate * deltaTime;
                    if (Math.Abs(_currentValue) > 0.01f)
                    {
                        _currentValue -= Math.Sign(_currentValue) *
                                        Math.Min(decayAmount, Math.Abs(_currentValue));
                    }
                    else
                    {
                        _currentValue = 0f;
                    }
                }
            }
        }
    }
}