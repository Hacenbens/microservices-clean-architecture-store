using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.API.Extensions;
using OrderService.Application.Features.Orders.Commands.CreateOrder;
using OrderService.Application.Features.Orders.Commands.DeleteOrder;
using OrderService.Application.Features.Orders.Commands.AddOrderItem;
using OrderService.Application.Features.Orders.Commands.UpdateOrderItemQuantity;
using OrderService.Application.Features.Orders.Commands.DeleteOrderItem;
using OrderService.Application.Features.Orders.Queries.GetOrderById;
using OrderService.Application.Features.Orders.Queries.GetOrdersByUser;

namespace OrderService.API.Controllers;

[ApiController]
[Route("api/orders")]
public sealed class OrdersController : ControllerBase
{
    private readonly ISender _sender;

    public OrdersController(ISender sender)
    {
        _sender = sender;
    }

    // ─────────────────────────────────────────────
    // CREATE ORDER
    // ─────────────────────────────────────────────
    [HttpPost]
    public async Task<IActionResult> CreateOrder(
        [FromBody] CreateOrderCommand command,
        CancellationToken ct)
    {
        var result = await _sender.Send(command, ct);

        if (!result.IsSuccess)
            return result.Error.ToProblem();

        return CreatedAtAction(
            nameof(GetById),
            new { orderId = result.Value },
            result.Value);
    }

    // ─────────────────────────────────────────────
    // GET ORDER BY ID
    // ─────────────────────────────────────────────
    [HttpGet("{orderId:guid}")]
    public async Task<IActionResult> GetById(
        Guid orderId,
        CancellationToken ct)
    {
        var result = await _sender.Send(
            new GetOrderByIdQuery(orderId), ct);

        if (!result.IsSuccess)
            return result.Error.ToProblem();

        return Ok(result.Value);
    }

    // ─────────────────────────────────────────────
    // GET ORDERS BY USER
    // ─────────────────────────────────────────────
    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUser(
        Guid userId,
        CancellationToken ct)
    {
        var result = await _sender.Send(
            new GetOrdersByUserQuery(userId), ct);

        if (!result.IsSuccess)
            return result.Error.ToProblem();

        return Ok(result.Value);
    }

    // ─────────────────────────────────────────────
    // ADD ORDER ITEM
    // ─────────────────────────────────────────────
    [HttpPost("{orderId:guid}/items")]
    public async Task<IActionResult> AddItem(
        Guid orderId,
        [FromBody] AddOrderItemCommand command,
        CancellationToken ct)
    {
        var result = await _sender.Send(
            command with { OrderId = orderId }, ct);

        if (!result.IsSuccess)
            return result.Error.ToProblem();

        return NoContent();
    }

    // ─────────────────────────────────────────────
    // UPDATE ORDER ITEM
    // ─────────────────────────────────────────────
    [HttpPut("{orderId:guid}/items/{productId:guid}")]
    public async Task<IActionResult> UpdateItem(
        Guid orderId,
        Guid productId,
        [FromBody] UpdateOrderItemQuantityCommand command,
        CancellationToken ct)
    {
        var result = await _sender.Send(
            command with
            {
                OrderId = orderId,
                ProductId = productId
            },
            ct);

        if (!result.IsSuccess)
            return result.Error.ToProblem();

        return NoContent();
    }

    // ─────────────────────────────────────────────
    // DELETE ORDER ITEM
    // ─────────────────────────────────────────────
    [HttpDelete("{orderId:guid}/items/{productId:guid}")]
    public async Task<IActionResult> DeleteItem(
        Guid orderId,
        Guid productId,
        CancellationToken ct)
    {
        var result = await _sender.Send(
            new DeleteOrderItemCommand(orderId, productId), ct);

        if (!result.IsSuccess)
            return result.Error.ToProblem();

        return NoContent();
    }

    // ─────────────────────────────────────────────
    // DELETE ORDER
    // ─────────────────────────────────────────────
    [HttpDelete("{orderId:guid}")]
    public async Task<IActionResult> DeleteOrder(
        Guid orderId,
        CancellationToken ct)
    {
        var result = await _sender.Send(
            new DeleteOrderCommand(orderId), ct);

        if (!result.IsSuccess)
            return result.Error.ToProblem();

        return NoContent();
    }
}
