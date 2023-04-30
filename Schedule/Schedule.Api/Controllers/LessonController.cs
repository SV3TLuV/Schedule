using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Lessons.Commands.Create;
using Schedule.Application.Features.Lessons.Commands.Delete;
using Schedule.Application.Features.Lessons.Commands.Update;
using Schedule.Application.Features.Lessons.Queries.Get;
using Schedule.Application.Features.Lessons.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public sealed class LessonController : BaseController
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<LessonViewModel>> Get(int id)
    {
        var query = new GetLessonQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<LessonViewModel>>> GetAll(
        [FromQuery] GetLessonListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLessonCommand command)
    {
        var id = await Mediator.Send(command);
        return Created(string.Empty, id);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateLessonCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteLessonCommand(id);
        await Mediator.Send(command);
        return NoContent();
    }
}