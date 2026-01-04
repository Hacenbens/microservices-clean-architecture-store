using MediatR;
using OrderService.Application.Abstractions.Repositories;
using OrderService.Domain.Comman.Result;

namespace OrderService.Application.Features.Orders.Commands.DeleteOrder;

public sealed class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Result>
{
    private readonly IOrderRepository _orderRepository;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(command.OrderId, cancellationToken);
        if (order is null)
        {
            return Result.Failure(new Domain.Comman.Result.Error(
                "Order.NotFound",
                $"Order with ID {command.OrderId} not found",
                Domain.Comman.Result.ErrorKind.NotFound));
        }

        await _orderRepository.DeleteAsync(order, cancellationToken);
        return Result.Success();
    }
}