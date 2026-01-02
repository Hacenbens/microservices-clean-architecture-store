using ProductService.Domain.Comman.IEntities;
using ProductService.Domain.Comman.Result;
using ProductService.Domain.Comman.Exceptions;


namespace ProductService.Domain.Entities;

public sealed class Product : AuditableEntity
{
    public string Name { get; private set; } = default!;
    public string Category { get; private set; } = default!;
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }

    private Product() { } // Required by EF Core

    private Product(
        string name,
        string category,
        decimal price,
        int stockQuantity)
    {
        Name = name;
        Category = category;
        Price = price;
        StockQuantity = stockQuantity;
    }

    public static Result<Product> Create(
        string name,
        string category,
        decimal price,
        int stockQuantity)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<Product>.Failure(
                new Error(
                    "Product.Name.Empty",
                    "Product name is required",
                    ErrorKind.Validation));
        }

        if (string.IsNullOrWhiteSpace(category))
        {
            return Result<Product>.Failure(
                new Error(
                    "Product.Category.Empty",
                    "Product category is required",
                    ErrorKind.Validation));
        }

        if (price <= 0)
        {
            return Result<Product>.Failure(
                new Error(
                    "Product.Price.Invalid",
                    "Product price must be greater than zero",
                    ErrorKind.Validation));
        }

        if (stockQuantity < 0)
        {
            return Result<Product>.Failure(
                new Error(
                    "Product.Stock.Invalid",
                    "Stock quantity cannot be negative",
                    ErrorKind.Validation));
        }

        return Result<Product>.Success(
            new Product(name, category, price, stockQuantity));
    }

    public void UpdatePrice(decimal price)
    {
        if (price <= 0)
            throw new DomainException("Price must be greater than zero");

        Price = price;
        MarkUpdated();
    }

    public void ChangeCategory(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
            throw new DomainException("Category cannot be empty");

        Category = category;
        MarkUpdated();
    }

    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be positive");

        StockQuantity += quantity;
        MarkUpdated();
    }

    public void DecreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be positive");

        if (StockQuantity - quantity < 0)
            throw new DomainException("Insufficient stock");

        StockQuantity -= quantity;
        MarkUpdated();
    }
}
