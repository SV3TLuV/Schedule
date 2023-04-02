using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Times.Queries.Get;

public sealed record GetTimeQuery(int Id) : IRequest<TimeViewModel>;