using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.DisciplineCodes.Commands.Create;
using Schedule.Application.Features.DisciplineCodes.Commands.Delete;
using Schedule.Application.Features.DisciplineCodes.Commands.Restore;
using Schedule.Application.Features.DisciplineCodes.Commands.Update;
using Schedule.Application.Features.DisciplineCodes.Queries.Get;
using Schedule.Application.Features.DisciplineCodes.Queries.GetList;
using Schedule.Application.Features.DisciplineNames.Queries.GetList;
using Schedule.Application.Features.Disciplines.Commands.Create;
using Schedule.Application.Features.Disciplines.Commands.Delete;
using Schedule.Application.Features.Disciplines.Commands.Restore;
using Schedule.Application.Features.Disciplines.Commands.Update;
using Schedule.Application.Features.Disciplines.Queries.Get;
using Schedule.Application.Features.Disciplines.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public sealed class DisciplineCodeController : BaseController
{
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DisciplineCodeViewModel>> Get(int id)
    {
        var query = new GetDisciplineCodeQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PagedList<DisciplineCodeViewModel>>> GetAll(
        [FromQuery] GetDisciplineCodeListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateDisciplineCodeCommand command)
    {
        var id = await Mediator.Send(command);
        return Created(string.Empty, id);
    }

    [Authorize]
    [HttpPost]
    [Route("Restore", Name = "RestoreDisciplineCode")]
    public async Task<IActionResult> Post([FromBody] RestoreDisciplineCodeCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateDisciplineCodeCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var query = new DeleteDisciplineCodeCommand(id);
        await Mediator.Send(query);
        return NoContent();
    }
}