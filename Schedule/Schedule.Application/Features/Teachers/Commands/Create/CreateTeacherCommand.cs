using AutoMapper;
using MediatR;
using Schedule.Core.Common.Extensions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Teachers.Commands.Create;

public sealed class CreateTeacherCommand : IRequest<int>, IMapWith<Teacher>
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string MiddleName { get; set; }
    public required string Email { get; set; }
    public required ICollection<int> GroupIds { get; set; }
    public required ICollection<int> DisciplineIds { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Teacher, CreateTeacherCommand>()
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(teacher =>
                    teacher.Name.Capitalize()))
            .ForMember(command => command.Surname, expression =>
                expression.MapFrom(teacher =>
                    teacher.Surname.Capitalize()))
            .ForMember(command => command.MiddleName, expression =>
                expression.MapFrom(teacher =>
                    teacher.MiddleName.Capitalize()))
            .ForMember(command => command.GroupIds, expression =>
                expression.MapFrom(teacher => teacher.Groups
                    .Select(group => group.GroupId)))
            .ForMember(command => command.DisciplineIds, expression =>
                expression.MapFrom(teacher => teacher.Disciplines
                    .Select(discipline => discipline.DisciplineId)))
            .ReverseMap();
    }
}