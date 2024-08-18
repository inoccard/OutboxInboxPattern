using Outbox.Api.Data.Helpers;
using Outbox.Api.Domain.Models.PersonAggregate.Enums;

namespace Outbox.Api.Domain.Models.PersonAggregate.Entities;

public class Person : Document
{
    public Person(int id, string name, string document, DocumentType documentType)
    {
        Id = id;
        Name = name;
        Document = document;
        DocumentType = documentType;
    }

    public string Name { get; private set; }
    public string Document { get; private set; }
    public DocumentType DocumentType { get; private set; }
}