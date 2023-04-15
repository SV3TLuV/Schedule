using MediatR;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Dates.Queries.Get;
using Schedule.Application.Features.Dates.Queries.GetCurrent;
using Schedule.Application.Features.Dates.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public class DateController : BaseController
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DateViewModel>> Get(int id)
    {
        var query = new GetDateQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    [Route("Current", Name = "CurrentDate")]
    public async Task<ActionResult<DateViewModel>> Get()
    {
        var query = new GetCurrentDateQuery();
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<DateViewModel>>> GetAll(
        [FromQuery] GetDateListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}