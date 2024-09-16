using Domain.Models.PersonAggregate.Enums;
using MediatR;

namespace Application.Outbox;

public sealed record PersonCreatedNotification(
    int Id,
    string Name,
    string Document,
    DocumentType DocumentType
) : INotification;
