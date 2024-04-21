using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Teachers.Commands.Update;

public sealed class UpdateTeacherCommand : IRequest<Unit>, IMapWith<Teacher>
{
    public required int Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string Surname { get; set; }
    
    public string? MiddleName { get; set; }
    
    public required string Email { get; set; }
    
    public void Map(Profile profile)
    {
        profile.CreateMap<UpdateTeacherCommand, Teacher>()
            .ForMember(teacher => teacher.TeacherId, expression =>
                expression.MapFrom(command => command.Id))
            .ForMember(teacher => teacher.Account, expression =>
                expression.MapFrom(command => command.Name))
                .ForPath(teacher => teacher.Account.Name, expression =>
                    expression.MapFrom(command => command.Name))
            .ForMember(teacher => teacher.Account, expression =>
                expression.MapFrom(command => command.Surname))
                .ForPath(teacher => teacher.Account.Surname, expression =>
                    expression.MapFrom(command => command.Surname))
            .ForMember(teacher => teacher.Account, expression =>
                expression.MapFrom(command => command.MiddleName))
                .ForPath(teacher => teacher.Account.MiddleName, expression =>
                    expression.MapFrom(command => command.MiddleName))
            .ForMember(teacher => teacher.Account, expression =>
                expression.MapFrom(command => command.Email))
                .ForPath(teacher => teacher.Account.Email, expression =>
                    expression.MapFrom(command => command.Email));

        profile.CreateMap<Teacher, UpdateTeacherCommand>()
            .ForMember(command => command.Id, expression =>
                expression.MapFrom(teacher => teacher.TeacherId))
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(teacher => teacher.Account))
                .ForPath(command => command.Name, expression =>
                    expression.MapFrom(teacher => teacher.Account.Name))
            .ForMember(command => command.Surname, expression =>
                expression.MapFrom(teacher => teacher.Account))
                .ForPath(command => command.Surname, expression =>
                    expression.MapFrom(teacher => teacher.Account.Surname))
            .ForMember(command => command.MiddleName, expression =>
                expression.MapFrom(teacher => teacher.Account))
                .ForPath(command => command.MiddleName, expression =>
                    expression.MapFrom(teacher => teacher.Account.MiddleName))
            .ForMember(command => command.Email, expression =>
                expression.MapFrom(teacher => teacher.Account))
                .ForPath(command => command.Email, expression =>
                    expression.MapFrom(teacher => teacher.Account.Email));
    }
}