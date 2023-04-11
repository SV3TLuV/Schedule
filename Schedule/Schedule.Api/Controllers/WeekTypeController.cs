using MediatR;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.WeekTypes.Queries.Get;
using Schedule.Application.Features.WeekTypes.Queries.GetCurrent;
using Schedule.Application.Features.WeekTypes.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

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
    [Route("Current", Name = "CurrentWeekType")]
    public async Task<ActionResult<WeekTypeViewModel>> Get()
    {
        var query = new GetCurrentWeekTypeQuery();
        return Ok(await Mediator.Send(query));
    }
    
    [HttpGet]
    public async Task<ActionResult<PagedList<WeekTypeViewModel>>> GetAll(
        [FromQuery] GetWeekTypeListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}