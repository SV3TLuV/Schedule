using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Classrooms.Queries.Get;

public record GetClassroomQuery(int Id) : IRequest<ClassroomViewModel>;