using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.LessonTemplates.Commands.Create;
using Schedule.Application.Features.LessonTemplates.Commands.Delete;
using Schedule.Application.Features.LessonTemplates.Commands.Update;
using Schedule.Application.Features.LessonTemplates.Queries.Get;
using Schedule.Application.Features.LessonTemplates.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public sealed class LessonTemplateController : BaseController
{
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<LessonTemplateViewModel>> Get(int id)
    {
        var query = new GetLessonTemplateQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PagedList<LessonTemplateViewModel>>> GetAll(
        [FromQuery] GetLessonTemplateListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLessonTemplateCommand command)
    {
        var id = await Mediator.Send(command);
        return Created(string.Empty, id);
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateLessonTemplateCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteLessonTemplateCommand(id);
        await Mediator.Send(command);
        return NoContent();
    }
}