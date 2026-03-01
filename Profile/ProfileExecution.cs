using JoyMap.Profile.Processing;

namespace JoyMap.Profile
{
    internal class ProfileExecution : IDisposable, IAsyncDisposable
    {
        private static ProfileExecution? Active { get; set; }
        public static void Start(IProfileInstance profileInstance)
        {
            if (Active?.Events == profileInstance.EventInstances)
            {
                return;
            }
            Active?.Dispose();
            Active = new ProfileExecution(profileInstance);
        }

        public static bool Stop(IProfileInstance profileInstance)
        {
            if (Active?.Events == profileInstance.EventInstances)
            {
                Active.Dispose();
                Active = null;
                return true;
            }
            return false;
        }

        public static bool IsActive => Active is not null;
        public static void Stop()
        {
            Active?.Dispose();
            Active = null;
        }

        public ProfileExecution(IProfileInstance profileInstance)
        {
            Events = profileInstance.EventInstances;
            Mappings = profileInstance.XBoxAxisBindings;
            GlobalStatuses = profileInstance.GlobalStatusInstances;
            MainForm.Log($"Profile execution started with {Events.Count} events");


            ListenCancel = new CancellationTokenSource();
            var cancel = ListenCancel.Token;
            IEnumerable<IProcessor> processors = Events
                .Select(ev => ev.ToProcessor())
                .OfType<IProcessor>();

            if (GlobalStatuses.Count > 0)
                processors = processors.Prepend(new GlobalStatusProcessor(GlobalStatuses));

            if (Mappings.Count > 0)
                processors = processors.Append(new XBoxAxisProcessor(Mappings));

            Task.Run(() => ListenLoop([.. processors], cancel), cancel);
        }


        public IReadOnlyList<EventInstance> Events { get; }
        public IReadOnlyList<XBoxAxisBindingInstance> Mappings { get; }
        public IReadOnlyList<GlobalStatusInstance> GlobalStatuses { get; }

        private CancellationTokenSource ListenCancel { get; } = new();

        private TaskCompletionSource DisposeCompletion { get; } = new();

        private async Task ListenLoop(IReadOnlyList<IProcessor> processors, CancellationToken cancel)
        {
            try
            {
                while (true)
                {
                    foreach (var p in processors)
                    {
                        try
                        {
                            p.Update();
                        }
                        catch (Exception ex)
                        {
                            MainForm.Log($"Processor error [{p.GetType().Name}]", ex);
                        }
                    }
                    await Task.Delay(5, cancel).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                MainForm.Log("ListenLoop fatal error", ex);
                throw;
            }
            finally
            {
                MainForm.Log("Profile execution stopped");
                foreach (var processor in processors)
                {
                    processor.Dispose();
                }

                DisposeCompletion.SetResult();
            }
        }

        public void Dispose()
        {
            ListenCancel.Cancel();
        }
        public async ValueTask DisposeAsync()
        {
            Dispose();
            await DisposeCompletion.Task.ConfigureAwait(false);
        }
    }
}
