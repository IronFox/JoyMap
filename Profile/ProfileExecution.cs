using JoyMap.Windows;

namespace JoyMap.Profile
{
    internal class ProfileExecution : IDisposable, IAsyncDisposable
    {
        private static ProfileExecution? Active { get; set; }
        public static void Start(IProfileInstance profileInstance, Form owner)
        {
            if (Active?.Events == profileInstance.EventInstances)
            {
                return;
            }
            Active?.Dispose();
            Active = new ProfileExecution(profileInstance, owner);
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

        public ProfileExecution(IProfileInstance profileInstance, Form owner)
        {
            Events = profileInstance.EventInstances;

            ListenCancel = new CancellationTokenSource();
            ListenOwner = owner;
            var cancel = ListenCancel.Token;
            var processors = Events.Select(ev => ev.ToProcessor()).ToList();
            var handle = owner.Handle;
            Task.Run(() => ListenLoop(processors, handle, cancel), cancel);
        }


        public IReadOnlyList<EventInstance> Events { get; }


        private CancellationTokenSource ListenCancel { get; } = new();
        private Form ListenOwner { get; }

        private TaskCompletionSource DisposeCompletion { get; } = new();

        private async Task ListenLoop(List<EventProcessor> processors, IntPtr ownerHandle, CancellationToken cancel)
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
