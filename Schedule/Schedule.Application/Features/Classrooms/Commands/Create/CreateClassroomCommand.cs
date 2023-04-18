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
                    classroom.ClassroomClassroomTypes.Select(type =>
                        type.ClassroomTypeId)))
            .ForMember(command => command.Cabinet, expression =>
                expression.MapFrom(classroom =>
                    classroom.Cabinet.ToLower()));

        profile.CreateMap<CreateClassroomCommand, Classroom>()
            .ForMember(classroom => classroom.ClassroomClassroomTypes, expression =>
                expression.MapFrom(command =>
                    command.TypeIds.Select(id => new ClassroomClassroomType
                    {
                        ClassroomTypeId = id,
                    })))
            .ForMember(classroom => classroom.Cabinet, expression =>
                expression.MapFrom(command =>
                    command.Cabinet.ToLower()));
    }
}