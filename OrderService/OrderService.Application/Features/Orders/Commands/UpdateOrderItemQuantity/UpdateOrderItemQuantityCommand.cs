using MediatR;
using OrderService.Domain.Comman.Result;

namespace OrderService.Application.Features.Orders.Commands.UpdateOrderItemQuantity;

public sealed record UpdateOrderItemQuantityCommand(
    Guid OrderId,
    Guid ProductId,
    int NewQuantity
) : IRequest<Result>;