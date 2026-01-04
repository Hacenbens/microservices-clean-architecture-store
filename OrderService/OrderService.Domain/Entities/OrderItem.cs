using OrderService.Domain.Comman.IEntities;
using OrderService.Domain.ValueObjects;
using OrderService.Domain.Comman.Result;
using OrderService.Domain.Comman.Exceptions;


namespace OrderService.Domain.Entities;

public sealed class OrderItem : AuditableEntity
{
    public Guid ProductId { get; private set; }
    public string Name { get; private set; } = default!;
    public string Category { get; private set; } = default!;
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; }
    public Money SubTotal => UnitPrice * Quantity;

    private OrderItem() { } // EF Core

    private OrderItem(Guid productId, string name, string category, int quantity, Money unitPrice)
    {
        ProductId = productId;
        Name = name;
        Category = category;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public static Result<OrderItem> Create(Guid productId, string name, string category, int quantity, decimal unitPrice)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<OrderItem>.Failure(new Error("OrderItem.Name.Empty", "Product name is required", ErrorKind.Validation));
        if (string.IsNullOrWhiteSpace(category))
            return Result<OrderItem>.Failure(new Error("OrderItem.Category.Empty", "Product category is required", ErrorKind.Validation));
        if (quantity <= 0)
            return Result<OrderItem>.Failure(new Error("OrderItem.Quantity.Invalid", "Quantity must be positive", ErrorKind.Validation));
        if (unitPrice <= 0)
            return Result<OrderItem>.Failure(new Error("OrderItem.UnitPrice.Invalid", "Unit price must be greater than zero", ErrorKind.Validation));

        var money = new Money(unitPrice);
        return Result<OrderItem>.Success(new OrderItem(productId, name, category, quantity, money));
    }

    public void IncreaseQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be positive");
        Quantity += quantity;
        MarkUpdated();
    }

    public void DecreaseQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be positive");
        if (Quantity - quantity < 0)
            throw new DomainException("Cannot reduce quantity below zero");
        Quantity -= quantity;
        MarkUpdated();
    }
}
