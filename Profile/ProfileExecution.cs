using JoyMap.Windows;

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

        public static void Stop()
        {
            Active?.Dispose();
            Active = null;
        }

        public ProfileExecution(IProfileInstance profileInstance)
        {
            Events = profileInstance.EventInstances;
            Console.WriteLine($"Starting ProfileExecution with {Events.Count} events.");


            ListenCancel = new CancellationTokenSource();
            var cancel = ListenCancel.Token;
            var processors = Events.Select(ev => ev.ToProcessor()).OfType<EventProcessor>().ToList();
            Task.Run(() => ListenLoop(processors, cancel), cancel);
        }


        public IReadOnlyList<EventInstance> Events { get; }


        private CancellationTokenSource ListenCancel { get; } = new();

        private TaskCompletionSource DisposeCompletion { get; } = new();

        private async Task ListenLoop(List<EventProcessor> processors, CancellationToken cancel)
        {
            try
            {
                while (true)
                {
                    var focused = WindowReference.OfFocused();
                    foreach (var p in processors)
                    {
                        p.Update();
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
                // Log exception
                Console.WriteLine($"ListenLoop Exception: {ex}");
                throw;
            }
            finally
            {
                Console.WriteLine($"ProfileExecution ListenLoop ending.");
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
