using JoyMap.ControllerTracking;
using JoyMap.Util;
using JoyMap.Windows;
using System.Text.Json;

namespace JoyMap.Profile
{
    public static class Registry
    {

        class ProfileSlot
        {
            public Guid Id { get; set; }
            public required Profile Profile { get; set; }
            public ProfileInstance? Loaded { get; set; }
            public ProcessRegex? ProcessNameRegex { get; set; }
        }

        private static Dictionary<Guid, ProfileSlot> Profiles { get; } = [];



        public static ProfileInstance? FindAndLoadForWindow(WindowReference window, InputMonitor monitor)
        {
            foreach (var slot in Profiles.Values)
            {
                if (slot.ProcessNameRegex is null)
                {
                    if (slot.Loaded is not null)
                    {
                        slot.ProcessNameRegex = slot.Loaded.ProcessNameRegex;
                    }
                    else
                        slot.ProcessNameRegex = new(slot.Profile.ProcessNameRegex, slot.Profile.WindowNameRegex);
                }
                if (slot.ProcessNameRegex.IsMatch(window))
                {
                    if (slot.Loaded is not null)
                        return slot.Loaded;
                    return slot.Loaded = ProfileInstance.Load(monitor, slot.Profile);
                }
            }
            return null;
        }

        public static Profile Persist(WorkProfile p
#if DEBUG
            , bool force = false
#endif
            )
        {
            var profile = p.ToProfileInstance();
#if DEBUG
            if (!force)
                return profile.Profile;
#endif
            if (Profiles.TryGetValue(p.Id, out var slot))
            {
                slot.Profile = profile.Profile;
                slot.Loaded = profile;
                slot.ProcessNameRegex = profile.ProcessNameRegex;
            }
            else
                Profiles.Add(p.Id, new() { Id = p.Id, Profile = profile.Profile, Loaded = profile });
            SaveAll();

            p.RestartListenIfRunning();
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

        internal static void DeleteProfile(Guid profileId)
        {
            Profiles.Remove(profileId);
            SaveAll();
        }
    }
}
