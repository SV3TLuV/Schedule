using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Classrooms.Commands.Update;

public sealed class UpdateClassroomCommand : IRequest<Unit>, IMapWith<Classroom>
{
    public required int Id { get; set; }
    public required string Cabinet { get; set; }
    public required int[] TypeIds { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Classroom, UpdateClassroomCommand>()
            .ForMember(command => command.Id, expression =>
                expression.MapFrom(classroom => classroom.ClassroomId))
            .ForMember(command => command.TypeIds, expression =>
                expression.MapFrom(classroom =>
                    classroom.ClassroomClassroomTypes.Select(type =>
                        type.ClassroomTypeId)))
            .ForMember(command => command.Cabinet, expression =>
                expression.MapFrom(classroom =>
                    classroom.Cabinet.ToLower()))
            .ReverseMap();

        profile.CreateMap<UpdateClassroomCommand, Classroom>()
            .ForMember(classroom => classroom.ClassroomId, expression =>
                expression.MapFrom(command => command.Id))
            .ForMember(classroom => classroom.ClassroomClassroomTypes, expression =>
                expression.MapFrom(command =>
                    command.TypeIds.Select(id => new ClassroomClassroomType
                    {
                        ClassroomTypeId = id
                    })))
            .ForMember(classroom => classroom.Cabinet, expression =>
                expression.MapFrom(command =>
                    command.Cabinet.ToLower()));
    }
}