using MediatR;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.TimeTypes.Commands.Create;
using Schedule.Application.Features.TimeTypes.Commands.Delete;
using Schedule.Application.Features.TimeTypes.Commands.Update;
using Schedule.Application.Features.TimeTypes.Queries.Get;
using Schedule.Application.Features.TimeTypes.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public class TimeTypeController : BaseController
{
    public TimeTypeController(IMediator mediator) 
        : base(mediator)
    {
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TimeTypeViewModel>> Get(int id)
    {
        var query = new GetTimeTypeQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<TimeTypeViewModel>>> GetAll(
        [FromQuery] GetTimeTypeListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateTimeTypeCommand command)
    {
        var timeTypeId = await Mediator.Send(command);
        return Created(string.Empty, timeTypeId);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateTimeTypeCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var query = new DeleteTimeTypeCommand(id);
        await Mediator.Send(query);
        return NoContent();
    }
}