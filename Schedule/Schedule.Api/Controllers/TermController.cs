using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Terms.Queries.GetAll;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Api.Controllers;

public class TermController : BaseController
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PagedList<TermViewModel>>> GetAll(
        [FromQuery] GetTermListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}