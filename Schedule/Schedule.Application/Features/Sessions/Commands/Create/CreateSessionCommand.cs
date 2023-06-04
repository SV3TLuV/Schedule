using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Sessions.Commands.Create;

public sealed class CreateSessionCommand : IRequest<Guid>, IMapWith<Session>
{
    public Guid Id { get; set; }

    public string RefreshToken { get; set; } = null!;

    public int UserId { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<CreateSessionCommand, Session>()
            .ForMember(session => session.SessionId, expression =>
                expression.MapFrom(command => command.Id))
            .ReverseMap();
    }
}