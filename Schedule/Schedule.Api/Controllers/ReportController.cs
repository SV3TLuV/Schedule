using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Reports.Queries.GetReportForDate;
using Schedule.Application.Features.Reports.Queries.GetReportForDateRange;

namespace Schedule.Api.Controllers;

public sealed class ReportController : BaseController
{
    [HttpGet("date")]
    public async Task<IResult> Get([FromQuery] GetReportForDateQuery query)
    {
        var report = await Mediator.Send(query);
        return Results.File(report.Content, report.ContentType, report.ReportName);
    }
    
    [HttpGet("date-range")]
    public async Task<IResult> Get([FromQuery] GetReportForDateRangeQuery query)
    {
        var report = await Mediator.Send(query);
        return Results.File(report.Content, report.ContentType, report.ReportName);
    }
}