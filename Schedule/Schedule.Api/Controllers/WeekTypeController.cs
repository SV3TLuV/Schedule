using MediatR;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.WeekTypes.Commands.Create;
using Schedule.Application.Features.WeekTypes.Commands.Delete;
using Schedule.Application.Features.WeekTypes.Commands.Update;
using Schedule.Application.Features.WeekTypes.Queries.Get;
using Schedule.Application.Features.WeekTypes.Queries.GetList;
using Schedule.Application.ViewModels;

namespace Schedule.Api.Controllers;

public class WeekTypeController : BaseController
{
    public WeekTypeController(IMediator mediator) 
        : base(mediator)
    {
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<WeekTypeViewModel>> Get(int id)
    {
        var query = new GetWeekTypeQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<WeekTypeViewModel[]>> GetAll(
        [FromQuery] GetWeekTypeListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateWeekTypeCommand command)
    {
        var weekTypeId = await Mediator.Send(command);
        return Created(string.Empty, weekTypeId);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateWeekTypeCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var query = new DeleteWeekTypeCommand(id);
        await Mediator.Send(query);
        return NoContent();
    }
}