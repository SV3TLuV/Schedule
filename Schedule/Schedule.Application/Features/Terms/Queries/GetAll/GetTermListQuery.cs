using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Terms.Queries.GetAll;

public sealed record GetTermListQuery : PaginatedQuery, IRequest<PagedList<TermViewModel>>;