namespace OrderService.Application.DTOs;

public sealed record OrderItemDto(
    Guid ProductId,
    string Name,
    string Category,
    int Quantity,
    decimal UnitPrice,
    decimal SubTotal);