namespace JoyMap
{
    public readonly record struct InputAxisChange(
        Input Which,
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
                changes.Add(new InputAxisChange(Input.Slider, updated.Slider));
            }
            if (Math.Abs(previous.X - updated.X) > threshold)
            {
                changes.Add(new InputAxisChange(Input.X, updated.X));
            }
            if (Math.Abs(previous.Y - updated.Y) > threshold)
            {
                changes.Add(new InputAxisChange(Input.Y, updated.Y));
            }
            if (Math.Abs(previous.Z - updated.Z) > threshold)
            {
                changes.Add(new InputAxisChange(Input.Z, updated.Z));
            }
            if (Math.Abs(previous.PovX - updated.PovX) > threshold)
            {
                changes.Add(new InputAxisChange(Input.PovX, updated.PovX));
            }
            if (Math.Abs(previous.PovY - updated.PovY) > threshold)
            {
                changes.Add(new InputAxisChange(Input.PovY, updated.PovY));
            }
            if (Math.Abs(previous.RotationX - updated.RotationX) > threshold)
            {
                changes.Add(new InputAxisChange(Input.RotationX, updated.RotationX));
            }
            if (Math.Abs(previous.RotationY - updated.RotationY) > threshold)
            {
                changes.Add(new InputAxisChange(Input.RotationY, updated.RotationY));
            }
            if (Math.Abs(previous.RotationZ - updated.RotationZ) > threshold)
            {
                changes.Add(new InputAxisChange(Input.RotationZ, updated.RotationZ));
            }
            int len = Math.Min(previous.Buttons.Length, updated.Buttons.Length);
            for (int i = 0; i < len; i++)
            {
                if (previous.Buttons[i] != updated.Buttons[i])
                {
                    changes.Add(new InputAxisChange((Input)((int)Input.Button0 + i), updated.Buttons[i] ? 1f : 0f));
                }
            }
        }

        internal float Get(Input which)
        {
            switch (which)
            {
                case Input.X:
                    return X;
                case Input.Y:
                    return Y;
                case Input.Z:
                    return Z;
                case Input.Slider:
                    return Slider;
                case Input.RotationX:
                    return RotationX;
                case Input.RotationY:
                    return RotationY;
                case Input.RotationZ:
                    return RotationZ;
                case Input.PovX:
                    return PovX;
                case Input.PovY:
                    return PovY;
                default:
                    {
                        if (which >= Input.Button0 && which <= Input.Button31)
                        {
                            int index = (int)which - (int)Input.Button0;
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
