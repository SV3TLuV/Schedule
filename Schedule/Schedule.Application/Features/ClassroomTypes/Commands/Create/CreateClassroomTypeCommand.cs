using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.ClassroomTypes.Commands.Create;

public sealed class CreateClassroomTypeCommand : IRequest<int>, IMapWith<ClassroomType>
{
    public required string Name { get; set; }
}