using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Classrooms.Queries.Get;

public sealed record GetClassroomQuery(int Id) : IRequest<ClassroomViewModel>;