﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.WeekTypes.Queries.Get;
using Schedule.Application.Features.WeekTypes.Queries.GetList;
using Schedule.Application.ViewModels;

namespace Schedule.Api.Controllers;

public class WeekTypeController : BaseController
{
    public WeekTypeController(IMediator mediator) 
        : base(mediator)
    {
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<WeekTypeViewModel>> Get(int id)
    {
        var query = new GetWeekTypeQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<WeekTypeViewModel[]>> GetAll(
        [FromQuery] GetWeekTypeListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}