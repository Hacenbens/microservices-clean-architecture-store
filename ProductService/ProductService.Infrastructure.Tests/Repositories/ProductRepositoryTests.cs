using FluentAssertions;
using Xunit;
using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.Persistence.Repositories;

namespace ProductService.Infrastructure.Tests.Repositories;

public class ProductRepositoryTests
{
    [Fact]
    public async Task AddAsync_ShouldPersistProduct()
    {
        var context = TestDbContextFactory.Create();
        var repository = new ProductRepository(context);

        var product = Product.Create(
            "Phone",
            "Electronics",
            800m,
            5).Value!;

        await repository.AddAsync(product);

        var stored = await context.Products.FirstAsync();

        stored.Name.Should().Be("Phone");
        stored.Category.Should().Be("Electronics");
        stored.Price.Should().Be(800m);
        stored.StockQuantity.Should().Be(5);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProduct()
    {
        var context = TestDbContextFactory.Create();
        var repository = new ProductRepository(context);

        var product = Product.Create(
            "TV",
            "Electronics",
            1500m,
            2).Value!;

        await repository.AddAsync(product);

        var result = await repository.GetByIdAsync(product.Id);

        result.Should().NotBeNull();
        result!.Name.Should().Be("TV");
    }

    [Fact]
    public async Task GetByCategoryAsync_ShouldFilterCorrectly()
    {
        var context = TestDbContextFactory.Create();
        var repository = new ProductRepository(context);

        await repository.AddAsync(Product.Create("A", "Food", 10m, 1).Value!);
        await repository.AddAsync(Product.Create("B", "Electronics", 20m, 1).Value!);
        await repository.AddAsync(Product.Create("C", "Food", 30m, 1).Value!);

        var result = await repository.GetByCategoryAsync("Food");

        result.Should().HaveCount(2);
        result.All(p => p.Category == "Food").Should().BeTrue();
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnTrue_WhenProductExists()
    {
        var context = TestDbContextFactory.Create();
        var repository = new ProductRepository(context);

        var product = Product.Create("Table", "Furniture", 200m, 1).Value!;
        await repository.AddAsync(product);

        var exists = await repository.ExistsAsync(product.Id);

        exists.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveProduct()
    {
        var context = TestDbContextFactory.Create();
        var repository = new ProductRepository(context);

        var product = Product.Create("Chair", "Furniture", 100m, 2).Value!;
        await repository.AddAsync(product);

        await repository.DeleteAsync(product);

        var exists = await repository.ExistsAsync(product.Id);
        exists.Should().BeFalse();
    }
}
