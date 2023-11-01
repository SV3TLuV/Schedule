using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Disciplines.Commands.Create;

[SignalRNotification(typeof(Discipline), CommandTypes.Create)]
public sealed class CreateDisciplineCommand : IRequest<int>, IMapWith<Discipline>
{
    public required int NameId { get; set; }
    public required int CodeId { get; set; }
    public required int TotalHours { get; set; }
    public required int SpecialityId { get; set; }
    public required int DisciplineTypeId { get; set; }
    public required int TermId { get; set; }
}