using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Classrooms.Queries.GetList;

public sealed record GetClassroomListQuery : IRequest<ClassroomViewModel[]>;