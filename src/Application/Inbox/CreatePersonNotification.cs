using Domain.Models.PersonAggregate.Enums;
using MediatR;

namespace Application.Inbox;

public sealed record CreatePersonNotification(
    int Id,
    string Name,
    string Document,
    DocumentType DocumentType
) : INotification;
