using AutoMapper;
using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineNames.Commands.Update;

[SignalRNotification(typeof(DisciplineName), CommandTypes.Update)]
public sealed class UpdateDisciplineNameCommand : IRequest<Unit>, IMapWith<DisciplineName>
{
    public required int Id { get; set; }
    public required string Name { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<DisciplineName, UpdateDisciplineNameCommand>()
            .ForMember(command => command.Id, expression =>
                expression.MapFrom(disciplineName => disciplineName.DisciplineNameId));

        profile.CreateMap<UpdateDisciplineNameCommand, DisciplineName>()
            .ForMember(disciplineName => disciplineName.DisciplineNameId, expression =>
                expression.MapFrom(command => command.Id));
    }
}