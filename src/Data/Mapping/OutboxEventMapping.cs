using Domain.Models.InboxAggregate.Entities;
using Domain.Models.OutboxAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping;

public class OutboxEventMapping : IEntityTypeConfiguration<OutboxEvent>
{
    public void Configure(EntityTypeBuilder<OutboxEvent> builder)
    {
        builder.ToTable(nameof(OutboxEvent));
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();
        builder.Property(p => p.Type)
            .IsRequired();
        builder.Property(p => p.Payload)
            .IsRequired();
        builder.HasIndex(p => p.Error);
        builder.Property(p => p.Attempts)
            .IsRequired();
        builder.Property(p => p.Status)
            .IsRequired();
        builder.Property(p => p.RegisteredDate)
            .IsRequired();
    }
}

public class InboxEventMapping : IEntityTypeConfiguration<InboxEvent>
{
    public void Configure(EntityTypeBuilder<InboxEvent> builder)
    {
        builder.ToTable(nameof(InboxEvent));
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();
        builder.Property(p => p.Type)
            .IsRequired();
        builder.Property(p => p.Payload)
            .IsRequired();
        builder.HasIndex(p => p.Error);
        builder.Property(p => p.Attempts)
            .IsRequired();
        builder.Property(p => p.Status)
            .IsRequired();
        builder.Property(p => p.RegisteredDate)
            .IsRequired();
    }
}