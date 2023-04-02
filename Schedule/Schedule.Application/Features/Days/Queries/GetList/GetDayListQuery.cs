using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Days.Queries.GetList;

public sealed record GetDayListQuery : IRequest<DayViewModel[]>;