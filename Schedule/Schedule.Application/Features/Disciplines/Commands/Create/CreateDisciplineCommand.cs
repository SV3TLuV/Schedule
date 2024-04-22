using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Disciplines.Commands.Create;

public sealed class CreateDisciplineCommand : IRequest<int>, IMapWith<Discipline>
{
    public required int NameId { get; set; }
    public required int CodeId { get; set; }
    public required int TotalHours { get; set; }
    public required int SpecialityId { get; set; }
    public required int TypeId { get; set; }
    public required int TermId { get; set; }
}