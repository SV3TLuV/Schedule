using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Employees.Commands.Update;

public sealed class UpdateEmployeeCommand : IRequest<Unit>, IMapWith<Employee>
{
    public required int Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string Surname { get; set; }
    
    public string? MiddleName { get; set; }
    
    public required string Email { get; set; }
    
    public void Map(Profile profile)
    {
        profile.CreateMap<UpdateEmployeeCommand, Employee>()
            .ForMember(employee => employee.EmployeeId, expression =>
                expression.MapFrom(command => command.Id))
            .ForMember(employee => employee.Account, expression =>
                expression.MapFrom(command => command.Name))
                .ForPath(employee => employee.Account.Name, expression =>
                    expression.MapFrom(command => command.Name))
            .ForMember(employee => employee.Account, expression =>
                expression.MapFrom(command => command.Surname))
                .ForPath(employee => employee.Account.Surname, expression =>
                    expression.MapFrom(command => command.Surname))
            .ForMember(employee => employee.Account, expression =>
                expression.MapFrom(command => command.MiddleName))
                .ForPath(employee => employee.Account.MiddleName, expression =>
                    expression.MapFrom(command => command.MiddleName))
            .ForMember(employee => employee.Account, expression =>
                expression.MapFrom(command => command.Email))
                .ForPath(employee => employee.Account.Email, expression =>
                    expression.MapFrom(command => command.Email));

        profile.CreateMap<Employee, UpdateEmployeeCommand>()
            .ForMember(command => command.Id, expression =>
                expression.MapFrom(employee => employee.EmployeeId))
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(employee => employee.Account))
                .ForPath(command => command.Name, expression =>
                    expression.MapFrom(employee => employee.Account.Name))
            .ForMember(command => command.Surname, expression =>
                expression.MapFrom(employee => employee.Account))
                .ForPath(command => command.Surname, expression =>
                    expression.MapFrom(employee => employee.Account.Surname))
            .ForMember(command => command.MiddleName, expression =>
                expression.MapFrom(employee => employee.Account))
                .ForPath(command => command.MiddleName, expression =>
                    expression.MapFrom(employee => employee.Account.MiddleName))
            .ForMember(command => command.Email, expression =>
                expression.MapFrom(employee => employee.Account))
                .ForPath(command => command.Email, expression =>
                    expression.MapFrom(employee => employee.Account.Email));
    }
}