using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Users.Commands.Create;
using Schedule.Application.Features.Users.Commands.Delete;
using Schedule.Application.Features.Users.Commands.Login;
using Schedule.Application.Features.Users.Commands.Logout;
using Schedule.Application.Features.Users.Commands.Refresh;
using Schedule.Application.Features.Users.Commands.Update;
using Schedule.Application.Features.Users.Queries.Get;
using Schedule.Application.Features.Users.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public sealed class UserController : BaseController
{
    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserViewModel>> Get(int id)
    {
        var query = new GetUserQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<PagedList<UserViewModel>>> GetAll(
        [FromQuery] GetUserListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthorizationResultViewModel>> Login([FromBody] LoginCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<AuthorizationResultViewModel>> Logout([FromBody] RefreshCommand command)
    {
        return await Mediator.Send(command);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var id = await Mediator.Send(command);
        return Created(string.Empty, id);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteUserCommand(id);
        await Mediator.Send(command);
        return NoContent();
    }
}