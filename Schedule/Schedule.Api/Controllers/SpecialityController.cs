using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Specialities.Commands.Create;
using Schedule.Application.Features.Specialities.Commands.Delete;
using Schedule.Application.Features.Specialities.Commands.Import;
using Schedule.Application.Features.Specialities.Commands.Restore;
using Schedule.Application.Features.Specialities.Commands.Update;
using Schedule.Application.Features.Specialities.Queries.Get;
using Schedule.Application.Features.Specialities.Queries.GetList;
using Schedule.Application.Features.Specialities.Queries.GetSpecialityReport;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public class SpecialityController : BaseController
{
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<SpecialityViewModel>> Get(int id)
    {
        var query = new GetSpecialityQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PagedList<SpecialityViewModel>>> GetAll(
        [FromQuery] GetSpecialityListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateSpecialityCommand command)
    {
        var id = await Mediator.Send(command);
        return Created(string.Empty, id);
    }

    [Authorize]
    [HttpPost]
    [Route("Restore", Name = "RestoreSpeciality")]
    public async Task<IActionResult> Post([FromBody] RestoreSpecialityCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateSpecialityCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var query = new DeleteSpecialityCommand(id);
        await Mediator.Send(query);
        return NoContent();
    }
    
    [Authorize]
    [HttpGet("export")]
    public async Task<IResult> Get([FromQuery] GetSpecialityReportQuery query)
    {
        var report = await Mediator.Send(query);
        return Results.File(report.Content, report.ContentType, report.ReportName);
    }

    [Authorize]
    [HttpPost("import")]
    public async Task<IActionResult> Post()
    {
        await using var ms = new MemoryStream();
        await Request.Body.CopyToAsync(ms);
        
        var command = new ImportSpecialityCommand
        {
            Content = ms.ToArray()
        };
        await Mediator.Send(command);
        return NoContent();
    }
}