using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.ClassroomTypes.Commands.Create;
using Schedule.Application.Features.ClassroomTypes.Commands.Delete;
using Schedule.Application.Features.ClassroomTypes.Commands.Restore;
using Schedule.Application.Features.ClassroomTypes.Commands.Update;
using Schedule.Application.Features.ClassroomTypes.Queries.Get;
using Schedule.Application.Features.ClassroomTypes.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public class ClassroomTypeController : BaseController
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClassroomTypeViewModel>> Get(int id)
    {
        var query = new GetClassroomTypeQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<ClassroomTypeViewModel>>> GetAll(
        [FromQuery] GetClassroomTypeListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateClassroomTypeCommand command)
    {
        var id = await Mediator.Send(command);
        return Created(string.Empty, id);
    }

    [HttpPost]
    [Route("Restore", Name = "RestoreClassroomType")]
    public async Task<IActionResult> Post([FromBody] RestoreClassroomTypeCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
    
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateClassroomTypeCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var query = new DeleteClassroomTypeCommand(id);
        await Mediator.Send(query);
        return NoContent();
    }
}