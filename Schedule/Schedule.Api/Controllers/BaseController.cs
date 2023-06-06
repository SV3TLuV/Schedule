using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Schedule.Api.Common;
using Schedule.Application.Features.Reports.Queries.GetReportForDate;
using Schedule.Application.ViewModels;

namespace Schedule.Api.Controllers;

[ApiController]
[EnableCors(Constants.CorsName)]
[Route("/api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
}

public sealed class ReportController : BaseController
{
    [HttpGet]
    public async Task<IResult> Get([FromQuery] GetReportForDateQuery query)
    {
        var report = await Mediator.Send(query);
        return Results.File(report.Content, report.ContentType, report.ReportName);
    }
}