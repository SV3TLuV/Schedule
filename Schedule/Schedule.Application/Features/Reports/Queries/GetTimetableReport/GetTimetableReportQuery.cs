using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Reports.Queries.GetTimetableReport;

public sealed record GetTimetableReportQuery : IRequest<ReportViewModel>
{
    public int StartDateId { get; set; }
    public int EndDateId { get; set; }
}