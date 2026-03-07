using Microsoft.Win32;
using Nefarius.Drivers.HidHide;

namespace JoyMap.ControllerTracking
{
    internal static class HidHideService
    {
        internal record HidDeviceInstance(string InstanceId, string FriendlyName);

        private static HidHideControlService? TryCreateService()
        {
            try
            {
                var svc = new HidHideControlService();
                return svc.IsInstalled ? svc : null;
            }
            catch
            {
                return null;
            }
        }

        internal static bool IsAvailable
        {
            get
            {
                try
                {
                    return new HidHideControlService().IsInstalled;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal static IReadOnlyList<HidDeviceInstance> FindHidInstancesForProduct(Guid productGuid)
        {
            var bytes = productGuid.ToByteArray();
            var vid = (bytes[1] << 8) | bytes[0];
            var pid = (bytes[3] << 8) | bytes[2];
            var vidPidPattern = $"VID_{vid:X4}&PID_{pid:X4}";

            var result = new List<HidDeviceInstance>();
            try
            {
                using var hidKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Enum\HID");
                if (hidKey is null)
                    return result;

                foreach (var subKeyName in hidKey.GetSubKeyNames())
                {
                    if (!subKeyName.Contains(vidPidPattern, StringComparison.OrdinalIgnoreCase))
                        continue;

                    using var subKey = hidKey.OpenSubKey(subKeyName);
                    if (subKey is null)
                        continue;

                    foreach (var instanceName in subKey.GetSubKeyNames())
                    {
                        var instanceId = $@"HID\{subKeyName}\{instanceName}";
                        using var instanceKey = subKey.OpenSubKey(instanceName);
                        var friendlyName = instanceKey?.GetValue("FriendlyName") as string
                                        ?? instanceKey?.GetValue("DeviceDesc") as string
                                        ?? subKeyName;
                        result.Add(new(instanceId, friendlyName));
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"HidHide device enumeration error: {ex.Message}");
            }
            return result;
        }

        internal static IReadOnlySet<string> GetBlockedInstanceIds()
        {
            try
            {
                var svc = TryCreateService();
                if (svc is null)
                    return new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                return new HashSet<string>(svc.BlockedInstanceIds, StringComparer.OrdinalIgnoreCase);
            }
            catch
            {
                return new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            }
        }

        private static HashSet<string>? SessionBlockedIds { get; set; }

        internal static void HideForProfile(IEnumerable<Guid> productGuids)
        {
            var svc = TryCreateService();
            if (svc is null)
                return;
            var currentlyBlocked = new HashSet<string>(svc.BlockedInstanceIds, StringComparer.OrdinalIgnoreCase);
            var sessionBlocked = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var guid in productGuids)
                foreach (var instance in FindHidInstancesForProduct(guid))
                {
                    if (!currentlyBlocked.Contains(instance.InstanceId))
                        svc.AddBlockedInstanceId(instance.InstanceId);
                    sessionBlocked.Add(instance.InstanceId);
                }
            if (sessionBlocked.Count > 0)
            {
                EnsureAppWhitelisted(svc);
                if (!svc.IsActive)
                    svc.IsActive = true;
            }
            SessionBlockedIds = sessionBlocked;
        }

        internal static void RevealSessionBlocked()
        {
            var ids = SessionBlockedIds;
            if (ids is null || ids.Count == 0)
                return;
            SessionBlockedIds = null;
            var svc = TryCreateService();
            if (svc is null)
                return;
            foreach (var id in ids)
                try { svc.RemoveBlockedInstanceId(id); } catch { }
            if (!svc.BlockedInstanceIds.Any() && svc.IsActive)
                svc.IsActive = false;
        }

        private static void EnsureAppWhitelisted(HidHideControlService svc)
        {
            var exePath = Environment.ProcessPath;
            if (exePath is null)
                return;
            try
            {
                if (!svc.ApplicationPaths.Any(p => string.Equals(p, exePath, StringComparison.OrdinalIgnoreCase)))
                    svc.AddApplicationPath(exePath, throwIfInvalid: false);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"HidHide whitelist error: {ex.Message}");
            }
        }
    }
}
