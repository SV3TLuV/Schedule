using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Times.Queries.GetList;

public sealed record GetTimeListQuery : IRequest<TimeViewModel[]>;