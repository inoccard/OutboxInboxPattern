using System.ComponentModel.DataAnnotations;
using Domain.Models.PersonAggregate.Enums;

namespace Domain.Models.PersonAggregate.Entities;

public class Person
{
    public Person(string name, string document, DocumentType documentType)
    {
        Name = name;
        Document = document;
        DocumentType = documentType;
    }

    [Key] public int Id { get; set; }

    public string Name { get; private set; }
    public string Document { get; private set; }
    public DocumentType DocumentType { get; private set; }

    public void Update(string name, string document, DocumentType documentType)
    {
        Name = name;
        Document = document;
        DocumentType = documentType;
    }
}