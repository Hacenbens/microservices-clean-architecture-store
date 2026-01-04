using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Features.Users.Commands.CreateUser;
using UserService.Application.Features.Users.Queries.GetUserById;

namespace UserService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Error); // Map Error object

        return CreatedAtAction(nameof(GetUserById), new { id = result.Value.Id }, result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var query = new GetUserByIdQuery(id);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return NotFound(result.Error);

        return Ok(result.Value);
    }
}