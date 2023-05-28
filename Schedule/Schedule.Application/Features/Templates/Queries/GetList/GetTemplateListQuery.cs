using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Templates.Queries.GetList;

public sealed record GetTemplateListQuery : PaginatedQuery, IRequest<PagedList<TemplateViewModel>>
{
    public int? WeekTypeId { get; set; }
    public int? TermId { get; set; }
    public int? DayId { get; set; }
    public int? GroupId { get; set; }
}