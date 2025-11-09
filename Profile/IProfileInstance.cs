
namespace JoyMap.Profile
{
    internal interface IProfileInstance
    {
        IReadOnlyList<EventInstance> EventInstances { get; }
    }
}
