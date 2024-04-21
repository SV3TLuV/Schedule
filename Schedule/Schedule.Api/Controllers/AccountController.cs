using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Accounts.Commands.ChangePassword;
using Schedule.Application.Features.Accounts.Commands.Delete;
using Schedule.Application.Features.Accounts.Commands.Login;
using Schedule.Application.Features.Accounts.Commands.Logout;
using Schedule.Application.Features.Accounts.Commands.Refresh;
using Schedule.Application.Features.Accounts.Queries.Get;
using Schedule.Application.Features.Accounts.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public sealed class AccountController : BaseController
{
    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AccountViewModel>> Get(int id)
    {
        var query = new GetAccountQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<PagedList<AccountViewModel>>> GetAll(
        [FromQuery] GetAccountListQuery query)
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
    public async Task<ActionResult<AuthorizationResultViewModel>> Refresh([FromBody] RefreshCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("change_password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangeAccountPasswordCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteAccountCommand(id);
        await Mediator.Send(command);
        return NoContent();
    }
}