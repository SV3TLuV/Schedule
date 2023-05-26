using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Times.Commands.Update;

public sealed class UpdateTimeCommand : IRequest, IMapWith<Time>
{
    public required int Id { get; set; }
    public required TimeSpan Start { get; set; }
    public required TimeSpan End { get; set; }
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