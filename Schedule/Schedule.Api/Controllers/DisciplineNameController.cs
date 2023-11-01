using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.DisciplineCodes.Commands.Create;
using Schedule.Application.Features.DisciplineCodes.Commands.Delete;
using Schedule.Application.Features.DisciplineCodes.Commands.Restore;
using Schedule.Application.Features.DisciplineCodes.Commands.Update;
using Schedule.Application.Features.DisciplineCodes.Queries.Get;
using Schedule.Application.Features.DisciplineCodes.Queries.GetList;
using Schedule.Application.Features.DisciplineNames.Commands.Create;
using Schedule.Application.Features.DisciplineNames.Commands.Delete;
using Schedule.Application.Features.DisciplineNames.Commands.Restore;
using Schedule.Application.Features.DisciplineNames.Commands.Update;
using Schedule.Application.Features.DisciplineNames.Queries.Get;
using Schedule.Application.Features.DisciplineNames.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public sealed class DisciplineNameController : BaseController
{
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DisciplineNameViewModel>> Get(int id)
    {
        var query = new GetDisciplineNameQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PagedList<DisciplineNameViewModel>>> GetAll(
        [FromQuery] GetDisciplineNameQueryList query)
    {
        return Ok(await Mediator.Send(query));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateDisciplineNameCommand command)
    {
        var id = await Mediator.Send(command);
        return Created(string.Empty, id);
    }

    [Authorize]
    [HttpPost]
    [Route("Restore", Name = "RestoreDisciplineName")]
    public async Task<IActionResult> Post([FromBody] RestoreDisciplineNameCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateDisciplineNameCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var query = new DeleteDisciplineNameCommand(id);
        await Mediator.Send(query);
        return NoContent();
    }
}