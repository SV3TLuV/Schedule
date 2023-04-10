using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Disciplines.Queries.GetList;

public sealed record GetDisciplineListQuery : PaginatedQuery, IRequest<PagedList<DisciplineViewModel>>;