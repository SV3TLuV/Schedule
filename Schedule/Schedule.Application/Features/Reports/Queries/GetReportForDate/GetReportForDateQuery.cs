using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Reports.Queries.GetReportForDate;

public sealed record GetReportForDateQuery(int DateId) : IRequest<ReportViewModel>;