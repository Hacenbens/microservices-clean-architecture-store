using MediatR;
using OrderService.Domain.Comman.Result;
using OrderService.Application.DTOs;

namespace OrderService.Application.Features.Orders.Commands.CreateOrder;

public sealed record CreateOrderCommand(
    Guid UserId,
    List<CreateOrderItemDto> Items) : IRequest<Result<OrderDto>>;

public sealed record CreateOrderItemDto(
    Guid ProductId,
    string Name,
    string Category,
    int Quantity,
    decimal UnitPrice);    