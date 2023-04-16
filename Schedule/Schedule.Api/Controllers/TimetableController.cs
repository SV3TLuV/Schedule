using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Timetables.Queries.GetCurrentList;

namespace Schedule.Api.Controllers;

public sealed class TimetableController : BaseController
{
    [HttpGet]
    [Route("Current", Name = "CurrentTimetable")]
    public async Task<IActionResult> Get([FromQuery] GetCurrentTimetableListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}