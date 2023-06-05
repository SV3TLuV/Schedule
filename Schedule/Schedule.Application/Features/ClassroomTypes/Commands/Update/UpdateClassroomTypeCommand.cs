using AutoMapper;
using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.ClassroomTypes.Commands.Update;

[SignalRNotification(typeof(ClassroomType), CommandTypes.Update)]
public sealed class UpdateClassroomTypeCommand : IRequest<Unit>, IMapWith<ClassroomType>
{
    public required int Id { get; set; }
    public required string Name { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<ClassroomType, UpdateClassroomTypeCommand>()
            .ForMember(command => command.Id, expression =>
                expression.MapFrom(classroomType => classroomType.ClassroomTypeId));

        profile.CreateMap<UpdateClassroomTypeCommand, ClassroomType>()
            .ForMember(classroomType => classroomType.ClassroomTypeId, expression =>
                expression.MapFrom(command => command.Id));
    }
}