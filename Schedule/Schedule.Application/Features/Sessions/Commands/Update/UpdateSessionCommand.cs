using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Sessions.Commands.Update;

public sealed class UpdateSessionCommand : IRequest, IMapWith<Session>
{
    public Guid Id { get; set; }

    public string RefreshToken { get; set; } = null!;

    public int UserId { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<UpdateSessionCommand, Session>()
            .ForMember(session => session.SessionId, expression =>
                expression.MapFrom(command => command.Id))
            .ReverseMap();
    }
}