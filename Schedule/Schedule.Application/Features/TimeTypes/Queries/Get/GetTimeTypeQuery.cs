using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.TimeTypes.Queries.Get;

public sealed record GetTimeTypeQuery(int Id) : IRequest<TimeTypeViewModel>;