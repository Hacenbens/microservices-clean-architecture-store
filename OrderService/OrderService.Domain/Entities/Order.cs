using OrderService.Domain.Comman.IEntities;
using OrderService.Domain.Comman.Result;
using OrderService.Domain.Comman.Exceptions;
using OrderService.Domain.ValueObjects;

namespace OrderService.Domain.Entities;

public sealed class Order : AuditableEntity
{
    public Guid UserId { get; private set; }
    public List<OrderItem> Items { get; private set; } = new();
    public Money TotalPrice => Items.Aggregate(new Money(0), (sum, item) => sum + item.SubTotal);
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public DateTime OrderDate { get; private set; } = DateTime.UtcNow;

    private Order() { } // EF Core

    private Order(Guid userId, List<OrderItem> items)
    {
        UserId = userId;
        Items = items;
    }

    public static Result<Order> Create(Guid userId, List<OrderItem> items)
    {
        if (items == null || !items.Any())
            return Result<Order>.Failure(new Error("Order.Items.Empty", "Cannot create an order with no items", ErrorKind.Validation));

        var order = new Order(userId, items);
        return Result<Order>.Success(order);
    }

    public Result AddItem(OrderItem item)
    {
        if (item is null)
            return Result.Failure(new Error("OrderItem.Null", "Cannot add a null item", ErrorKind.Validation));

        Items.Add(item);
        MarkUpdated();
        return Result.Success();
    }

    public Result RemoveItem(Guid productId)
    {
        var item = Items.FirstOrDefault(i => i.ProductId == productId);
        if (item == null)
            return Result.Failure(new Error("OrderItem.NotFound", "Item not found in order", ErrorKind.NotFound));

        Items.Remove(item);
        MarkUpdated();
        return Result.Success();
    }

    public Result Confirm()
    {
        if (Status != OrderStatus.Pending)
            return Result.Failure(new Error("Order.Status.Invalid", "Only pending orders can be confirmed", ErrorKind.Validation));

        Status = OrderStatus.Confirmed;
        MarkUpdated();
        return Result.Success();
    }

    public Result Cancel()
    {
        if (Status == OrderStatus.Shipped)
            return Result.Failure(new Error("Order.Status.Invalid", "Cannot cancel a shipped order", ErrorKind.Validation));

        Status = OrderStatus.Cancelled;
        MarkUpdated();
        return Result.Success();
    }
}
