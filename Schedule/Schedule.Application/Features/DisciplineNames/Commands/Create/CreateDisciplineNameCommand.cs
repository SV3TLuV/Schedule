using AutoMapper;
using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineNames.Commands.Create;

[SignalRNotification(typeof(DisciplineName), CommandTypes.Create)]
public sealed class CreateDisciplineNameCommand : IRequest<int>, IMapWith<DisciplineName>
{
    public required string Name { get; set; } 
    
    public void Map(Profile profile)
    {
        profile.CreateMap<DisciplineName, CreateDisciplineNameCommand>()
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(discipline =>
                    discipline.Name.ToUpper()));

        profile.CreateMap<CreateDisciplineNameCommand, DisciplineName>()
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(discipline =>
                    discipline.Name.ToUpper()));
    }
}