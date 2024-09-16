using Domain.Models.PersonAggregate.Enums;

namespace Domain.Models.PersonAggregate.Dtos;

public record PersonDto(string Name, string Document, DocumentType Doctype);