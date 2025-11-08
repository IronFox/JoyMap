using JoyMap.Undo;
using JoyMap.Windows;

namespace JoyMap.Profile
{
    public class WorkProfile
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; set; } = "";
        public string ProcessNameRegex { get; set; } = "";
        public string WindowNameRegex { get; set; } = "";
        public List<EventInstance> Events { get; init; } = [];

        public static bool SuppressEventProcessing { get; set; } = false;

        public bool Exists { get; set; }
        public bool HasChanged { get; set; }

        public UndoHistory History { get; } = new();

        internal static WorkProfile? Load(ProfileInstance? profile)
        {
            if (profile is null)
                return null;
            var workProfile = new WorkProfile
            {
                Id = profile.Profile.Id,
                Name = profile.Profile.Name,
                ProcessNameRegex = profile.Profile.ProcessNameRegex ?? "",
                WindowNameRegex = profile.Profile.WindowNameRegex ?? "",
                Events = [.. profile.EventInstances]
            };
            return workProfile;
        }

        public ProfileInstance ToProfileInstance()
        {
            return new ProfileInstance
            (
                Profile: ToProfile(),
                ProcessNameRegex: new(ProcessNameRegex, WindowNameRegex),
                EventInstances: Events
            );
        }

        public Profile ToProfile()
        {
            return new Profile
            (
                Id: Id,
                Name: Name,
                ProcessNameRegex: ProcessNameRegex,
                WindowNameRegex: WindowNameRegex,
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
                    if (!SuppressEventProcessing)
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
