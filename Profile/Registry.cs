using JoyMap.ControllerTracking;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JoyMap.Profile
{
    public static class Registry
    {

        class ProfileSlot
        {
            public Guid Id { get; set; }
            public required Profile Profile { get; set; }
            public ProfileInstance? Loaded { get; set; }
        }

        private static Dictionary<Guid, ProfileSlot> Profiles { get; } = [];

        // Shared serializer options: enums as strings, indented output.
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public static Profile Persist(WorkProfile p)
        {
            var profile = p.ToProfileInstance();
            if (Profiles.TryGetValue(p.Id, out var slot))
            {
                slot.Profile = profile.Profile;
                slot.Loaded = profile;
            }
            else
                Profiles.Add(p.Id, new() { Id = p.Id, Profile = profile.Profile, Loaded = profile });
            SaveAll();
            return profile.Profile;
        }

        public static void LoadAll()
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var dir = Path.Combine(documents, "JoyMap");
            Directory.CreateDirectory(dir);
            var path = Path.Combine(dir, "Profiles.json");
            if (!File.Exists(path))
                return;
            var json = File.ReadAllText(path);
            var list = JsonSerializer.Deserialize<List<Profile>>(json, JsonOptions);
            if (list is null)
                return;
            Profiles.Clear();
            bool anyDropped = false;
            foreach (var p in list)
            {
                if (p.Events.Count == 0)
                {
                    anyDropped = true;
                    continue;
                }
                Profiles[p.Id] = new()
                {
                    Id = p.Id,
                    Profile = p,
                };
            }
            if (anyDropped)
                SaveAll();
        }

        public static void SaveAll()
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var dir = Path.Combine(documents, "JoyMap");
            Directory.CreateDirectory(dir);

            var path = Path.Combine(dir, "Profiles.json");

            var json = JsonSerializer.Serialize(Profiles.Values.Select(x => x.Profile), JsonOptions);
            File.WriteAllText(path, json);
        }

        internal static IEnumerable<Profile> GetAllProfiles()
        {
            return Profiles.Values.Select(x => x.Profile);
        }

        public static ProfileInstance? Instantiate(InputMonitor monitor, Profile? profile)
        {
            if (profile is null)
                return null;
            var slot = Profiles[profile.Id];
            if (slot.Loaded is not null)
                return slot.Loaded;
            return slot.Loaded = ProfileInstance.Load(monitor, profile);
        }
    }
}
