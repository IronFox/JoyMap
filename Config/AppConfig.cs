using JoyMap.Util;

namespace JoyMap.Config
{
    /// <summary>
    /// Persisted in My Documents/JoyMap/Config.json. Allows redirecting each data
    /// file and the log output to arbitrary directories (e.g. a cloud-synced folder).
    /// A missing file is auto-generated on launch with <c>.\</c> defaults, which
    /// resolve to the application's base directory.
    /// </summary>
    internal class AppConfig
    {
        private static readonly string ConfigDir =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "JoyMap");

        private static readonly string ConfigPath =
            Path.Combine(ConfigDir, "Config.json");

        private static readonly string DefaultRelative = @".\";

        // ── Singleton ────────────────────────────────────────────────────────

        private static AppConfig? _instance;

        internal static AppConfig Instance
        {
            get
            {
                if (_instance is null)
                    throw new InvalidOperationException("AppConfig has not been loaded yet. Call AppConfig.Load() first.");
                return _instance;
            }
        }

        // ── Config properties ─────────────────────────────────────────────────

        /// <summary>Directory that contains <c>Profiles.json</c>.</summary>
        public string ProfilesDirectory { get; set; } = DefaultRelative;

        /// <summary>Directory that contains <c>ControllerFamilies.json</c>.</summary>
        public string ControllerFamiliesDirectory { get; set; } = DefaultRelative;

        /// <summary>Directory that contains <c>HiddenDevices.json</c>.</summary>
        public string HiddenDevicesDirectory { get; set; } = DefaultRelative;

        /// <summary>Directory to which <c>joymap.log</c> is written.</summary>
        public string LogDirectory { get; set; } = DefaultRelative;

        // ── Resolved path helpers ─────────────────────────────────────────────

        private static string Resolve(string directory)
        {
            return Path.GetFullPath(directory, AppContext.BaseDirectory);
        }

        internal string GetProfilesFilePath() =>
            Path.Combine(Resolve(ProfilesDirectory), "Profiles.json");

        internal string GetControllerFamiliesFilePath() =>
            Path.Combine(Resolve(ControllerFamiliesDirectory), "ControllerFamilies.json");

        internal string GetHiddenDevicesFilePath() =>
            Path.Combine(Resolve(HiddenDevicesDirectory), "HiddenDevices.json");

        internal string GetLogFilePath() =>
            Path.Combine(Resolve(LogDirectory), "joymap.log");

        // ── Load / Save ───────────────────────────────────────────────────────

        /// <summary>
        /// Loads the config from My Documents/JoyMap/Config.json, creating it
        /// with defaults if absent. Sets <see cref="Instance"/>.
        /// </summary>
        internal static AppConfig Load()
        {
            Directory.CreateDirectory(ConfigDir);

            AppConfig cfg;
            if (File.Exists(ConfigPath))
            {
                try
                {
                    cfg = JsonUtil.Deserialize<AppConfig>(File.ReadAllText(ConfigPath));
                }
                catch
                {
                    cfg = new AppConfig();
                    Save(cfg);
                }
            }
            else
            {
                cfg = new AppConfig();
                Save(cfg);
            }

            _instance = cfg;
            return cfg;
        }

        private static void Save(AppConfig cfg)
        {
            try
            {
                Directory.CreateDirectory(ConfigDir);
                File.WriteAllText(ConfigPath, JsonUtil.Serialize(cfg));
            }
            catch { }
        }
    }
}
