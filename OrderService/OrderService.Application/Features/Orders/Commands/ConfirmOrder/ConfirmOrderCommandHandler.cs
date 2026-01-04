using MediatR;
using OrderService.Application.Abstractions.Repositories;
using OrderService.Domain.Comman.Result;

namespace OrderService.Application.Features.Orders.Commands.ConfirmOrder;

public sealed class ConfirmOrderCommandHandler
    : IRequestHandler<ConfirmOrderCommand, Result>
{
    private readonly IOrderRepository _orderRepository;

    public ConfirmOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result> Handle(
        ConfirmOrderCommand command,
        CancellationToken ct)
    {
        var order = await _orderRepository.GetByIdAsync(command.OrderId, ct);
        if (order is null)
        {
            return Result.Failure(
                Error.NotFound("Order.NotFound", "Order not found"));
        }

        var result = order.Confirm();
        if (!result.IsSuccess)
            return result;

        await _orderRepository.UpdateAsync(order, ct);

        return Result.Success();
    }
}