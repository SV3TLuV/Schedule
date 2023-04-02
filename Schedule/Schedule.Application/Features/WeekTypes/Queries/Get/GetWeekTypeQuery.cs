using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.WeekTypes.Queries.Get;

public sealed record GetWeekTypeQuery(int Id) : IRequest<WeekTypeViewModel>;