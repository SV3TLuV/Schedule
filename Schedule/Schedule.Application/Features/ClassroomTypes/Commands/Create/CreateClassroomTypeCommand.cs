using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.ClassroomTypes.Commands.Create;

[SignalRNotification(typeof(ClassroomType), CommandTypes.Create)]
public sealed class CreateClassroomTypeCommand : IRequest<int>, IMapWith<ClassroomType>
{
    public required string Name { get; set; }
}