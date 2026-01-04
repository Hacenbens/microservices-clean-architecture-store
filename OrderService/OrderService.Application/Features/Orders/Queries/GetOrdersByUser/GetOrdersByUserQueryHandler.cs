using MediatR;
using OrderService.Application.Abstractions.Repositories;
using OrderService.Application.DTOs;
using OrderService.Domain.Comman.Result;

namespace OrderService.Application.Features.Orders.Queries.GetOrdersByUser;

public sealed class GetOrdersByUserQueryHandler
    : IRequestHandler<GetOrdersByUserQuery, Result<List<OrderDto>>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersByUserQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<List<OrderDto>>> Handle(
        GetOrdersByUserQuery query,
        CancellationToken ct)
    {
        var orders = await _orderRepository.GetByUserIdAsync(query.UserId, ct);

        if (orders is null || orders.Count == 0)
        {
            return Result<List<OrderDto>>.Failure(
                Error.NotFound(
                    "Order.NotFound",
                    "No orders found for this user"));
        }

        var dtos = orders.Select(order =>
        {
            var items = order.Items.Select(i => new OrderItemDto(
                i.ProductId,
                i.Name,
                i.Category,
                i.Quantity,
                i.UnitPrice.Amount,
                i.SubTotal.Amount
            )).ToList();

            return new OrderDto(
                order.Id,
                order.UserId,
                items,
                order.TotalPrice.Amount,
                order.Status.ToString(),
                order.OrderDate
            );
        }).ToList();

        return Result<List<OrderDto>>.Success(dtos);
    }
}