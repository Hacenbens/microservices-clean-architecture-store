using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;

namespace ProductService.Infrastructure.Persistence;

public sealed class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all IEntityTypeConfiguration<T> automatically
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ProductDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}