using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Reports.Queries.GetReportForDate;

namespace Schedule.Api.Controllers;

public sealed class ReportController : BaseController
{
    [HttpGet]
    public async Task<IResult> Get([FromQuery] GetReportForDateQuery query)
    {
        var report = await Mediator.Send(query);
        return Results.File(report.Content, report.ContentType, report.ReportName);
    }
}