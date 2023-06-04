using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Times.Commands.Create;
using Schedule.Application.Features.Times.Commands.Delete;
using Schedule.Application.Features.Times.Commands.Restore;
using Schedule.Application.Features.Times.Commands.Update;
using Schedule.Application.Features.Times.Queries.Get;
using Schedule.Application.Features.Times.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public class TimeController : BaseController
{
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TimeViewModel>> Get(int id)
    {
        var query = new GetTimeQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PagedList<TimeViewModel>>> GetAll(
        [FromQuery] GetTimeListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateTimeCommand command)
    {
        var id = await Mediator.Send(command);
        return Created(string.Empty, id);
    }

    [Authorize]
    [HttpPost]
    [Route("Restore", Name = "RestoreTime")]
    public async Task<IActionResult> Post([FromBody] RestoreTimeCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateTimeCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var query = new DeleteTimeCommand(id);
        await Mediator.Send(query);
        return NoContent();
    }
}