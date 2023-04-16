using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Classrooms.Commands.Update;

public sealed class UpdateClassroomCommand : IRequest, IMapWith<Classroom>
{
    public required int Id { get; set; }
    public required string Cabinet { get; set; }
    public required int[] TypeIds { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Classroom, UpdateClassroomCommand>()
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