using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.TimeTypes.Commands.Update;

public sealed class UpdateTimeTypeCommand : IRequest<Unit>, IMapWith<TimeType>
{
    public required int Id { get; set; }
    public required string Name { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<TimeType, UpdateTimeTypeCommand>()
            .ForMember(command => command.Id, expression =>
                expression.MapFrom(timeType => timeType.TimeTypeId))
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(speciality =>
                    speciality.Name.Trim(' ', '.', ',').ToUpper()));

        profile.CreateMap<UpdateTimeTypeCommand, TimeType>()
            .ForMember(timeType => timeType.TimeTypeId, expression =>
                expression.MapFrom(command => command.Id))
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(speciality =>
                    speciality.Name.Trim(' ', '.', ',').ToUpper()));
    }
}