using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Groups.Commands.Create;
using Schedule.Application.Features.Groups.Commands.Delete;
using Schedule.Application.Features.Groups.Commands.Restore;
using Schedule.Application.Features.Groups.Commands.Update;
using Schedule.Application.Features.Groups.Queries.Get;
using Schedule.Application.Features.Groups.Queries.GetGroupDisciplines;
using Schedule.Application.Features.Groups.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public class GroupController : BaseController
{
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<GroupViewModel>> Get(int id)
    {
        var query = new GetGroupQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<GroupViewModel>>> GetAll(
        [FromQuery] GetGroupListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [Authorize]
    [HttpGet]
    [Route("get-group-disciplines", Name = "GetGroupDisciplines")]
    public async Task<ActionResult<GroupViewModel[]>> GetGroupDisciplines(
        [FromQuery] GetGroupDisciplinesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateGroupCommand command)
    {
        var id = await Mediator.Send(command);
        return Created(string.Empty, id);
    }

    [Authorize]
    [HttpPost]
    [Route("Restore", Name = "RestoreGroup")]
    public async Task<IActionResult> Post([FromBody] RestoreGroupCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateGroupCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var query = new DeleteGroupCommand(id);
        await Mediator.Send(query);
        return NoContent();
    }
}