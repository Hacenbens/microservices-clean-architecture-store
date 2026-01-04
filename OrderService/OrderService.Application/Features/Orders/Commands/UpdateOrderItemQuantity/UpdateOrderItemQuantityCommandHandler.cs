using MediatR;
using OrderService.Application.Abstractions.Repositories;
using OrderService.Domain.Comman.Result;

namespace OrderService.Application.Features.Orders.Commands.UpdateOrderItemQuantity;

public sealed class UpdateOrderItemQuantityCommandHandler
    : IRequestHandler<UpdateOrderItemQuantityCommand, Result>
{
    private readonly IOrderRepository _orderRepository;

    public UpdateOrderItemQuantityCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result> Handle(UpdateOrderItemQuantityCommand command, CancellationToken ct)
    {
        var order = await _orderRepository.GetByIdAsync(command.OrderId, ct);
        if (order is null)
            return Result.Failure(Error.NotFound("Order.NotFound", "Order not found"));

        var item = order.Items.FirstOrDefault(i => i.ProductId == command.ProductId);
        if (item is null)
            return Result.Failure(Error.NotFound("OrderItem.NotFound", "Item not found"));

        if (command.NewQuantity <= 0)
            return Result.Failure(Error.Validation("OrderItem.Quantity.Invalid", "Quantity must be > 0"));

        var diff = command.NewQuantity - item.Quantity;

        if (diff > 0)
            item.IncreaseQuantity(diff);
        else if (diff < 0)
            item.DecreaseQuantity(Math.Abs(diff));

        await _orderRepository.UpdateAsync(order, ct);
        return Result.Success();
    }
}