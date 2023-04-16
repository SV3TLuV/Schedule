using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.SpecialityCodes.Commands.Update;

public sealed class UpdateSpecialityCodeCommand : IRequest, IMapWith<SpecialityCode>
{
    public required int Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required ICollection<int> DisciplineIds { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<SpecialityCode, UpdateSpecialityCodeCommand>()
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