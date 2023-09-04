using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Groups.Queries.GetGroupDisciplines;

public sealed record GetGroupDisciplinesQuery : IRequest<ICollection<DisciplineViewModel>>
{
    public int GroupId { get; set; }
}