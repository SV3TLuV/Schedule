using AutoMapper;
using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Common.Extensions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Teachers.Commands.Update;

[SignalRNotification(typeof(Teacher), CommandTypes.Update)]
public sealed class UpdateTeacherCommand : IRequest<Unit>, IMapWith<Teacher>
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string MiddleName { get; set; }
    public required string Email { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Teacher, UpdateTeacherCommand>()
            .ForMember(command => command.Id, expression =>
                expression.MapFrom(teacher => teacher.TeacherId))
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(teacher =>
                    teacher.Name.Capitalize()))
            .ForMember(command => command.Surname, expression =>
                expression.MapFrom(teacher =>
                    teacher.Surname.Capitalize()))
            .ForMember(command => command.MiddleName, expression =>
                expression.MapFrom(teacher =>
                    teacher.MiddleName.Capitalize()));

        profile.CreateMap<UpdateTeacherCommand, Teacher>()
            .ForMember(teacher => teacher.TeacherId, expression =>
                expression.MapFrom(command => command.Id))
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(teacher =>
                    teacher.Name.Capitalize()))
            .ForMember(command => command.Surname, expression =>
                expression.MapFrom(teacher =>
                    teacher.Surname.Capitalize()))
            .ForMember(command => command.MiddleName, expression =>
                expression.MapFrom(teacher =>
                    teacher.MiddleName.Capitalize()));
    }
}