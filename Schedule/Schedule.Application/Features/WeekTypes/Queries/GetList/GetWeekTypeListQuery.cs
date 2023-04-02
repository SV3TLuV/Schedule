using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.WeekTypes.Queries.GetList;

public sealed record GetWeekTypeListQuery : IRequest<WeekTypeViewModel[]>;