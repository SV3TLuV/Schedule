using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Accounts.Commands.Update;

public sealed class UpdateAccountCommand : IRequest<Unit>, IMapWith<Account>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? MiddleName { get; set; }
    public string? Email { get; set; }
    public int? GroupId { get; set; }
    public int? RoleId { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<UpdateAccountCommand, Account>()
            .ForMember(user => user.AccountId, expression =>
                expression.MapFrom(command => command.Id))
            .ReverseMap();
    }
}