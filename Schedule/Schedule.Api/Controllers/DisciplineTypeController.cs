using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.DisciplineTypes.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public class DisciplineTypeController : BaseController
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PagedList<DisciplineTypeViewModel>>> GetAll(
        [FromQuery] GetDisciplineTypeListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}