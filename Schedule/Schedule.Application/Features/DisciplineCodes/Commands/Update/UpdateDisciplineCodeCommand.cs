using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Update;

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
                    discipline.Code.Trim(' ', '.', ',').ToUpper()));

        profile.CreateMap<UpdateDisciplineCodeCommand, DisciplineCode>()
            .ForMember(command => command.DisciplineCodeId, expression =>
                expression.MapFrom(discipline => discipline.Id))
            .ForMember(command => command.Code, expression =>
                expression.MapFrom(discipline =>
                    discipline.Code.Trim(' ', '.', ',').ToUpper()));
    }
}