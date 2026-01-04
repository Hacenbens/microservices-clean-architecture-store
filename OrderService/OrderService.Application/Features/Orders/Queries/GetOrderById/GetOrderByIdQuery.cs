using MediatR;
using OrderService.Application.DTOs;
using OrderService.Domain.Comman.Result;

namespace OrderService.Application.Features.Orders.Queries.GetOrderById;

public sealed record GetOrderByIdQuery(Guid OrderId)
    : IRequest<Result<OrderDto>>;