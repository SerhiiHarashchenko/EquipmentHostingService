using EquipmentHostingService.BackgroundProcessing;

namespace EquipmentHostingService.Services
{
    public class BackgroundLoggingService
    {
        private readonly IMessageQueue _messageQueue;

        public BackgroundLoggingService(IMessageQueue messageQueue)
        {
            _messageQueue = messageQueue;
        }

        public Task LogErrorAsync(Exception exception)
        {
            var logMessage = $"{DateTime.UtcNow}: {exception.Message}\n{exception.StackTrace}";
            _messageQueue.Enqueue(logMessage);
            return Task.CompletedTask;
        }
    }
}
