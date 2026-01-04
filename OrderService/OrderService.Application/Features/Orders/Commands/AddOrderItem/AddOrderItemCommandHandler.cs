using MediatR;
using OrderService.Application.Abstractions.Repositories;
using OrderService.Domain.Comman.Result;
using OrderService.Domain.Entities;

namespace OrderService.Application.Features.Orders.Commands.AddOrderItem;

public sealed class AddOrderItemCommandHandler
    : IRequestHandler<AddOrderItemCommand, Result>
{
    private readonly IOrderRepository _orderRepository;

    public AddOrderItemCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result> Handle(AddOrderItemCommand command, CancellationToken ct)
    {
        var order = await _orderRepository.GetByIdAsync(command.OrderId, ct);
        if (order is null)
            return Result.Failure(Error.NotFound("Order.NotFound", "Order not found"));

        var itemResult = OrderItem.Create(
            command.ProductId,
            command.Name,
            command.Category,
            command.Quantity,
            command.UnitPrice);

        if (!itemResult.IsSuccess)
            return itemResult;

        var addResult = order.AddItem(itemResult.Value!);
        if (!addResult.IsSuccess)
            return addResult;

        await _orderRepository.UpdateAsync(order, ct);
        return Result.Success();
    }
}