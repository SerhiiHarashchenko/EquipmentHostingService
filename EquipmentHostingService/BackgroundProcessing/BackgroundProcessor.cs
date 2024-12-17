namespace EquipmentHostingService.BackgroundProcessing
{
    public class BackgroundProcessor : BackgroundService
    {
        private readonly IMessageQueue _messageQueue;
        private readonly ILogger<BackgroundProcessor> _logger;

        public BackgroundProcessor(IMessageQueue messageQueue, ILogger<BackgroundProcessor> logger)
        {
            _messageQueue = messageQueue;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("BackgroundProcessor started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var message = await _messageQueue.DequeueAsync(stoppingToken);

                    _logger.LogInformation("Processing message: {Message}", message);
                    await Task.Delay(1000);
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("BackgroundProcessor is stopping.");
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing message.");
                }
            }
        }
    }
}
