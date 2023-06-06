using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Reports.Queries.GetReportForDateRange;

public sealed record GetReportForDateRangeQuery(int StartDateId, int EndDateId) : IRequest<ReportViewModel>;