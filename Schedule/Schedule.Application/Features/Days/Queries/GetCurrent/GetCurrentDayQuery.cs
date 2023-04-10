using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Days.Queries.GetCurrent;

public sealed record GetCurrentDayQuery : IRequest<DayViewModel>;