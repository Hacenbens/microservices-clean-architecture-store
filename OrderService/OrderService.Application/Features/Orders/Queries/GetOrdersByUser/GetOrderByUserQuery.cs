using MediatR;
using OrderService.Application.DTOs;
using OrderService.Domain.Comman.Result;

namespace OrderService.Application.Features.Orders.Queries.GetOrdersByUser;

public sealed record GetOrdersByUserQuery(Guid UserId)
    : IRequest<Result<List<OrderDto>>>;