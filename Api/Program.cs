using Api.Notification;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.AddConsumer<NotificationConsumer, NotificationConsumerDefinition>();

    busConfigurator.UsingRabbitMq((busContext, rabbitMqBusConfigurator) =>
    {
        rabbitMqBusConfigurator.Host("localhost", "/", c =>
        {
            c.Username("guest");
            c.Password("guest");
        });
        rabbitMqBusConfigurator.ConfigureEndpoints(busContext);
    });
});

var app = builder.Build();

app.MapPost("/notification",
        async ([FromServices] IPublishEndpoint publishEndpoint, CancellationToken cancellationToken) =>
        {
            await publishEndpoint.Publish<INotification>(new
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Component = "this is a notification containing information"
            }, cancellationToken);

            return Results.Ok();
        })
    .WithName("PostNotification");

app.Run();