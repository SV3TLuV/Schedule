using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.ClassroomTypes.Queries.Get;

public sealed record GetClassroomTypeQuery(int Id) : IRequest<ClassroomTypeViewModel>;