using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Disciplines.Commands.Update;

public sealed class UpdateDisciplineCommand : IRequest<Unit>, IMapWith<Discipline>
{
    public required int Id { get; set; }
    public required int NameId { get; set; }
    public required int CodeId { get; set; }
    public required int TotalHours { get; set; }
    public required int SpecialityId { get; set; }
    public required int TypeId { get; set; }
    public required int TermId { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Discipline, UpdateDisciplineCommand>()
            .ForMember(command => command.Id, expression =>
                expression.MapFrom(discipline => discipline.DisciplineId));

        profile.CreateMap<UpdateDisciplineCommand, Discipline>()
            .ForMember(discipline => discipline.DisciplineId, expression =>
                expression.MapFrom(command => command.Id));
    }
}