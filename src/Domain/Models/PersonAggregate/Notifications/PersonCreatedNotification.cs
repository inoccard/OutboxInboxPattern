using Domain.Models.PersonAggregate.Enums;
using MediatR;

namespace Domain.Models.PersonAggregate.Notifications;

public sealed record PersonCreatedNotification(
    int Id,
    string Name,
    string Document,
    DocumentType DocumentType
) : INotification;