using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Terms.Queries.Get;
using Schedule.Application.Features.Terms.Queries.GetAll;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public class TermController : BaseController
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TermViewModel>> Get(int id)
    {
        var query = new GetTermQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<TermViewModel>>> GetAll(
        [FromQuery] GetTermListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}