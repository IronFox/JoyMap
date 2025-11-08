using JoyMap.ControllerTracking;
using JoyMap.Util;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace JoyMap.Profile
{
    public static class Registry
    {

        class ProfileSlot
        {
            public Guid Id { get; set; }
            public required Profile Profile { get; set; }
            public ProfileInstance? Loaded { get; set; }
            public Regex? WindowNameRegex { get; set; }
        }

        private static Dictionary<Guid, ProfileSlot> Profiles { get; } = [];



        public static ProfileInstance? FindAndLoadForWindow(string windowName, InputMonitor monitor)
        {
            foreach (var slot in Profiles.Values)
            {
                if (slot.WindowNameRegex is null)
                {
                    if (slot.Loaded is not null)
                    {
                        slot.WindowNameRegex = slot.Loaded.WindowNameRegex;
                    }
                    else
                        slot.WindowNameRegex = new Regex(slot.Profile.WindowNameRegex, RegexOptions.Compiled);
                }
                if (slot.WindowNameRegex.IsMatch(windowName))
                {
                    if (slot.Loaded is not null)
                        return slot.Loaded;
                    return slot.Loaded = ProfileInstance.Load(monitor, slot.Profile);
                }
            }
            return null;
        }

        public static Profile Persist(WorkProfile p)
        {
            var profile = p.ToProfileInstance();
#if DEBUG
            return profile.Profile;
#else
            if (Profiles.TryGetValue(p.Id, out var slot))
            {
                slot.Profile = profile.Profile;
                slot.Loaded = profile;
                slot.WindowNameRegex = profile.WindowNameRegex;
            }
            else
                Profiles.Add(p.Id, new() { Id = p.Id, Profile = profile.Profile, Loaded = profile });
            SaveAll();

            p.RestartListenIfRunning();
            return profile.Profile;
#endif
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
            try
            {
                var list = JsonUtil.Deserialize<List<Profile>>(json);
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
            catch (JsonException)
            {
                // ignore
            }
        }

        public static void SaveAll()
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var dir = Path.Combine(documents, "JoyMap");
            Directory.CreateDirectory(dir);

            var path = Path.Combine(dir, "Profiles.json");

            var json = JsonUtil.Serialize(Profiles.Values.Select(x => x.Profile));
            File.WriteAllText(path, json);
        }

        internal static IEnumerable<Profile> GetAllProfiles()
        {
            return Profiles.Values.Select(x => x.Profile).OrderBy(x => x.Name);
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
