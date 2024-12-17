using System.Collections.Concurrent;

namespace EquipmentHostingService.BackgroundProcessing
{
    public class InMemoryMessageQueue : IMessageQueue
    {
        private readonly ConcurrentQueue<string> _queue = new();
        private readonly SemaphoreSlim _signal = new(0);

        public void Enqueue(string message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            _queue.Enqueue(message);
            _signal.Release();
        }

        public async Task<string?> DequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _queue.TryDequeue(out var message);
            return message;
        }
    }
}
