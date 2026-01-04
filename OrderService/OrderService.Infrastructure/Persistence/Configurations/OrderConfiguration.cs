using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        // Table name
        builder.ToTable("Orders");

        // Primary key
        builder.HasKey(o => o.Id);

        // Properties
        builder.Property(o => o.UserId)
            .IsRequired();

        builder.Property(o => o.Status)
            .HasConversion<string>() // store enum as string
            .IsRequired();

        builder.Property(o => o.OrderDate)
            .IsRequired();

        // Relationship: Order -> OrderItems (1:N)
        builder.HasMany(o => o.Items)
            .WithOne() // OrderItem does not have a navigation back to Order
            .HasForeignKey("OrderId") // shadow FK property in OrderItem table
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(o => o.UserId);
    }
}