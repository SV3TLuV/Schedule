using MediatR;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Days.Commands.Update;
using Schedule.Application.Features.Days.Queries.Get;
using Schedule.Application.Features.Days.Queries.GetCurrent;
using Schedule.Application.Features.Days.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public class DayController : BaseController
{
    public DayController(IMediator mediator) 
        : base(mediator)
    {
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DayViewModel>> Get(int id)
    {
        var query = new GetDayQuery(id);
        return Ok(await Mediator.Send(query));
    }
    
    [HttpGet("/current")]
    public async Task<ActionResult<DayViewModel>> Get()
    {
        var query = new GetCurrentDayQuery();
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<DayViewModel>>> GetAll(
        [FromQuery] GetDayListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateDayCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
}