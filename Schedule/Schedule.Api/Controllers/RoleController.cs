using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Roles.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public sealed class RoleController : BaseController
{
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<PagedList<RoleViewModel>>> GetAll(
        [FromQuery] GetRoleListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}