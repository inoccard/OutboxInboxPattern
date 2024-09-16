using Domain.Models.OutboxAggregrate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Outbox.Api.Data.Mapping;

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