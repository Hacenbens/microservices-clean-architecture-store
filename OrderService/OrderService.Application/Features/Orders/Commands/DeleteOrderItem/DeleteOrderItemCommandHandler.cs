using MediatR;
using OrderService.Application.Abstractions.Repositories;
using OrderService.Domain.Comman.Result;

namespace OrderService.Application.Features.Orders.Commands.DeleteOrderItem;

public sealed class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand, Result>
{
    private readonly IOrderRepository _orderRepository;

    public DeleteOrderItemCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result> Handle(DeleteOrderItemCommand command, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(command.OrderId, cancellationToken);
        if (order is null)
        {
            return Result.Failure(new Domain.Comman.Result.Error(
                "Order.NotFound",
                $"Order with ID {command.OrderId} not found",
                Domain.Comman.Result.ErrorKind.NotFound));
        }

        var result = order.RemoveItem(command.ProductId);
        if (!result.IsSuccess)
            return result;

        await _orderRepository.UpdateAsync(order, cancellationToken);
        return Result.Success();
    }
}