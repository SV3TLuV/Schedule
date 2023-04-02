using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.ClassroomTypes.Commands.Update;

public sealed class UpdateClassroomTypeCommand : IRequest, IMapWith<ClassroomType>
{
    public required int Id { get; set; }
    public required string Name { get; set; }
}