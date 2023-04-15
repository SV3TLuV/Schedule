using MediatR;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Disciplines.Commands.Create;
using Schedule.Application.Features.Disciplines.Commands.Delete;
using Schedule.Application.Features.Disciplines.Commands.Update;
using Schedule.Application.Features.Disciplines.Queries.Get;
using Schedule.Application.Features.Disciplines.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public class DisciplineController : BaseController
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DisciplineViewModel>> Get(int id)
    {
        var query = new GetDisciplineQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<DisciplineViewModel>>> GetAll(
        [FromQuery] GetDisciplineListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateDisciplineCommand command)
    {
        var timeTypeId = await Mediator.Send(command);
        return Created(string.Empty, timeTypeId);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateDisciplineCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var query = new DeleteDisciplineCommand(id);
        await Mediator.Send(query);
        return NoContent();
    }
}