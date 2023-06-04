using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Lessons.Commands.Create;
using Schedule.Application.Features.Lessons.Commands.Delete;
using Schedule.Application.Features.Lessons.Commands.Update;
using Schedule.Application.Features.Lessons.Commands.UpdateFilledLessonsTimeCommand;
using Schedule.Application.Features.Lessons.Queries.Get;
using Schedule.Application.Features.Lessons.Queries.GetLessonNumberList;
using Schedule.Application.Features.Lessons.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public sealed class LessonController : BaseController
{
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<LessonViewModel>> Get(int id)
    {
        var query = new GetLessonQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PagedList<LessonViewModel>>> GetAll(
        [FromQuery] GetLessonListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [Authorize]
    [HttpGet("number/{dateId:int}")]
    public async Task<ActionResult<ICollection<int>>> GetLessonNumbers(int dateId)
    {
        var query = new GetLessonNumberListQuery(dateId);
        return Ok(await Mediator.Send(query));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLessonCommand command)
    {
        var id = await Mediator.Send(command);
        return Created(string.Empty, id);
    }

    [Authorize]
    [HttpPost("update-filled-lessons-time")]
    public async Task<IActionResult> UpdateFilledLessonsTime(
        [FromBody] UpdateFilledLessonsTimeCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateLessonCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteLessonCommand(id);
        await Mediator.Send(command);
        return NoContent();
    }
}