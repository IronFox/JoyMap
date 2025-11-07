namespace JoyMap.ControllerTracking
{
    public readonly record struct InputAxisChange(
        InputAxis Which,
        float Status
        );

    public readonly record struct InputState(
        float X,
        float Y,
        float Z,
        float Slider,
        float RotationX,
        float RotationY,
        float RotationZ,
        float PovX,
        float PovY,
        bool[] Buttons
        )
    {

        public static void DetectSignificantChanges(InputState previous, InputState updated, List<InputAxisChange> changes)
        {
            changes.Clear();
            const float threshold = 0.05f;
            if (Math.Abs(previous.Slider - updated.Slider) > threshold)
            {
                changes.Add(new InputAxisChange(InputAxis.Slider, updated.Slider));
            }
            if (Math.Abs(previous.X - updated.X) > threshold)
            {
                changes.Add(new InputAxisChange(InputAxis.X, updated.X));
            }
            if (Math.Abs(previous.Y - updated.Y) > threshold)
            {
                changes.Add(new InputAxisChange(InputAxis.Y, updated.Y));
            }
            if (Math.Abs(previous.Z - updated.Z) > threshold)
            {
                changes.Add(new InputAxisChange(InputAxis.Z, updated.Z));
            }
            if (Math.Abs(previous.PovX - updated.PovX) > threshold)
            {
                changes.Add(new InputAxisChange(InputAxis.PovX, updated.PovX));
            }
            if (Math.Abs(previous.PovY - updated.PovY) > threshold)
            {
                changes.Add(new InputAxisChange(InputAxis.PovY, updated.PovY));
            }
            if (Math.Abs(previous.RotationX - updated.RotationX) > threshold)
            {
                changes.Add(new InputAxisChange(InputAxis.RotationX, updated.RotationX));
            }
            if (Math.Abs(previous.RotationY - updated.RotationY) > threshold)
            {
                changes.Add(new InputAxisChange(InputAxis.RotationY, updated.RotationY));
            }
            if (Math.Abs(previous.RotationZ - updated.RotationZ) > threshold)
            {
                changes.Add(new InputAxisChange(InputAxis.RotationZ, updated.RotationZ));
            }
            int len = Math.Min(previous.Buttons.Length, updated.Buttons.Length);
            for (int i = 0; i < len; i++)
            {
                if (previous.Buttons[i] != updated.Buttons[i])
                {
                    changes.Add(new InputAxisChange((InputAxis)((int)InputAxis.Button0 + i), updated.Buttons[i] ? 1f : 0f));
                }
            }
        }

        internal float Get(InputAxis which)
        {
            switch (which)
            {
                case InputAxis.X:
                    return X;
                case InputAxis.Y:
                    return Y;
                case InputAxis.Z:
                    return Z;
                case InputAxis.Slider:
                    return Slider;
                case InputAxis.RotationX:
                    return RotationX;
                case InputAxis.RotationY:
                    return RotationY;
                case InputAxis.RotationZ:
                    return RotationZ;
                case InputAxis.PovX:
                    return PovX;
                case InputAxis.PovY:
                    return PovY;
                default:
                    {
                        if (which >= InputAxis.Button0 && which <= InputAxis.Button31)
                        {
                            int index = (int)which - (int)InputAxis.Button0;
                            if (index >= 0 && index < Buttons.Length)
                            {
                                return Buttons[index] ? 1f : 0f;
                            }
                        }
                        return 0;
                    }
            }
        }
    }
}
