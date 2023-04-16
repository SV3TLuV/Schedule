using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.SpecialityCodes.Commands.Create;

public sealed class CreateSpecialityCodeCommand : IRequest<int>, IMapWith<SpecialityCode>
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required ICollection<int> DisciplineIds { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<SpecialityCode, CreateSpecialityCodeCommand>()
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(specialityCode =>
                    specialityCode.Name.ToUpper()))
            .ForMember(command => command.Code, expression =>
                expression.MapFrom(specialityCode =>
                    specialityCode.Code.ToUpper()))
            .ForMember(command => command.DisciplineIds, expression =>
                expression.MapFrom(specialityCode => specialityCode.Disciplines
                    .Select(discipline => discipline.DisciplineId)))
            .ReverseMap();
    }
}