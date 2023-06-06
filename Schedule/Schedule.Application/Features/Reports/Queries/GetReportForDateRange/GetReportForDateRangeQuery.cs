using MediatR;

namespace Schedule.Application.Features.Reports.Queries.GetReportForDateRange;

public sealed record GetReportForDateRangeQuery(int StartDateId, int EndDateId)
    : IRequest;