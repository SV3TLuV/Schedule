using AutoMapper;
using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Classrooms.Commands.Create;

[SignalRNotification(typeof(Classroom), CommandTypes.Create)]
public sealed class CreateClassroomCommand : IRequest<int>, IMapWith<Classroom>
{
    public required string Cabinet { get; set; }
    
    public void Map(Profile profile)
    {
        profile.CreateMap<Classroom, CreateClassroomCommand>()
            .ForMember(command => command.Cabinet, expression =>
                expression.MapFrom(classroom =>
                    classroom.Cabinet.Trim().ToLower()));

        profile.CreateMap<CreateClassroomCommand, Classroom>()
            .ForMember(classroom => classroom.Cabinet, expression =>
                expression.MapFrom(command =>
                    command.Cabinet.Trim().ToLower()));
    }
}