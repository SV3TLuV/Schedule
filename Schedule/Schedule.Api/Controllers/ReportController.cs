using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Reports.Queries.GetTimetableReport;

namespace Schedule.Api.Controllers;

public sealed class ReportController : BaseController
{
    [HttpGet("timetable")]
    public async Task<IResult> Get([FromQuery] GetTimetableReportQuery query)
    {
        var report = await Mediator.Send(query);
        return Results.File(report.Content, report.ContentType, report.ReportName);
    }
}