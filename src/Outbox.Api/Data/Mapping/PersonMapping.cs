using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Outbox.Api.Domain.Models.PersonAggregate.Entities;

namespace Outbox.Api.Data.Mapping;

public class PersonMapping : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable(nameof(Person));
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();
        builder.Property(p => p.Name)
            .IsRequired(true);
        builder.Property(p => p.Document)
            .IsRequired(true);
        builder.Property(p => p.DocumentType)
            .IsRequired(true);
    }
}