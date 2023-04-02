using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.TimeTypes.Queries.GetList;

public sealed record GetTimeTypeListQuery : IRequest<TimeTypeViewModel[]>;