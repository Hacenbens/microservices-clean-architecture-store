using MediatR;
using OrderService.Application.Abstractions.Repositories;
using OrderService.Application.DTOs;
using OrderService.Domain.Comman.Result;

namespace OrderService.Application.Features.Orders.Queries.GetOrderById;

public sealed class GetOrderByIdQueryHandler
    : IRequestHandler<GetOrderByIdQuery, Result<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<OrderDto>> Handle(
        GetOrderByIdQuery query,
        CancellationToken ct)
    {
        var order = await _orderRepository.GetByIdAsync(query.OrderId, ct);
        if (order is null)
        {
            return Result<OrderDto>.Failure(
                Error.NotFound("Order.NotFound", "Order not found"));
        }

        var items = order.Items
            .Select(i => new OrderItemDto(
                i.ProductId,
                i.Name,
                i.Category,
                i.Quantity,
                i.UnitPrice.Amount,
                i.SubTotal.Amount
            ))
            .ToList();

        var dto = new OrderDto(
            order.Id,
            order.UserId,
            items,
            order.TotalPrice.Amount,
            order.Status.ToString(),
            order.OrderDate
        );

        return Result<OrderDto>.Success(dto);
    }
}