namespace JoyMap.ControllerTracking
{
    public record Product(
        Guid Guid,
        string Name
        )
    {
        public override string ToString() => $"{Name} ({Guid})";

    }
}
