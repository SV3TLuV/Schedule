using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.WeekTypes.Queries.GetCurrent;

public sealed record GetCurrentWeekTypeQuery : IRequest<WeekTypeViewModel>;