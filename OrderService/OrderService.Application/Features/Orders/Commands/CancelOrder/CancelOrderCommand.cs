using MediatR;
using OrderService.Domain.Comman.Result;

namespace OrderService.Application.Features.Orders.Commands.CancelOrder;

public sealed record CancelOrderCommand(Guid OrderId)
    : IRequest<Result>;