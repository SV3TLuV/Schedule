using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Teachers.Queries.GetTeacherReport;

public sealed record GetTeacherReportQuery : IRequest<ReportViewModel>;