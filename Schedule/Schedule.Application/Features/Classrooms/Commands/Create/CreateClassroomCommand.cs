using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Classrooms.Commands.Create;

public sealed class CreateClassroomCommand : IRequest<int>, IMapWith<Classroom>
{
    public required string Cabinet { get; set; }
    public required int[] TypeIds { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Classroom, CreateClassroomCommand>()
            .ForMember(command => command.TypeIds, expression =>
                expression.MapFrom(classroom =>
                    classroom.ClassroomTypes.Select(type =>
                        type.ClassroomTypeId)))
            .ForMember(command => command.Cabinet, expression =>
                expression.MapFrom(classroom =>
                    classroom.Cabinet.ToLower()))
            .ReverseMap();
    }
}