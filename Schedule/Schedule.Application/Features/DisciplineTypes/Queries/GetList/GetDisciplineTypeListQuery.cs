using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineTypes.Queries.GetList;

public sealed record GetDisciplineTypeListQuery : PaginatedQuery, IRequest<PagedList<DisciplineTypeViewModel>>;