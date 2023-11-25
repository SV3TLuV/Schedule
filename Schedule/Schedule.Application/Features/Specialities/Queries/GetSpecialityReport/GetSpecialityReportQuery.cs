using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Specialities.Queries.GetSpecialityReport;

public sealed record GetSpecialityReportQuery : IRequest<ReportViewModel>;