using AutoMapper;
using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Times.Commands.Update;

[SignalRNotification(typeof(Time), CommandTypes.Update)]
public sealed class UpdateTimeCommand : IRequest<Unit>, IMapWith<Time>
{
    public required int Id { get; set; }
    public required string Start { get; set; }
    public required string End { get; set; }
    public required int LessonNumber { get; set; }
    public required int Duration { get; set; }
    public required int TypeId { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Time, UpdateTimeCommand>()
            .ForMember(command => command.Id, expression =>
                expression.MapFrom(time => time.TimeId));

        profile.CreateMap<UpdateTimeCommand, Time>()
            .ForMember(time => time.TimeId, expression =>
                expression.MapFrom(command => command.Id));
    }
}