using FluentAssertions;
using ProductService.Domain.Entities;
using ProductService.Domain.Comman.Exceptions;
using Xunit;

namespace ProductService.Domain.Tests;

public class ProductTests
{
    [Fact]
    public void Create_ShouldSucceed_WhenDataIsValid()
    {
        // Act
        var result = Product.Create(
            name: "Laptop",
            category: "Electronics",
            price: 1200m,
            stockQuantity: 10);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Name.Should().Be("Laptop");
        result.Value.Category.Should().Be("Electronics");
        result.Value.Price.Should().Be(1200m);
        result.Value.StockQuantity.Should().Be(10);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_ShouldFail_WhenNameIsInvalid(string name)
    {
        var result = Product.Create(name, "Category", 100m, 1);

        result.IsSuccess.Should().BeFalse();
        result.Error.Code.Should().Be("Product.Name.Empty");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_ShouldFail_WhenCategoryIsInvalid(string category)
    {
        var result = Product.Create("Product", category, 100m, 1);

        result.IsSuccess.Should().BeFalse();
        result.Error.Code.Should().Be("Product.Category.Empty");
    }

    [Fact]
    public void Create_ShouldFail_WhenPriceIsInvalid()
    {
        var result = Product.Create("Product", "Category", 0m, 1);

        result.IsSuccess.Should().BeFalse();
        result.Error.Code.Should().Be("Product.Price.Invalid");
    }

    [Fact]
    public void Create_ShouldFail_WhenStockIsNegative()
    {
        var result = Product.Create("Product", "Category", 100m, -1);

        result.IsSuccess.Should().BeFalse();
        result.Error.Code.Should().Be("Product.Stock.Invalid");
    }

    [Fact]
    public void UpdatePrice_ShouldUpdatePrice_WhenValid()
    {
        var product = Product.Create("Product", "Category", 100m, 5).Value!;

        product.UpdatePrice(150m);

        product.Price.Should().Be(150m);
    }

    [Fact]
    public void UpdatePrice_ShouldThrow_WhenInvalid()
    {
        var product = Product.Create("Product", "Category", 100m, 5).Value!;

        Action act = () => product.UpdatePrice(0m);

        act.Should().Throw<DomainException>()
           .WithMessage("Price must be greater than zero");
    }

    [Fact]
    public void ChangeCategory_ShouldUpdateCategory_WhenValid()
    {
        var product = Product.Create("Product", "Old", 100m, 5).Value!;

        product.ChangeCategory("New");

        product.Category.Should().Be("New");
    }

    [Fact]
    public void IncreaseStock_ShouldIncreaseQuantity()
    {
        var product = Product.Create("Product", "Category", 100m, 5).Value!;

        product.IncreaseStock(3);

        product.StockQuantity.Should().Be(8);
    }

    [Fact]
    public void DecreaseStock_ShouldDecreaseQuantity()
    {
        var product = Product.Create("Product", "Category", 100m, 5).Value!;

        product.DecreaseStock(2);

        product.StockQuantity.Should().Be(3);
    }

    [Fact]
    public void DecreaseStock_ShouldThrow_WhenInsufficientStock()
    {
        var product = Product.Create("Product", "Category", 100m, 2).Value!;

        Action act = () => product.DecreaseStock(5);

        act.Should().Throw<DomainException>()
           .WithMessage("Insufficient stock");
    }
}
