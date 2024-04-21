using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Lessons.Commands.Update;

namespace Schedule.Api.Controllers;

public sealed class LessonController : BaseController
{
    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateLessonCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
}