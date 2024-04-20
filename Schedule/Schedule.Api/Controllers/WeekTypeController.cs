using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.WeekTypes.Queries.GetCurrent;
using Schedule.Application.Features.WeekTypes.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public class WeekTypeController : BaseController
{
    [Authorize]
    [HttpGet]
    [Route("Current", Name = "CurrentWeekType")]
    public async Task<ActionResult<WeekTypeViewModel>> Get()
    {
        var query = new GetCurrentWeekTypeQuery();
        return Ok(await Mediator.Send(query));
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PagedList<WeekTypeViewModel>>> GetAll(
        [FromQuery] GetWeekTypeListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}