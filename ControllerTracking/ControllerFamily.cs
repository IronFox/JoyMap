namespace JoyMap.ControllerTracking
{
    public class ControllerFamily(Guid id, string familyName, bool isGeneric) : ControllerStatus(id)
    {
        public ControllerFamily(JsonControllerFamily jf)
            : this(jf.Id, jf.FamilyName, false)
        {
            Members.AddRange(jf.Members);
        }

        public bool IsGeneric { get; } = isGeneric;

        public string FamilyName { get; set; } = familyName;

        public List<Product> Members { get; } = [];

        public JsonControllerFamily ToJson()
        {
            return new JsonControllerFamily(
                Id: Id,
                FamilyName: FamilyName,
                Members: [.. Members]
                );
        }

    }

    public record JsonControllerFamily(
        Guid Id,
        string FamilyName,
        IReadOnlyList<Product> Members
        );



}
