using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineNames.Commands.Update;

public sealed class UpdateDisciplineNameCommand : IRequest<Unit>, IMapWith<DisciplineName>
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    
    public void Map(Profile profile)
    {
        profile.CreateMap<DisciplineName, UpdateDisciplineNameCommand>()
            .ForMember(command => command.Id, expression =>
                expression.MapFrom(disciplineName => disciplineName.DisciplineNameId))
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(discipline =>
                    discipline.Name.Trim(' ', '.', ',').ToUpper()));

        profile.CreateMap<UpdateDisciplineNameCommand, DisciplineName>()
            .ForMember(command => command.DisciplineNameId, expression =>
                expression.MapFrom(disciplineName => disciplineName.Id))
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(discipline =>
                    discipline.Name.Trim(' ', '.', ',').ToUpper()));
    }
}