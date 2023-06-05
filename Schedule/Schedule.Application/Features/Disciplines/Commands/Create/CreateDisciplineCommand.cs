using AutoMapper;
using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Disciplines.Commands.Create;

[SignalRNotification(typeof(Discipline), CommandTypes.Create)]
public sealed class CreateDisciplineCommand : IRequest<int>, IMapWith<Discipline>
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required int TotalHours { get; set; }
    public required int SpecialityId { get; set; }
    public required int DisciplineTypeId { get; set; }
    public required int TermId { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Discipline, CreateDisciplineCommand>()
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(discipline =>
                    discipline.Name.ToUpper()))
            .ForMember(command => command.Code, expression =>
                expression.MapFrom(discipline =>
                    discipline.Code.ToUpper()));

        profile.CreateMap<CreateDisciplineCommand, Discipline>()
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(discipline =>
                    discipline.Name.ToUpper()))
            .ForMember(command => command.Code, expression =>
                expression.MapFrom(discipline =>
                    discipline.Code.ToUpper()));
    }
}