using Outbox.Api.Domain.Models.PersonAggregate.Enums;

namespace Outbox.Api.Domain.Models.PersonAggregate.Dtos;

public record PersonDto(string Name, string Document, DocumentType Doctype);