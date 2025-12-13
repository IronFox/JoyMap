
namespace JoyMap.Profile
{
    internal interface IProfileInstance
    {
        IReadOnlyList<EventInstance> EventInstances { get; }
        IReadOnlyList<XBoxMappingInstance> MappingInstances { get; }
    }
}
