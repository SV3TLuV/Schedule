using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Employees.Commands.Create;
using Schedule.Application.Features.Employees.Commands.Delete;
using Schedule.Application.Features.Employees.Commands.Restore;
using Schedule.Application.Features.Employees.Commands.Update;
using Schedule.Application.Features.Employees.Commands.UpdatePermissions;
using Schedule.Application.Features.Employees.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public class EmployeeController : BaseController
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PagedList<EmployeeViewModel>>> GetAll(
        [FromQuery] GetEmployeeListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateEmployeeCommand command)
    {
        var id = await Mediator.Send(command);
        return Created(string.Empty, id);
    }

    [Authorize]
    [HttpPost]
    [Route("Restore", Name = "RestoreEmployee")]
    public async Task<IActionResult> Post([FromBody] RestoreEmployeeCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpPost]
    [Route("Permission", Name = "Permission")]
    public async Task<IActionResult> Post([FromBody] EmployeeUpdatePermissionsCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateEmployeeCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var query = new DeleteEmployeeCommand(id);
        await Mediator.Send(query);
        return NoContent();
    }
}