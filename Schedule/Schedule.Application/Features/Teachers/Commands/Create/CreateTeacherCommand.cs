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

    public void Map(Profile profile)
    {
        profile.CreateMap<Teacher, CreateTeacherCommand>()
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(teacher =>
                    teacher.Name.Trim(' ', '.', ',').Capitalize()))
            .ForMember(command => command.Surname, expression =>
                expression.MapFrom(teacher =>
                    teacher.Surname.Trim(' ', '.', ',').Capitalize()))
            .ForMember(command => command.MiddleName, expression =>
                expression.MapFrom(teacher =>
                    teacher.MiddleName.Trim(' ', '.', ',').Capitalize()))
            .ForMember(command => command.Email, expression =>
                expression.MapFrom(teacher => 
                    teacher.Email.Trim(' ', '.', ',').ToLower()));

        profile.CreateMap<CreateTeacherCommand, Teacher>()
            .ForMember(teacher => teacher.Name, expression =>
                expression.MapFrom(teacher =>
                    teacher.Name.Trim(' ', '.', ',').Capitalize()))
            .ForMember(teacher => teacher.Surname, expression =>
                expression.MapFrom(teacher =>
                    teacher.Surname.Trim(' ', '.', ',').Capitalize()))
            .ForMember(teacher => teacher.MiddleName, expression =>
                expression.MapFrom(teacher =>
                    teacher.MiddleName.Trim(' ', '.', ',').Capitalize()))
            .ForMember(teacher => teacher.Email, expression =>
                expression.MapFrom(teacher => 
                    teacher.Email.Trim(' ', '.', ',').ToLower()));
    }
}