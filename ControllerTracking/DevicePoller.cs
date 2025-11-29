using SharpDX.DirectInput;
using System.Collections;
using System.Collections.Concurrent;

namespace JoyMap.ControllerTracking
{
    public class DevicePoller : IDisposable, IEnumerable<Product>
    {
        private readonly CancellationTokenSource cancel = new();
        private ConcurrentDictionary<Guid, Product> Products { get; } = [];


        public DevicePoller()
        {
            var di = new DirectInput();
            Task.Run(() => RunAsync(di, cancel.Token));

        }

        public void Dispose()
        {
            cancel.Cancel();
        }

        private async Task RunAsync(DirectInput di, CancellationToken cancel)
        {
            try
            {
                while (true)
                {
                    var devices = di.GetDevices(DeviceClass.GameControl, DeviceEnumerationFlags.AttachedOnly);
                    foreach (var dev in devices)
                    {
                        Products.TryAdd(dev.ProductGuid, new Product(dev.ProductGuid, dev.ProductName));
                    }
                    await Task.Delay(500, cancel).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException)
            {
                // Expected on cancellation
            }

            di.Dispose();
        }

        public IEnumerator<Product> GetEnumerator()
        {
            return Products.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
