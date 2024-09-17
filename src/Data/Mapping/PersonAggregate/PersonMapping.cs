using Domain.Models.PersonAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping.PersonAggregate;

public class PersonMapping : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable(nameof(Person));
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();
        builder.Property(p => p.Name)
            .IsRequired();
        builder.Property(p => p.Document)
            .IsRequired();
        builder.HasIndex(p => p.Document)
            .IsUnique();
        builder.Property(p => p.DocumentType)
            .IsRequired();
    }
}