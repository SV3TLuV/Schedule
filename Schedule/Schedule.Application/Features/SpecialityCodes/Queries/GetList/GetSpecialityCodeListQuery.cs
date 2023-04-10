using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.SpecialityCodes.Queries.GetList;

public sealed record GetSpecialityCodeListQuery : PaginatedQuery, IRequest<PagedList<SpecialityCodeViewModel>>;