using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Courses.Queries.Get;
using Schedule.Application.Features.Courses.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public class CourseController : BaseController
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CourseViewModel>> Get(int id)
    {
        var query = new GetCourseQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<CourseViewModel>>> GetAll(
        [FromQuery] GetCourseListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}