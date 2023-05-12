using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.DisciplineTypes.Queries.Get;
using Schedule.Application.Features.DisciplineTypes.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public class DisciplineTypeController : BaseController
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DisciplineTypeViewModel>> Get(int id)
    {
        var query = new GetDisciplineTypeQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<DisciplineTypeViewModel>>> GetAll(
        [FromQuery] GetDisciplineTypeListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}