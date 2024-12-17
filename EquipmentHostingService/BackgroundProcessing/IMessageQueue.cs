namespace EquipmentHostingService.BackgroundProcessing
{
    public interface IMessageQueue
    {
        void Enqueue(string message);
        Task<string?> DequeueAsync(CancellationToken cancellationToken);
    }
}
