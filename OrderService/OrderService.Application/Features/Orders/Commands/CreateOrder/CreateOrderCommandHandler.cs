using MediatR;
using OrderService.Domain.Entities;
using OrderService.Domain.Comman.Result;
using OrderService.Application.Abstractions.Repositories;
using OrderService.Application.DTOs;

namespace OrderService.Application.Features.Orders.Commands.CreateOrder;

public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<OrderDto>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderItems = new List<OrderItem>();

        foreach (var item in command.Items)
        {
            var createResult = OrderItem.Create(
                item.ProductId,
                item.Name,
                item.Category,
                item.Quantity,
                item.UnitPrice);

            if (!createResult.IsSuccess)
                return Result<OrderDto>.Failure(createResult.Error);

            orderItems.Add(createResult.Value!);
        }

        var orderResult = Order.Create(command.UserId, orderItems);
        if (!orderResult.IsSuccess)
            return Result<OrderDto>.Failure(orderResult.Error);

        var order = orderResult.Value!;
        await _orderRepository.AddAsync(order, cancellationToken);

        // Map to DTO
        var orderDto = new OrderDto(
            order.Id,
            order.UserId,
            order.Items.Select(i => new OrderItemDto(
                i.ProductId,
                i.Name,
                i.Category,
                i.Quantity,
                i.UnitPrice.Amount,
                i.SubTotal.Amount)).ToList(),
            order.TotalPrice.Amount,
            order.Status.ToString(),
            order.OrderDate
        );

        return Result<OrderDto>.Success(orderDto);
    }
}