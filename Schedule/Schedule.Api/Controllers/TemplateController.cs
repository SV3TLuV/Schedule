using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Templates.Queries.Get;
using Schedule.Application.Features.Templates.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public sealed class TemplateController : BaseController
{
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TemplateViewModel>> Get(int id)
    {
        var query = new GetTemplateQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PagedList<TemplateViewModel>>> GetAll(
        [FromQuery] GetTemplateListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}