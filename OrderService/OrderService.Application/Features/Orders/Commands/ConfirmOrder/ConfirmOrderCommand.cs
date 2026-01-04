using MediatR;
using OrderService.Domain.Comman.Result;

namespace OrderService.Application.Features.Orders.Commands.ConfirmOrder;

public sealed record ConfirmOrderCommand(Guid OrderId)
    : IRequest<Result>;