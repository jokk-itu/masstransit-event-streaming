using MassTransit;

namespace Api.Notification;

public class NotificationConsumerDefinition : ConsumerDefinition<NotificationConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<NotificationConsumer> consumerConfigurator,
        IRegistrationContext context)
    {
        endpointConfigurator.PrefetchCount = 100;
        endpointConfigurator.ConcurrentMessageLimit = 1;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rabbitMqEndpointConfigurator)
        {
            rabbitMqEndpointConfigurator.Stream("main-consumer", streamConfigurator =>
            {
                streamConfigurator.MaxAge = TimeSpan.FromDays(2);
                streamConfigurator.FromFirst();
            });
        }

        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator, context);
    }
}
