using MediatR;
using OrderService.Domain.Comman.Result;

namespace OrderService.Application.Features.Orders.Commands.DeleteOrder;

public sealed record DeleteOrderCommand(Guid OrderId) : IRequest<Result>;