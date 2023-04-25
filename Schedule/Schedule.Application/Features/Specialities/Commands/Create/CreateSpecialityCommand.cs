using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Specialities.Commands.Create;

public sealed class CreateSpecialityCommand : IRequest<int>, IMapWith<Speciality>
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required ICollection<int> DisciplineIds { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Speciality, CreateSpecialityCommand>()
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(speciality =>
                    speciality.Name.ToUpper()))
            .ForMember(command => command.Code, expression =>
                expression.MapFrom(speciality =>
                    speciality.Code.ToUpper()))
            .ForMember(command => command.DisciplineIds, expression =>
                expression.MapFrom(speciality => speciality.Disciplines
                    .Select(discipline => discipline.DisciplineId)));
        
        profile.CreateMap<CreateSpecialityCommand, Speciality>()
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(speciality =>
                    speciality.Name.ToUpper()))
            .ForMember(command => command.Code, expression =>
                expression.MapFrom(speciality =>
                    speciality.Code.ToUpper()))
            .ForMember(speciality => speciality.Disciplines, expression =>
                expression.MapFrom(command => command.DisciplineIds
                    .Select(id => new Discipline
                    {
                        DisciplineId = id,
                    })));
    }
}