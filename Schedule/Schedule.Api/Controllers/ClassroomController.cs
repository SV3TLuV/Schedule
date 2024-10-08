﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Classrooms.Commands.Create;
using Schedule.Application.Features.Classrooms.Commands.Delete;
using Schedule.Application.Features.Classrooms.Commands.Restore;
using Schedule.Application.Features.Classrooms.Commands.Update;
using Schedule.Application.Features.Classrooms.Queries.Get;
using Schedule.Application.Features.Classrooms.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public class ClassroomController : BaseController
{
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClassroomViewModel>> Get(int id)
    {
        var query = new GetClassroomQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PagedList<ClassroomViewModel>>> GetAll(
        [FromQuery] GetClassroomListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateClassroomCommand command)
    {
        var id = await Mediator.Send(command);
        return Created(string.Empty, id);
    }

    [Authorize]
    [HttpPost]
    [Route("Restore", Name = "RestoreClassroom")]
    public async Task<IActionResult> Post([FromBody] RestoreClassroomCommand typeCommand)
    {
        await Mediator.Send(typeCommand);
        return NoContent();
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateClassroomCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var query = new DeleteClassroomCommand(id);
        await Mediator.Send(query);
        return NoContent();
    }
}