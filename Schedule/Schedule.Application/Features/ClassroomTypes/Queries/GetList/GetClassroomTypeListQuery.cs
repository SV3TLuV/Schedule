using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.ClassroomTypes.Queries.GetList;

public sealed record GetClassroomTypeListQuery : IRequest<ClassroomTypeViewModel[]>;