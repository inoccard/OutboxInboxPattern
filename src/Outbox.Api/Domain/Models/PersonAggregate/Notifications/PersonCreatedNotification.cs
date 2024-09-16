using MediatR;
using Outbox.Api.Domain.Models.PersonAggregate.Enums;

namespace Outbox.Api.Domain.Models.PersonAggregate.Notifications;

public sealed record PersonCreatedNotification(
    int Id, string Name, string Document, DocumentType Type
    ) : INotification;