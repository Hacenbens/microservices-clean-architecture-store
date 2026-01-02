using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.DTOs;
using ProductService.Application.Features.Products.Commands.CreateProduct;
using ProductService.Application.Features.Products.Commands.DeleteProduct;
using ProductService.Application.Features.Products.Queries;
using ProductService.Domain.Comman.Result;

namespace ProductService.API.Controllers;

[ApiController]
[Route("api/products")]
public sealed class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST api/products
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Value },
            result.Value);
    }

    // GET api/products/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(
            new GetProductByIdQuery(id));

        if (!result.IsSuccess)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    // GET api/products/category/{category}
    [HttpGet("category/{category}")]
    public async Task<IActionResult> GetByCategory(string category)
    {
        var result = await _mediator.Send(
            new GetProductsByCategoryQuery(category));

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    // DELETE api/products/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(
            new DeleteProductCommand(id));

        if (!result.IsSuccess)
            return NotFound(result.Error);

        return NoContent();
    }
}
