using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.LessonChanges.Commands.Create;
using Schedule.Application.Features.LessonChanges.Commands.Delete;
using Schedule.Application.Features.LessonChanges.Commands.Update;

namespace Schedule.Api.Controllers;

public sealed class LessonChangeController : BaseController
{
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateLessonChangeCommand command)
    {
        var id = await Mediator.Send(command);
        return Created(string.Empty, id);
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateLessonChangeCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var query = new DeleteLessonChangeCommand(id);
        await Mediator.Send(query);
        return NoContent();
    }
}