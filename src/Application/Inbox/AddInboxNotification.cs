using MediatR;

namespace Application.Inbox;

public record AddInboxNotification(
    string Type,
    string Payload,
    string Error)
    : INotification;
