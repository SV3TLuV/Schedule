using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Users.Commands.Update;

public sealed class UpdateUserCommand : IRequest, IMapWith<User>
{
    public int Id { get; set; }

    public required string Login { get; set; }

    public required string Password { get; set; }

    public int RoleId { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<UpdateUserCommand, User>()
            .ForMember(user => user.UserId, expression =>
                expression.MapFrom(command => command.Id))
            .ReverseMap();
    }
}