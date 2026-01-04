using MediatR;
using OrderService.Domain.Comman.Result;

namespace OrderService.Application.Features.Orders.Commands.AddOrderItem;

public sealed record AddOrderItemCommand(
    Guid OrderId,
    Guid ProductId,
    string Name,
    string Category,
    int Quantity,
    decimal UnitPrice
) : IRequest<Result>;