using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Timetables.Queries.Get;

public sealed record GetTimetableQuery(int Id) : IRequest<TimetableViewModel>;