namespace OrderService.Domain.ValueObjects;

public enum OrderStatus
{
    Pending,
    Confirmed,
    Shipped,
    Cancelled,
    Failed
}