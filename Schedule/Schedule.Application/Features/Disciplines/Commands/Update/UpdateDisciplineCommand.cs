using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Disciplines.Commands.Update;

public sealed class UpdateDisciplineCommand : IRequest, IMapWith<Discipline>
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required int TotalHours { get; set; }
    public required int SpecialityCodeId { get; set; }
    public required int TermId { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Discipline, UpdateDisciplineCommand>()
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(discipline =>
                    discipline.Name.ToUpper()))
            .ForMember(command => command.Code, expression =>
                expression.MapFrom(discipline =>
                    discipline.Code.ToUpper()))
            .ReverseMap();
    }
}