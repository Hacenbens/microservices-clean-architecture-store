using MediatR;
using OrderService.Domain.Comman.Result;

namespace OrderService.Application.Features.Orders.Commands.DeleteOrderItem;

public sealed record DeleteOrderItemCommand(Guid OrderId, Guid ProductId) : IRequest<Result>;