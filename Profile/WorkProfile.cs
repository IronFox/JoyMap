using JoyMap.Windows;

namespace JoyMap.Profile
{
    public class WorkProfile
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; set; } = "";
        public string WindowRegex { get; set; } = "";
        public List<EventInstance> Events { get; init; } = [];

        public bool Exists { get; set; }
        public bool HasChanged { get; set; }

        internal static WorkProfile? Load(ProfileInstance? profile)
        {
            if (profile is null)
                return null;
            var workProfile = new WorkProfile
            {
                Id = profile.Profile.Id,
                Name = profile.Profile.Name,
                WindowRegex = profile.Profile.WindowNameRegex,
                Events = [.. profile.EventInstances]
            };
            return workProfile;
        }

        public ProfileInstance ToProfileInstance()
        {
            return new ProfileInstance
            (
                Profile: ToProfile(),
                WindowNameRegex: new System.Text.RegularExpressions.Regex(WindowRegex, System.Text.RegularExpressions.RegexOptions.Compiled),
                EventInstances: Events
            );
        }

        public Profile ToProfile()
        {
            return new Profile
            (
                Id: Id,
                Name: Name,
                WindowNameRegex: WindowRegex,
                Events: Events.Select(x => x.Event).ToList()
            );

        }

        private CancellationTokenSource ListenCancel { get; set; } = new();
        private Form? ListenOwner { get; set; } = null;
        internal void StartListen(Form owner)
        {
            ListenCancel = new CancellationTokenSource();
            ListenOwner = owner;
            var cancel = ListenCancel.Token;
            var processors = Events.Select(ev => ev.ToProcessor()).ToList();
            var handle = owner.Handle;
            Task.Run(() => ListenLoop(processors, handle, cancel), cancel);

        }

        internal void RestartListenIfRunning()
        {
            if (ListenOwner is not null)
            {
                Stop();
                StartListen(ListenOwner);
            }
        }

        private async Task ListenLoop(List<EventProcessor> processors, IntPtr ownerHandle, CancellationToken cancel)
        {
            try
            {
                while (true)
                {
                    var focused = WindowReference.OfFocused();
                    if (focused is not null && !focused.Value.IsChildOf(ownerHandle))
                    {
                        foreach (var p in processors)
                        {
                            p.Update();
                        }
                    }
                    await Task.Delay(5, cancel).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException)
            {
                foreach (var processor in processors)
                {
                    processor.Stop();
                }
                throw;
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine($"ListenLoop Exception: {ex}");
            }
            finally
            {
                ListenOwner = null;
            }
        }

        internal void Stop()
        {
            ListenCancel.Cancel();
        }
    }
}
