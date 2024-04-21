using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public sealed class TimetableController : BaseController
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PagedList<TimetableViewModel>>> GetAll(
        [FromQuery] GetTimetableListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    [Route("Current", Name = "CurrentTimetable")]
    public async Task<ActionResult<PagedList<CurrentTimetableViewModel>>> Get(
        [FromQuery] GetCurrentTimetableListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}