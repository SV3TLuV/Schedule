using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Teachers.Queries.Get;
using Schedule.Application.Features.Teachers.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public sealed class TeacherController : BaseController
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TeacherViewModel>> Get(int id)
    {
        var query = new GetTeacherQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<TeacherViewModel>>> GetAll(
        [FromQuery] GetTeacherListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}