using MediatR;

namespace Application.Outbox;

public record OutboxJobNotification : INotification;