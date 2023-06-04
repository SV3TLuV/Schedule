using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.TimeTypes.Commands.Create;
using Schedule.Application.Features.TimeTypes.Commands.Delete;
using Schedule.Application.Features.TimeTypes.Commands.Restore;
using Schedule.Application.Features.TimeTypes.Commands.Update;
using Schedule.Application.Features.TimeTypes.Queries.Get;
using Schedule.Application.Features.TimeTypes.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public class TimeTypeController : BaseController
{
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TimeTypeViewModel>> Get(int id)
    {
        var query = new GetTimeTypeQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PagedList<TimeTypeViewModel>>> GetAll(
        [FromQuery] GetTimeTypeListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateTimeTypeCommand command)
    {
        var id = await Mediator.Send(command);
        return Created(string.Empty, id);
    }

    [Authorize]
    [HttpPost]
    [Route("Restore", Name = "RestoreTimeType")]
    public async Task<IActionResult> Post([FromBody] RestoreTimeTypeCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateTimeTypeCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var query = new DeleteTimeTypeCommand(id);
        await Mediator.Send(query);
        return NoContent();
    }
}