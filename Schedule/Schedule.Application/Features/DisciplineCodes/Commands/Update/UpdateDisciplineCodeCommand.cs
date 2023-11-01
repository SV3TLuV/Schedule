using AutoMapper;
using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Update;

[SignalRNotification(typeof(DisciplineCode), CommandTypes.Update)]
public sealed class UpdateDisciplineCodeCommand : IRequest<Unit>, IMapWith<DisciplineCode>
{
    public required int Id { get; set; }
    public required string Code { get; set; } 
    
    public void Map(Profile profile)
    {
        profile.CreateMap<DisciplineCode, UpdateDisciplineCodeCommand>()
            .ForMember(command => command.Id, expression =>
                expression.MapFrom(discipline => discipline.DisciplineCodeId))
            .ForMember(command => command.Code, expression =>
                expression.MapFrom(discipline =>
                    discipline.Code.ToUpper()));

        profile.CreateMap<UpdateDisciplineCodeCommand, DisciplineCode>()
            .ForMember(command => command.DisciplineCodeId, expression =>
                expression.MapFrom(discipline => discipline.Id))
            .ForMember(command => command.Code, expression =>
                expression.MapFrom(discipline =>
                    discipline.Code.ToUpper()));
    }
}