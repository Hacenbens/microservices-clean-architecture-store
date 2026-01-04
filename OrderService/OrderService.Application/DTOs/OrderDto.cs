namespace OrderService.Application.DTOs;

public sealed record OrderDto(
    Guid Id,
    Guid UserId,
    List<OrderItemDto> Items,
    decimal TotalPrice,
    string Status,
    DateTime OrderDate);