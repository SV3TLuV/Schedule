using AutoMapper;
using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Classrooms.Commands.Update;

[SignalRNotification(typeof(Classroom), CommandTypes.Update)]
public sealed class UpdateClassroomCommand : IRequest<Unit>, IMapWith<Classroom>
{
    public required int Id { get; set; }
    public required string Cabinet { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Classroom, UpdateClassroomCommand>()
            .ForMember(command => command.Id, expression =>
                expression.MapFrom(classroom => classroom.ClassroomId))
            .ForMember(command => command.Cabinet, expression =>
                expression.MapFrom(classroom =>
                    classroom.Cabinet.Trim(' ', '.', ',').ToLower()))
            .ReverseMap();

        profile.CreateMap<UpdateClassroomCommand, Classroom>()
            .ForMember(classroom => classroom.ClassroomId, expression =>
                expression.MapFrom(command => command.Id))
            .ForMember(classroom => classroom.Cabinet, expression =>
                expression.MapFrom(command =>
                    command.Cabinet.Trim(' ', '.', ',').ToLower()));
    }
}