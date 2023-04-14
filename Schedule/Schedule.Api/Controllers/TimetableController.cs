using MediatR;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Timetables.Queries.GetCurrentList;

namespace Schedule.Api.Controllers;

public sealed class TimetableController : BaseController
{
    public TimetableController(IMediator mediator) 
        : base(mediator)
    {
    }
    
    [HttpGet]
    [Route("Current", Name = "CurrentTimetable")]
    public async Task<IActionResult> Get([FromQuery] GetCurrentTimetableListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}