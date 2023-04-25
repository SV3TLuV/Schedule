using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Specialities.Commands.Create;
using Schedule.Application.Features.Specialities.Commands.Delete;
using Schedule.Application.Features.Specialities.Commands.Update;
using Schedule.Application.Features.Specialities.Queries.Get;
using Schedule.Application.Features.Specialities.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public class SpecialityController : BaseController
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<SpecialityViewModel>> Get(int id)
    {
        var query = new GetSpecialityQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<SpecialityViewModel>>> GetAll(
        [FromQuery] GetSpecialityListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateSpecialityCommand command)
    {
        var id = await Mediator.Send(command);
        return Created(string.Empty, id);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateSpecialityCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var query = new DeleteSpecialityCommand(id);
        await Mediator.Send(query);
        return NoContent();
    }
}