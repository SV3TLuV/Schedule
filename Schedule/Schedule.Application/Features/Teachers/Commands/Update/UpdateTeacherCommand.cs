using AutoMapper;
using MediatR;
using Schedule.Core.Common.Extensions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Teachers.Commands.Update;

public sealed class UpdateTeacherCommand : IRequest, IMapWith<Teacher>
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string MiddleName { get; set; }
    public required string Email { get; set; }
    public required ICollection<int> GroupIds { get; set; }
    public required ICollection<int> DisciplineIds { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Teacher, UpdateTeacherCommand>()
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
                expression.MapFrom(teacher => teacher.TeacherGroups
                    .Select(group => group.GroupId)))
            .ForMember(command => command.DisciplineIds, expression =>
                expression.MapFrom(teacher => teacher.TeacherDisciplines
                    .Select(discipline => discipline.DisciplineId)));
        
        profile.CreateMap<UpdateTeacherCommand, Teacher>()
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(teacher =>
                    teacher.Name.Capitalize()))
            .ForMember(command => command.Surname, expression =>
                expression.MapFrom(teacher =>
                    teacher.Surname.Capitalize()))
            .ForMember(command => command.MiddleName, expression =>
                expression.MapFrom(teacher =>
                    teacher.MiddleName.Capitalize()))
            .ForMember(teacher => teacher.TeacherGroups, expression =>
                expression.MapFrom(command => command.GroupIds
                    .Select(id => new TeacherGroup
                    {
                        GroupId = id
                    })))
            .ForMember(teacher => teacher.TeacherDisciplines, expression =>
                expression.MapFrom(command => command.DisciplineIds
                    .Select(id => new TeacherDiscipline
                    {
                        DisciplineId = id
                    })));
    }
}