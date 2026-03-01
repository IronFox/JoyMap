namespace JoyMap.Forms
{
    /// <summary>
    /// Carries the identity, display name and live status function of a global status,
    /// used to populate combiner help dialogs and to build expression resolver dictionaries.
    /// </summary>
    public sealed record GlobalStatusRef(string Id, string Name, Func<bool> IsActive);
}
