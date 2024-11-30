using MassTransit;

namespace Api.Notification;

public class NotificationConsumer : IConsumer<INotification>
{
    private readonly ILogger<NotificationConsumer> _logger;

    public NotificationConsumer(ILogger<NotificationConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<INotification> context)
    {
        _logger.LogInformation("Received notification {@Notification}", context.Message);
        return Task.CompletedTask;
    }
}
