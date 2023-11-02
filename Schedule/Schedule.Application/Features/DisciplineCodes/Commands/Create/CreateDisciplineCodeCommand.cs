using AutoMapper;
using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Create;

[SignalRNotification(typeof(DisciplineCode), CommandTypes.Create)]
public sealed class CreateDisciplineCodeCommand : IRequest<int>, IMapWith<DisciplineCode>
{
    public required string Code { get; set; } 
    
    public void Map(Profile profile)
    {
        profile.CreateMap<DisciplineCode, CreateDisciplineCodeCommand>()
            .ForMember(command => command.Code, expression =>
                expression.MapFrom(discipline =>
                    discipline.Code.Trim().ToUpper()));

        profile.CreateMap<CreateDisciplineCodeCommand, DisciplineCode>()
            .ForMember(command => command.Code, expression =>
                expression.MapFrom(discipline =>
                    discipline.Code.Trim().ToUpper()));
    }
}