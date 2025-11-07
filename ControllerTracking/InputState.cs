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
        float Slider1,
        float RotationX,
        float RotationY,
        float RotationZ,
        float PovX,
        float PovY,
        int ButtonCount,
        ulong ButtonsBits
        )
    {
        private static bool GetBit(ulong bits, int index) => ((bits >> index) & 1UL) != 0;

        public static void DetectSignificantChanges(InputState previous, InputState updated, List<InputAxisChange> changes)
        {
            changes.Clear();
            const float threshold = 0.05f;

            if (Math.Abs(previous.Slider1 - updated.Slider1) > threshold)
                changes.Add(new InputAxisChange(InputAxis.Slider1, updated.Slider1));
            if (Math.Abs(previous.X - updated.X) > threshold)
                changes.Add(new InputAxisChange(InputAxis.X, updated.X));
            if (Math.Abs(previous.Y - updated.Y) > threshold)
                changes.Add(new InputAxisChange(InputAxis.Y, updated.Y));
            if (Math.Abs(previous.Z - updated.Z) > threshold)
                changes.Add(new InputAxisChange(InputAxis.Z, updated.Z));
            if (Math.Abs(previous.PovX - updated.PovX) > threshold)
                changes.Add(new InputAxisChange(InputAxis.PovX, updated.PovX));
            if (Math.Abs(previous.PovY - updated.PovY) > threshold)
                changes.Add(new InputAxisChange(InputAxis.PovY, updated.PovY));
            if (Math.Abs(previous.RotationX - updated.RotationX) > threshold)
                changes.Add(new InputAxisChange(InputAxis.RotationX, updated.RotationX));
            if (Math.Abs(previous.RotationY - updated.RotationY) > threshold)
                changes.Add(new InputAxisChange(InputAxis.RotationY, updated.RotationY));
            if (Math.Abs(previous.RotationZ - updated.RotationZ) > threshold)
                changes.Add(new InputAxisChange(InputAxis.RotationZ, updated.RotationZ));

            if (previous.ButtonsBits != updated.ButtonsBits)
            {
                const int MaxButtonEnumSpan = (int)InputAxis.Button63 - (int)InputAxis.Button0 + 1;

                int maxCount = Math.Max(previous.ButtonCount, updated.ButtonCount);
                int limit = Math.Min(maxCount, MaxButtonEnumSpan);
                for (int i = 0; i < limit; i++)
                {
                    bool prevPressed = i < previous.ButtonCount && GetBit(previous.ButtonsBits, i);
                    bool updatedPressed = i < updated.ButtonCount && GetBit(updated.ButtonsBits, i);
                    if (prevPressed != updatedPressed)
                    {
                        changes.Add(new InputAxisChange((InputAxis)((int)InputAxis.Button0 + i), updatedPressed ? 1f : 0f));
                    }
                }
            }
        }

        internal float Get(InputAxis which)
        {
            switch (which)
            {
                case InputAxis.X: return X;
                case InputAxis.Y: return Y;
                case InputAxis.Z: return Z;
                case InputAxis.Slider1: return Slider1;
                case InputAxis.RotationX: return RotationX;
                case InputAxis.RotationY: return RotationY;
                case InputAxis.RotationZ: return RotationZ;
                case InputAxis.PovX: return PovX;
                case InputAxis.PovY: return PovY;
                default:
                    // After this range check we know (int)which - Button0 is in [0,63].
                    if (which >= InputAxis.Button0 && which <= InputAxis.Button63)
                    {
                        int index = (int)which - (int)InputAxis.Button0;
                        // Only need to verify index < ButtonCount; index >=0 and <64 are implied by enum range.
                        if (index < ButtonCount && GetBit(ButtonsBits, index))
                            return 1f;
                        return 0f;
                    }
                    return 0f;
            }
        }
    }
}
