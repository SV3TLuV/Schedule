using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Days.Queries.Get;

public sealed record GetDayQuery(int Id) : IRequest<DayViewModel>;