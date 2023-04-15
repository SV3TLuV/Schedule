﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.SpecialityCodes.Commands.Create;
using Schedule.Application.Features.SpecialityCodes.Commands.Delete;
using Schedule.Application.Features.SpecialityCodes.Commands.Update;
using Schedule.Application.Features.SpecialityCodes.Queries.Get;
using Schedule.Application.Features.SpecialityCodes.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public class SpecialityCodeController : BaseController
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<SpecialityCodeViewModel>> Get(int id)
    {
        var query = new GetSpecialityCodeQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<SpecialityCodeViewModel>>> GetAll(
        [FromQuery] GetSpecialityCodeListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateSpecialityCodeCommand command)
    {
        var timeTypeId = await Mediator.Send(command);
        return Created(string.Empty, timeTypeId);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateSpecialityCodeCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var query = new DeleteSpecialityCodeCommand(id);
        await Mediator.Send(query);
        return NoContent();
    }
}