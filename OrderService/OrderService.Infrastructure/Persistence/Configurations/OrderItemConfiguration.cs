using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;
using OrderService.Domain.ValueObjects;

namespace OrderService.Infrastructure.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.ProductId)
            .IsRequired();

        builder.Property(oi => oi.Name)
            .IsRequired();

        builder.Property(oi => oi.Category)
            .IsRequired();

        builder.Property(oi => oi.Quantity)
            .IsRequired();

        // Map Money value object
        builder.Property(oi => oi.UnitPrice)
            .HasConversion(
                v => v.Amount,
                v => new Money(v))
            .HasPrecision(18, 2)
            .IsRequired();

        // Optional: SubTotal as computed column
        builder.Ignore(oi => oi.SubTotal); // EF will ignore this for now
    }
}