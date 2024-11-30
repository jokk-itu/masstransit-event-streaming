namespace Api.Notification;

public interface INotification
{
    Guid Id { get; }
    Guid UserId { get; }
    string Component { get; }
}
