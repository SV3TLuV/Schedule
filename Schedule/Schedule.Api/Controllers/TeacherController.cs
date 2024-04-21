﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Lessons.Queries.GetTeacherLessonList;
using Schedule.Application.Features.Teachers.Commands.Create;
using Schedule.Application.Features.Teachers.Commands.Delete;
using Schedule.Application.Features.Teachers.Commands.Restore;
using Schedule.Application.Features.Teachers.Commands.Update;
using Schedule.Application.Features.Teachers.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public class TeacherController : BaseController
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PagedList<TeacherViewModel>>> GetAll(
        [FromQuery] GetTeacherListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [Authorize]
    [HttpGet("{id:int}/Lessons")]
    public async Task<ActionResult<LessonViewModel[]>> GetAll(
        [FromQuery] GetTeacherLessonListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateTeacherCommand command)
    {
        var id = await Mediator.Send(command);
        return Created(string.Empty, id);
    }

    [Authorize]
    [HttpPost]
    [Route("Restore", Name = "RestoreTeacher")]
    public async Task<IActionResult> Post([FromBody] RestoreTeacherCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateTeacherCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var query = new DeleteTeacherCommand(id);
        await Mediator.Send(query);
        return NoContent();
    }
}